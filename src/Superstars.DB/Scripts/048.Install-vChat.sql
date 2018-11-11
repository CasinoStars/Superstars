create view sp.vChat
as 
  SELECT 
  Id = c.Id,
  TextMessage = c.TextMessage,
  CreationDate = c.CreationDate,
  UserId = c.UserId,
  UserName = u.UserName
	
	FROM sp.tChat c
	LEFT OUTER JOIN sp.tUser u
	ON c.UserId = u.UserId