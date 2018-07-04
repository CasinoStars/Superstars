create table sp.tGameYams
(
	YamsGameId int identity(0,1),
	Pot varchar(20)

	constraint PK_tGameYams foreign key (YamsGameId) references sp.tGames(GameId)
)