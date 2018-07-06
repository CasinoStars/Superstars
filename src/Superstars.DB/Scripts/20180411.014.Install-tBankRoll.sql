create table sp.tBankRoll
(
	RealCoins int,
	FakeCoins int default 5000000
);

insert into sp.tBankRoll(RealCoins, FakeCoins) values(0, 0);