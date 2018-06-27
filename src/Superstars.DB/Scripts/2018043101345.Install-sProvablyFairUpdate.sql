create procedure sp.sProvablyFairUpdate
(
	@UserId int,
	@UncryptedPreviousServerSeed nvarchar(128),
	@UncryptedServerSeed nvarchar(128),
	@CryptedServerSeed nvarchar(128),
	@ClientSeed nvarchar(128)
)
as
begin
    update sp.tProvablyFair
    set UncryptedPreviousServerSeed = @UncryptedPreviousServerSeed, UncryptedServerSeed = @UncryptedServerSeed, CryptedServerSeed = @CryptedServerSeed, ClientSeed = @ClientSeed
    where UserId = @UserId;
    return 0;
end;