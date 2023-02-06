namespace ProductsAPI.Data.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<Product>> ReturnProductsByFiltes(ProductFilter productFilter);
}