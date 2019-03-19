using System;
using Bogus;
using BuffetDesigner.Domain._Base;
using BuffetDesigner.Domain.Enums;
using BuffetDesigner.Domain.Exception;
using BuffetDesigner.Domain.User;
using BuffetDesigner.Domain.User.Dtos;
using BuffetDesigner.DomainTest._Builders;
using BuffetDesigner.DomainTest._Util;
using Moq;
using Xunit;
using Xunit.Abstractions;

namespace BuffetDesigner.DomainTest.Users
{
    public class UserStorerTest
    {
        private readonly ITestOutputHelper _output;
        private readonly UserDto _userDto;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly UserStorer _userStorer;

        public UserStorerTest(ITestOutputHelper output)
        {
            _output = output;
            var faker = new Faker();

            _userDto = new UserDto
            {
                //Id = faker.Random.Number(1000),
                Nome = faker.Person.FullName,
                Email = faker.Person.Email,
                Empresa = faker.Company.CompanyName(),
                Telefone = faker.Person.Phone,
                Senha = faker.Internet.Password(),
                GeoLocalizacao = faker.Address.City(),
                TipoUsuario = TipoUsuario.Cliente.ToString(),
                Status = Status.Ativo.ToString()
            };

            _userRepositoryMock = new Mock<IUserRepository>();
            _userStorer = new UserStorer(_userRepositoryMock.Object);

        }

        [Fact]
        public void ShouldCreateUser()
        {
            _userStorer.Storer(_userDto);

            _userRepositoryMock.Verify(r =>
                r.Add(
                    It.Is<User>(
                        u => u.Nome == _userDto.Nome &&
                        u.Email == _userDto.Email &&
                        u.Empresa == _userDto.Empresa &&
                        u.Telefone == _userDto.Telefone &&
                        u.Senha == _userDto.Senha &&
                        u.GeoLocalizacao == _userDto.GeoLocalizacao &&
                        u.TipoUsuario.ToString() == _userDto.TipoUsuario &&
                        u.Status.ToString() == _userDto.Status
                    )
                ), Times.AtLeast(1)
            );
        }

        [Fact]
        public void ShouldNotCreateUserWithEmailAlreadyExist()
        {
            var userWithEmailExist = UserBuilder.New().WithId(999).WithEmail(_userDto.Email).Build();

            _userRepositoryMock.Setup(u => u.GetByEmail(userWithEmailExist.Email)).Returns(userWithEmailExist);

            Assert.Throws<DomainException>(() =>
                _userStorer.Storer(_userDto))
            .WithMessage(Resource.UserAlreadyExists);
        }

        [Fact]
        public void ShoulNotChangeUserNotExist()
        {
            var userIdNotExist = 123;
            _userDto.Id = userIdNotExist;

            User userNotFound = null;

            _userRepositoryMock.Setup(u => u.GetById(userIdNotExist)).Returns(userNotFound);

            Assert.Throws<DomainException>(() => 
                _userStorer.Storer(_userDto))
            .WithMessage(Resource.UserNotFound);

        }

        [InlineData(null)]
        [InlineData("")]
        [Theory]
        public void ShouldNotCreateUserWithInvalidEmail(string InvalidEmail)
        {
            _userDto.Email = InvalidEmail;

            Assert.Throws<DomainException>(() =>
                _userStorer.Storer(_userDto)
            )
            .WithMessage(Resource.InvalidUserEmail);
        }

        [Fact]
        public void ShouldNotCreateUserWithInvalidTipoUsuario()
        {
            const string invalidTipoUsuario = "Mestre";
            _userDto.TipoUsuario = invalidTipoUsuario;

            Assert.Throws<DomainException>(() => 
                _userStorer.Storer(_userDto))
            .WithMessage(Resource.InvalidTipoUsuario);
        }

        [Fact]
        public void ShouldChangeEmail()
        {
            var newEmail = "eu@eunovamente.com.br";
            User userNotExist = null;

            _userDto.Id =123;
            var userSaved = UserBuilder.New().WithEmail(_userDto.Email).WithEmpresa(_userDto.Empresa)
                                .WithGeoLocalizacao(_userDto.GeoLocalizacao).WithId(_userDto.Id)
                                .WithNome(_userDto.Nome).WithSenha(_userDto.Senha)
                                .WithStatus(Enum.Parse<Status>(_userDto.Status))
                                .WithTelefone(_userDto.Telefone)
                                .WithTipoUsuario(Enum.Parse<TipoUsuario>(_userDto.TipoUsuario))
                                .Build();            

            _userRepositoryMock.Setup(u => u.GetByEmail(newEmail)).Returns(userNotExist);
            _userRepositoryMock.Setup(u => u.GetById(_userDto.Id)).Returns(userSaved);

            _userStorer.ChangeEmail(_userDto, newEmail);

            Assert.Equal(userSaved.Email, newEmail);
        
        }

        [Fact]
        public void ShouldNotChangeEmailIfEmailExist()
        {
            _userDto.Id = 999;
            var emailExist = "fredycarvalho@hotmail.com";
            var userAlreadyExist = UserBuilder.New().WithId(123).WithEmail(emailExist).Build();

            _userRepositoryMock.Setup(u => u.GetByEmail(emailExist)).Returns(userAlreadyExist);

            Assert.Throws<DomainException>(() =>
                _userStorer.ChangeEmail(_userDto, emailExist))
            .WithMessage(Resource.UserAlreadyExists);

        }
    }
}