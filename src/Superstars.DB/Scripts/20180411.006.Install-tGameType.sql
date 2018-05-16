create table sp.tGameType
(
	GameType varchar(64) not null,

	constraint PK_GameType primary key(GameType)
);

insert into sp.tGameType(GameType)values('Yams')
