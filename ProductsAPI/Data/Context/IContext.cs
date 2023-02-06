namespace ProductsAPI.Data.Context;

public interface IContext
{
    string Products { get; set; }
    string Brands { get; set; }
    string Categories { get; set; }
    string ConnectionString { get; set; }

    IncludeDetails GetIncludeDetails(FieldToFilter field);
}