create proc sp.sYamsAICreate
(	
	@PlayerId int out, 
	@UserId int out,
	@NbrRevives int,
	@Dices varchar(6),
	@DicesValue int
)
as
	declare @YamsPlayerId int;
	set @YamsPlayerId = (select t.UserId from sp.tUser t where t.UserName = (select CONCAT('AI', @UserId) AS ConcatenatedString));
	declare @GameId int;
	set @GameId = (select top 1 GameId from sp.tGames order by StartDate desc);

begin
    set transaction isolation level serializable;
	begin tran;

        if exists(select * from sp.tYamsPlayer y where y.[YamsPlayerId] = @YamsPlayerId)
	begin
		rollback;
		return 1;
	end;

    insert into sp.tYamsPlayer(YamsPlayerId, YamsGameId, [Dices], [NbrRevives], [DicesValue]) values(@YamsPlayerId, @GameId, @Dices, @NbrRevives, @DicesValue);
	set @PlayerId = @YamsPlayerId; 
	commit;
    return 0;
end;