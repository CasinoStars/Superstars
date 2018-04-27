create view sp.vElo
as
     select
          GameTypeID = e.GameTypeID,
          UserID = e.UserID,
          Elo = e.Elo
     from sp.tElo e
