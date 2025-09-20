using BTM.Products.Application.Results;

namespace BTM.Products.Application.Abstractions
{
    public interface ITokenService
    {
        Task<TokenResult> RequestClientCredentialsTokenAsync();
    }
}
