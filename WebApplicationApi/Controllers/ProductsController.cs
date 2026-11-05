using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplicationApi.Models;
using WebApplicationApi.Models.Dtos;
using WebApplicationApi.Repository.IRepository;

namespace WebApplicationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        private readonly IMapper _mapper;
        public ProductsController(IProductRepository productRepository, ICategoryRepository categoryRepository,IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult GetProducts()
        {
            var products = _productRepository.GetProducts();
            var productsDto = _mapper.Map<List<ProductDto>>(products);
            return Ok(productsDto);
        }
        [HttpGet("{id:int}", Name = "GetProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult GetProduct(int id)
        {
            var product = _productRepository.GetProduct(id);
            if (product == null)
            {
                return NotFound($"Producto con ID {id} no encontrada");
            }
            var productDto = _mapper.Map<ProductDto>(product);
            return Ok(productDto);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult CreateProduct([FromBody] CreateProductDto createProductDto)
        {
            if (createProductDto == null)
            {
                return BadRequest(ModelState);
            }
            if (_productRepository.ProductExist(createProductDto.Name))
            {
                ModelState.AddModelError("CustomError", "El producto ya existe");
                return BadRequest(ModelState);
            }
            if (!_categoryRepository.CategoryExists(createProductDto.CategoryId)) {
                ModelState.AddModelError("CustomError", "La categoria no existe");
                return BadRequest(ModelState);
            }
            var product = _mapper.Map<Product>(createProductDto);
            if (!_productRepository.CreateProduct(product))
            {
                return StatusCode(500, "Error al crear la categoria");
            }
            var productDto = _mapper.Map<ProductDto>(product);
            var createdProduct = _productRepository.GetProduct(product.ProductId);
            return CreatedAtRoute("GetProduct", new { productId = product.ProductId }, productDto);
        }
        [HttpGet("searchByCategory/{categoryId:int}", Name = "GetProductForCategory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult GetProductsForCategory(int categoryId)
        {
            var products = _productRepository.GetProductsForCategory(categoryId);
            if (products.Count == 0)
            {
                return NotFound($"Producto con ID {categoryId} de categoria no encontrada");
            }
            var productsDto = _mapper.Map<List<ProductDto>>(products);
            return Ok(productsDto);
        }
        /*[HttpPatch("{id:int}", Name = "UpdateCategory")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateCategory(int id, [FromBody] CreateCategoryDto updateCategoryDto)
        {
            if (updateCategoryDto == null)
            {
                return BadRequest(ModelState);
            }
            if (!_categoryRepository.CategoryExists(id))
            {
                return NotFound($"Categoria con ID {id} no encontrada");
            }
            if (_categoryRepository.CategoryExists(updateCategoryDto.Name))
            {
                ModelState.AddModelError("CustomError", "La categoria ya existe");
                return BadRequest(ModelState);
            }
            var category = _mapper.Map<Category>(updateCategoryDto);
            category.Id = id;
            if (category == null)
            {
                return NotFound($"Proporcione un ID de categoria");
            }

            _mapper.Map(updateCategoryDto, category);
            if (!_categoryRepository.UpdateCategory(category))
            {
                return StatusCode(500, "Error al actualizar la categoria");
            }

            return NoContent();
        }

        [HttpDelete("{id:int}", Name = "DeleteCategory")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteCategory(int id)
        {
            if (!_categoryRepository.CategoryExists(id))
            {
                return NotFound($"Categoria con ID {id} no encontrada");
            }

            var category = _categoryRepository.GetCategory(id);
            if (!_categoryRepository.DeleteCategory(category))
            {
                ModelState.AddModelError("CustomError", "Error al eliminar la categoria");
                return StatusCode(500, "Error al eliminar la categoria");
            }

            return NoContent();
        }*/

    }
}
