using AutoMapper;
using PRN232.Lab1.CoffeeStore.Repositories.Interfaces;
using PRN232.Lab1.CoffeeStore.Repositories.Models;
using PRN232.Lab1.CoffeeStore.Services.BusinessModels;
using PRN232.Lab1.CoffeeStore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232.Lab1.CoffeeStore.Services.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository repository, ICategoryService categoryService, IMapper mapper)
        {
            _repository = repository;
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductModel>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductModel>>(entities);
        }

        public async Task<ProductModel> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new KeyNotFoundException($"Product {id} không tồn tại");

            return _mapper.Map<ProductModel>(entity);
        }

        public async Task<ProductModel> CreateAsync(ProductModel model)
        {
            // Validate category
            var category = await _categoryService.GetByIdAsync(model.CategoryId);
            if (category == null)
            {
                var categories = await _categoryService.GetAllAsync();
                var list = string.Join(", ", categories.Select(c => $"{c.Id} - {c.Name}"));
                throw new InvalidOperationException($"CategoryId {model.CategoryId} không tồn tại. Thử các category sau: {list}");
            }

            var entity = _mapper.Map<Product>(model);
            entity.Category = null;

            await _repository.AddAsync(entity);
            var created = await _repository.GetByIdAsync(entity.ProductId);
            return _mapper.Map<ProductModel>(created);
        }

        public async Task<ProductModel> UpdateAsync(ProductModel model)
        {
            var existing = await _repository.GetByIdAsync(model.Id);
            if (existing == null)
                throw new KeyNotFoundException($"Product {model.Id} không tồn tại");

            // Validate category
            var category = await _categoryService.GetByIdAsync(model.CategoryId);
            if (category == null)
            {
                var categories = await _categoryService.GetAllAsync();
                var list = string.Join(", ", categories.Select(c => $"{c.Id} - {c.Name}"));
                throw new InvalidOperationException($"CategoryId {model.CategoryId} không tồn tại. Thử các category sau: {list}");
            }

            // Update trực tiếp field
            existing.Name = model.Name;
            existing.Price = model.Price;
            existing.Description = model.Description;
            existing.CategoryId = model.CategoryId;

            await _repository.UpdateAsync(existing);

            var updated = await _repository.GetByIdAsync(existing.ProductId);
            return _mapper.Map<ProductModel>(updated);
        }

        public async Task DeleteAsync(int id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException($"Product {id} không tồn tại");

            
            if (await _repository.IsProductInMenu(id))
                throw new InvalidOperationException(
                    $"Product {id} đang được sử dụng trong Menu, không thể xóa");

            await _repository.DeleteAsync(id);
        }
    }


}
