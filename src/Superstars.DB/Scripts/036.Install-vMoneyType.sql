create view sp.vMoneyType
as
   select
	MoneyTypeId = m.MoneyTypeId,
	MoneyName = m.MoneyName
   from sp.tMoneyType m