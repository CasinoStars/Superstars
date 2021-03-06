create proc sp.sGameBlackJackCreate
(
	@BlackJackGameId int out,
	@Pot varchar(20)
)
as
begin
	set transaction isolation level serializable;
	begin tran;
	declare @GameId int;
    select top 1 @GameId = GameId from sp.tGames where GameTypeId = 1 order by StartDate desc;
	set @BlackJackGameId = @GameId;
    insert into sp.tGameBlackJack(BlackJackGameId,Pot) values(@BlackJackGameId,@Pot);
	commit;
    return 0;
end;