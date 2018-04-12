create procedure sp.sUserUpdate
(
    @UserId int,
	@UserPassword varbinary(68),
	@UserName nvarchar(64),
    @Email  nvarchar(64)
)
as
begin
    update sp.tUser
    set Email = @Email, UserName = @UserName, UserPassword = @UserPassword
    where UserId = @UserId;
    return 0;
end;