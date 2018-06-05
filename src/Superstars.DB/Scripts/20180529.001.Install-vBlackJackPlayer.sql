create view sp.vBlackJackPlayer
as
   select
     BlackJackPlayerId = bj.BlackJackPlayerId,
     BlackJackGameId = bj.BlackJackGameId,
     PlayerCards = bj.PlayerCards
   from sp.tBlackJackPlayer bj
    where bj.BlackJackPlayerId <> 0;