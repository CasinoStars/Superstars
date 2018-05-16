create proc sp.sEloUpdate
(
        @GameTypeId varchar,
        @UserId int,
        @Elo int
)
as 
begin
         set transaction isolation level serializable;
         begin tran;

         if not exists (select * from sp.tElo e where e.UserId = UserId and e.GameTypeId = GameTypeId )
         begin 
                 rollback;
                 return 1;
         end;        
      update sp.tElo set [Elo] = @Elo where UserId = @UserId and GameTypeId = @GameTypeId;
         commit;
      return 0;
end;