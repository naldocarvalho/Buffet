namespace BuffetDesigner.Domain._Base
{
     public static class Resource
    {
        //Category
        public static string InvalidCategoryDescription = "A Descrição da Categoria é inválida";
        //public static string InvalidStatus = "O Status é inválido";
        
        
        //Product
        public static string InvalidProductCode = "O Codigo Bonanza é inválido";
        public static string InvalidProductApresentation = "O Nome de Apresentação é inválido";
        public static string InvalidProductProjectPhoto = "A Foto de Projeto é inválida";
        
        
        // Project
        public static string InvalidProjectDescription = "A Descrição do Projeto é inválida";
        public static string InvalidProjectWidth = "Largura do Projeto é inválida";
        public static string InvalidProjectLength = "Comprimento do Projeto é inválida";

        // User
        public static string InvalidUserNome = "O nome do usuário é inválido";
        public static string InvalidUserEmpresa = "O nome da empresa é inválido";
        public static string InvalidUserEmail = "O email do usuário é inválido";

        public static string InvalidUserTelefone = "O telefone do usuário é inválido";
    }
}