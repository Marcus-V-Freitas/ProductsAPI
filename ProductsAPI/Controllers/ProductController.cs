namespace ProductsAPI.Controllers;

/// <summary>
/// Products endpoint
/// </summary>
/// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public sealed class ProductController : ControllerBase
{
    private readonly IProductRepository _productRepository;
    private readonly ILogger<ProductController> _logger;

    public ProductController(ILogger<ProductController> logger, IProductRepository productRepository)
    {
        _logger = logger;
        _productRepository = productRepository;
    }

    /// <summary>
    /// Return list with products infos
    /// </summary>
    /// <param name="productFilter">The product filter.</param>
    /// <response code="200">Return items filtered</response>
    /// <response code="404">Informs that no items were found with the filter</response>
    [HttpPost("", Name = nameof(PostProductsAsync))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PostProductsAsync([FromBody] ProductFilter productFilter)
    {
        var products = await _productRepository.ReturnProductsByFiltes(productFilter);

        if (products.ListIsNullOrEmpty())
        {
            return NotFound();
        }

        return Ok(products);
    }
}