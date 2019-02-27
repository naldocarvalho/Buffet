using System;
using System.Globalization;
using BuffetDesigner.Domain._Base;
using BuffetDesigner.Domain.Enums;
using BuffetDesigner.Domain.Project.Dtos;

namespace BuffetDesigner.Domain.Project
{
    public class ProjectStorer
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectStorer(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public void Storer(ProjectDto projectDto)
        {
            var dataOrcamento = new DateTime();

            RuleValidator.New()
                .When(!Enum.TryParse<Status>(projectDto.Status, out var status), Resource.InvalidStatus)
                .When(!string.IsNullOrEmpty(projectDto.DataOrcamento) && !DateTime.TryParseExact(projectDto.DataOrcamento, "ddMMyyyy", CultureInfo.InvariantCulture, 
                                                System.Globalization.DateTimeStyles.None, out dataOrcamento), Resource.InvalidDataOrcamentoProjeto)
                .ThrowExceptionIfExists();

            if (projectDto.Id == 0)
            {
                var project = new Project(projectDto.Descricao, projectDto.Largura, projectDto.Comprimento, 
                                            dataOrcamento, status);

                _projectRepository.Add(project);
            }
            else
            {
                var project = _projectRepository.GetById(projectDto.Id);

                RuleValidator.New()
                    .When(project == null, Resource.ProjectNotFound)
                    .ThrowExceptionIfExists();

                project.ChangeDataOrcamento(dataOrcamento);
                project.ChangeDescricao(projectDto.Descricao);
                project.ChangeStatus(status);
            }
        }
    }
}