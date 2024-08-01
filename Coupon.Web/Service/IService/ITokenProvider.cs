namespace Commerce.Web.Service.IService
{
    public interface ITokenProvider
    {
        void setToken (string token);
        string? GetToken ();
        void ClearToken();
    }
}
