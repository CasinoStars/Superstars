create table sp.tGames
(
	GameId int identity(0,1),
	GameType int not null,
	StartDate date not null,
	EndDate date,
	Winner int,

	constraint PK_tGames primary key(GameId),
	constraint FK_tGames foreign key(GameType) references tGameType(GameType)
)