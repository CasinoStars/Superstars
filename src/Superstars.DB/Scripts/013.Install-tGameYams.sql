create table sp.tGameYams
(
	YamsGameId int ,
	Pot varchar(20)

	constraint PK_tGameYams foreign key (YamsGameId) references sp.tGames(GameId) ON DELETE CASCADE
)