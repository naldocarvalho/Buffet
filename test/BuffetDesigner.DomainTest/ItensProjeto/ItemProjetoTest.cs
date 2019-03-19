using Bogus;
using BuffetDesigner.Domain._Base;
using BuffetDesigner.Domain.Exception;
using ExpectedObjects;
using BuffetDesigner.DomainTest._Util;
using Xunit;
using BuffetDesigner.DomainTest._Builders;
using BuffetDesigner.Domain.ItemProjeto;

namespace BuffetDesigner.DomainTest.ItensProjeto
{
    public class ItemProjetoTest
    {
        private readonly Faker _faker;
        private readonly int _projetoId;
        private readonly int _produtoId;
        private readonly double _coordenadoX;
        private readonly double _coordenadoY;

        public ItemProjetoTest()
        {
            _faker = new Faker();

            _projetoId = 1;
            _produtoId = 1;
            _coordenadoX = _faker.Random.Number(100, 1000);
            _coordenadoY = _faker.Random.Number(100, 1000);
        }

        [Fact]
        public void ShouldCreateItemProjeto()
        {
            var ItemProjetoExpected = new
            {
                ProjetoId = _projetoId,
                ProdutoId = _produtoId,
                CoordenadaX = _coordenadoX,
                CoordenadaY = _coordenadoY
            };

            var itemProjeto = new ItemProjeto(ItemProjetoExpected.ProjetoId, ItemProjetoExpected.ProdutoId, ItemProjetoExpected.CoordenadaX, ItemProjetoExpected.CoordenadaY);

            ItemProjetoExpected.ToExpectedObject().ShouldMatch(itemProjeto);
        }
        
        [Theory]
        [InlineData(-1)]
        [InlineData(-10)]
        public void ShouldNotCreateItemProjetoWithInvalidProjetoId(int invalidValue)
        {
            Assert.Throws<DomainException>(() => 
                ItemProjetoBuilder.New().WithProjetoId(invalidValue).Build())
            .WithMessage(Resource.InvalidItemProjetoProjetoId);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-10)]
        public void ShouldNotCreateItemProjetoWithInvalidProdutoId(int invalidValue)
        {
            Assert.Throws<DomainException>(() => 
                ItemProjetoBuilder.New().WithProdutoId(invalidValue).Build())
            .WithMessage(Resource.InvalidItemProjetoProdutoId);
        }
    }

    
}