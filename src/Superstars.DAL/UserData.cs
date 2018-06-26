namespace Superstars.DAL
{
    public class UserData
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public byte[] UserPassword { get; set; }

        public  string UncryptedPreviousServerSeed { get; set; }

        public string CryptedPreviousServerSeed { get; set; }

        public string CryptedServerSeed { get; set; }

    }
}