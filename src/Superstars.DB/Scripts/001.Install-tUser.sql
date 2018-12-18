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
--insert into sp.tUser(UserName,UserPassword,Email,PrivateKey,Country,IsAdmin) values('admin',0x010000000100002710000000100CA8F8BF8AE13CA67CF27468B82B8770001E28DB98068D6DE7550A5509EF26C2DD39E88CC816C5A624829ADAF013C63B,'','wxcvbn','France',1);