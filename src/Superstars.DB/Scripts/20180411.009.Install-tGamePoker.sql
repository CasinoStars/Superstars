create table sp.tGamePoker 
(
	PokerGameId int not null,
	Pot int,
	TableCardsValue int,
	TableCards nvarchar,
	NumTour int

	constraint PK_tGamePoker foreign key (PokerGameId) references sp.tGames(GameId)
)