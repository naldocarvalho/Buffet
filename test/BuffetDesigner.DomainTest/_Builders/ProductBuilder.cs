using System;
using BuffetDesigner.Domain.Enums;
using BuffetDesigner.Domain.Product;
using static BuffetDesigner.DomainTest.Products.ProductTest;

namespace BuffetDesigner.DomainTest._Builders
{
    public class ProductBuilder
    {
        private int _id;
        private string _nomeApresentacao = "Travessa Redonda";
        private string _descricao = "Travessa em aluminio polido";
        private string _codBonanza = "2020";
        private Status _status = Status.Ativo;
        private string _fotoProjeto = "https://res.cloudinary.com/fredycarvalho/image/upload/v1547383101/Bonanza/projeto.jpg";
        private string _fotoIlustrativa = "https://res.cloudinary.com/fredycarvalho/image/upload/v1547383101/Bonanza/ilustrativa.jpg";
        private string _fotoDetalhe1 = "https://res.cloudinary.com/fredycarvalho/image/upload/v1547383101/Bonanza/detalhe1.jpg";
        private string _fotoDetalhe2 = "https://res.cloudinary.com/fredycarvalho/image/upload/v1547383101/Bonanza/detalhe2.jpg";
        private string _fotoDetalhe3 = "https://res.cloudinary.com/fredycarvalho/image/upload/v1547383101/Bonanza/detalhe3.jpg";

        public static ProductBuilder New()
        {
            return new ProductBuilder();
        }

        public ProductBuilder WithNomeApresentacao(string nomeApresentacao)
        {
            _nomeApresentacao = nomeApresentacao;
            return this;
        }
        public ProductBuilder WithDescricao(string descricao)
        {
            _descricao = descricao;
            return this;
        }
        public ProductBuilder WithCodBonanza(string codBonanza)
        {
            _codBonanza = codBonanza;
            return this;
        }
        public ProductBuilder WithStatus(Status status)
        {
            _status = status;
            return this;
        }
        public ProductBuilder WithFotoProjeto(string fotoProjeto)
        {
            _fotoProjeto = fotoProjeto;
            return this;
        }

        public ProductBuilder WithId(int id)
        {
            _id = id;
            return this;
        }

        public Product Build()
        {
            var product = new Product(_nomeApresentacao, _descricao, _codBonanza, _status, _fotoProjeto, _fotoIlustrativa, _fotoDetalhe1, _fotoDetalhe2, _fotoDetalhe3);

            if (_id > 0)
            {
                var propertyInfo = product.GetType().GetProperty("Id");
                propertyInfo.SetValue(product, Convert.ChangeType(_id, propertyInfo.PropertyType), null);
            }

            return product;
        }

    }
}