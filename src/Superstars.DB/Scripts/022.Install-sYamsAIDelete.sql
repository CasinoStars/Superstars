create procedure sp.sYamsAIDelete
(
    @UserId int out
)
as
begin
delete sp.tYamsPlayer from sp.tYamsPlayer t left JOIN sp.tUser u ON t.YamsPlayerId = u.UserId where u.UserName = (select CONCAT('#AI', CONVERT(VARCHAR, @UserId) + CONVERT(VARCHAR, 0)) AS ConcatenatedString);
    return 0;
end;