create procedure sp.sUserAIDelete
(
    @UserId int
)
as
begin
    delete sp.tUser from sp.tUser u where u.UserName = (select CONCAT('AI', @UserId) AS ConcatenatedString)
    return 0;
end;