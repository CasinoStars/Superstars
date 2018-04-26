create table sp.tUser
(
	UserId int identity(0,1),
	UserName nvarchar(64) not null,
	UserPassword varbinary(128) not null,
	Email nvarchar(64) default '',
	IsConnected bit default 0,
	FakeMoney int default 0,
	RealMoney int default 0,
	FakeProfit int default 0,
	RealProfit int default 0,

	constraint PK_tUser primary key(UserId),
	constraint UK_tUser_Name unique(UserName)
);

create unique index IX_tUser_Email on sp.tUser(Email) where Email <> '';