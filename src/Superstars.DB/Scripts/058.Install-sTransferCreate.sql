create procedure sp.sTransferCreate
(
	@TransferId int out,
	@UserId int,
	@Amount int,
	@ReceiverId int
)
as 
begin
	set transaction isolation level serializable;
	begin tran;

	if exists(select * from sp.tTransfer u where u.TransferId = @TransferId)
	begin
		rollback;
		return 1;		
	end;

    insert into sp.tTransfer(UserId, Amount, ReceiverId) values(@UserId, @Amount,@ReceiverId);

	commit;
	return 0;
end;