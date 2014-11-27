namespace Labo.Cms.Modules.Users.Models
{
    public struct EncryptedPassword
    {
        public string Password { get; set; }

        public string Salt { get; set; }

        public PasswordFormat PasswordFormat { get; set; }
    }
}