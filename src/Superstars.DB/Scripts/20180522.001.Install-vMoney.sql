create view sp.vMoney
as
	select
	MoneyId = m.MoneyId,
	MoneyType = m.MoneyType,
	Balance = m.Balance,
	Profit = m.Profit,
	Credit = m.Credit
	from sp.tMoney m