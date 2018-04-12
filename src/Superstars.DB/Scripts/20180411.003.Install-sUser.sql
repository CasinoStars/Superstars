create procedure sp.sUser
(
	@UserId int out,
	@UserName nvarchar(68),
	@UserPassword varbinary(128),
	@Email nvarchar(68)
)
as 
begin
	set transaction isolation level serializable;
	begin tran;

	if exists(select * from sp.tUser u where u.Email = @Email)
	begin
		rollback;
		return 1;
	end;

    insert into sp.tUser(Email) values(@Email);
    select @UserId = scope_identity();
    insert into sp.tUser(UserId, UserName, UserPassword)
                           values(@userId, @UserName, @UserPassword);
	commit;
    return 0;
end;