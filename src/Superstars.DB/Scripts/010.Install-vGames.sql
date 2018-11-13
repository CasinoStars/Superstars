create view sp.vGames
as
    select
       GameId = g.GameId,
       GameTypeId = g.GameTypeId,
       StartDate = g.StartDate,
       EndDate = g.EndDate,
       Winner = g.Winner
    from sp.tGames g
