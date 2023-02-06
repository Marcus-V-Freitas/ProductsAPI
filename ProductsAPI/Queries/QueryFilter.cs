namespace ProductsAPI.Queries;

public sealed class QueryFilter
{
    public FieldToFilter Field { get; set; }
    public OperatorToFilter Operator { get; set; }
    public string Value { get; set; }
}