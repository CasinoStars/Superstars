create table sp.tYamsPlayer
(
	YamsPlayerId int not null,
	YamsGameId int not null					,
	NbrRevives int,
	DicesValue int,

	constraint FK_tYamsPlayer foreign key (YamsPlayerId) references sp.tUser(UserId),
	constraint FK_tYamsPlayer foreign key (YamsGameId) references sp.tGames(GameId)

)