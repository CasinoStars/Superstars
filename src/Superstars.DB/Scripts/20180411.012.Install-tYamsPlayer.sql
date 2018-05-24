create table sp.tYamsPlayer
(
	YamsPlayerId int not null,
	YamsGameId int not null,
	NbrRevives int,
	Dices varchar(6),
	DicesValue int

	constraint FK_tYamsPlayer_UserId foreign key (YamsPlayerId) references sp.tUser(UserId),
	constraint FK_tYamsPlayer_GameId foreign key (YamsGameId) references sp.tGames(GameId)
)