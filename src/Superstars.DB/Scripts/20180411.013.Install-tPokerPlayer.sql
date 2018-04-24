create table sp.tPokerPlayer
(
	PokerPlayerId int not null,
	PokerGameId int not null,
	PlayerCards nvarchar,

	constraint FK_tPokerPlayer_UserId foreign key (PokerPlayerId) references sp.tUser(UserId),
	constraint FK_tPokerPlayer_GameId foreign key (PokerGameId) references sp.tGames(GameId)

)