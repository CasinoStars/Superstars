create table sp.tElo
(
	GameTypeId int not null,
	UserId int not null,
	Elo int,

	constraint FK_tGameTypeId foreign key (GameTypeId) references sp.tGameType(Gametype),
	constraint FK_tUserId foreign key (UserId) references sp.tUser(UserId)

)
