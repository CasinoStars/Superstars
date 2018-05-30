create view sp.vBlackJackPlayer
as
   select
     BlackJackPlayerId = bj.BlackJackPlayerIdId,
     BlackJackGameId = bj.BlackJackGameId,
     PlayerCards = bj.PlayerCards
   from sp.tBlackJackPlayer bj
    where bj.BlackJackPlayerId <> 0;