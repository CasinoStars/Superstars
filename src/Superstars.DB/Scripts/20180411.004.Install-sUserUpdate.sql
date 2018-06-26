create procedure sp.sUserUpdate
(
    @UserId int,
	@UserPassword varbinary(128),
	@UserName nvarchar(64),
    @Email  nvarchar(64),
	@UncryptedPreviousServerSeed nvarchar(128),
	@UncryptedServerSeed nvarchar(128),
	@CryptedServerSeed nvarchar(128)
)
as
begin
    update sp.tUser
    set Email = @Email, UserName = @UserName, UserPassword = @UserPassword, UncryptedPreviousServerSeed = @UncryptedPreviousServerSeed,
	UncryptedServerSeed = @UncryptedServerSeed, CryptedServerSeed = @cryptedServerSeed
    where UserId = @UserId;
    return 0;
end;