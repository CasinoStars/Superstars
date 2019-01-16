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
insert into sp.tUser(UserName,UserPassword,Email,PrivateKey,Country,IsAdmin) values('admin',0x01000000010000271000000010F2A190011AA096B8262340A1AECFEA8929F6D22FAEE1136D737BC5C9C468BBEDA925A792890179E4FF57CA15D3AD87AE,'','wxcvbn','France',1);