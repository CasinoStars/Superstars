create table sp.tGameYams
(
	YamsGameId int ,
	Pot varchar(20)

	constraint PK_tGameYams_YamsGameId primary key (YamsGameId),
	constraint FK_tGameYams foreign key (YamsGameId) references sp.tGames(GameId) ON DELETE CASCADE
)