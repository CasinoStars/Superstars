create table sp.tGameBlackJack
(
	BlackJackGameId int,
	Pot varchar(20)

	constraint PK_tGameBlackJack_BlackJackGameId primary key (BlackJackGameId),
	constraint FK_tGameBlackJack foreign key (BlackJackGameId) references sp.tGames(GameId) ON DELETE CASCADE
)