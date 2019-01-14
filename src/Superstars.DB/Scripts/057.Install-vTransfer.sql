create view sp.vTransfer
as
    select TransferId = u.TransferId,
		   UserId = u.UserId,
           Amount = u.Amount,
		   ReceiverId = u.ReceiverId,
		   TransferDate = u.TransferDate
	from sp.tTransfer u