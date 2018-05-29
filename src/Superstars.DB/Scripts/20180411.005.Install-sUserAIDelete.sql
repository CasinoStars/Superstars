create procedure sp.sUserAIDelete
(
    @Pseudo varchar
)
as
begin
    delete sp.tUser from sp.tUser u where u.UserName = 'AI' + @Pseudo
    return 0;
end;