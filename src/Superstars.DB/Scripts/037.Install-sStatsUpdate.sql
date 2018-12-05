create proc sp.sStatsUpdate
(
	@GameTypeId int,
	@UserId int,
	@Wins int,
	@Losses int,
	@AverageBet INT,
	@AverageTime INT
)
as 
begin
      set transaction isolation level serializable;
      begin tran;

      if not exists (select * from sp.tStats e where e.UserId = UserId and e.GameTypeId = GameTypeId )
       begin 
               rollback;
               return 1;
      end;  
		 
      update sp.tStats set Wins += @Wins, Losses += @Losses, AverageBet = @AverageBet, AverageTime = @AverageTime where UserId = @UserId and GameTypeId = @GameTypeId;
         commit;
      return 0;
end;