using _0_FrameWork.Application;

namespace ShopManagement.Application.Contracts.Product;

public interface IProductApplication
{
    OperationResult Create(CreateProduct command);
    OperationResult Edit(EditProduct command);
    OperationResult IsInStock(long id);
    OperationResult NotInStock(long id);
    EditProduct GetDetails(long id);
    List<ProductViewModel> Search(ProductSearchModel searchModel);
}