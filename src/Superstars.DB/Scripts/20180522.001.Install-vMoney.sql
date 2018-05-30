create view sp.vMoney
as
	select
	MoneyId = m.MoneyId,
	MoneyType = m.MoneyType,
	Balance = m.Balance,
	Profit = m.Profit
	from sp.tMoney m