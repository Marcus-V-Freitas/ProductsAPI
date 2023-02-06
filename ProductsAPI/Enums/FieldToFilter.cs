namespace ProductsAPI.Enums;

public enum FieldToFilter
{
    [Display(Name = "product_id")]
    ProductId,
    [Display(Name = "product_name")]
    ProductName,
    [Display(Name = "brand_id")]
    BrandId,
    [Display(Name = "category_id")]
    CategoryId,
    [Display(Name = "model_year")]
    Year,
    [Display(Name = "list_price")]
    Price
}