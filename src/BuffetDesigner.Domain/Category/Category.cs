using System;
using System.Collections.Generic;
using BuffetDesigner.Domain._Base;
using BuffetDesigner.Domain.Enums;
using BuffetDesigner.Domain.Exception;

namespace BuffetDesigner.Domain.Category
{
    public class Category: BaseEntity
    {
        public string Descricao { get; private set; }
        public Status Status { get; private set; }

        private Category() { }
        public Category(string descricao, Status status)
        {

            RuleValidator.New()
                .When(string.IsNullOrEmpty(descricao), Resource.InvalidCategoryDescription)                
                .ThrowExceptionIfExists();
            

            Descricao = descricao;
            Status = status;
        }

        public void ChangeDescricao(string descricao)
        {
            Descricao = descricao;
        }

        public void ChangeStatus(Status status)
        {
            Status = status;
        }
        
    }
}