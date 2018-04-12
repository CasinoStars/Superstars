create view iti.vUser
as
    select UserId = u.UserId,
           Email = u.Email,
		   UserName = u.UserName,
		   UserPassword = u.UserPassword
	from sp.tUser u