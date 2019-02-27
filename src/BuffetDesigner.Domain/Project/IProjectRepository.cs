using System;
using System.Collections.Generic;
using BuffetDesigner.Domain._Base;

namespace BuffetDesigner.Domain.Project
{
    public interface IProjectRepository: IRepository<Project>
    {
        Project GetByDescricao(string descricao);
        IList<Project> GetByDataOrcamento(DateTime from, DateTime until);
    }
}