namespace Superstars.DAL
{
    public class UserData
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public byte[] UserPassword { get; set; }

        public string PrivateKey { get; set; }

        public string Country { get; set; }

        public bool IsAdmin { get; set; }

    }
}