create proc sp.sBlackJackPlayerUpdate
(
	@BlackJackPlayerId int out,
	@BlackJackGameId int,
	@PlayerCards nvarchar
)
as
begin
	set transaction isolation level serializable;
	begin tran;

	     if not exists(select * from sp.tBlackJackPlayer bj where bj.[BlackJackGameId] = @BlackJackGameId)
	begin
		rollback;
		return 1;
	end;

	if exists(select * from sp.tBlackJackPlayer bj where bj.BlackJackPlayerId <> @BlackJackPlayerId and bj.BlackJackGameId = @BlackJackGameId and bj.PlayerCards = @PlayerCards) 
	begin
	   rollback
	   return 2;
	end;

	    update sp.tBlackJackPlayer set [PlayerCards] = @PlayerCards where BlackJackPlayerId = @BlackJackPlayerId and BlackJackGameId = @BlackJackGameId;
        commit;
    return 0;
end;