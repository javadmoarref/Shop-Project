﻿using _0_FrameWork.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Infrastructure.EFCore.Repository;

public class ProductRepository:RepositoryBase<long,Product>,IProductRepository
{
    private readonly ShopContext _context;

    public ProductRepository(ShopContext  context) : base(context)
    {
        _context = context;
    }

    public EditProduct GetDetails(long id)
    {
        return _context.Products.Select(x => new EditProduct()
        {
            Id = x.Id,
            Name = x.Name,
            Code = x.Code,
            Description = x.Description,
            PictureAlt = x.PictureAlt,
            Picture = x.Picture,
            PictureTitle = x.PictureTitle,
            Keywords = x.Keywords,
            MetaDescription = x.MetaDescription,
            ShortDescription = x.ShortDescription,
            Slug = x.Slug,
            UnitPrice = x.UnitPrice,
            CategoryId = x.CategoryId
        }).FirstOrDefault(x => x.Id == id);
    }

    public List<ProductViewModel> Search(ProductSearchModel searchModel)
    {
        var query = _context.Products.Include(x=>x.Category).Select(x => new ProductViewModel()
        {
            Id = x.Id,
            Name = x.Name,
            Code = x.Code,
            UnitPrice = x.UnitPrice,
            Category = x.Category.Name,
            Picture = x.Picture,
            CreationDate = x.CreationDate.ToString(),
            CategoryId = x.CategoryId,
            InStock = x.InStock
        });

        if (!string.IsNullOrWhiteSpace(searchModel.Name))
        {
            query = query.Where(x => x.Name.Contains(searchModel.Name));
        }

        if (!string.IsNullOrWhiteSpace(searchModel.Code))
        {
            query = query.Where(x => x.Code.Contains(searchModel.Code));
        }

        if (searchModel.CategoryId != 0)
        {
            query = query.Where(x => x.CategoryId == searchModel.CategoryId);
        }

        return query.OrderByDescending(x => x.Id).ToList();
    }
}