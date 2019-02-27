namespace BuffetDesigner.Domain.Project.Dtos
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public string Descricao { get;  set; }
        public double Largura { get;  set; }
        public double Comprimento { get;  set; }
        public string DataOrcamento { get;  set; }
        public string Status { get;  set; }
    }
}