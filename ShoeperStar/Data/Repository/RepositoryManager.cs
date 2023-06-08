﻿using ShoeperStar.Data.Contracts;

namespace ShoeperStar.Data.Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly AppDbContext _context;
        private IBrandRepository _brands;
        private IGenderRepository _genders;
        private ICategoryRepository _categories;
        private IShoeRepository _shoes;
        private IVariantRepository _variants;
        private ISizeRepository _sizes;

        public RepositoryManager(AppDbContext context)
        {
            _context = context;
        }

        public IBrandRepository Brands => _brands ??= new BrandRepository(_context);

        public IGenderRepository Genders => _genders ??= new GenderRepository(_context);

        public ICategoryRepository Categories => _categories ??= new CategoryRepository(_context);

        public IShoeRepository Shoes => _shoes ??= new ShoeRepository(_context);

        public IVariantRepository Variants => _variants ??= new VariantRepository(_context);

        public ISizeRepository Sizes => _sizes ??= new SizeRepository(_context);

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}