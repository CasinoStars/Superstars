create procedure sp.sUserDelete
(
    @UserId int
)
as
begin
	delete from sp.tBlackJackPlayer where BlackJackPlayerId = @UserId;
	delete from sp.tYamsPlayer where YamsPlayerId = @UserId;
	delete from sp.tCrash where UserId = @UserId;
	delete from sp.tProvablyFair where UserId = @UserId;
	delete from sp.tLogTable where UserId = @UserId;
	delete from sp.tMoney where UserId = @UserId;
	delete from sp.tStats where UserId = @UserId;
	delete from sp.tChat where UserId = @UserId;
    delete from sp.tUser where UserId = @UserId;
    return 0;
end;