create table sp.tGames
(
	GameId int identity(0,1),
	UserId int not null,
	GameType nvarchar(64) not null,
	StartDate datetime not null,
	EndDate datetime,
	Winner int,

	constraint PK_tGames primary key(GameId),
	constraint FK_tGames_UserId foreign key(UserId) references sp.tUser(UserId)
	--constraint FK_tGames_GameType foreign key(GameType) references sp.tGameType(GameType)
);