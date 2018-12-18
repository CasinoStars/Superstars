create procedure sp.sUserUpdate
(
    @UserId int,
	@UserPassword varbinary(128) = null,
	@UserName nvarchar(64) = null,
    @Email  nvarchar(64) = null,
	@Country nvarchar(64) = null,
	@LastConnexionDate datetime = null,
	@LastDeconnexionDate datetime = null,
	@IsAdmin BIT = 0
)
as
begin
    update sp.tUser
    set Email = isNull(@Email, Email), UserName = isNull(@UserName, UserName), UserPassword = isNull(@UserPassword, UserPassword), Country = isNull(@Country, Country), LastConnexionDate = isNull(@LastConnexionDate, LastConnexionDate),
	LastDeconnexionDate = isNull(@LastDeconnexionDate, LastDeconnexionDate), IsAdmin = ISNULL(@IsAdmin, IsAdmin)
    where UserId = @UserId;
    return 0;
	
end;