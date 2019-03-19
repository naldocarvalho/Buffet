using Bogus;
using BuffetDesigner.Domain._Base;
using BuffetDesigner.Domain.ItemProjeto;
using BuffetDesigner.Domain.ItemProjeto.Dtos;
using Xunit.Abstractions;

namespace BuffetDesigner.DomainTest.ItensProjeto
{
    public class ItemProjetoStorerTest
    {
        private readonly ItemProjetoDto _itemProjetoDto;
        private readonly ITestOutputHelper _output;

        public ItemProjetoStorerTest(ITestOutputHelper output)
        {
            _output = output;            
            var fake = new Faker();
            
            _itemProjetoDto = new ItemProjetoDto
            {
                ProjetoId = 1,
                ProdutoId = 1,
                CoordenadaX = fake.Random.Double(100, 500),
                CoordenadaY = fake.Random.Double(200, 300)
            };
            
        }   
    }

    
}