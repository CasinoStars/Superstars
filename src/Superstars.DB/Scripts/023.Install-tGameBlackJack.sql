create table sp.tGameBlackJack
(
	BlackJackGameId int identity(0,1),
	Pot varchar(20)

	constraint PK_tGameBlackJack foreign key (BlackJackGameId) references sp.tGames(GameId)
)