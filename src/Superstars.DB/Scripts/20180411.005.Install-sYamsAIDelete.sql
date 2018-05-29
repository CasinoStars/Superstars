create procedure sp.sYamsAIDelete
(
    @Pseudo varchar
)
as
begin
delete sp.tYamsPlayer from sp.tYamsPlayer t left JOIN sp.tUser u ON t.YamsPlayerId = u.UserId where u.UserName = 'AI' + @Pseudo;
    return 0;
end;