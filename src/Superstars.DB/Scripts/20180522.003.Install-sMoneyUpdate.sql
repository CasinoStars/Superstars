create proc sp.sMoneyUpdate
(
	@MoneyId int out,
	@MoneyType varchar
)
as
begin
	set transaction isolation level serializable;
	begin tran;

	if not exists(select * from sp.tMoney m where m.MoneyType = @MoneyType)
	begin
		rollback;
		return 1;
	end;

    update sp.tMoney set MoneyType = @MoneyType where MoneyId = @MoneyId;
	commit;
    return 0;
end;