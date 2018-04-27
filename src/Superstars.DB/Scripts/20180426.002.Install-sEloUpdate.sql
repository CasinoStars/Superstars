create proc sp.sEloUpdate
(
		@GameTypeId int,
        @UserId int,
        @Elo int
)
as 
begin
         set transaction isolation level serializable;
         begin tran;

         if not exists (select * from sp.tElo e where e.Elo = @Elo)
         begin 
                 rollback;
                 return 1;
         end;
         if exists (select * from sp.tElo e where e.Elo <> @Elo and e.UserId = @UserId and e.GameTypeId = @GameTypeId)
         begin
                  rollback;
                  return 2;
         end;         
      update sp.tElo set [UserId] = @UserId, [GameTypeId] = @GameTypeId where Elo = @Elo;
         commit;
      return 0;
end;