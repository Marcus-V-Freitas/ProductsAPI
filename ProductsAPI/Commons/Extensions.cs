namespace ProductsAPI.Commons;

public static class Extensions
{
    public static async Task<IEnumerable<T>> GetComplexObjectAsync<T>(this Query? query)
    {
        var result = await query.GetAsync();

        return JsonSerializer.Deserialize<IEnumerable<T>>(JsonSerializer.Serialize(result))!;
    }

    public static object TransformToObjectType(this string value)
    {
        if (value.Contains(".") && double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var resultDouble))
        {
            return resultDouble;
        }

        if (long.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var resultLong))
        {
            return resultLong;
        }

        return value;
    }

    public static bool ListIsNullOrEmpty<T>(this IEnumerable<T> collection)
    {
        return collection == null || !collection.Any();
    }

    public static string GetDisplayName(this Enum enumValue)
    {
        return enumValue.GetType()
                        .GetMember(enumValue.ToString())
                        .FirstOrDefault()!
                        .GetCustomAttribute<DisplayAttribute>()!
                        .GetName()!;
    }
}