create proc sp.sGameYamsCreate
(
	@YamsGameId int out,
	@Pot int,
)
as
begin
	set transaction isolation level serializable;
	begin tran;

    insert into sp.tGameYams(Pot) values(@Pot);
	set @YamsGameId = scope_identity();
	commit;
    return 0;
end;