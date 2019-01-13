using Xunit;
using Xunit.Abstractions;
using Bogus;
using BuffetDesigner.Domain.Enums;
using System;
using ExpectedObjects;
using BuffetDesigner.Domain.Exception;
using BuffetDesigner.DomainTest._Builders;
using BuffetDesigner.DomainTest._Util;
using BuffetDesigner.Domain._Base;
using BuffetDesigner.Domain.Project;

namespace BuffetDesigner.DomainTest.Projects
{
    public class ProjectTest
    {
        private readonly string _descricao;
        private readonly double _largura;
        private readonly double _comprimento;
        private readonly DateTime _dataOrcamento;
        private readonly Status _status;

        public ProjectTest()
        {
            var _faker = new Faker();

            _descricao = _faker.Company.CompanyName();
            _largura = _faker.Random.Int(100, 300);
            _comprimento = _faker.Random.Int(200, 500);
            _dataOrcamento = _faker.Date.Recent();
            _status = Status.Ativo;
        }

        [Fact]
        public void ShoulCreateProject()
        {
            var exceptedProject = new
            {
                Descricao = _descricao,
                Largura = _largura,
                Comprimento = _comprimento,
                DataOrcamento = _dataOrcamento,
                Status = _status
            };

            var project = new Project(exceptedProject.Descricao, exceptedProject.Largura, exceptedProject.Comprimento, exceptedProject.DataOrcamento, exceptedProject.Status);

            exceptedProject.ToExpectedObject().Matches(project);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ShouldNotCreateInvalidDescricao(string invalidValue)
        {
            Assert.Throws<DomainException>(() =>
                ProjectBuilder.New().WithDescricao(invalidValue).Build())
                .WithMessage(Resource.InvalidProjectDescription);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        public void ShouldNotCreateWithInvalidLargura(double invalidValue)
        {
            Assert.Throws<DomainException>(() =>
                ProjectBuilder.New().WithLargura(invalidValue).Build())
                .WithMessage(Resource.InvalidProjectLength);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-20)]
        public void ShouldNotCreateWithInvalidComprimento(double invalidValue)
        {
            Assert.Throws<DomainException>(() =>
                ProjectBuilder.New().WithComprimento(invalidValue).Build())
                .WithMessage(Resource.InvalidProjectWidth);
        }


        [Fact]
        public void ShouldChangeDataOrcamento()
        {
            var exceptedValue = _dataOrcamento;
            var project = ProjectBuilder.New().Build();

            project.ChangeDataOrcamento(exceptedValue);

            Assert.Equal(exceptedValue, project.DataOrcamento);
        }

        [Fact]
        public void ShouldChangeToNullDataOrcamento()
        {
            var project = ProjectBuilder.New().WithDataOrcamento(DateTime.Now).Build();
            
            project.ChangeDataOrcamento(null);

            Assert.Null(project.DataOrcamento);
        }

        [Theory]
        [InlineData(Status.Desabilitado)]
        [InlineData(Status.Excluido)]
        public void ShouldChangeStatus(Status status)
        {
            var project = ProjectBuilder.New().Build();

            project.ChangeStatus(status);

            Assert.Equal(status, project.Status);
        }


    }
}