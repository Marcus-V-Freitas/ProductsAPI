namespace ProductsAPI.Queries;

public sealed class SortFilter
{
    public SortToFilter Sort { get; set; }
    public FieldToFilter Field { get; set; }
}