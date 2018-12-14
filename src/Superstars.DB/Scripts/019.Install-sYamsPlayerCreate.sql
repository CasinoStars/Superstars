create proc sp.sYamsPlayerCreate
(	
	@PlayerId int,
	@NbrRevives int,
	@Dices varchar(6),
	@DicesValue int
)
as
	declare @GameId int;
	set @GameId = (select top 1 GameId from sp.tGames where GameTypeId = 0 order by StartDate desc);

begin
    set transaction isolation level serializable;
	begin tran;

    insert into sp.tYamsPlayer(YamsPlayerId, YamsGameId, [Dices], [NbrRevives], [DicesValue]) values(@PlayerId, @GameId, @Dices, @NbrRevives, @DicesValue);
	commit;
    return 0;
end;