create procedure sp.sUserAIDelete
(
    @UserId int,
	@GameType int
)
as
BEGIN
    delete sp.tUser from sp.tUser u where u.UserName = (select CONCAT('#AI', CONVERT(VARCHAR, @UserId) + CONVERT(VARCHAR, @GameType) ) AS ConcatenatedString)
    return 0;
end;