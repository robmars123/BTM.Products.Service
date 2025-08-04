namespace BTM.Products.Contracts.ProductCommands
{
    /// <summary>
    /// Immutable dto class for creating a product.
    /// </summary>
    /// <param name="Name"></param>
    /// <param name="Price"></param>
    public record CreateProductRequest(string Name, decimal Price);
}
