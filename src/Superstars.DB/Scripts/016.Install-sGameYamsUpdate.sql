create proc sp.sGameYamsUpdate
(
           @YamsGameId int,
           @Pot varchar(20)
)
as
begin
	set transaction isolation level serializable;
	begin tran;

	if not exists(select * from sp.tGameYams g where g.YamsGameId = @YamsGameId)
	begin
		rollback;
		return 1;
	end;

    update sp.tGameYams set [Pot] = @Pot where YamsGameId = @YamsGameId;
	commit;
    return 0;
end;