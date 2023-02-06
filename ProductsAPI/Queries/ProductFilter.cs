namespace ProductsAPI.Queries;

public sealed class ProductFilter
{
    public bool IncludeCategoryDetails { get; set; } = true;
    public bool IncludeBrandDetails { get; set; } = true;
    public IEnumerable<QueryFilter> Filters { get; set; }
    public HashSet<FieldToFilter> Fields { get; set; }

    public IEnumerable<SortFilter> Sorts { get; set; }
}