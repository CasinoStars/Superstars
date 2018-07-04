create proc sp.sBankRollUpdate(
	@RealCoins decimal(15,8),
	@FakeCoins int
) as
begin
	set transaction isolation level serializable;
	begin tran;
	update sp.tBankRoll set FakeCoins += @FakeCoins, RealCoins = RealCoins + @RealCoins;
	commit;
end;