create table sp.tBlackJackPlayer
(
	BlackJackPlayerId int not null,
    BlackJackGameId int not null,
    PlayerCards nvarchar(32),
	SecondPlayerCards nvarchar(32),
	NbTurn int,
	HandValue int,
	SecondHandValue int,

	constraint FK_tBlackJackPlayer_UserId foreign key (BlackJackPlayerId) references sp.tUser(UserId),
	constraint FK_tBlackJackPlayer_GameId foreign key (BlackJackGameId) references sp.tGames(GameId)
);