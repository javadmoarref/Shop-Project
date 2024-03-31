﻿using System.ComponentModel.DataAnnotations;
using _0_FrameWork.Application;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ShopManagement.Application.Contracts.Product;

public class CreateProduct
{
    [Required(ErrorMessage = ValidationMessages.IsRequired)]
    public string Name { get;  set; }

    [Required(ErrorMessage = ValidationMessages.IsRequired)]
    public string Code { get;  set; }

    [Range(1,int.MaxValue,ErrorMessage = ValidationMessages.IsRequired)]
    public Double UnitPrice { get;  set; }

    [Required(ErrorMessage = ValidationMessages.IsRequired)]
    public string ShortDescription { get;  set; }
    public string Description { get;  set; }
    public string Picture { get;  set; }
    public string PictureAlt { get;  set; }
    public string PictureTitle { get;  set; }

    [Required(ErrorMessage = ValidationMessages.IsRequired)]
    public string Slug { get;  set; }

    [Required(ErrorMessage = ValidationMessages.IsRequired)]
    public string Keywords { get;  set; }

    [Required(ErrorMessage = ValidationMessages.IsRequired)]
    public string MetaDescription { get;  set; }

    [Range(0,100000,ErrorMessage = ValidationMessages.IsRequired)]
    public long CategoryId { get;  set; }
    public List<ProductCategoryViewModel> Categories { get; set; }
}