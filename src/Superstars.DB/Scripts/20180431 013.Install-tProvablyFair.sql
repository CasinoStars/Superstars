create table sp.tProvablyFair
(
	UserId int,
	UncryptedPreviousServerSeed varchar(128),
	UncryptedServerSeed varchar(128),
	CryptedServerSeed varchar(128),
	ClientSeed nvarchar(128),
	Nonce int

	constraint FK_tProvablyFair_UserId foreign key(UserId) references sp.tUser(UserId)
);
