using System;
using Bogus;
using BuffetDesigner.Domain._Base;
using BuffetDesigner.Domain.Category;
using BuffetDesigner.Domain.Category.Dtos;
using BuffetDesigner.Domain.Enums;
using BuffetDesigner.Domain.Exception;
using BuffetDesigner.DomainTest._Builders;
using BuffetDesigner.DomainTest._Util;
using Moq;
using Xunit;
using Xunit.Abstractions;

namespace BuffetDesigner.DomainTest.Categories
{
    public class CategoryStorerTest
    {
        private ITestOutputHelper _output;
        private readonly CategoryDto _categoryDto;
        private readonly Mock<ICategoryRepository> _categoryRepositoryMock;
        private readonly CategoryStorer _categoryStorer;

        public CategoryStorerTest(ITestOutputHelper output)
        {
            var fake = new Faker();
            _output = output;
            _output.WriteLine("Iniciando teste na classe de servico de categoria");

            _categoryDto = new CategoryDto
            {
                //Id = fake.Random.Number(1000),
                Descricao = "Panela Grande",//fake.Commerce.Categories(1)[0],
                Status = Status.Ativo.ToString()
            };

           _categoryRepositoryMock = new Mock<ICategoryRepository>();
           _categoryStorer = new CategoryStorer(_categoryRepositoryMock.Object);
        }

        [Fact]
        public void ShouldStoreCategory()
        {
            
            _categoryStorer.Storer(_categoryDto);

            _categoryRepositoryMock.Verify(r => 
                r.Add(
                    It.Is<Category>(
                        c => c.Descricao == _categoryDto.Descricao &&
                         c.Status.ToString() == _categoryDto.Status
                    )
                ), Times.AtLeast(1)
            );

        }

        [Theory]
        [InlineData("Undefined")]
        [InlineData(null)]
        public void ShouldNotStoreCategoryWithInvalidStatus(string invalidStatus)
        {
            _categoryDto.Status = invalidStatus;

            Assert.Throws<DomainException>(() => 
                _categoryStorer.Storer(_categoryDto))
            .WithMessage(Resource.InvalidStatus);

        }

        [Fact]
        public void ShouldChangeCategory()
        {
            _categoryDto.Id = 321;

            var category = CategoryBuilder.New().WithDescription("Panela Pequena").WithStatus(Status.Excluido).Build();

            _categoryRepositoryMock.Setup(c => c.GetById(_categoryDto.Id)).Returns(category);

            _categoryStorer.Storer(_categoryDto);

            //_categoryRepositoryMock.Verify(c => c.TesteAlterado(), Times.AtLeast(1));

            Assert.Equal(_categoryDto.Descricao, category.Descricao);
            Assert.Equal(_categoryDto.Status, category.Status.ToString());


        }

        [Fact]
        public void ShouldNotChangeCategoryNotExist()
        {
            var categoryIdNotExist = 123;
            _categoryDto.Id = categoryIdNotExist;

            Category categoryNotFound = null;

            _categoryRepositoryMock.Setup(c => c.GetById(categoryIdNotExist)).Returns(categoryNotFound);

            Assert.Throws<DomainException>(() =>
                _categoryStorer.Storer(_categoryDto))
            .WithMessage(Resource.CategoryNotFound);
        }

        [Fact]
        public void ShouldNotCreateCategoryIfAlreadyExistDescription()
        {
            var categoryExist = CategoryBuilder.New().WithId(999).WithDescription(_categoryDto.Descricao).Build();

            _categoryRepositoryMock.Setup(c => c.GetByDescricao(categoryExist.Descricao)).Returns(categoryExist);

            Assert.Throws<DomainException>(() => 
                _categoryStorer.Storer(_categoryDto))
            .WithMessage(Resource.CategoryDescriptionAlreadyExists);
        }

    }

}