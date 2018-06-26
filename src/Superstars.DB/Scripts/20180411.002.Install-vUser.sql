create view sp.vUser
as
    select UserId = u.UserId,
           Email = u.Email,
		   UserName = u.UserName,
		   UserPassword = u.UserPassword,
		   PrivateKey = u.PrivateKey,
		   UncryptedPreviousServerSeed = u.UncryptedPreviousServerSeed,
		   UncryptedServerSeed  = u.UncryptedServerSeed,
		   CryptedServerSeed = u.cryptedServerSeed
	from sp.tUser u