create view sp.vStats
as
     select
          GameType = e.GameType,
          UserID = e.UserID,
          Wins = e.Wins,
		  Losses = e.Losses,
		  AverageBet = e.AverageBet,
		  AverageTime = e.AverageTime,
		  ClientSeedChanges = e.ClientSeedChanges
     from sp.tStats e
