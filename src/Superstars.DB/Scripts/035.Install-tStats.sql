create table sp.tStats
(
	GameTypeId int not null,
	UserId int not null,
	Wins int default 0,
	Losses int default 0,
	AverageBet int default 0,
	AverageTime int default 0,
	ClientSeedChanges int default 0

	constraint PK_tStats_Id primary key (GameTypeId, UserId),
	constraint FK_tStats_GameType foreign key(GameTypeId) references sp.tGameType(GameTypeId),
	constraint FK_tStats_UserId foreign key (UserId) references sp.tUser(UserId)
);
