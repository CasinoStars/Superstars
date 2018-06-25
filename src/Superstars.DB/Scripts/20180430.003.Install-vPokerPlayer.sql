create view sp.vPokerPlayer
as 
  select
	PokerPlayerId = p.PokerPlayerId,
	PokerGameId = p.PokerGameId,
	PlayerCards = p.PlayerCards
  from sp.tPokerPlayer p;

