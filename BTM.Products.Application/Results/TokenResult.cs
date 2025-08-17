namespace BTM.Products.Application.Results
{
    public class TokenResult
    {
        public bool Success { get; set; }
        public string? AccessToken { get; set; }
        public string? ErrorMessage { get; set; }
    }

}
