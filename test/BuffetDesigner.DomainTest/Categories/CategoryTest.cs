using Xunit;
using Xunit.Abstractions;
using System;
using Bogus;
using ExpectedObjects;
using BuffetDesigner.DomainTest._Util;
using BuffetDesigner.DomainTest._Builders;
using BuffetDesigner.Domain.Category;
using BuffetDesigner.Domain.Enums;
using BuffetDesigner.Domain.Exception;
using BuffetDesigner.Domain._Base;

namespace BuffetDesigner.DomainTest.Categories
{
    public class CategoryTest : IDisposable
    {
        private ITestOutputHelper _output;
        public Faker _faker { get; }
        private readonly string _descricao;
        private readonly Status _status;

        public CategoryTest(ITestOutputHelper output)
        {
           _output = output;
           _output.WriteLine("Iniciando teste na classe de dominio de categoria");
           _faker = new Faker();

           _descricao = _faker.Commerce.Categories(1)[0];
           _status = Status.Ativo;
        }

        public void Dispose()
        {
            _output.WriteLine("Finalizando ...");
        }

        [Fact(DisplayName = "ShouldCreateCatogory")]
        public void ShouldCreateCatogory()
        {
            var categoryExpected = new
            {
                Descricao = _descricao,
                Status = _status
            };

            var category = new Category(categoryExpected.Descricao, categoryExpected.Status);
            //Assert.Equal("teste", _descricao);
            categoryExpected.ToExpectedObject().ShouldMatch(category);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ShouldNotCreateCategoryWithInvalidDescription(string invalidDescription)
        {
            // Assert.Throws<ArgumentException>(() =>
            //     CategoryBuilder.New().WithDescription(invalidDescription).Build());

            Assert.Throws<DomainException>(() =>
                CategoryBuilder.New().WithDescription(invalidDescription).Build())
            .WithMessage(Resource.InvalidCategoryDescription);
        }
       
    }

}

