create proc sp.sGamePokerUpdate 
(
	@PokerGameId int,
	@Pot int,
	@TableCardsValue int,
	@TableCards varchar(4),
	@NumTour int
)
as 
begin
	set transaction isolation level serializable;
	begin tran;

	if not exists(select * from sp.tGamePoker g where g.PokerGameId = @PokerGameId)
	begin
		rollback;
		return 1;
	end;
    update sp.tGamePoker set [Pot] = @Pot, [TableCardsValue] = @TableCardsValue,[TableCards]=@TableCards,[NumTour]=@NumTour where PokerGameId = @PokerGameId;
	commit;
    return 0;
end;