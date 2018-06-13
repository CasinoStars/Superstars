create proc sp.sGameBlackJackUpdate
(
	@BlackJackGameId int,
	@Pot int
)
as
begin
	set transaction isolation level serializable;
	begin tran;

	if not exists(select * from sp.tGameBlackJack g where g.BlackJackGameId = @BlackJackGameId)
	begin
		rollback;
		return 1;
	end;

    update sp.tGameBlackJack set [Pot] = @Pot where BlackJackGameId = @BlackJackGameId;
	commit;
    return 0;
end;