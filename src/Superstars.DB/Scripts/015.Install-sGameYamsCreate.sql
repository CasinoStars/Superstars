create proc sp.sGameYamsCreate
(
	@YamsGameId int out,
	@Pot varchar(20)
)
as
begin
	set transaction isolation level serializable;
	begin tran;
	declare @GameId int;
   select top 1 @GameId = GameId from sp.tGames order by StartDate desc;
	set @YamsGameId = @GameId;
    insert into sp.tGameYams(YamsGameId,Pot) values(@YamsGameId,@Pot);
	commit;
    return 0;
end;