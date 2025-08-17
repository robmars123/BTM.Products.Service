namespace BTM.Products.Application.Results
{
    public class TokenResult
    {
        public TokenResult(){ }
        public TokenResult(bool success, string? accessToken, string? errorMEssage)
        {
            Success = success;
            AccessToken = accessToken;
            ErrorMEssage = errorMEssage;
        }
        public bool Success { get; set; }
        public string? AccessToken { get; set; }
        public string? ErrorMEssage { get; }
        public string? ErrorMessage { get; set; }
    }

}
