using BuffetDesigner.Domain._Base;

namespace BuffetDesigner.Domain.ItemProjeto
{
    public class ItemProjeto : BaseEntity
    {
        public int ProjetoId {get; private set; }
        public int ProdutoId {get; private set; }
        public double CoordenadaX {get; private set; }
        public double CoordenadaY {get; private set; }

        public ItemProjeto(int projetoId, int produtoId, double coordenadaX, double coordenadaY)
        {
            RuleValidator.New()
                .When(projetoId <= 0, Resource.InvalidItemProjetoProjetoId)
                .When(produtoId <= 0, Resource.InvalidItemProjetoProdutoId)
                .ThrowExceptionIfExists();

            ProjetoId = projetoId;
            ProdutoId = produtoId;
            CoordenadaX = coordenadaX;
            CoordenadaY = coordenadaY;
        }
    }

    
}