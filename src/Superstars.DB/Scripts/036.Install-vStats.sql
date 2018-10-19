create view sp.vStats
as
     select
          GameType = e.GameType,
          UserID = e.UserID,
          Wins = e.Wins,
		  Losses = e.Losses
     from sp.tStats e
