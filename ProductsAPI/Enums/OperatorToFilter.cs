namespace ProductsAPI.Enums;

public enum OperatorToFilter
{
    [Display(Name = "=")]
    Equal,
    [Display(Name = ">")]
    GreaterThan,
    [Display(Name = ">=")]
    GreaterThanOrEqual,
    [Display(Name = "=<")]
    LessThan,
    [Display(Name = "<")]
    LessThanOrEqual,
    [Display(Name = "<>")]
    Different
}