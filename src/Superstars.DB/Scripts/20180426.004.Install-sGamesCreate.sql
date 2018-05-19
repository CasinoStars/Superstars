create procedure sp.sGamesCreate
(
	@GameId int out,
	@GameType int,
	@StartDate datetime
)
as
begin
      declare @v_gametype varchar(64);
       set transaction isolation level serializable;
       begin tran;

       if exists (select * from sp.tGames g where g.StartDate = @StartDate)
       begin 
               rollback;
               return 1;
       end
		
	   if @GameType = 0
	   begin
	   	   select @v_gametype = GameType from sp.tGameType where GameType = 'Yams'
	   end
	   else
	   begin
	   	   select @v_gametype = GameType from sp.tGameType where GameType = 'Poker'
	   end;

       insert into sp.tGames(GameType,StartDate) values(@v_gametype,@StartDate)
       set @GameId = scope_identity();
	   commit;
end;