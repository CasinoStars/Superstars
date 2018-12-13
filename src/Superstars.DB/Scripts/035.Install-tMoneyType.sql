create table sp.tMoneyType
(
	MoneyTypeId int identity(0,1),
	MoneyName nvarchar(64) not null,

	constraint PK_tMoneyType primary key(MoneyTypeId),
	constraint UK_tMoneyType_MoneyName unique(MoneyName),
);

insert into sp.tMoneyType(MoneyName) values('Fakecoin')
insert into sp.tMoneyType(MoneyName) values('Bitcoin')