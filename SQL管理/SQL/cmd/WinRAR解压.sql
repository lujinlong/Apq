
DECLARE @cmd nvarchar(4000);
SELECT @cmd = N'"C:\Program Files\WinRAR\WinRAR.exe" x D:\Programs\Serv-U.Corporate.v6.4.0.6_HLH(gla).rar D:\Programs';
EXEC xp_cmdshell @cmd;
