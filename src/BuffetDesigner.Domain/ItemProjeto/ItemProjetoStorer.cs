using BuffetDesigner.Domain._Base;
using BuffetDesigner.Domain.ItemProjeto.Dtos;
using BuffetDesigner.Domain.Project;

namespace BuffetDesigner.Domain.ItemProjeto
{
    public class ItemProjetoStorer
    {
        private readonly IProjectRepository _projectRepository;

        public ItemProjetoStorer(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }
        public void Storer(ItemProjetoDto itemProjetoDto)
        {
            
            var projetoFound = _projectRepository.GetById(itemProjetoDto.ProjetoId);

            RuleValidator.New()
                .When(projetoFound == null, Resource.ProjectNotFound);
        }
    }
}