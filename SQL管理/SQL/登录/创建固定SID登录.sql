
USE master
GO

-- sa 改名 -----------------------------------------------------------------------------------------
IF(EXISTS(SELECT TOP 1 1 FROM sys.syslogins WHERE name = 'sa'))
	ALTER LOGIN sa WITH NAME = sx	-- UM
GO

-- 创建Windows登录 ---------------------------------------------------------------------------------
-- 添加Windows管理员组并设为Sql管理员
IF(NOT EXISTS(SELECT TOP 1 1 FROM sys.syslogins WHERE name = 'BUILTIN\administrators'))
BEGIN
	CREATE LOGIN [BUILTIN\administrators] FROM WINDOWS
	EXEC master..sp_addsrvrolemember @loginame = N'BUILTIN\administrators', @rolename = N'sysadmin'
END
IF(NOT EXISTS(SELECT TOP 1 1 FROM sys.syslogins WHERE name = 'BUILTIN\Performance Log Users'))
	CREATE LOGIN [BUILTIN\Performance Log Users] FROM WINDOWS

IF(NOT EXISTS(SELECT TOP 1 1 FROM sys.syslogins WHERE name = 'NT AUTHORITY\NETWORK SERVICE'))
	CREATE LOGIN [NT AUTHORITY\NETWORK SERVICE] FROM WINDOWS

IF(NOT EXISTS(SELECT TOP 1 1 FROM sys.syslogins WHERE name = 'NT AUTHORITY\SYSTEM'))
	CREATE LOGIN [NT AUTHORITY\SYSTEM] FROM WINDOWS

-- 重建固定SID登录 ---------------------------------------------------------------------------------
IF(EXISTS(SELECT TOP 1 1 FROM sys.syslogins WHERE name = 'BcpIn')) DROP LOGIN BcpIn;
CREATE LOGIN BcpIn WITH PASSWORD = N'm6hvdNCRMAJ71059', SID = 0x45AF6DA7BA5D0C47A3B1198F22E1000B;

IF(EXISTS(SELECT TOP 1 1 FROM sys.syslogins WHERE name = 'LinkIn')) DROP LOGIN LinkIn;
CREATE LOGIN LinkIn WITH PASSWORD = N'ucw81R3AdFDe2NEh', SID = 0xFA488300C7EE6A4591D33BEAD66C5976;


IF(EXISTS(SELECT TOP 1 1 FROM sys.syslogins WHERE name = 'Web')) DROP LOGIN Web;
CREATE LOGIN Web WITH PASSWORD = N'AcfGE4j81MRhB57i', SID = 0xB7BEF97DBD3A9C468F8E098897BE2F2C;

IF(EXISTS(SELECT TOP 1 1 FROM sys.syslogins WHERE name = 'Web_Bg')) DROP LOGIN Web_Bg;
CREATE LOGIN Web_Bg WITH PASSWORD = N'iAE9Cjeum6BnaG8v', SID = 0xBAA9F7C874F6F74CAB3D1C97CDC24B4E;

IF(EXISTS(SELECT TOP 1 1 FROM sys.syslogins WHERE name = 'Web_WS')) DROP LOGIN Web_WS;
CREATE LOGIN Web_WS WITH PASSWORD = N'JEkunAGrLBYW91t6', SID = 0xD42D3E7C161A9C49AA1D87DED20877E5;


IF(EXISTS(SELECT TOP 1 1 FROM sys.syslogins WHERE name = 'DynaTerminal')) DROP LOGIN DynaTerminal;
CREATE LOGIN DynaTerminal WITH PASSWORD = N'57nfGrtpAeiJvCuw', SID = 0xDB617274F9F79E4E958552C34978648E;

IF(EXISTS(SELECT TOP 1 1 FROM sys.syslogins WHERE name = 'GameW')) DROP LOGIN GameW;
CREATE LOGIN GameW WITH PASSWORD = N'yxF29T1jtf5cuhap', SID = 0x8D804EC8FC7DC04D980D66F7A720C5FC;

IF(EXISTS(SELECT TOP 1 1 FROM sys.syslogins WHERE name = 'Videos')) DROP LOGIN Videos;
CREATE LOGIN Videos WITH PASSWORD = N'Aktav7Jcy40YweGm', SID = 0xFD6307EF676E8944AC2738870E31AD22;

IF(EXISTS(SELECT TOP 1 1 FROM sys.syslogins WHERE name = 'StreamMedia')) DROP LOGIN StreamMedia;
CREATE LOGIN StreamMedia WITH PASSWORD = N'n0Cau7hwtp2RvBke', SID = 0x3056328E3822E142B4FE8CAB75920549;

IF(EXISTS(SELECT TOP 1 1 FROM sys.syslogins WHERE name = 'PayCenter')) DROP LOGIN PayCenter;
CREATE LOGIN PayCenter WITH PASSWORD = N'J1TarDN5MLCWjBEe', SID = 0xC05AFE3A4424E14F9B7F1CE1BCA00A4C;

GO
