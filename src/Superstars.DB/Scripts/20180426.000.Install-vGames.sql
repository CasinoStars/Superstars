create view sp.tGames
as
    select
       GameID = g.GameID,
       GameType = g.GameType,
       StartDate = g.StartDate,
       EndDate = g.EndDate,
       Winner = g.Winner
    from sp.tGames g