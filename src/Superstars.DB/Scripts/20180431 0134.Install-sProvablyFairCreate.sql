create procedure sp.sProvablyFairCreate
(	
	@UserId int,
	@UncryptedPreviousServerSeed nvarchar(128),
	@UncryptedServerSeed nvarchar(128),
	@CryptedServerSeed nvarchar(128),
	@ClientSeed nvarchar(128),
	@PreviousClientSeed nvarchar(128),
	@PreviousCryptedServerSeed nvarchar(128),
	@Nonce int

)
as 

	declare @Userfkid int;
	set @Userfkid = (select UserId from sp.tUser u where u.UserId = @UserId);
begin
	set transaction isolation level serializable;
	begin tran;

	if exists(select * from sp.tProvablyFair u where u.UserId = @UserId)
	begin
		rollback;
		return 1;
	end;

    insert into sp.tProvablyFair(UserId,UncryptedPreviousServerSeed, UncryptedServerSeed, CryptedServerSeed, ClientSeed,PreviousClientSeed,PreviousCryptedServerSeed, Nonce) values(@Userfkid, @UncryptedPreviousServerSeed, @UncryptedServerSeed,@CryptedServerSeed,@ClientSeed,@PreviousClientSeed,@PreviousCryptedServerSeed,@Nonce);
	commit;
	return 0;
end;