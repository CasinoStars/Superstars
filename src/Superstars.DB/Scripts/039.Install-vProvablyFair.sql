create view sp.vProvablyFair
as
    select UserId = u.UserId,
   	UncryptedPreviousServerSeed = u.UncryptedPreviousServerSeed,
	UncryptedServerSeed = u.UncryptedPreviousServerSeed,
	CryptedServerSeed = u.CryptedServerSeed,
	ClientSeed = u.ClientSeed,
	PreviousClientSeed = u.PreviousClientSeed,
	PreviousCryptedServerSeed = u.PreviousCryptedServerSeed,
	Nonce = u.Nonce
		 
	from sp.tProvablyFair u