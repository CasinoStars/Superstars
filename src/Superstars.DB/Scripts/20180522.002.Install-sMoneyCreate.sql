create proc sp.sMoneyCreate
(
	@MoneyId int out,
	@MoneyType varchar(64)
)
as
begin
	set transaction isolation level serializable;
	begin tran;

	if exists(select * from sp.tMoney m where m.MoneyType = @MoneyType)
	begin
		rollback;
		return 1;
	end;

    insert into sp.tMoney(MoneyType) values( @MoneyType );
	set @MoneyId = scope_identity();
	commit;
    return 0;
end;