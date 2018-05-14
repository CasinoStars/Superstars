create proc sp.sYamsPlayerCreate
(
	@YamsPlayerId int out,
	@YamsGameId int,
	@NbrRevives int,
	@DicesValue int
)
as
begin
    set transaction isolation level serializable;
	begin tran;

        if exists(select * from sp.tYamsPlayer y where y.[YamsGameId] = @YamsGameId)
	begin
		rollback;
		return 1;
	end;
    insert into sp.tYamsPlayer([NbrRevives], [DicesValue]) values(@NbrRevives, @DicesValue);
	set @YamsPlayerId = scope_identity();
	commit;
    return 0;
end;