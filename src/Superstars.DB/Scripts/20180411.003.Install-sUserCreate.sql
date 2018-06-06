create procedure sp.sUserCreate
(
	@UserName nvarchar(68),
	@UserPassword varbinary(128),
	@Email nvarchar(68),
	@PrivateKey nvarchar(64)
	
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

    insert into sp.tUser(UserName, UserPassword, Email, PrivateKey) values(@UserName, @UserPassword, (case when @Email is null then '' else @Email end),@PrivateKey);
	commit;
	return 0;
end;