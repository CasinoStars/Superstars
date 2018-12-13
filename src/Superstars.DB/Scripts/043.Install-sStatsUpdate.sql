create proc sp.sStatsUpdate
(
	@GameTypeId int,
	@MoneyTypeId int,
	@UserId int,
	@Profit int,
	@Wins int,
	@Losses int,
	@Equality int,
	@TotalBet int,
	@AverageTime int
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
		 
      update sp.tStats set Wins += @Wins, Losses += @Losses, Equality += @Equality, Profit += @Profit, TotalBet += @TotalBet, AverageTime = @AverageTime where UserId = @UserId and GameTypeId = @GameTypeId and MoneyTypeId = @MoneyTypeId;
         commit;
      return 0;
end;