create table sp.tGameBlackJack
(
	BlackJackGameId int identity(0,1),
	Pot int,
	DealerCards nvarchar(25),

	constraint PK_tGameBlackJack foreign key (BlackJackGameId) references sp.tGames(GameId)
)