create view sp.vGameType
as
   select
	GameTypeId = g.GameTypeId,
	GameName = g.GameName
   from sp.tGameType g