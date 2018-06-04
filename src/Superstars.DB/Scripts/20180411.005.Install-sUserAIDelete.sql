create procedure sp.sUserAIDelete
(
    @Pseudo varchar(66)
)
as
begin
    delete sp.tUser from sp.tUser u where u.UserName = 'AI' + @Pseudo
    return 0;
end;