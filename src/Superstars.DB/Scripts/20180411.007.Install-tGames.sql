create table sp.tGames
(
	GameId int identity(0,1),
	GameType varchar(64) not null,
	StartDate datetime not null,
	EndDate datetime,
	Winner int,

	constraint PK_tGames primary key(GameId),
	constraint FK_tGameType foreign key(GameType) references sp.tGameType(GameType)
);