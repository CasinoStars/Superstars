create view sp.vUser
as
    select UserId = u.UserId,
           Email = u.Email,
		   UserName = u.UserName,
		   UserPassword = u.UserPassword,
		   PrivateKey = u.PrivateKey,
		   Country = u.Country,
		   LastConnexionDate = u.LastConnexionDate,
		   LastDeconnexionDate = u.LastDeconnexionDate
	from sp.tUser u