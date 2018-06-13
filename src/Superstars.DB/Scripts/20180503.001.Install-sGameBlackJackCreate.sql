create proc sp.sGameBlackJackCreate
(
	@BlackJackGameId int out,
	@Pot int
)
as
begin
	set transaction isolation level serializable;
	begin tran;

    insert into sp.tGameBlackJack(Pot) values(@Pot);
	set @BlackJackGameId = scope_identity();
	commit;
    return 0;
end;