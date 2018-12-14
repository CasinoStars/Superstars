create view sp.vStats
as
     select
          GameTypeId = e.GameTypeId,
		  MoneyTypeId = e.MoneyTypeId,
          UserId = e.UserId,
		  Profit = e.Profit,
          Wins = e.Wins,
		  Losses = e.Losses,
		  Equality = e.Equality,
		  TotalBet = e.TotalBet,
		  AverageTime = e.AverageTime,
		  ClientSeedChanges = e.ClientSeedChanges
     from sp.tStats e
