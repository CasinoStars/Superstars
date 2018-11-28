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
insert into sp.tUser(UserName, UserPassword, Email, PrivateKey, Country, IsAdmin) values('Admin', 0x01000000010000271000000010C85B0DD13C4AF075EAFE549DE4295970B7AF17A516012590191EC3FBD0BF64AD6BB25A9841A60D73132E22E4BE2E363C, '', 'Admin', 'France', 1);
