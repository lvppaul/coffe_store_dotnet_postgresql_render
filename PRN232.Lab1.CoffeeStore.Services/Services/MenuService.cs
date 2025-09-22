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
    public class MenuService : IMenuService
    {
        private readonly IMenuRepository _repository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public MenuService(IMenuRepository repository, IProductRepository productRepository, IMapper mapper)
        {
            _repository = repository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MenuModel>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<MenuModel>>(entities);
        }

        public async Task<MenuModel?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new KeyNotFoundException($"Menu {id} không tồn tại");

            return _mapper.Map<MenuModel>(entity);
        }


        public async Task<MenuModel> CreateAsync(MenuModel model)
        {
            var entity = _mapper.Map<Menu>(model);

            // Xử lý tránh EF insert Product mới
            foreach (var pim in entity.ProductInMenus)
            {
                var product = await _productRepository.GetByIdAsync(pim.ProductId);
                if (product == null)
                    throw new InvalidOperationException($"Product {pim.ProductId} không tồn tại");

                pim.Product = null;
            }

            await _repository.AddAsync(entity);

            var created = await _repository.GetByIdAsync(entity.MenuId);
            return _mapper.Map<MenuModel>(created);
        }

        public async Task<MenuModel> UpdateAsync(MenuModel model)
        {
            var existing = await _repository.GetByIdAsync(model.Id);
            if (existing == null)
                throw new KeyNotFoundException($"Menu {model.Id} không tồn tại");

            // Update field cơ bản
            existing.Name = model.Name;
            existing.FromDate = model.FromDate;
            existing.ToDate = model.ToDate;

            // Clear product cũ + add mới
            existing.ProductInMenus.Clear();
            foreach (var p in model.Products)
            {
                var product = await _productRepository.GetByIdAsync(p.ProductId);
                if (product == null)
                    throw new InvalidOperationException($"Product {p.ProductId} không tồn tại");

                existing.ProductInMenus.Add(new ProductInMenu
                {
                    ProductId = p.ProductId,
                    Quantity = p.Quantity
                });
            }

            await _repository.UpdateAsync(existing);

            var updated = await _repository.GetByIdAsync(existing.MenuId);
            return _mapper.Map<MenuModel>(updated);
        }

        public async Task DeleteAsync(int id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException($"Menu {id} không tồn tại");

            await _repository.DeleteAsync(id);
        }
    }
}
