create table sp.tGameYams
(
	YamsGameId int identity(0,1),
	Pot int,

	constraint PK_tGameYams foreign key (YamsGameId) references sp.tGames(GameId)
)