using BuffetDesigner.Domain._Base;
using BuffetDesigner.Domain.Enums;

namespace BuffetDesigner.Domain.User
{
    public class User : BaseEntity
    {
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Empresa { get; private set; }
        public string Telefone { get; private set; }
        public string Senha { get; private set; }
        public string GeoLocalizacao { get; private set; }
        public TipoUsuario TipoUsuario { get; private set; }
        public Status Status { get; private set; }
       
        public User(string nome, string email, string empresa, string telefone, string senha, string geoLocalizacao, TipoUsuario tipoUsuario, Status status)
        {            

            RuleValidator.New()
                .When(string.IsNullOrEmpty(nome), Resource.InvalidUserNome)
                .When(string.IsNullOrEmpty(email), Resource.InvalidUserEmail)
                .When(string.IsNullOrEmpty(empresa), Resource.InvalidUserEmpresa)
                .When(string.IsNullOrEmpty(telefone), Resource.InvalidUserTelefone)
                .ThrowExceptionIfExists();

            this.Nome = nome;
            this.Email = email;
            this.Empresa = empresa;
            this.Telefone = telefone;
            this.Senha = senha;
            this.GeoLocalizacao = geoLocalizacao;
            this.TipoUsuario = tipoUsuario;
            this.Status = status;

        }

        public void ChangeNome(string nome)
        {
            RuleValidator.New()
                .When(string.IsNullOrEmpty(nome), Resource.InvalidUserNome)
                .ThrowExceptionIfExists();

            Nome = nome;
        }
        public void ChangeEmail(string email)
        {
            RuleValidator.New()
                .When(string.IsNullOrEmpty(email), Resource.InvalidUserEmail)
                .ThrowExceptionIfExists();

            Email = email;
        }
        public void ChangeEmpresa(string empresa)
        {
            RuleValidator.New()
                .When(string.IsNullOrEmpty(empresa), Resource.InvalidUserEmpresa)
                .ThrowExceptionIfExists();

            Empresa = empresa;
        }
        public void ChangeTelefone(string telefone)
        {
            RuleValidator.New()
                .When(string.IsNullOrEmpty(telefone), Resource.InvalidUserTelefone)
                .ThrowExceptionIfExists();

            Telefone = telefone;

        }
        public void ChangeGeoLocalizacao(string geoLocalizacao)
        {
            GeoLocalizacao = geoLocalizacao;
        }
        public void ChangeSenha(string senha)
        {
            RuleValidator.New()
                .When(string.IsNullOrEmpty(senha), Resource.InvalidUserSenha);
        }

    }
}