create procedure sp.sGamesCreate
(
	@GameId int out,
	@GameType int,
	@StartDate date
)
as
begin
       set transaction isolation level serializable;
       begin tran;
      
       if exist(selec * from sp.tGames g where g.GameType = @GameType and g.StartDate = @StartDate)
       begin 
               rollback;
               return 1;
       end
       insert into sp.tGames(GameType,StartDate) values(@GameType,@StartDate)
           set @GameId = scope_identity();
       return 0;
end;