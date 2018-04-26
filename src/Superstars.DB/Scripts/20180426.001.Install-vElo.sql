create view sp.tElo
as
     select
          GameTypeID = e.GameTypeID,
          UserID = e.UserID,
          Elo = e.Elo
     from sp.Elo e
