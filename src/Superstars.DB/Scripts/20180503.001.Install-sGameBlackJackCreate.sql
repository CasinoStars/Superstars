create proc sp.sGameBlackJackCreate
(
	@BlackJackGameId int out,
	@Pot int,
	@DealerCards nvarchar(25)
)
as
begin
	set transaction isolation level serializable;
	begin tran;

    insert into sp.tGameBlackJack(Pot,DealerCards) values(@Pot,@DealerCards);
	set @BlackJackGameId = scope_identity();
	commit;
    return 0;
end;