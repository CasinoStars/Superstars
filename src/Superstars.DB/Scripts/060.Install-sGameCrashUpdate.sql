create proc sp.sGameCrashUpdate
(
	@CrashHash nvarchar(150),
	@CrashValue float
)
as

	declare @GameId int;
	set @GameId = (select top 1 GameId from sp.tGames where GameTypeId = 2 order by StartDate desc);

begin
	set transaction isolation level serializable;
	begin tran;

	if  NOT EXISTS (select * from sp.tGameCrash c where c.GameId = @GameId)
	begin
		rollback;
		return 1;
	end;

	update sp.tGameCrash set CrashValue = @CrashValue, CrashHash = @CrashHash where GameId = @GameId;
	commit;
    return 0;
end;