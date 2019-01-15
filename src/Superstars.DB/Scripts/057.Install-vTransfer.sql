create view sp.vTransfer
as
    select TransferId = u.TransferId,
		   UserId = u.UserId,
		   UserName = u.UserName,
           Amount = u.Amount,
		   ReceiverId = u.ReceiverId,
		   ReceiverName = u.ReceiverName,
		   TransferDate = u.TransferDate
	from sp.tTransfer u