create view sp.tPokerPlayer
as 
  select
	PokerPlayerId = p.PokerPlayerId,
	PokerGameId = p.PokerGameId,
	PlayerCards = p.PlayerCards,
  from sp.tPokerPlayer p
  where p.PokerPlayerId<>0 and p.PokerGameId<>0;

