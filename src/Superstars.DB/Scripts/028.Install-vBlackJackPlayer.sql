create view sp.vBlackJackPlayer
as
   select
     BlackJackPlayerId = bj.BlackJackPlayerId,
     BlackJackGameId = bj.BlackJackGameId,
     PlayerCards = bj.PlayerCards,
	 SecondPlayerCards = bj.SecondPlayerCards,
	 NbTurn = bj.NbTurn,
	 HandValue = bj.HandValue
   from sp.tBlackJackPlayer bj
    where bj.BlackJackPlayerId <> 0;