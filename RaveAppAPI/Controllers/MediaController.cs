using Amazon.S3.Model;
using Amazon.S3;
using Amazon;
using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RaveAppAPI.Services.Helpers;
using RaveAppAPI.Services.Models;
using RaveAppAPI.Services.Repository.Contracts;
using RaveAppAPI.Services.RequestModel.Media;
using Amazon.Runtime;
using Amazon.S3.Model.Internal.MarshallTransformations;
using System.Net.Http.Headers;
using System.IO;

namespace RaveAppAPI.Controllers
{
    [Authorize]
    public class MediaController : ApiController
    {

        private readonly IMediaService _mediaService;
        private readonly string _bucketName = EnvHelper.GetBucketName();
        private readonly string _accessKey = EnvHelper.GetAccessKey();
        private readonly string _secretKey = EnvHelper.GetSecretKey();
        private readonly string _r2Endpoint = EnvHelper.GetR2Endpoint();
        public MediaController(IMediaService mediaService)
        {
            _mediaService = mediaService;
        }

        [HttpGet]
        public IActionResult GetMedia(string idEntidadMedia)
        {
            ErrorOr<List<Media>> getMediaResult = _mediaService.GetMedia(idEntidadMedia);
            if (!getMediaResult.IsError)
            {
                foreach (var media in getMediaResult.Value)
                {
                    media.Url = GetMediaUrl(media.IdMedia);
                }
            }
            return getMediaResult.Match(
                medias => Ok(MapMediaResponse(medias)),
                errors => Problem(errors));
        }

        [HttpPost]
        public async Task<IActionResult> CreateMedia([FromForm]CreateMediaRequest request)
        {
            ErrorOr<Media> requestToMediaResult = Media.From(request);

            if (requestToMediaResult.IsError)
            {
                return Problem(requestToMediaResult.Errors);
            }

            var media = requestToMediaResult.Value;
            ErrorOr<Created> createMediaResult = _mediaService.CreateMedia(media);

            if (!createMediaResult.IsError)
            {
                try
                {
                    var credentials = new BasicAWSCredentials(_accessKey, _secretKey);

                    var config = new AmazonS3Config
                    {
                        RegionEndpoint = RegionEndpoint.USEast1,
                        ServiceURL = _r2Endpoint,
                        ForcePathStyle = true
                    };

                    var preSignedRequest = new GetPreSignedUrlRequest
                    {
                        BucketName = _bucketName,
                        Key = media.IdMedia,
                        Verb = HttpVerb.PUT,
                        Expires = DateTime.UtcNow.AddMinutes(5)
                    };

                    string url;
                    using (var client = new AmazonS3Client(credentials, config))
                    {
                        url = client.GetPreSignedURL(preSignedRequest);
                    }

                    using (var memStream = new MemoryStream())
                    {
                        await request.File.CopyToAsync(memStream);
                        using (var httpClient = new HttpClient())
                        {
                            var content = new ByteArrayContent(memStream.ToArray());
                            content.Headers.ContentType = new MediaTypeHeaderValue(request.File.ContentType);
                            var response = httpClient.PutAsync(url, content);
                            response.Wait();
                            if (!response.Result.IsSuccessStatusCode)
                            {
                                return Problem("Ocurrio un error al subir imagen al bucket");
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Logger.LogError(e.Message);
                    return Problem(e.Message);
                }
            }

            return createMediaResult.Match(
                created => CreatedAtCreateMedia(media),
                errors => Problem(errors));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedia(string id)
        {
            var credentials = new BasicAWSCredentials(_accessKey, _secretKey);
            var config = new AmazonS3Config
            {
                RegionEndpoint = RegionEndpoint.USEast1,
                ServiceURL = _r2Endpoint,
                ForcePathStyle = true
            };
            var deleteRequest = new DeleteObjectRequest
            {
                BucketName = _bucketName,
                Key = id
            };

            using (var client = new AmazonS3Client(credentials, config)) 
            {
                try
                {
                    await client.DeleteObjectAsync(deleteRequest);
                }
                catch (Exception e)
                {
                    return Problem(e.Message);
                }
            
            }

            ErrorOr<Deleted> deleteMediaResult = _mediaService.DeleteMedia(id);

            return deleteMediaResult.Match(
                deleted => Ok(),
                errors => Problem(errors));
        }

        private string GetMediaUrl(string fileName)
        {
            try
            {
                var credentials = new BasicAWSCredentials(_accessKey, _secretKey);

                var config = new AmazonS3Config
                {
                    RegionEndpoint = RegionEndpoint.USEast1,
                    ServiceURL = _r2Endpoint,
                    ForcePathStyle = true
                };

                var request = new GetPreSignedUrlRequest
                {
                    BucketName = _bucketName,
                    Key = fileName,
                    Verb = HttpVerb.GET,
                    Expires = DateTime.UtcNow.AddMinutes(15)
                };

                using (var client = new AmazonS3Client(credentials, config))
                {
                    return client.GetPreSignedURL(request);
                };
            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }

        private CreatedAtActionResult CreatedAtCreateMedia(Media media)
        {
            return CreatedAtAction(nameof(CreateMedia),
                new { id = media.IdMedia }, MapCreateMediaResponse(media));
        }

        private CreateMediaResponse MapCreateMediaResponse(Media media)
        {
            return new(media.IdMedia);
        }

        private MediaResponse MapMediaResponse(List<Media> medias)
        {
            return new(medias);
        }
    }
}
