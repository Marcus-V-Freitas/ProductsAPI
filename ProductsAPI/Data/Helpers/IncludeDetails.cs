namespace ProductsAPI.Data.Helpers;

public sealed class IncludeDetails
{
    public string TableName { get; set; }

    public string RelationName { get; set; }

    public IncludeDetails(string tableName, string relationName)
    {
        TableName = tableName;
        RelationName = relationName;
    }
}