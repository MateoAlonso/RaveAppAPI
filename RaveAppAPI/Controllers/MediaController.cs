using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RaveAppAPI.Services.Helpers;
using RaveAppAPI.Services.Models;
using RaveAppAPI.Services.Repository.Contracts;
using RaveAppAPI.Services.RequestModel.Media;
using System.Net;

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
                    media.Url = string.IsNullOrEmpty(media.MdVideo) ? GetMediaUrl(media.IdMedia) : string.Empty;
                }
            }
            return getMediaResult.Match(
                medias => Ok(MapMediaResponse(medias)),
                errors => Problem(errors));
        }

        [HttpPost]
        public async Task<IActionResult> CreateMedia([FromForm] CreateMediaRequest request)
        {
            ErrorOr<Media> requestToMediaResult = Media.From(request);

            if (requestToMediaResult.IsError)
            {
                return Problem(requestToMediaResult.Errors);
            }

            var media = requestToMediaResult.Value;
            ErrorOr<Created> createMediaResult = _mediaService.CreateMedia(media);

            if (!createMediaResult.IsError && request.File != null)
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

                    using var client = new AmazonS3Client(credentials, config);

                    using (var memStream = new MemoryStream())
                    {
                        await request.File.CopyToAsync(memStream);

                        var putRequest = new PutObjectRequest
                        {
                            BucketName = _bucketName,
                            Key = media.IdMedia,
                            InputStream = memStream,
                            ContentType = request.File.ContentType,
                            DisablePayloadSigning = true,
                            DisableDefaultChecksumValidation = true
                        };
                        putRequest.MD5Digest = Convert.ToBase64String(System.Security.Cryptography.MD5.HashData(memStream.ToArray()));
                        var result = await client.PutObjectAsync(putRequest);
                        if (result.HttpStatusCode != HttpStatusCode.OK)
                        {
                            return Problem("Ocurrio un error al subir imagen al bucket");
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
                }
                ;
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message);
                return string.Empty;
            }
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ErrorOr<Created>> CrearMediaQrEntrada(byte[] QrEntrada, string idEntrada)
        {
            try
            {
                ErrorOr<Media> requestToMediaResult = Media.From(idEntrada);

                if (requestToMediaResult.IsError)
                {
                    return requestToMediaResult.Errors;
                }

                var media = requestToMediaResult.Value;
                ErrorOr<Created> createMediaResult = _mediaService.CreateMedia(media);

                if (createMediaResult.IsError)
                {
                    return createMediaResult.Errors;
                }
                using var stream = new MemoryStream(QrEntrada);

                var credentials = new BasicAWSCredentials(_accessKey, _secretKey);

                var config = new AmazonS3Config
                {
                    RegionEndpoint = RegionEndpoint.USEast1,
                    ServiceURL = _r2Endpoint,
                    ForcePathStyle = true
                };

                var putRequest = new PutObjectRequest
                {
                    BucketName = _bucketName,
                    Key = idEntrada,
                    InputStream = stream,
                    ContentType = "image/png",
                    DisablePayloadSigning = true,
                    DisableDefaultChecksumValidation = true
                };
                putRequest.MD5Digest = Convert.ToBase64String(System.Security.Cryptography.MD5.HashData(QrEntrada));

                using (var client = new AmazonS3Client(credentials, config))
                {
                    var result = await client.PutObjectAsync(putRequest);
                    if (result.HttpStatusCode != HttpStatusCode.OK)
                    {
                        return Error.Unexpected("Ocurrio un error al subir imagen al bucket");
                    }
                }

                return Result.Created;

            }
            catch (Exception e)
            {
                Logger.LogError(e.Message);
                return Error.Unexpected(e.Message);
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
