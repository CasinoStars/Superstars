create view sp.vGamePoker 
as 
   select
	PokerGameId = g.PokerGameId,
	Pot = g.Pot,
	TableCardsValue = g.TableCardsValue,
	TableCards = g.TableCards,
	NumTour = g.NumTour
   from sp.tGamePoker g