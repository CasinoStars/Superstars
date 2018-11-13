create view sp.vMoney
as
	select
	UserId = m.UserId,
	MoneyTypeId = m.MoneyTypeId,
	Balance = m.Balance,
	Profit = m.Profit,
	Credit = m.Credit
	from sp.tMoney m