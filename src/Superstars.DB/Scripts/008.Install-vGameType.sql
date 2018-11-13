create view sp.sGameType
as
   select
	GameTypeId = g.GameTypeId,
	GameName = g.GameName
   from sp.tGameType g