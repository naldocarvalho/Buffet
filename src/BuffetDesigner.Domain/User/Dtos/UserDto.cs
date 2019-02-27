namespace BuffetDesigner.Domain.User.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Nome { get;  set; }
        public string Email { get;  set; }
        public string Empresa { get;  set; }
        public string Telefone { get;  set; }
        public string Senha { get;  set; }
        public string GeoLocalizacao { get;  set; }
        public string TipoUsuario { get;  set; }
        public string Status { get;  set; }
    }
}