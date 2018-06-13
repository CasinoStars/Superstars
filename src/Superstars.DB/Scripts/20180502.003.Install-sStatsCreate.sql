create proc sp.sStatsCreate
(
	    @GameTypeId varchar(25),
        @UserId int,
		@Wins int,
		@Losses int
)
as
begin
	set transaction isolation level serializable;
	begin tran;

	if exists(select * from sp.tStats e where e.UserId = @UserId and e.GameTypeId = @GameTypeId)
	begin
		rollback;
		return 1;
	end;

    insert into sp.tStats(GameTypeId, UserId,Wins,Losses) values(@GameTypeId, @UserId,@Wins, @Losses);
	commit;
    return 0;
end;