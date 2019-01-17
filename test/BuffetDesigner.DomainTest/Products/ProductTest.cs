using Xunit;
using Xunit.Abstractions;
using System;
using Bogus;
using ExpectedObjects;
using BuffetDesigner.Domain.Enums;
using BuffetDesigner.Domain._Base;
using BuffetDesigner.Domain.Exception;
using BuffetDesigner.DomainTest._Builders;
using BuffetDesigner.DomainTest._Util;
using BuffetDesigner.Domain.Product;

namespace BuffetDesigner.DomainTest.Products
{
    public class ProductTest
    {
        private readonly string _nomeApresentacao;
        private readonly string _descricao;
        private readonly string _codBonanza;
        private readonly Status _status;
        private readonly string _fotoProjeto;
        private readonly string _fotoIlustrativa;
        private readonly string _fotoDetalhe1;
        private readonly string _fotoDetalhe2;
        private readonly string _fotoDetalhe3;
        private Faker _faker { get; }

        public ProductTest()
        {
            _faker = new Faker();

            _nomeApresentacao = _faker.Commerce.ProductName();
            _descricao = _faker.Commerce.ProductAdjective();
            _codBonanza = _faker.Random.Int(100, 999).ToString();
            _status = Status.Ativo;
            _fotoProjeto = "https://res.cloudinary.com/fredycarvalho/image/upload/v1547383101/Bonanza/projeto.jpg";
            _fotoIlustrativa = "https://res.cloudinary.com/fredycarvalho/image/upload/v1547383101/Bonanza/ilustrativa.jpg";
            _fotoDetalhe1 = "https://res.cloudinary.com/fredycarvalho/image/upload/v1547383101/Bonanza/detalhe1.jpg";
            _fotoDetalhe2 = "https://res.cloudinary.com/fredycarvalho/image/upload/v1547383101/Bonanza/detalhe2.jpg";
            _fotoDetalhe3 = "https://res.cloudinary.com/fredycarvalho/image/upload/v1547383101/Bonanza/detalhe3.jpg";

        }

        [Fact]
        public void ShouldCreateProduct()
        {
            var expectedProduct = new
            {
                NomeApresentacao = _nomeApresentacao,
                Descricao = _descricao,
                CodBonanza = _codBonanza,
                Status = _status,
                FotoProjeto = _fotoProjeto,
                FotoIlustrativa = _fotoIlustrativa,
                FotoDetalhe1 = _fotoDetalhe1,
                FotoDetalhe2 = _fotoDetalhe2,
                FotoDetalhe3 = _fotoDetalhe3
            };

            var product = new Product(expectedProduct.NomeApresentacao, expectedProduct.Descricao, expectedProduct.CodBonanza, 
                                        expectedProduct.Status, expectedProduct.FotoProjeto, expectedProduct.FotoIlustrativa,
                                        expectedProduct.FotoDetalhe1, expectedProduct.FotoDetalhe2, expectedProduct.FotoDetalhe3);

            expectedProduct.ToExpectedObject().ShouldMatch(product);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ShouldNotCreateProductWithInvalidNomeApresentacao(string nomeApresentacaoInvalido)
        {
            Assert.Throws<DomainException>(() =>
                ProductBuilder.New().WithNomeApresentacao(nomeApresentacaoInvalido).Build())
                .WithMessage(Resource.InvalidProductApresentation);                
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ShouldNotCreateProductWithInvalidCodBonanza(string codBonanzaInvalido)
        {
            Assert.Throws<DomainException>(() =>
                ProductBuilder.New().WithCodBonanza(codBonanzaInvalido).Build())
                .WithMessage(Resource.InvalidProductCode);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ShouldNotCreateProductWithInvalidFotoProjeto(string fotoProjetoInvalida)
        {
            Assert.Throws<DomainException>(() =>
                ProductBuilder.New().WithFotoProjeto(fotoProjetoInvalida).Build())
                .WithMessage(Resource.InvalidProductProjectPhoto);
        }

        [Theory]
        [InlineData("Travessa Oval")]
        [InlineData("Travessa Retangular")]
        public void ShouldChangeNomeApresentacao(string nomeApresentacao)
        {
            var exceptedNomeApresentacao = nomeApresentacao;
            var product = ProductBuilder.New().Build();

            product.ChangeNomeApresentacao(exceptedNomeApresentacao);

            Assert.Equal(exceptedNomeApresentacao, product.NomeApresentacao);
        }
        
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ShouldNotChangeNomeApresentacaoWithInvalidValue(string invalidValue)
        {
            var product = ProductBuilder.New().Build();

            Assert.Throws<DomainException>(() =>
                product.ChangeNomeApresentacao(invalidValue)
            ).WithMessage(Resource.InvalidProductApresentation);
        }

        
        [Theory]
        [InlineData("2030")]
        [InlineData("1")]
        public void ShouldChangeCodBonanza(string codBonanza)
        {
            var expectedCodBonanza = codBonanza;
            var product = ProductBuilder.New().Build();

            product.ChangeCodBonanza(expectedCodBonanza);

            Assert.Equal(expectedCodBonanza, product.CodBonanza);
        }
        
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ShouldNotChangeCodBonanzaWithInvalidValue(string invalidValue)
        {
            var product = ProductBuilder.New().Build();

            Assert.Throws<DomainException>(() =>
                product.ChangeCodBonanza(invalidValue)
            ).WithMessage(Resource.InvalidProductCode);
        }

        [Theory]
        [InlineData("https://res.cloudinary.com/fredycarvalho/image/upload/v1547383101/Bonanza/projetoEdited.jpg")]
        public void ShouldChangeFotoProjeto(string photoProjeto)
        {
            var exceptedFotoProjeto = photoProjeto;
            var product = ProductBuilder.New().Build();

            product.ChangeFotoProjeto(exceptedFotoProjeto);

            Assert.Equal(exceptedFotoProjeto, product.FotoProjeto);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ShoulNotChangeFotoProjetoWithInvalidValue(string invalidValue)
        {
            var product = ProductBuilder.New().Build();

            Assert.Throws<DomainException>(() => 
                product.ChangeFotoProjeto(invalidValue)                
            ).WithMessage(Resource.InvalidProductProjectPhoto);


        }

        [Theory]
        [InlineData(Status.Desabilitado)]
        [InlineData(Status.Excluido)]
        public void ShouldChangeStatus(Status status)
        {
            var expectedStatus = status;
            var product = ProductBuilder.New().Build();

            product.ChangeStatus(expectedStatus);

            Assert.Equal(expectedStatus, product.Status);
        }
        
        [Fact]
        public void ShouldChangeDetais()
        {
            var expectedDescricao = _descricao;
            var expectedFotoIlustrativa = _fotoIlustrativa;
            var expectedFotoDetalhe1 = _fotoDetalhe1;
            var expectedfotoDetalhe2 = _fotoDetalhe2;
            var expectedfotoDetalhe3 = _fotoDetalhe3;

            var product = ProductBuilder.New().Build();

            product.ChangeDetails(expectedDescricao, expectedFotoIlustrativa, expectedFotoDetalhe1, expectedfotoDetalhe2, expectedfotoDetalhe3);

            Assert.Equal(expectedDescricao, product.Descricao);
            Assert.Equal(expectedFotoIlustrativa, product.FotoIlustrativa);
            Assert.Equal(expectedFotoDetalhe1, product.FotoDetalhe1);
            Assert.Equal(expectedfotoDetalhe2, product.FotoDetalhe2);
            Assert.Equal(expectedfotoDetalhe3, product.FotoDetalhe3);

        }

    }
}