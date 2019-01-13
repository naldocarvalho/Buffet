using System;
using BuffetDesigner.Domain.Category;
using BuffetDesigner.Domain.Enums;
using BuffetDesigner.DomainTest.Categories;

namespace BuffetDesigner.DomainTest._Builders
{
    public class CategoryBuilder
    {
        private int _id;
        private string _descricao = "Travessas";
        private Status _status = Status.Ativo;

        public static CategoryBuilder New()
        {
            return new CategoryBuilder();
        }

        public CategoryBuilder WithDescription(string descricao) 
        {
            _descricao = descricao;
            return this;
        }
        public CategoryBuilder WithStatus(Status status) 
        {
            _status = status;
            return this;
        }
        public CategoryBuilder WithId(int id)
        {
            _id = id;
            return this;
        }

        public Category Build() {
            var category = new Category(_descricao, _status);

             if (_id > 0)
            {
                var propertyInfo = category.GetType().GetProperty("Id");
                propertyInfo.SetValue(category, Convert.ChangeType(_id, propertyInfo.PropertyType), null);
            }

            return category;

        }
    }
}