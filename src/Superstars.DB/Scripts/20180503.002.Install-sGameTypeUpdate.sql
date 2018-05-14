create proc sp.sGameTypeCreate
(
	@GameType int,
)
as
begin
	set transaction isolation level serializable;
	begin tran;

	set @GameType = scope_identity();
	commit;
    return 0;
end;