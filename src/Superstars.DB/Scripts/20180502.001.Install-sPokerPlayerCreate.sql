create proc sp.sPokerPlayerCreate
(
	@PokerPlayerId int,
	@PokerGameId int,
	@PlayerCards nvarchar(4)
)
as
begin
    set transaction isolation level serializable;
	begin tran;

	if exists(select * from sp.sPokerPlayer p where p.[PokerGameId] = @PokerGameId)
	begin
		rollback;
		return 1;
	end;

    insert into sp.tPokerPlayer([PokerGameId], [PlayerCards]) values(@PokerGameId, @PlayerCards);
	set @PokerPlayerId = scope_identity();
	commit;
    return 0;
end;