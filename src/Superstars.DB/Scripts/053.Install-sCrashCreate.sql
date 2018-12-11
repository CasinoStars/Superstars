create proc sp.sCrashCreate
(
	@UserId int,
	@Bet int,
	@Multi float,
	@MoneyTypeId int
)
as

	declare @GameId int;
	set @GameId = (select top 1 GameId from sp.tGames where GameTypeId = 2 order by StartDate desc);

begin
	set transaction isolation level serializable;
	begin tran;

	if exists(select * from sp.tCrash c where c.GameId = @GameId and c.UserId = @UserId)
	begin
		rollback;
		return 1;
	end;

    insert into sp.tCrash(GameId, UserId, Bet, Multi, MoneyTypeId) values(@GameId, @UserId, @Bet, @Multi, @MoneyTypeId);
	commit;
    return 0;
end;