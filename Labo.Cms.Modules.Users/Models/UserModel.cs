namespace Labo.Cms.Modules.Users.Models
{
    using System;

    [Serializable]
    public sealed class UserModel
    {
        public int ID { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public DateTime CreateDate { get; set; }

        public string Password { get; set; }

        public int PasswordFormat { get; set; }

        public string PasswordSalt { get; set; }

        public bool IsApproved { get; set; }

        public bool IsLockedOut { get; set; }
    }
}
