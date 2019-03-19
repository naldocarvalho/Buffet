using System;
using BuffetDesigner.Domain.ItemProjeto;
using BuffetDesigner.DomainTest.ItensProjeto;

namespace BuffetDesigner.DomainTest._Builders
{
    public class ItemProjetoBuilder
    {
        private int _projetoId = 1;
        private int _produtoId = 1;
        private double _coordenadoX = 10;
        private double _coordenadaY = 20;
        private int _id;

        public static ItemProjetoBuilder New()
        {
            return new ItemProjetoBuilder();
        }

        public ItemProjetoBuilder WithId(int id)
        {
            _id = id;
            return this;
        }

        public ItemProjetoBuilder WithProjetoId(int projetoId)
        {
            _projetoId = projetoId;
            return this;
        }

        public ItemProjetoBuilder WithProdutoId(int produtoId)
        {
            _produtoId = produtoId;
            return this;
        }

        public ItemProjetoBuilder WithCoordenadaX(double coordenadaX)
        {
            _coordenadoX = coordenadaX;
            return this;
        }

        public ItemProjetoBuilder WithCoordenadaY(double coordenadaY)
        {
            _coordenadaY = coordenadaY;
            return this;
        }

        public ItemProjeto Build()
        {
            var itemProjeto = new ItemProjeto(_projetoId, _produtoId, _coordenadoX, _coordenadaY);

            if (_id > 0)
            {
                var propertyInfo = itemProjeto.GetType().GetProperty("Id");
                propertyInfo.SetValue(itemProjeto, Convert.ChangeType(_id, propertyInfo.PropertyType), null);
            }

            return itemProjeto;
        }

    }

    
}