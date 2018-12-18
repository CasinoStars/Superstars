create table sp.tStats
(
	GameTypeId int not null,
	MoneyTypeId int not null,
	UserId int not null,
	Profit int default 0,
	Wins int default 0,
	Losses int default 0,
	Equality int default 0,
	TotalBet int default 0,
	AverageTime int default 0,
	ClientSeedChanges int default 0

	constraint PK_tStats_Id primary key (GameTypeId, UserId, MoneyTypeId),
	constraint FK_tStats_GameType foreign key(GameTypeId) references sp.tGameType(GameTypeId),
	constraint FK_tStats_UserId foreign key (UserId) references sp.tUser(UserId),
	constraint FK_tStats_MoneyTypeId foreign key (MoneyTypeId) references sp.tMoneyType(MoneyTypeId)
);
