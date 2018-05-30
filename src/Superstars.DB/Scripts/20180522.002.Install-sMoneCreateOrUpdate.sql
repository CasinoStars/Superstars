create proc sp.sMoneyCreateOrUpdate
(
	@Balance int,
	@MoneyType varchar(64),
	@Pseudo nvarchar(64) out
)
as
	declare @MoneyId int;
	set @MoneyId = (select t.UserId from sp.tUser t where t.UserName = @Pseudo);
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
		where MoneyId = @MoneyId;
	end;
	commit;
    return 0;
end;