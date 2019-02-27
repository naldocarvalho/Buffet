using System;
using Bogus;
using BuffetDesigner.Domain._Base;
using BuffetDesigner.Domain.Enums;
using BuffetDesigner.Domain.Exception;
using BuffetDesigner.Domain.Product;
using BuffetDesigner.Domain.Product.Dtos;
using BuffetDesigner.DomainTest._Builders;
using BuffetDesigner.DomainTest._Util;
using Moq;
using Xunit;
using Xunit.Abstractions;

namespace BuffetDesigner.DomainTest.Products
{
    public class ProductStorerTest
    {
        private readonly ITestOutputHelper _output;
        private readonly ProductDto _productDto;
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly ProductStorer _productStorer;

        public ProductStorerTest(ITestOutputHelper output)
        {
            _output = output;
            _output.WriteLine("Iniciando teste na classe de serviço de produto");

            var faker = new Faker();

            _productDto = new ProductDto
            {
               //Id = fake.Random.Number(1000),
               NomeApresentacao = faker.Commerce.ProductName(),
               Descricao = faker.Commerce.ProductAdjective(),
               CodBonanza = faker.Random.Int(100, 999).ToString(),
               Status = Status.Ativo.ToString(),
               FotoProjeto = "https://res.cloudinary.com/fredycarvalho/image/upload/v1547383101/Bonanza/projeto.jpg",
               FotoIlustrativa = "https://res.cloudinary.com/fredycarvalho/image/upload/v1547383101/Bonanza/ilustrativa.jpg",
               FotoDetalhe1 = "https://res.cloudinary.com/fredycarvalho/image/upload/v1547383101/Bonanza/detalhe1.jpg",
               FotoDetalhe2 = "https://res.cloudinary.com/fredycarvalho/image/upload/v1547383101/Bonanza/detalhe2.jpg",
               FotoDetalhe3 = "https://res.cloudinary.com/fredycarvalho/image/upload/v1547383101/Bonanza/detalhe3.jpg"
            };

            _productRepositoryMock = new Mock<IProductRepository>();
            _productStorer = new ProductStorer(_productRepositoryMock.Object);

        }

        [Fact]
        public void ShouldStorerProduct() 
        {
            _productStorer.Storer(_productDto);

            _productRepositoryMock.Verify(r => 
                r.Add(
                    It.Is<Product>(
                        p => p.NomeApresentacao == _productDto.NomeApresentacao &&
                        p.Descricao == _productDto.Descricao &&
                        p.CodBonanza == _productDto.CodBonanza &&
                        p.Status.ToString() == _productDto.Status &&
                        p.FotoProjeto == _productDto.FotoProjeto &&
                        p.FotoIlustrativa == _productDto.FotoIlustrativa &&
                        p.FotoDetalhe1 == _productDto.FotoDetalhe1 &&
                        p.FotoDetalhe2 == _productDto.FotoDetalhe2 &&
                        p.FotoDetalhe3 == _productDto.FotoDetalhe3
                    )
                ), Times.AtLeast(1)
            );
        }

        [Fact]
        public void ShouldChangeProduct()
        {
            _productDto.Id = 321;

            var product = ProductBuilder.New().WithDescricao("Panela Branca").WithCodBonanza("500")
                            .WithFotoProjeto("http://google.com/").WithStatus(Status.Excluido).Build();
            
            _productRepositoryMock.Setup(p => p.GetById(_productDto.Id)).Returns(product);
            _productStorer.Storer(_productDto);

            Assert.Equal(_productDto.Descricao, product.Descricao);
            Assert.Equal(_productDto.CodBonanza, product.CodBonanza);
            Assert.Equal(_productDto.FotoProjeto, product.FotoProjeto);
        
        }

        [Fact]
        public void ShouldNotChangeProductNotExist()
        {
            var productIdNotExist = 123;
            _productDto.Id = productIdNotExist;

            Product productNotFound = null;

            _productRepositoryMock.Setup(p => p.GetById(productIdNotExist)).Returns(productNotFound);

            Assert.Throws<DomainException>(() =>
                _productStorer.Storer(_productDto))
            .WithMessage(Resource.ProductNotFound);
            
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ShouldNotCreateProductWithInvalidNomeApresentacao(string nomeApresentacao)
        {
            _productDto.NomeApresentacao = nomeApresentacao;

            Assert.Throws<DomainException>(() =>
                _productStorer.Storer(_productDto))
            .WithMessage(Resource.InvalidProductApresentation);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ShouldNotCreateProductWithInvalidCodBonanza(string codBonanza)
        {
            _productDto.CodBonanza = codBonanza;

            Assert.Throws<DomainException>(() =>
                _productStorer.Storer(_productDto))
            .WithMessage(Resource.InvalidProductCode);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ShouldNotCreateProductWithInvalidFotoProjeto(string fotoProjeto)
        {
            _productDto.FotoProjeto = fotoProjeto;

            Assert.Throws<DomainException>(() =>
                _productStorer.Storer(_productDto))
            .WithMessage(Resource.InvalidProductProjectPhoto);
        }

    }
}