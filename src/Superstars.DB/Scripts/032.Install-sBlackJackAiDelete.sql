create procedure sp.sBlackJackAIDelete
(
    @UserId int
)
as
begin
delete sp.tBlackJackPlayer from sp.tBlackJackPlayer bj left JOIN sp.tUser u ON bj.BlackJackPlayerId = u.UserId where u.UserName = (select CONCAT('#AI', CONVERT(VARCHAR, @UserId) + CONVERT(VARCHAR, 1)) AS ConcatenatedString);
    return 0;
end;