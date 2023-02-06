namespace ProductsAPI.Data.Repositories;

public sealed class ProductRepository : IProductRepository
{
    private readonly SqlServerCompiler _compiler;
    private readonly IContext _context;

    public ProductRepository(IContext context)
    {
        _compiler = new SqlServerCompiler();
        _context = context;
    }

    public async Task<IEnumerable<Product>> ReturnProductsByFiltes(ProductFilter productFilter)
    {
        using SqlConnection connection = new(_context.ConnectionString);

        using QueryFactory db = new(connection, _compiler);

        var query = db.Query(_context.Products);

        query = IncludeDetails(db, query, productFilter);
        query = SelectFields(query, productFilter.Fields);
        query = ApplyFiltersToQuery(query, productFilter.Filters);
        query = SortFields(query, productFilter.Sorts);

        return await query.GetComplexObjectAsync<Product>();
    }

    private Query SortFields(Query query, IEnumerable<SortFilter> sortFilters)
    {
        if (sortFilters.ListIsNullOrEmpty())
        {
            return query;
        }

        foreach (var sortFilter in sortFilters)
        {
            query = query.OrderByRaw($"{sortFilter.Field.GetDisplayName()} {sortFilter.Sort.GetDisplayName()}");
        }

        return query;
    }

    private Query SelectFields(Query query, IEnumerable<FieldToFilter> fields)
    {
        if (fields.ListIsNullOrEmpty())
        {
            return query;
        }

        query = query.Select(fields.Select(x => x.GetDisplayName()));

        return query;
    }


    private Query ApplyInclude(QueryFactory db, Query query, FieldToFilter field, ProductFilter productFilter)
    {
        productFilter.Fields.Add(field);

        IncludeDetails includeDetails = _context.GetIncludeDetails(field);
        string fieldName = field.GetDisplayName();

        var detailsQuery = db.Query(includeDetails.TableName);

        query = query.Include(includeDetails.RelationName, detailsQuery, fieldName, fieldName);

        return query;
    }

    private Query IncludeDetails(QueryFactory db, Query query, ProductFilter productFilter)
    {
        if (productFilter.IncludeBrandDetails)
        {
            ApplyInclude(db, query, FieldToFilter.BrandId, productFilter);
        }

        if (productFilter.IncludeCategoryDetails)
        {
            ApplyInclude(db, query, FieldToFilter.CategoryId, productFilter);
        }

        return query;
    }

    private Query ApplyFiltersToQuery(Query query, IEnumerable<QueryFilter> filters)
    {
        if (filters.ListIsNullOrEmpty())
        {
            return query;
        }

        foreach (var filter in filters)
        {
            query = query.Where(filter.Field.GetDisplayName(), filter.Operator.GetDisplayName(), filter.Value.TransformToObjectType());
        }

        return query;
    }
}