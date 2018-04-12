create table sp.tGames
(
	GameId int identity(0,1),
	StartDate date not null,
	EndDate date,
	Winner int,

	constraint PK_tGames primary key(GameId)
)