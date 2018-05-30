create table sp.tMoney
(
	MoneyId int,
	MoneyType varchar(5),
	Balance int default 0,
	Profit int default 0,

	constraint FK_tMoney_UserId foreign key (MoneyId) references sp.tUser(UserId),
);