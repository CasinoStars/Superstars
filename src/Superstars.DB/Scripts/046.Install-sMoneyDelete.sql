create proc sp.sMoneyDelete
(
	@UserId int out,
	@MoneyTypeId int
)
as
begin
	delete from sp.tMoney where MoneyTypeId = @MoneyTypeId;
	return 0;
end;