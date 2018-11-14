create procedure sp.sGamesCreate
(
	@GameId int out,
	@GameTypeId int,
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
		
       insert into sp.tGames(GameTypeId,StartDate) values(@GametypeId,@StartDate)
       set @GameId = scope_identity();
	   commit;
end;