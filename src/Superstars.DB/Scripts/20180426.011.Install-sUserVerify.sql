create procedure sp.sUserVerify
(
    @UserName nvarchar(64),
	@UserPassword nvarchar(64),
	@UserId   int out
)

as
begin
    set transaction isolation level serializable;
	begin tran;

	if exists(select * from sp.tUser u where u.UserId = @UserName and u.UserPassword = @UserPassword)
	
	begin
		rollback;
		return 1;
	end;
	
	else 
	
	begin
		return 0;
	end;
end;

