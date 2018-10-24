create proc sp.sBlackJackAICreate
(	
	@PlayerId int out,
	@UserId int,
	@PlayerCards nvarchar(25),
	@SecondPlayerCards nvarchar(25),
	@NbTurn int,
	@HandValue int,
	@SecondHandValue int
)
as
	declare @BlackJackPlayerId int;
	set @BlackJackPlayerId = (select t.UserId from sp.tUser t where t.UserName = (select CONCAT('#AI', @UserId) AS ConcatenatedString));
	declare @BlackJackGameId int;
	set @BlackJackGameId = (select top 1 GameId from sp.tGames order by StartDate desc);

begin
    set transaction isolation level serializable;
	begin tran;

	if exists(select * from sp.tBlackJackPlayer bj where bj.BlackJackGameId = @BlackJackGameId and bj.BlackJackPlayerId = @BlackJackPlayerId)
	begin
		rollback;
		return 1;
	end;

    insert into sp.tBlackJackPlayer(BlackJackPlayerId,BlackJackGameId,PlayerCards,SecondPlayerCards,NbTurn,HandValue,SecondHandValue) values(@BlackJackPlayerId, @BlackJackGameId, @PlayerCards,@SecondPlayerCards, @NbTurn,@HandValue,@SecondHandValue);
	set @PlayerId = @BlackJackPlayerId;
	commit;
    return 0;
end;