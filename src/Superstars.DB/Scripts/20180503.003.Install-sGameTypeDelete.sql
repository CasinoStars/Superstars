create proc sp.sGameTypeDelete
(
	@GameType varchar
)
as
begin
    delete from sp.tGameType where GameType = @GameType;
    return 0;
end;