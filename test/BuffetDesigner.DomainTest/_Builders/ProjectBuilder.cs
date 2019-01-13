using System;
using BuffetDesigner.Domain.Enums;
using BuffetDesigner.Domain.Project;
using static BuffetDesigner.DomainTest.Projects.ProjectTest;

namespace BuffetDesigner.DomainTest._Builders
{
    public class ProjectBuilder
    {
        private string _descricao = "Buffet Central";
        private double _largura = 100;
        private double _comprimento = 300;
        private DateTime? _dataOrcamento = null;
        private Status _status;

        public static ProjectBuilder New()
        {
            return new ProjectBuilder();
        }

        public ProjectBuilder WithDescricao(string descricao)
        {
            _descricao = descricao;
            return this;
        }
        public ProjectBuilder WithLargura(double largura)
        {
            _largura = largura;
            return this;
        }
        public ProjectBuilder WithComprimento(double comprimento)
        {
            _comprimento = comprimento;
            return this;
        }
        public ProjectBuilder WithDataOrcamento(DateTime? dataOrcamento)
        {
            _dataOrcamento = dataOrcamento;
            return this;
        }
        public ProjectBuilder WithStatus (Status status)
        {
            _status = status;
            return this;
        }

        public Project Build()
        {
            var project = new Project(_descricao, _largura, _comprimento, _dataOrcamento, _status);

            return project;
        }

    }
}