create proc sp.sMoneyUpdate
(
	@UserId int,
	@Balance int,
	@MoneyTypeId int,
	@Credit int
)
as
begin
	set transaction isolation level serializable;
	begin tran;
	if not exists( select * from sp.tMoney where UserId = @UserId)
	begin
        rollback;
        return 1;
    end;
	update sp.tMoney set Balance += @Balance, Credit = Credit + @Credit where UserId = @UserId and MoneyTypeId = @MoneyTypeId;
	commit;
    return 0;
end;