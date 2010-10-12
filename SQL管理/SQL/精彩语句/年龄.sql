
	DECLARE	@Now datetime, @age int;
	SELECT	@Now = getdate();
	SELECT	@age = DATEDIFF(Year, p.BirthTime, @Now) - 
					CASE
						WHEN DATEADD(Year, DATEDIFF(Year, p.BirthTime, @Now), p.BirthTime) > @Now THEN 1 
						ELSE 0
					END