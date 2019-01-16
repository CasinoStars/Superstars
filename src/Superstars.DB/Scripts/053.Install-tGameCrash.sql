create table sp.tGameCrash
(
GameId int not null,
CrashHash nvarchar(150) not null,
CrashValue float not null,

constraint PK_tGameCrash_Id primary key (GameId),
constraint FK_tGameCrash_GameId foreign key (GameId) references sp.tGames(GameId),
);