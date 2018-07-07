create proc sp.sGameBlackJackCreate
(
	@BlackJackGameId int out,
	@Pot varchar(20)
)
as
begin
	set transaction isolation level serializable;
	begin tran;
    SET IDENTITY_INSERT sp.tGameBlackJack ON;
	declare @GameId int;
   select top 1 @GameId = GameId from sp.tGames order by StartDate desc;
	set @BlackJackGameId = @GameId;
    insert into sp.tGameBlackJack(BlackJackGameId,Pot) values(@BlackJackGameId,@Pot);
	set @BlackJackGameId = scope_identity();
	commit;
    return 0;
end;