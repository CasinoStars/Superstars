create proc sp.sYamsPlayerUpdate
(
	@YamsPlayerId int,
	@YamsGameId int,
	@NbrRevives int,
	@DicesValue int
)
as 
begin
        set transaction isolation level serializable;
	begin tran;

        if not exists(select * from sp.tYamsPlayer y where y.YamsPlayerId = @YamsPlayerId and y.YamsGameId = @YamsGameId)
	begin
		rollback;
		return 1;
	end;

	if exists(select * from sp.tYamsPlayer y where y.YamsPlayerId <> @YamsPlayerId and y.YamsGameId = @YamsGameId and y.DicesValue = @DicesValue and y.NbrRevives = @NbrRevives)
	begin
		rollback;
		return 2;
	end;

    update sp.tYamsPlayer set [NbrRevives] = @NbrRevives, [DicesValue] = @DicesValue where YamsPlayerId = @YamsPlayerId and YamsGameId = @YamsGameId;
        commit;
    return 0;
end;