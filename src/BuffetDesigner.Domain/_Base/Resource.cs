namespace BuffetDesigner.Domain._Base
{
     public static class Resource
    {
        //Category
        public static string InvalidCategoryDescription = "A Descrição da Categoria é inválida";
        public static string CategoryDescriptionAlreadyExists = "Esta categoria já existe";
        public static string CategoryNotFound = "Categoria não encontrada";
        
        
        //Product
        public static string InvalidProductCode = "O Codigo Bonanza é inválido";
        public static string InvalidProductApresentation = "O Nome de Apresentação é inválido";
        public static string InvalidProductProjectPhoto = "A Foto de Projeto é inválida";
        public static string ProductCodBonanzaAlreadyExists = "Codigo Bonanza já existe";
        public static string ProductNotFound = "Produto não encontrado";
        
        // Project
        public static string InvalidProjectDescription = "A Descrição do Projeto é inválida";
        public static string InvalidProjectWidth = "Largura do Projeto é inválida";
        public static string InvalidProjectLength = "Comprimento do Projeto é inválida";
        public static string InvalidDataOrcamentoProjeto = "A data do orçamento é inválida";
        public static string ProjectNotFound = "Projeto não encontrado";

        // User
        public static string InvalidUserNome = "O nome do usuário é inválido";
        public static string InvalidUserEmpresa = "O nome da empresa é inválido";
        public static string InvalidUserEmail = "O email do usuário é inválido";
        public static string InvalidUserTelefone = "O telefone do usuário é inválido";
        public static string InvalidUserSenha = "A senha do usuário é inválida";
        public static string UserAlreadyExists = "Já existe um usuário com este endereço de email";
        public static string UserNotFound = "Usuário não encontrado";

        // Status
        public static string InvalidStatus = "O Status é inválido";

        // Tipo de Usuario
        public static string InvalidTipoUsuario = "Tipo de usuário inválido";
 
    }
}