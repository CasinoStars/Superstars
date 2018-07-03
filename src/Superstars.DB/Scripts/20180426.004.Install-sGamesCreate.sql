create procedure sp.sGamesCreate
(
	@GameId int out,
	@GameType nvarchar(64),
	@StartDate datetime
)
as
begin
       set transaction isolation level serializable;
       begin tran;
	   
       if exists (select * from sp.tGames g where g.StartDate = @StartDate)
       begin 
               rollback;
               return 1;
       end
		
       insert into sp.tGames(GameType,StartDate) values(@Gametype,@StartDate)
       set @GameId = scope_identity();
	   commit;
end;