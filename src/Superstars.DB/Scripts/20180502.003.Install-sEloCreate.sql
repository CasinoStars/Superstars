create proc sp.sEloCreate
(
	@GameTypeId int out,
	@UserId int,
	@Elo int
)
as
begin
	set transaction isolation level serializable;
	begin tran;

	if exists(select * from sp.tElo e where e.UserId = @UserId and e.GameTypeId = @GameTypeId)
	begin
		rollback;
		return 1;
	end;

    insert into sp.tElo(GameTypeId, UserId,Elo) values(@GameTypeId, @UserId,@Elo);
	commit;
    return 0;
end;