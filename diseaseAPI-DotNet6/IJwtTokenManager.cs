namespace diseaseAPI_DotNet6
{
    public interface IJwtTokenManager
    {
        string Authenticate(string email, string password);
    }
}
