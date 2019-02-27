using BuffetDesigner.Domain._Base;

namespace BuffetDesigner.Domain.Category
{
    public interface ICategoryRepository: IRepository<Category>
    {
        Category GetByDescricao(string descricao);
        //void TesteAlterado();
    }
}