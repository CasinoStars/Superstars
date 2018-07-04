create table sp.tGamePoker 
(
	PokerGameId int not null,
	Pot varchar(20),
	TableCardsValue int,
	TableCards nvarchar,
	NumTour int

	constraint PK_tGamePoker foreign key (PokerGameId) references sp.tGames(GameId)
)