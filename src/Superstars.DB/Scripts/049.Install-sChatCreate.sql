create procedure sp.sChatCreate
(
	@UserId int out,
	@Message TEXT
)
as 
begin
	set transaction isolation level serializable;
	begin tran;

    insert into sp.tChat(UserId, TextMessage, CreationDate) values(@UserId, @Message, GETDATE());
	commit;
	return 0;
end;