create view sp.vStats
as
     select
          GameTypeId = e.GameTypeId,
          UserId = e.UserId,
          Wins = e.Wins,
		  Losses = e.Losses,
		  AverageBet = e.AverageBet,
		  AverageTime = e.AverageTime,
		  ClientSeedChanges = e.ClientSeedChanges
     from sp.tStats e
