create proc sp.sGameBlackJackCreate
(
	@BlackJackGameId int out,
	@Pot varchar(20)
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