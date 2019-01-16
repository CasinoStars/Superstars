create view sp.vGameCrash
as
	select
	GameId = c.GameId,
	CrashHash = c.CrashHash,
	CrashValue = c.CrashValue
	from sp.tGameCrash c