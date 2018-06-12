create view sp.vBlackJackPlayer
as
   select
     BlackJackPlayerId = bj.BlackJackPlayerId,
     BlackJackGameId = bj.BlackJackGameId,
     PlayerCards = bj.PlayerCards,
	 NbTurn = bj.NbTurn
   from sp.tBlackJackPlayer bj
    where bj.BlackJackPlayerId <> 0;