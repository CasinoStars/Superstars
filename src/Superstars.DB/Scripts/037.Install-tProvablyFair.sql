create table sp.tProvablyFair
(
	UserId int,
	UncryptedPreviousServerSeed varchar(128),
	UncryptedServerSeed varchar(128),
	CryptedServerSeed varchar(128),
	ClientSeed nvarchar(128),
	PreviousClientSeed nvarchar(128),
	PreviousCryptedServerSeed nvarchar(128),
	Nonce int

	constraint PK_tProvablyFair_UserId primary key (UserId),
	constraint FK_tProvablyFair_UserId foreign key(UserId) references sp.tUser(UserId)
);
