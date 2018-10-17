create procedure sp.sUserDelete
(
    @UserId int
)
as
begin
    delete from sp.tUser where UserId = @UserId;
    return 0;
end;