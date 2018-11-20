create table sp.tGames
(
	GameId int identity(0,1),
	GameTypeId int not null,
	StartDate datetime not null,
	EndDate datetime,
	Winner NVARCHAR(64),

	constraint PK_tGames primary key(GameId),
	constraint FK_tGames_GameTypeId foreign key(GameTypeId) references sp.tGameType(GameTypeId)
);