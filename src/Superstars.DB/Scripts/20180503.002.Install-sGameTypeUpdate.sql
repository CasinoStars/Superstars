create proc sp.sGameTypeUpdate
(
	@GameType varchar
)
as
begin
	set transaction isolation level serializable;
	begin tran;

	set @GameType = scope_identity();
	commit;
    return 0;
end;