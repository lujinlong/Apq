DECLARE @str varchar(Max), @Time datetime, @m int;
SELECT @str = '', @Time = '20070101', @m=1;

WHILE(@Time < '20500101')
BEGIN
	SELECT @str = @str+'ALTER DATABASE ^DBName$ ADD FILE ( NAME = N'''''+CONVERT(varchar,@Time,112)+''''', FILENAME = N''''^Folder$\^DBName$\'+CONVERT(varchar,@Time,112)+'.ndf'''', SIZE = 512KB , FILEGROWTH = 200MB ) TO FILEGROUP ['+CONVERT(varchar,@Time,112)+'];';
	
	IF(@m%12=0) SELECT @str = @str+'
';
	SELECT @Time = DATEADD(MM,1,@Time),@m=@m+1;
END

PRINT @str;

DECLARE @str varchar(Max), @Time datetime, @m int;
SELECT @str = '', @Time = '20070101', @m=1;

WHILE(@Time < '20500101')
BEGIN
	SELECT @str = @str+'['+CONVERT(varchar,@Time,112)+'],';
	
	IF(@m%12=0) SELECT @str = @str+'
';
	SELECT @Time = DATEADD(MM,1,@Time),@m=@m+1;
END

PRINT @str;
