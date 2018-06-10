create view sp.vYamsPlayer
as
  select
	YamsPlayerId = y.YamsPlayerId,
	YamsGameId = y.YamsGameId,
	NbrRevives = y.NbrRevives,
	Dices = y.Dices,
	DicesValue = y.DicesValue
from sp.tYamsPlayer y;