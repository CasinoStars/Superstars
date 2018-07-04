create view sp.vUser
as
    select UserId = u.UserId,
           Email = u.Email,
		   UserName = u.UserName,
		   UserPassword = u.UserPassword,
		   PrivateKey = u.PrivateKey
	from sp.tUser u