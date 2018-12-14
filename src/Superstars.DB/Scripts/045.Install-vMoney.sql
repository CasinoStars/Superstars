create view sp.vMoney
as
	select
	UserId = m.UserId,
	MoneyTypeId = m.MoneyTypeId,
	Balance = m.Balance,
	Credit = m.Credit
	from sp.tMoney m