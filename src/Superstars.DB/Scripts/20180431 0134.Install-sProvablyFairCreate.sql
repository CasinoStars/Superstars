create procedure sp.sProvablyFairCreate
(	
	@UserId int,
	@UncryptedPreviousServerSeed nvarchar(128),
	@UncryptedServerSeed nvarchar(128),
	@CryptedServerSeed nvarchar(128),
	@ClientSeed nvarchar(128)
)
as 
begin
	set transaction isolation level serializable;
	begin tran;

	if exists(select * from sp.tProvablyFair u where u.UserId = @UserId)
	begin
		rollback;
		return 1;
	end;

    insert into sp.tProvablyFair(UserId,UncryptedPreviousServerSeed, UncryptedServerSeed, CryptedServerSeed, ClientSeed) values(@UserId, @UncryptedPreviousServerSeed, @UncryptedServerSeed,@CryptedServerSeed,@ClientSeed);
	set @UserId = scope_identity();
	commit;
	return 0;
end;