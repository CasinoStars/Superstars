create proc sp.sGameCrashCreate
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

	if exists(select * from sp.tGameCrash c where c.GameId = @GameId)
	begin
		rollback;
		return 1;
	end;

    insert into sp.tGameCrash(GameId, CrashHash, CrashValue) values(@GameId, @CrashHash, @CrashValue);
	commit;
    return 0;
end;