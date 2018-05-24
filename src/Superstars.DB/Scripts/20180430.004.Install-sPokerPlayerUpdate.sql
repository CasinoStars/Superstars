create proc sp.sPokerPlayerUpdate
(
	@PokerPlayerId int,
	@PokerGameId int,
	@PlayerCards nvarchar(4)
)
as 
begin
      set transaction isolation level serializable;
      begin tran;

      if not exists(select * from sp.tPokerPlayer p where p.PokerPlayerId = @PokerPlayerId and p.PokerGameId = @PokerGameId)
      begin
		rollback;
		return 1;
      end;
      if exists(select * from sp.tPokerPlayer p where p.PokerGameId <> @PokerGameId and p.[PokerGameId] = @PokerGameId and p.[PlayerCards]=@PlayerCards)
      begin
		rollback;
		return 2;
      end;
      update sp.tPokerPlayer set [PlayerCards] = @PlayerCards where PokerPlayerId = @PokerPlayerId and PokerGameId = @PokerGameId;
	commit;
    return 0;
end;