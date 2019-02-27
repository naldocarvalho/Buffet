using System;
using BuffetDesigner.Domain.Enums;
using BuffetDesigner.Domain.User;
using BuffetDesigner.DomainTest.Users;

namespace BuffetDesigner.DomainTest._Builders
{
    public class UserBuilder
    {
        private int _id;
        private string _nome = "Fredy Carcalho";
        private string _email = "fredycarvalho@hotmail.com";
        private string _empresa = "TecFULL Soluções";
        private string _telefone = "31987460505";
        private string _senha = "123456";
        private string _geoLocalizacao = "Belo Horizonte";
        private TipoUsuario _tipoUsuario = TipoUsuario.Administrador;
        private Status _status = Status.Ativo;        
        
        public static UserBuilder New()
        {
            return new UserBuilder();
        }
        public UserBuilder WithId(int id)
        {
            _id = id;
            return this;
        }
        public UserBuilder WithNome(string nome)
        {
            _nome = nome;
            return this;
        }
        public UserBuilder WithEmail(string email)
        {
            _email = email;
            return this;
        }
        public UserBuilder WithEmpresa(string empresa)
        {
            _empresa = empresa;
            return this;
        }
        public UserBuilder WithTelefone(string telefone)
        {
            _telefone = telefone;
            return this;
        }
        public UserBuilder WithSenha(string senha)
        {
            _senha = senha;
            return this;
        }
        public UserBuilder WithGeoLocalizacao(string geoLocalizacao)
        {
            _geoLocalizacao = geoLocalizacao;
            return this;
        }
        public UserBuilder WithTipoUsuario(TipoUsuario tipoUsuario)
        {
            _tipoUsuario = tipoUsuario;
            return this;
        }
        public UserBuilder WithStatus(Status status)
        {
            _status = status;
            return this;
        }

        public User Build()
        {
            var user = new User(_nome, _email, _empresa, _telefone, _senha, _geoLocalizacao, _tipoUsuario, _status);
             
             if (_id > 0)
            {
                var propertyInfo = user.GetType().GetProperty("Id");
                propertyInfo.SetValue(user, Convert.ChangeType(_id, propertyInfo.PropertyType), null);
            }

            return user;
        }


    }
}