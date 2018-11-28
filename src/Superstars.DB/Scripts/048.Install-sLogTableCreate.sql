create procedure sp.sLogTableCreate 
(
	@UserId int,
	@ActionDate datetime,
	@ActionDescription nvarchar(500)
)
as 
begin
	set transaction isolation level serializable;
	begin tran;

	if exists(select * from sp.tLogTable t where t.ActionDate = @ActionDate)
	begin
		rollback;
		return 1;		
	end;

    insert into sp.tLogTable(UserId, ActionDate, ActionDescription) values(@UserId, @ActionDate, @ActionDescription);
	commit;
	return 0;
end;