create proc sp.sYamsPlayerCreate
(	
	@PlayerId int,
	@NbrRevives int,
	@Dices varchar(6),
	@DicesValue int
)
as
	declare @GameId int;
	set @GameId = (select top 1 GameId from sp.tGames order by StartDate desc);

begin
    set transaction isolation level serializable;
	begin tran;

        if exists(select * from sp.tYamsPlayer y where y.[YamsPlayerId] = @PlayerId)
	begin
		rollback;
		return 1;
	end;

    insert into sp.tYamsPlayer(YamsPlayerId, YamsGameId, [Dices], [NbrRevives], [DicesValue]) values(@PlayerId, @GameId, @Dices, @NbrRevives, @DicesValue);
	commit;
    return 0;
end;