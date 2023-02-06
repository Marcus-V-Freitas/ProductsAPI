namespace ProductsAPI.Data.Context;

public sealed class Context : IContext
{
    private readonly Dictionary<FieldToFilter, IncludeDetails> _includeDetails;

    public string Products { get; set; }
    public string Brands { get; set; }
    public string Categories { get; set; }
    public string ConnectionString { get; set; }

    public Context(IConfiguration configuration)
    {
        Products = configuration.GetValue<string>("Context:Products")!;
        Brands = configuration.GetValue<string>("Context:Brands")!;
        Categories = configuration.GetValue<string>("Context:Categories")!;
        ConnectionString = configuration.GetConnectionString("Default")!;

        _includeDetails = new()
        {
            { FieldToFilter.CategoryId, new(Categories, "Category") },
            { FieldToFilter.BrandId, new(Brands, "Brand") }
        };
    }

    public IncludeDetails GetIncludeDetails(FieldToFilter field)
    {
        if (_includeDetails.TryGetValue(field, out IncludeDetails value))
        {
            return value;
        }

        return null;
    }
}