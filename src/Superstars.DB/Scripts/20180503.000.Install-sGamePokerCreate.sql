create proc sp.sGamePokerCreate 
(
	@PokerGameId int out,
	@Pot int,
	@TableCardsValue int,
	@TableCards nvarchar(10),
	@NumTour int
)
as
begin
	set transaction isolation level serializable;
	begin tran;

	insert into sp.tGamePoker(Pot, TableCardsValue,TableCards,NumTour) values(@Pot, @TableCardsValue,@TableCards,@NumTour);
	set @PokerGameId = scope_identity();
	commit;
    return 0;
end;