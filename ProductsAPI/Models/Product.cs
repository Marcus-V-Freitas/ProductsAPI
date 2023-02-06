namespace ProductsAPI.Models;

public sealed class Product
{
    public int? product_id { get; set; }
    public string product_name { get; set; }
    public int? brand_id { get; set; }
    public int? category_id { get; set; }
    public int? model_year { get; set; }
    public double? list_price { get; set; }

    public Category Category { get; set; }
    public Brand Brand { get; set; }
}