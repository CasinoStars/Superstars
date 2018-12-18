create procedure sp.sUserCreate
(
	@UserName nvarchar(68),
	@UserPassword varbinary(128),
	@Email nvarchar(68),
	@PrivateKey nvarchar(64), 
	@UserId int out,
	@Country nvarchar(64),
	@IsAdmin bit
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

    insert into sp.tUser(UserName, UserPassword, Email, PrivateKey, Country, IsAdmin) values(@UserName, @UserPassword, (case when @Email is null then '' else @Email end),@PrivateKey, @Country,@IsAdmin);
	set @UserId = scope_identity();
	if(substring(@UserName, 1, 3)  <> '#AI')
	begin
	insert into sp.tMoney(UserId, MoneyTypeId, Balance) values(@UserId, 0, 0);
	insert into sp.tMoney(UserId, MoneyTypeId, Balance) values(@UserId, 1, 0);

	insert into sp.tStats(GameTypeId, MoneyTypeId, UserId, Wins, Losses) values(0, 0, @UserId, 0, 0);
	insert into sp.tStats(GameTypeId, MoneyTypeId , UserId, Wins, Losses) values(1, 0, @UserId, 0, 0);
	insert into sp.tStats(GameTypeId, MoneyTypeId , UserId, Wins, Losses) values(2, 0, @UserId, 0, 0);
	insert into sp.tStats(GameTypeId, MoneyTypeId, UserId, Wins, Losses) values(0, 1, @UserId, 0, 0);
	insert into sp.tStats(GameTypeId, MoneyTypeId , UserId, Wins, Losses) values(1, 1, @UserId, 0, 0);
	insert into sp.tStats(GameTypeId, MoneyTypeId , UserId, Wins, Losses) values(2, 1, @UserId, 0, 0);

	end;
	commit;
	return 0;
end;