namespace ORH.Application.Interface.Product
{
    public interface IUpdateProduct
    {
        Task<bool> ExecuteUpdateProductQuantityAsync(int id, int quantity);
    }
}
