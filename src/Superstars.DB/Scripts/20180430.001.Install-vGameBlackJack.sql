create view sp.vGameBlackJack
as 
   select
           BlackJackGameId = g.BlackJackGameId,
           Pot = g.Pot
   from sp.tGameBlackJack g;