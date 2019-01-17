using Bogus;
using BuffetDesigner.Domain._Base;
using BuffetDesigner.Domain.Enums;
using BuffetDesigner.Domain.Exception;
using BuffetDesigner.DomainTest._Builders;
using ExpectedObjects;
using Xunit;
using BuffetDesigner.DomainTest._Util;

namespace BuffetDesigner.DomainTest.Users
{
    public class UsersTest
    {

        private readonly string _nome;
        private readonly string _email;
        private readonly string _empresa;
        private readonly string _telefone;
        private readonly string _senha;
        private readonly string _geoLocalizacao;
        private readonly TipoUsuario _tipoUsuario;
        private readonly Status _status;
        private readonly Faker _faker;

        public UsersTest()
        {
            _faker = new Faker();

            _nome = _faker.Person.FullName;
            _email = _faker.Person.Email;
            _empresa = _faker.Company.CompanyName();
            _telefone = _faker.Person.Phone;
            _senha = _faker.Internet.Password(5);
            _geoLocalizacao = _faker.Address.City();
            _tipoUsuario = TipoUsuario.Cliente;
            _status = Status.Ativo;
        }

        [Fact]
        public void ShoulCreateUser()
        {
            var expectedUser = new
            {
                Nome = _nome,
                Email = _email,
                Empresa = _empresa,
                Telefone = _telefone,
                Senha = _senha,
                GeoLocalizacao = _geoLocalizacao,
                TipoUsuario = _tipoUsuario,
                Status = _status
            };

            var user = new User(expectedUser.Nome, expectedUser.Email, expectedUser.Empresa, expectedUser.Telefone, expectedUser.Senha, expectedUser.GeoLocalizacao, expectedUser.TipoUsuario, expectedUser.Status);

            expectedUser.ToExpectedObject().Matches(user);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ShouldNotCreateUserWithInvalidNome(string invalidNome)
        {
            Assert.Throws<DomainException>(() =>
                UserBuilder.New().WithNome(invalidNome).Build())
                .WithMessage(Resource.InvalidUserNome);    
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ShouldNotCreateUserWithInvalidEmail(string invalidEmail)
        {
            Assert.Throws<DomainException>(() =>
                UserBuilder.New().WithEmail(invalidEmail).Build())
                .WithMessage(Resource.InvalidUserEmail);    
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ShouldNotCreateUserWithInvalidEmpresa(string invalidEmpresa)
        {
            Assert.Throws<DomainException>(() =>
                UserBuilder.New().WithEmpresa(invalidEmpresa).Build())
                .WithMessage(Resource.InvalidUserEmpresa);    
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ShouldNotCreateUserWithInvalidTelefone(string invalidTelefone)
        {
            Assert.Throws<DomainException>(() =>
                UserBuilder.New().WithTelefone(invalidTelefone).Build())
                .WithMessage(Resource.InvalidUserTelefone);    
        }
    }


    public class User
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


     

    }
}