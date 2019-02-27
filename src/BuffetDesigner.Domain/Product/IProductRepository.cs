using BuffetDesigner.Domain._Base;

namespace BuffetDesigner.Domain.Product
{
    public interface IProductRepository: IRepository<Product>
    {
        Product GetByDescricao(string descricao);
        Product GetByCodBonanza(string codBonanza);


    }
}