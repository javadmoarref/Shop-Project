using _0_FrameWork.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.Administration.Pages.Shop.Products
{
    public class IndexModel : PageModel
    {
        public string Message { get; set; }
        public ProductSearchModel SearchModel { get; set; }
        public List<ProductViewModel> Products { get; set; }
        public SelectList ProductCategories { get; set; }
        private readonly IProductApplication _productApplication;
        private readonly IProductCategoryApplication _productCategoryApplication;


        public IndexModel(IProductApplication productApplication, IProductCategoryApplication productCategoryApplication)
        {
            _productApplication = productApplication;
            _productCategoryApplication = productCategoryApplication;
        }

        public void OnGet(ProductSearchModel searchModel)
        {
            ProductCategories = new SelectList(_productCategoryApplication.GetProductCategories(), "Id", "Name");
            Products = _productApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            var command = new CreateProduct()
            {
                Categories = _productCategoryApplication.GetProductCategories()
            };
            return Partial("./Create", command); 
        }

        public JsonResult OnPostCreate(CreateProduct command)
        {
            var result = _productApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var product = _productApplication.GetDetails(id);
            product.Categories = _productCategoryApplication.GetProductCategories();
            return Partial("./Edit", product);
        }

        public JsonResult OnPostEdit(EditProduct command)
        {
            var result=_productApplication.Edit(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetIsInStock(long id)
        {
            var result=_productApplication.IsInStock(id);
            if (result.IsSuccedded)
            {
                Message = ApplicationMessage.InventoryStatus;
                return RedirectToPage("./Index");
            }
            else
            {
                Message = ApplicationMessage.SomeThingWrong;
                return RedirectToPage("./Index");
            }
            
        }

        public IActionResult OnGetNotInStock(long id)
        {
            var result = _productApplication.NotInStock(id);
            if (result.IsSuccedded)
            {
                Message = ApplicationMessage.InventoryStatus;
                return RedirectToPage("./Index");
            }
            else
            {
                Message = ApplicationMessage.SomeThingWrong;
                return RedirectToPage("./Index");
            }
        }
    }
}
