create view sp.vStats
as
     select
          GameTypeID = e.GameTypeID,
          UserID = e.UserID,
          Wins = e.Wins,
		  Losses = e.Losses
     from sp.tStats e
