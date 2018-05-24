create table sp.tGameType
(
	GameType nvarchar(64) not null,

	constraint PK_GameType primary key(GameType)
);

insert into sp.tGameType(GameType)values('Yams')
insert into sp.tGameType(GameType)values('Poker')

