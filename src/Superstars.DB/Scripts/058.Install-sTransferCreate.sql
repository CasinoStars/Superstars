create procedure sp.sTransferCreate
(
	@TransferId int out,
	@UserId int,
	@Amount int,
	@TransferDate dateTime,
	@ReceiverId int
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


    insert into sp.tTransfer(UserId, Amount,TransferDate, ReceiverId) values(@UserId, @Amount,@TransferDate,@ReceiverId);
	set @TransferId = scope_identity();
	commit;
	return 0;
end;