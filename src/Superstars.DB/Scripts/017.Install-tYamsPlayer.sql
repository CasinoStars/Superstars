create table sp.tYamsPlayer
(
	YamsPlayerId int not null,
	YamsGameId int not null,
	NbrRevives int,
	Dices varchar(6),
	DicesValue int

	constraint PK_tYamsPlayer_Id primary key (YamsPlayerId, YamsGameId),
	constraint FK_tYamsPlayer_UserId foreign key (YamsPlayerId) references sp.tUser(UserId),
	constraint FK_tYamsPlayer_GameId foreign key (YamsGameId) references sp.tGameYams(YamsGameId) ON DELETE CASCADE
)