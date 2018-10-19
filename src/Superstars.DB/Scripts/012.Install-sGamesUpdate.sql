create proc sp.sGamesUpdate
(
     @GameId int,
     @GameType nvarchar(64),
     @StartDate datetime,
     @EndDate datetime,
     @Winner int	
)
as 
begin
       set transaction isolation level serializable;
       begin tran;

       if not exists (select* from sp.tGames g where g.GameId = @GameId and g.GameType = @GameType and g.StartDate = @StartDate)
       begin
             rollback;
             return 1;
       end;

       if exists (select * from sp.tGames g where g.GameId <> @GameId and g.GameType = @GameType and g.StartDate = @StartDate)
       begin
              rollback
              return 2;
       end;
	update sp.tGames set [EndDate] = @EndDate, [Winner] = @Winner  where [GameId] = @GameId;
	commit;
    return 0;
end;