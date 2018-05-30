create proc sp.sBlackJackPlayerCreate
(
	@BlackJackPlayerId int out,
	@BlackJackGameId int,
	@PlayerCards nvarchar
)
as
begin
	set transaction isolation level serializable;
	begin tran;

	if exists(select * from sp.tBlackJackPlayer bj where bj.BlackJackGameId = @BlackJackGameId
	begin
		rollback;
		return 1;
	end;

	if not exists(select * from sp.tBlackJackPlayer bj where bj.BlackJackPlayerId = @BlackJackPlayerId)
    begin
          rollback;
          return 2;
    end

    insert into sp.tBlackJackPlayer([BlackJackGameId],[PlayerCards]) values( @BlackJackGameId, @PlayerCards);
	set @BlackJackPlayerId = scope_identity();
	commit;
    return 0;
end;