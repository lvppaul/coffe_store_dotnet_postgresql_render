using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRN232.Lab1.CoffeeStore.APIS.RequestModels;
using PRN232.Lab1.CoffeeStore.APIS.ResponseModels;
using PRN232.Lab1.CoffeeStore.Services.BusinessModels;
using PRN232.Lab1.CoffeeStore.Services.Interfaces;

namespace PRN232.Lab1.CoffeeStore.APIS.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;
        private readonly IMapper _mapper;

        public ProductController(IProductService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductResponse>>> GetAll()
        {
            var products = await _service.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<ProductResponse>>(products));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponse>> GetById(int id)
        {
            var product = await _service.GetByIdAsync(id);
            return Ok(_mapper.Map<ProductResponse>(product));
        }

        [HttpPost]
        public async Task<ActionResult<ProductResponse>> Create(CreateProductRequest request)
        {
            var model = _mapper.Map<ProductModel>(request);
            var created = await _service.CreateAsync(model);

            
            return CreatedAtAction(nameof(GetById), new { id = created.Id },
                _mapper.Map<ProductResponse>(created));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProductResponse>> Update(int id, UpdateProductRequest request)
        {
            var model = _mapper.Map<ProductModel>(request);
            model.Id = id;

            var updated = await _service.UpdateAsync(model);
            return Ok(_mapper.Map<ProductResponse>(updated));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
