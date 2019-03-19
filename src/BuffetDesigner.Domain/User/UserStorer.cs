using System;
using BuffetDesigner.Domain._Base;
using BuffetDesigner.Domain.Enums;
using BuffetDesigner.Domain.User.Dtos;

namespace BuffetDesigner.Domain.User
{
    public class UserStorer
    {
        private readonly IUserRepository _userRepository;

        public UserStorer(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Storer(UserDto userDto)
        {
            var userFound = _userRepository.GetByEmail(userDto.Email);

            RuleValidator.New()
                .When(userFound != null && userFound.Id != userDto.Id, Resource.UserAlreadyExists)
                .When(!Enum.TryParse<Status>(userDto.Status, out var status), Resource.InvalidStatus)
                .When(!Enum.TryParse<TipoUsuario>(userDto.TipoUsuario, out var tipoUsuario), Resource.InvalidTipoUsuario)
                .ThrowExceptionIfExists();

            if (userDto.Id == 0)
            {
                var user = new User(userDto.Nome, userDto.Email, userDto.Empresa, userDto.Telefone, 
                                        userDto.Senha, userDto.GeoLocalizacao, tipoUsuario, status);
                _userRepository.Add(user);
            }
            else
            {
                userFound = _userRepository.GetById(userDto.Id);
                
                RuleValidator.New()
                    .When(userFound == null, Resource.UserNotFound)
                    .ThrowExceptionIfExists();
                
                userFound.ChangeEmail(userDto.Email);
                userFound.ChangeEmpresa(userDto.Empresa);
                userFound.ChangeGeoLocalizacao(userDto.GeoLocalizacao);
                userFound.ChangeNome(userDto.Nome);
                userFound.ChangeTelefone(userDto.Telefone);
            }
        }

        public void ChangeEmail(UserDto userDto, string newEmail)
        {
            var userAlreadyExist = _userRepository.GetByEmail(newEmail);
            var userUpdate = _userRepository.GetById(userDto.Id);
            
            RuleValidator.New()                
                .When(userAlreadyExist != null, Resource.UserAlreadyExists)
                .When(userUpdate == null, Resource.UserNotFound)
                .ThrowExceptionIfExists();
            
            userUpdate.ChangeEmail(newEmail); 
        }
    }
}