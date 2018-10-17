create proc sp.sMoneyDelete
(
	@MoneyId int out,
	@MoneyType varchar
)
as
begin
	delete from sp.tMoney where MoneyType = @MoneyType;
	return 0;
end;