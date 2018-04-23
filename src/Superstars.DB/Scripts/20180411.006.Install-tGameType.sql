create table sp.tGameType
(
	GameType int not null

	constraint FK_tG foreign key(GameType) references t.Elo (GameTypeId)
)