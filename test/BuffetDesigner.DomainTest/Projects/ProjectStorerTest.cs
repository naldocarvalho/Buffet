using System;
using System.Globalization;
using Bogus;
using BuffetDesigner.Domain._Base;
using BuffetDesigner.Domain.Enums;
using BuffetDesigner.Domain.Exception;
using BuffetDesigner.Domain.Project;
using BuffetDesigner.Domain.Project.Dtos;
using BuffetDesigner.DomainTest._Builders;
using BuffetDesigner.DomainTest._Util;
using Moq;
using Xunit;
using Xunit.Abstractions;

namespace BuffetDesigner.DomainTest.Projects
{
    public class ProjectStorerTest
    {
        private readonly ITestOutputHelper _output;    
        private readonly ProjectDto _projetctDto;
        private readonly Mock<IProjectRepository> _projectRepositoyMock;
        private readonly ProjectStorer _projectStorer;
        public ProjectStorerTest(ITestOutputHelper output)
        {
            _output = output;

            var faker = new Faker();

            _projetctDto = new ProjectDto
            {
               //Id = fake.Random.Number(1000),
               Descricao = faker.Company.CompanyName(),
               Largura = faker.Random.Double(200,500),
               Comprimento = faker.Random.Double(300, 600),
               DataOrcamento = DateTime.Today.ToString("ddMMyyyy"),// faker.Date.Recent().ToString("ddMMyyyy"),
               Status = Status.Ativo.ToString()
            };

            _projectRepositoyMock = new Mock<IProjectRepository>();
            _projectStorer = new ProjectStorer(_projectRepositoyMock.Object);
        }

        [Fact]
        public void ShouldStorerProject()
        {
            _projectStorer.Storer(_projetctDto);

            _projectRepositoyMock.Verify(r =>
                r.Add(
                    It.Is<Project>(
                        p => p.Descricao == _projetctDto.Descricao &&
                        p.Largura == _projetctDto.Largura &&
                        p.Status.ToString() == _projetctDto.Status
                    )

                ), Times.AtLeast(1)
            );

        }
        
        [Fact]
        public void ShouldChangeProject()
        {
            _projetctDto.Id = 321;

            var project = ProjectBuilder.New().WithDescricao("Bonanza Alimentos").WithComprimento(500)
                            .WithLargura(200).WithStatus(Status.Ativo).WithDataOrcamento(Convert.ToDateTime("01/01/2000")).Build();
            
            _projectRepositoyMock.Setup(p => p.GetById(_projetctDto.Id)).Returns(project);
            _projectStorer.Storer(_projetctDto);

            Assert.Equal(_projetctDto.Descricao, project.Descricao);
            Assert.Equal(_projetctDto.DataOrcamento, project.DataOrcamento?.ToString("ddMMyyyy"));
        
        }

        [Fact]
        public void ShouldNotChangeProjectNotExist()
        {
            var projectIdNotExist = 123;

            _projetctDto.Id = projectIdNotExist;

            Project projectNotFound = null;

            _projectRepositoyMock.Setup(p =>
                p.GetById(projectIdNotExist)).Returns(projectNotFound);

            Assert.Throws<DomainException>(() =>
                _projectStorer.Storer(_projetctDto)
            ).WithMessage(Resource.ProjectNotFound);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ShouldNotCreateProjectWithInvalidDescription(string invalidDescription)
        {
            _projetctDto.Descricao = invalidDescription;

            Assert.Throws<DomainException>(() =>
                _projectStorer.Storer(_projetctDto)
            ).WithMessage(Resource.InvalidProjectDescription);
        }

        [Theory]
        [InlineData(-10)]
        [InlineData(0)]
        public void ShoulNotCreateProjectWithInvalidWidth(double invalidWidth)
        {
            _projetctDto.Largura = invalidWidth;

            Assert.Throws<DomainException>(() =>
                _projectStorer.Storer(_projetctDto)
            ).WithMessage(Resource.InvalidProjectWidth);
        }
        
        [Theory]
        [InlineData(-50)]
        [InlineData(0)]
        public void ShouldNotCreateProjectWithInvalidLenght(double invalidLength)
        {
            _projetctDto.Comprimento = invalidLength;

            Assert.Throws<DomainException>(() => 
                _projectStorer.Storer(_projetctDto)
            ).WithMessage(Resource.InvalidProjectLength);
        }
    }
}