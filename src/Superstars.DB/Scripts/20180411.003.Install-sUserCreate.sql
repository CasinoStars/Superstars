create procedure sp.sUserCreate
(
	@UserName nvarchar(68),
	@UserPassword varbinary(128),
	@Email nvarchar(68)
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
	if exists(select * from sp.tUser u where u.Email = @Email)
	begin
		rollback;
		return 2;
	end;

    insert into sp.tUser(UserName, UserPassword, Email) values(@UserName, @UserPassword, (case when @Email is null then '' else @Email end));
	commit;
end;