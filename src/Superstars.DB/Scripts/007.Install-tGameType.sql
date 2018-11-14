create table sp.tGameType
(
	GameTypeId int identity(0,1),
	GameName nvarchar(64) not null,

	constraint PK_GameType primary key(GameTypeId)
);
insert into sp.tGameType(GameName) values('Yams');
insert into sp.tGameType(GameName) values('BlackJack');