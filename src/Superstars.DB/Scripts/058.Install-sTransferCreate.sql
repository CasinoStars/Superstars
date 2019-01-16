create procedure sp.sTransferCreate
(
	@TransferId int out,
	@UserId int,
	@UserName char(64),
	@Amount int,
	@TransferDate dateTime,
	@ReceiverId int,
	@ReceiverName char(64)
)
as 
begin
	set transaction isolation level serializable;
	begin tran;
	
	--if exists(select * from sp.tTransfer u where u.TransferId = @TransferId)
	--begin
	--	rollback;
	--	return 1;		
	--end;


    insert into sp.tTransfer(UserId,UserName,Amount,TransferDate, ReceiverId, ReceiverName) values(@UserId,@UserName, @Amount,@TransferDate,@ReceiverId,@ReceiverName);
	set @TransferId = scope_identity();
	commit;
	return 0;
end;