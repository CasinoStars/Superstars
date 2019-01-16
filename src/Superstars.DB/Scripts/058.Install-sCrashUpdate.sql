create proc sp.sCrashUpdate
(
	@UserId int,
	@Multi float
)
as

	declare @GameId int;
	set @GameId = (select top 1 GameId from sp.tGameCrash c order by c.GameId desc);

begin
	set transaction isolation level serializable;
	begin tran;

	if  NOT EXISTS (select * from sp.tCrash c where c.GameId = @GameId and c.UserId = @UserId)
	begin
		rollback;
		return 1;
	end;

	if @Multi > (select Multi from sp.tCrash c where c.GameId = @GameId and c.UserId = @UserId)
	begin
		rollback;
		return 1;
	end;

	update sp.tCrash set Multi = @Multi where UserId = @UserId and GameId = @GameId;

	commit;
    return 0;
end;