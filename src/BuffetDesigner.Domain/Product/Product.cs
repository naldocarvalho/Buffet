using BuffetDesigner.Domain._Base;
using BuffetDesigner.Domain.Enums;

namespace BuffetDesigner.Domain.Product
{
    public class Product
    {        
        public string NomeApresentacao { get; private set; }
        public string Descricao { get; private set; }
        public string CodBonanza { get; private set; }
        public Status Status { get; private set; }
        public string FotoProjeto { get; private set; }
        public string FotoIlustrativa { get; private set; }
        public string FotoDetalhe1 { get; private set; }
        public string FotoDetalhe2 { get; private set; }
        public string FotoDetalhe3 { get; private set; }

        public Product(string nomeApresentacao, string descricao, string codBonanza, Status status, string fotoProjeto, string fotoIlustrativa, string fotoDetalhe1, string fotoDetalhe2, string fotoDetalhe3)
        {
            RuleValidator.New()
                .When(string.IsNullOrEmpty(nomeApresentacao), Resource.InvalidProductApresentation)
                .When(string.IsNullOrEmpty(codBonanza), Resource.InvalidProductCode)
                .When(string.IsNullOrEmpty(fotoProjeto), Resource.InvalidProductProjectPhoto)
                .ThrowExceptionIfExists();

            NomeApresentacao = nomeApresentacao;
            Descricao = descricao;
            CodBonanza = codBonanza;
            Status = status;
            FotoProjeto = fotoProjeto;
            FotoIlustrativa = fotoIlustrativa;
            FotoDetalhe1 = fotoDetalhe1;
            FotoDetalhe2 = fotoDetalhe2;
            FotoDetalhe3 = fotoDetalhe3;
        }

        public void ChangeNomeApresentacao(string nomeApresentacao)
        {
            RuleValidator.New()
                .When(string.IsNullOrEmpty(nomeApresentacao), Resource.InvalidProductApresentation)
                .ThrowExceptionIfExists();
            NomeApresentacao = nomeApresentacao;
        }

        public void ChangeCodBonanza(string codBonanza)
        {
            RuleValidator.New()
                .When(string.IsNullOrEmpty(codBonanza), Resource.InvalidProductCode)
                .ThrowExceptionIfExists();
            CodBonanza = codBonanza;
        }

        public void ChangeFotoProjeto(string fotoProjeto)
        {
            RuleValidator.New()
                .When(string.IsNullOrEmpty(fotoProjeto), Resource.InvalidProductProjectPhoto)
                .ThrowExceptionIfExists();

            FotoProjeto = fotoProjeto;
        }

        public void ChangeStatus(Status status)
        {
            Status = status;
        }

        public void ChangeDetails(string descricao, string fotoIlustrativa, string fotoDetalhe1, string fotoDetalhe2, string fotoDetalhe3)
        {
            Descricao = descricao;
            FotoIlustrativa = fotoIlustrativa;
            FotoDetalhe1 = fotoDetalhe1;
            FotoDetalhe2 = fotoDetalhe2;
            FotoDetalhe3 = fotoDetalhe3;
        }
    }
}