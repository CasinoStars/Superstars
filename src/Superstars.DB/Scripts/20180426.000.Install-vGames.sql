create view sp.vGames
as
    select
       GameId = g.GameId,
       GameType = g.GameType,
       StartDate = g.StartDate,
       EndDate = g.EndDate,
       Winner = g.Winner
    from sp.tGames g
