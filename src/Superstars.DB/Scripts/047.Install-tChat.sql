create table sp.tChat
(
	Id int identity(0,1),
	UserId int not null,
	TextMessage TEXT not null,
	CreationDate DATETIME
	
	constraint PK_tChat primary key(Id),
	constraint FK_tChat_UserId foreign key (UserId) references sp.tUser(UserId)
);
