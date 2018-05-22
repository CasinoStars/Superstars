create view sp.vMoney
as
   select
     MoneyId = m.MoneyId,
     MoneyType = m.MoneyType
   from sp.tMoney m