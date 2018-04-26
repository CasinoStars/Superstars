create proc sp.tElo
(
	@GameTypeId,
        @UserId int,
        @Elo int,
)
as 
begin
         set transaction isolation level serializable;
         begin tran;

         if not exist(select *from sp.tElo e where e.Elo = @Elo)
         begin 
                 rollback;
                 return 1;
         end;
         if exist (select *from sp.tElo e where e.Elo <> @Elo and UserId = @UserId and GameTypeId = @GameTypeId)
         begin
                  rollback;
                  return 2;
         end;         
      update sp.tElo set [UserId]= @UserId, [GameTypeId]=@GameTypeId where Elo = @Elo;
         commit;
      return 0;
end;