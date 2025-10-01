using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRN232.Lab1.CoffeeStore.APIS.RequestModels;
using PRN232.Lab1.CoffeeStore.APIS.ResponseModels;
using PRN232.Lab1.CoffeeStore.Services.BusinessModels;
using PRN232.Lab1.CoffeeStore.Services.Interfaces;

namespace PRN232.Lab1.CoffeeStore.APIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenusController : ControllerBase
    {
        private readonly IMenuService _service;
        private readonly IMapper _mapper;

        public MenusController(IMenuService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET /api/menus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuResponse>>> GetAll()
        {
            var menus = await _service.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<MenuResponse>>(menus));
        }

        // GET /api/menus/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<MenuResponse>> GetById(int id)
        {
            var menu = await _service.GetByIdAsync(id);
            return Ok(_mapper.Map<MenuResponse>(menu));
        }

        // POST /api/menus
        [HttpPost]
        public async Task<ActionResult<MenuResponse>> Create(CreateMenuRequest request)
        {
            var model = _mapper.Map<MenuModel>(request);
            var created = await _service.CreateAsync(model);

           
            return CreatedAtAction(nameof(GetById), new { id = created.Id },
                _mapper.Map<MenuResponse>(created));
        }


        // PUT /api/menus/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<MenuResponse>> Update(int id, UpdateMenuRequest request)
        {
            var model = _mapper.Map<MenuModel>(request);
            model.Id = id;

            var updated = await _service.UpdateAsync(model);
            return Ok(_mapper.Map<MenuResponse>(updated));
        }

        // DELETE /api/menus/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
