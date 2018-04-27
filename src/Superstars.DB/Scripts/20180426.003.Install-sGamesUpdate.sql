create proc sp.sGamesUpdate
(
     @GameId int,
     @GameType int,
     @StartDate date,
     @EndDate date,
     @Winner int	
)
as 
begin
       set transaction isolation level serializable;
       begin tran;

       if not exists (select* from sp.tGames g where g.EndDate = @EndDate)
       begin
             rollback;
             return 1;
       end;

       if exists (select * from sp.tGames g where g.GameId = @GameId and g.GameType = @GameType and g.StartDate =@StartDate )
       begin
              rollback
              return 2;
       end;
	update sp.tGames
end;