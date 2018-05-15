create proc sp.sGameTypeDelete
(
	@GameType int,
)
as
begin
    delete from sp.tGameType where GameType = @GameType;
    return 0;
end;