create table sp.tTransfer
(
	TransferId int identity(0,1),
	UserId int not null,
	Amount int not null,
	ReceiverId int not null,
	TransferDate datetime,

	constraint PK_tTransfer_TransferId primary key(TransferId),
	constraint FK_tTransfer_UserId foreign key(UserId) references sp.tUser(UserId),
);
