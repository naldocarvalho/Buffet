using System;
using BuffetDesigner.Domain._Base;
using BuffetDesigner.Domain.Enums;

namespace BuffetDesigner.Domain.Project
{
    public class Project
    {
        public string Descricao { get; private set; }
        public double Largura { get; private set; }
        public double Comprimento { get; private set; }
        public DateTime? DataOrcamento { get; private set; }
        public Status Status { get; private set; }

        public Project(string descricao, double largura, double comprimento, DateTime? dataOrcamento, Status status)
        {
            RuleValidator.New()
                .When(string.IsNullOrEmpty(descricao), Resource.InvalidProjectDescription)
                .When(largura <= 0, Resource.InvalidProjectLength)
                .When(comprimento <= 0, Resource.InvalidProjectWidth)
                .ThrowExceptionIfExists();

            Descricao = descricao;
            Largura = largura;
            Comprimento = comprimento;
            DataOrcamento = dataOrcamento;
            Status = status;
        }

        public void ChangeDescricao(string descricao)
        {
            RuleValidator.New()
                .When(string.IsNullOrEmpty(descricao), Resource.InvalidProjectDescription)
                .ThrowExceptionIfExists();

            Descricao = descricao;
        }

        public void ChangeDataOrcamento(DateTime? dataOrcamento)
        {
            DataOrcamento = dataOrcamento;
        }

        public void ChangeStatus(Status status)
        {
            Status = status;
        }
    }
}