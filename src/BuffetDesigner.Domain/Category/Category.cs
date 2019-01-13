using System;
using System.Collections.Generic;
using BuffetDesigner.Domain._Base;
using BuffetDesigner.Domain.Enums;
using BuffetDesigner.Domain.Exception;

namespace BuffetDesigner.Domain.Category
{
    public class Category: BaseEntity
    {
        private Category() { }
        public Category(string descricao, Status status)
        {

            // if (string.IsNullOrEmpty(descricao))
            // {
            //     throw new ArgumentException(Resource.InvalidCategory);               
            // }

            RuleValidator.New()
                .When(string.IsNullOrEmpty(descricao), Resource.InvalidCategoryDescription)                
                .ThrowExceptionIfExists();
            

            Descricao = descricao;
            Status = status;
        }
        public string Descricao { get; private set; }
        public Status Status { get; private set; }

    }
}