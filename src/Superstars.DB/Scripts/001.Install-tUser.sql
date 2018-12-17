create table sp.tUser
(
	UserId int identity(0,1),
	UserName nvarchar(64) not null,
	UserPassword varbinary(128) not null,
	Email nvarchar(64) not null,
	PrivateKey varchar(64) not null,
	Country varchar(64),
	LastConnexionDate datetime,
	LastDeconnexionDate datetime,
	IsAdmin bit


	constraint PK_tUser primary key(UserId),
	constraint UK_tUser_Name unique(UserName),
	constraint UK_tUser_PrivateKey unique(PrivateKey)
);

create unique index IX_tUser_Email on sp.tUser(Email) where Email <> '';
insert into sp.tUser(UserName,UserPassword,Email,PrivateKey,Country,IsAdmin) values('Admin',Convert(varbinary(128),'cRuhig9SN9u7GNWoa9nmduHNzaodJeGaTSTeQtGsjg3HnzYg82Ut'),'','wxcvbn','France',1);