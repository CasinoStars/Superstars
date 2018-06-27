create view sp.vProvablyFair
as
    select UserId = u.UserId,
   	UncryptedPreviousServerSeed = u.UncryptedPreviousServerSeed,
	UncryptedServerSeed = u.UncryptedPreviousServerSeed,
	CryptedServerSeed = u.CryptedServerSeed,
	ClientSeed = u.ClientSeed,
	Nonce = u.Nonce
		 
	from sp.tProvablyFair u