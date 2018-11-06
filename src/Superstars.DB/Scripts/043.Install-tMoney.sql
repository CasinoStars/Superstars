create table sp.tMoney
(
	UserId int not null,
	MoneyTypeId int not null,
	Balance int not null constraint DF_tMoney_Balance default 0,
	Profit int not null constraint DF_tMoney_Profit default 0,
	Credit int not null constraint DF_tMoney_Credit default 0,

	constraint FK_tMoney_UserId foreign key (UserId) references sp.tUser(UserId),
	constraint FK_tMoney_MoneyTypeId foreign key (MoneyTypeId) references sp.tMoneyType(MoneyTypeId),
);