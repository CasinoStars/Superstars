create procedure sp.sUserUpdate
(
    @UserId int,
	@UserPassword varbinary(128),
	@UserName nvarchar(64),
    @Email  nvarchar(64),
	@Country nvarchar(64),
	@LastConnexionDate datetime,
	@LastDeconnexionDate datetime
)
as
begin
    update sp.tUser
    set Email = @Email, UserName = @UserName, UserPassword = @UserPassword, Country = @Country, LastConnexionDate = @LastConnexionDate, 
	LastDeconnexionDate = @LastDeconnexionDate
    where UserId = @UserId;
    return 0;
	
end;