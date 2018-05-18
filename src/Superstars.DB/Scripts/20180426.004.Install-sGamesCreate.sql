create procedure sp.sGamesCreate
(
	@GameId int out,
	@GameType int,
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
		
	   declare @v_gametype varchar(64)
	   begin
	   select @v_gametype = GameType from sp.tGameType where GameType = @GameType
	   end;

       insert into sp.tGames(GameType,StartDate) values(@v_gametype,@StartDate)
       set @GameId = scope_identity();
	   commit;
       return 0;
end;