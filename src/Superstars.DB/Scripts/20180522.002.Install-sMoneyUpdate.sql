create proc sp.sMoneyUpdate
(
	@MoneyId int,
	@Balance int,
	@MoneyType varchar(64)
)
as
begin
	set transaction isolation level serializable;
	begin tran;
	update sp.tMoney set Balance += @Balance where MoneyId = @MoneyId and MoneyType = @MoneyType;
	commit;
    return 0;
end;