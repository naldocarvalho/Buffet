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
                .WithMessage(Resource.ProductApresentationInvalid);                
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ShouldNotCreateProductWithInvalidCodBonanza(string codBonanzaInvalido)
        {
            Assert.Throws<DomainException>(() =>
                ProductBuilder.New().WithCodBonanza(codBonanzaInvalido).Build())
                .WithMessage(Resource.ProductCodeInvalid);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ShouldNotCreateProductWithInvalidFotoProjeto(string fotoProjetoInvalida)
        {
            Assert.Throws<DomainException>(() =>
                ProductBuilder.New().WithFotoProjeto(fotoProjetoInvalida).Build())
                .WithMessage(Resource.ProductProjectPhotoInvalid);
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
            ).WithMessage(Resource.ProductApresentationInvalid);
        }

        
        [Theory]
        [InlineData("2030")]
        [InlineData("1")]
        public void ShouldChangeCodBonanza(string codBonanza)
        {
            var exceptedCodBonanza = codBonanza;
            var product = ProductBuilder.New().Build();

            product.ChangeCodBonanza(exceptedCodBonanza);

            Assert.Equal(exceptedCodBonanza, product.CodBonanza);
        }
        
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ShouldNotChangeCodBonanzaWithInvalidValue(string invalidValue)
        {
            var product = ProductBuilder.New().Build();

            Assert.Throws<DomainException>(() =>
                product.ChangeCodBonanza(invalidValue)
            ).WithMessage(Resource.ProductCodeInvalid);
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
            ).WithMessage(Resource.ProductProjectPhotoInvalid);


        }

        [Theory]
        [InlineData(Status.Desabilitado)]
        [InlineData(Status.Excluido)]
        public void ShouldChangeStatus(Status status)
        {
            var exceptedStatus = status;
            var product = ProductBuilder.New().Build();

            product.ChangeStatus(exceptedStatus);

            Assert.Equal(exceptedStatus, product.Status);
        }
        
        [Fact]
        public void ShouldChangeDetais()
        {
            var exceptedDescricao = _descricao;
            var exceptedFotoIlustrativa = _fotoIlustrativa;
            var exceptedFotoDetalhe1 = _fotoDetalhe1;
            var exceptedfotoDetalhe2 = _fotoDetalhe2;
            var exceptedfotoDetalhe3 = _fotoDetalhe3;

            var product = ProductBuilder.New().Build();

            product.ChangeDetails(exceptedDescricao, exceptedFotoIlustrativa, exceptedFotoDetalhe1, exceptedfotoDetalhe2, exceptedfotoDetalhe3);

            Assert.Equal(exceptedDescricao, product.Descricao);
            Assert.Equal(exceptedFotoIlustrativa, product.FotoIlustrativa);
            Assert.Equal(exceptedFotoDetalhe1, product.FotoDetalhe1);
            Assert.Equal(exceptedfotoDetalhe2, product.FotoDetalhe2);
            Assert.Equal(exceptedfotoDetalhe3, product.FotoDetalhe3);

        }

    }
}