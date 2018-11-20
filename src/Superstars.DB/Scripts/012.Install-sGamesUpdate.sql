create proc sp.sGamesUpdate
(
	@GameId int,
	@GameTypeId int,
	@EndDate datetime,
	@Winner NVARCHAR(64)
)
as 
begin
       set transaction isolation level serializable;
       begin tran;

       if not exists (select * from sp.tGames g where g.GameId = @GameId and g.GameTypeId = @GameTypeId)
       begin
             rollback;
             return 1;
       end;

	update sp.tGames set [EndDate] = @EndDate, [Winner] = @Winner  where [GameId] = @GameId;
	commit;
    return 0;
end;