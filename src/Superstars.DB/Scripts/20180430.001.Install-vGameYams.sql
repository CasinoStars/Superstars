create view sp.vGameYams
as 
   select
           YamsGameId = g.YamsGameId,
           Pot = g.Pot
   from sp.tGameYams g;