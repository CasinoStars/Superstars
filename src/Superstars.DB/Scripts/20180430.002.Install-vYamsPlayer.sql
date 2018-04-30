create view sp.vYamsPlayer
as
  select
	YamsPlayerId = y.YamsPlayerId,
	YamsGameId = y.YamsGameId ,
	NbrRevives = y.NbrRevives,
	DicesValue = y.DicesValue
from sp.tYamsPlayer y
 where y.YamsGameId <> 0;