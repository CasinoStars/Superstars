create proc sp.sMoneyUpdate
(
	@MoneyId int,
	@Balance int,
	@Profit int,
	@MoneyType varchar(64),
	@Credit int
)
as
begin
	set transaction isolation level serializable;
	begin tran;
	if not exists( select * from sp.tMoney where MoneyId = @MoneyId)
	begin
        rollback;
        return 1;
    end;
	update sp.tMoney set Balance += @Balance, Credit = Credit + @Credit, Profit = Profit + @Profit where MoneyId = @MoneyId and MoneyType = @MoneyType;
	commit;
    return 0;
end;