create procedure sp.sYamsAIDelete
(
    @UserId int out
)
as
begin
delete sp.tYamsPlayer from sp.tYamsPlayer t left JOIN sp.tUser u ON t.YamsPlayerId = u.UserId where u.UserName = (select CONCAT('#AI', @UserId) AS ConcatenatedString);
    return 0;
end;