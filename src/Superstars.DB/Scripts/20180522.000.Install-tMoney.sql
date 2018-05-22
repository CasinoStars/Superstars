create table sp.tMoney
(
	MoneyId int identity(0,1),
	MoneyType varchar(64),

	constraint PK_tGames primary key(GameId)
);