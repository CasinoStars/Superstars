create table sp.tGameYams
(
	YamsGameId int not null,
	Pot int,

	constraint PK_tGameYams foreign key (YamsGameId) references sp.tGames(GameId)
)