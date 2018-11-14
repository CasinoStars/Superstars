create table sp.tStats
(
	GameTypeId int not null,
	UserId int not null,
	Wins int default 0,
	Losses int default 0,
	AverageBet int default 0,
	AverageTime float default 0,
	ClientSeedChanges int default 0

	constraint FK_tStats_GameType foreign key(GameTypeId) references sp.tGameType(GameTypeId),
	constraint FK_tStats_UserId foreign key (UserId) references sp.tUser(UserId)
);
