create table sp.tLogTable
(
	LogId int identity(0,1),
	UserId int,
	ActionDate datetime,
	ActionDescription varchar(500)

	constraint PK_tLogTable_LogId primary key (LogId),
	constraint FK_tLogTable_UserId foreign key (UserId) references sp.tUser(UserId)
);