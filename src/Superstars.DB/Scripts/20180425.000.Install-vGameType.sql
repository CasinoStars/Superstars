create view sp.sGameType
as
   select
	GameType = g.GameType
        
   from sp.tGameType g