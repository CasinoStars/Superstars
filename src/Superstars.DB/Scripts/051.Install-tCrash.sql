create table sp.tCrash
(
GameId int not null,
UserId int not null,
Bet int not null,
Multi float not null

constraint PK_tCrash_Id primary key (GameId, UserId),
constraint FK_tCrash_GameId foreign key (GameId) references sp.tGames(GameId),
constraint FK_tCrash_UserId foreign key (UserId) references sp.tUser(UserId)
);