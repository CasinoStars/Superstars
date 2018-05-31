create proc sp.sMoneyCreateOrUpdate
(
	@MoneyId int,
	@Balance int,
	@MoneyType varchar(64)
)
as
begin
	set transaction isolation level serializable;
	begin tran;

	if not exists(select * from sp.tMoney m where m.MoneyId = @MoneyId and m.MoneyType = @MoneyType)
	begin
		insert into sp.tMoney(MoneyId, MoneyType, Balance) values(@MoneyId, @MoneyType, @Balance );
	if(@MoneyType = 1)
		insert into sp.tMoneyType(RealCoins) values(1);
	else if(@MoneyType = 2)
		insert into sp.tMoneyType(FakeCoins) values(2);
	end;

	else if exists(select * from sp.tMoney m where m.MoneyId = @MoneyId and m.MoneyType = @MoneyType)
	begin
		update sp.tMoney
		set Balance += @Balance
		where MoneyId = @MoneyId and MoneyType = @MoneyType;
	end;
	commit;
    return 0;
end;