create view sp.vCrash
as
	select
	UserId = c.UserId,
	GameId = c.GameId,
	Bet = c.Bet,
	Multi = c.Multi
	from sp.tCrash c