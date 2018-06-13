create table sp.tStats
(
	GameTypeId nvarchar(64) not null,
	UserId int not null,
	Wins int default 0,
	Losses int default 0

	constraint FK_tGameType_GameTypeId foreign key(GameTypeId) references sp.tGameType(GameType),
	constraint FK_tUserId foreign key (UserId) references sp.tUser(UserId)
);
