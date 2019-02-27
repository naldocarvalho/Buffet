using System;
using BuffetDesigner.Domain._Base;
using BuffetDesigner.Domain.Category.Dtos;
using BuffetDesigner.Domain.Enums;

namespace BuffetDesigner.Domain.Category
{
    public class CategoryStorer
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryStorer(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public void Storer(CategoryDto categoryDto)
        {
            var categoryFound = _categoryRepository.GetByDescricao(categoryDto.Descricao);

            RuleValidator.New()
                .When(categoryFound != null && categoryFound.Id != categoryDto.Id, Resource.CategoryDescriptionAlreadyExists)
                .When(!Enum.TryParse<Status>(categoryDto.Status, out var status), Resource.InvalidStatus)
                .ThrowExceptionIfExists();

            
            if (categoryDto.Id == 0)
            {
                var category = new Category(categoryDto.Descricao, status);
                _categoryRepository.Add(category);
            }
            else
            {
                categoryFound = _categoryRepository.GetById(categoryDto.Id);

                RuleValidator.New()
                    .When(categoryFound == null, Resource.CategoryNotFound)
                    .ThrowExceptionIfExists();

                categoryFound.ChangeDescricao(categoryDto.Descricao);
                categoryFound.ChangeStatus(status);                

                //_categoryRepository.TesteAlterado();
            }
            
        }
    }
}