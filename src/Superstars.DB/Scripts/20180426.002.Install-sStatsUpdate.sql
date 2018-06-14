create proc sp.sStatsUpdate
(
        @GameType varchar(25),
        @UserId int,
		@Wins int,
		@Losses int
)
as 
begin
         set transaction isolation level serializable;
         begin tran;

         if not exists (select * from sp.tStats e where e.UserId = UserId and e.GameType = GameType )
         begin 
                 rollback;
                 return 1;
         end;  
		 
      update sp.tStats  set Wins = @Wins, Losses = @Losses where UserId = @UserId and GameType = @GameType;
         commit;
      return 0;
end;