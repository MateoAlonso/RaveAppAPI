namespace RaveAppAPI.Services.RequestModel.Social
{
    public record CreateSocialRequest(string IdSocial, string? Instagram, string? Spotify, string? Soundcloud);
}