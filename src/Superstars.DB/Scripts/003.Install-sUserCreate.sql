create procedure sp.sUserCreate
(
	@UserName nvarchar(68),
	@UserPassword varbinary(128),
	@Email nvarchar(68),
	@PrivateKey nvarchar(64), 
	@UserId int out,
	@Country nvarchar(64)
)
as 
begin
	set transaction isolation level serializable;
	begin tran;

	if exists(select * from sp.tUser u where u.UserName = @UserName)
	begin
		rollback;
		return 1;		
	end;

    insert into sp.tUser(UserName, UserPassword, Email, PrivateKey, Country) values(@UserName, @UserPassword, (case when @Email is null then '' else @Email end),@PrivateKey, @Country);
	set @UserId = scope_identity();
	if(substring(@UserName, 1, 3)  <> '#AI')
	begin
	insert into sp.tMoney(MoneyId, MoneyType, Balance, Profit) values(@UserId, 1, 0, 0);
	insert into sp.tMoney(MoneyId, MoneyType, Balance, Profit) values(@UserId, 2, 0, 0);
	insert into sp.tStats(GameType, UserId, Wins, Losses) values('Yams', @UserId, 0, 0);
	insert into sp.tStats(GameType, UserId, Wins, Losses) values('BlackJack', @UserId, 0, 0);
	end;
	commit;
	return 0;
end;