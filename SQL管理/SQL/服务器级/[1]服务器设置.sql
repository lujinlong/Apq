
-- 数据库内部设置 ----------------------------------------------------------------------------------
EXEC sys.sp_configure N'show advanced options', N'1'
RECONFIGURE
GO

EXEC sys.sp_configure N'xp_cmdshell', 1
RECONFIGURE
GO

EXEC sys.sp_configure N'Ole Automation Procedures', 1
RECONFIGURE
GO

EXEC sys.sp_configure N'awe enabled', N'1'
RECONFIGURE
GO

EXEC sys.sp_configure N'cross db ownership chaining', N'1'
EXEC sys.sp_configure N'backup compression default', N'1'	-- 压缩备份
RECONFIGURE
GO

--设置可用内存(3--(可用内存-1)G)
--EXEC sys.sp_configure N'min server memory', 3072-- 可不用设置
EXEC sys.sp_configure N'max server memory', 5120
RECONFIGURE
GO

--[实际用处不大]网络包改为8K
EXEC sys.sp_configure N'network packet size (B)', 8192
RECONFIGURE
GO

-- 登录审核(2000)
EXEC xp_regwrite @rootkey='HKEY_LOCAL_MACHINE', 
	@key='Software\Microsoft\MSSQLServer\MSSQLServer', 
	@value_name='AuditLevel', 
	@type='REG_DWORD',
	@value=3
GO

-- MSSQLServer端口改为3134
--2005
EXEC xp_regwrite @rootkey='HKEY_LOCAL_MACHINE', 
	@key='Software\Microsoft\MSSQLServer\MSSQLServer\SuperSocketNetLib\Tcp\IPALL', 
	@value_name='TcpPort', 
	@type='REG_SZ',
	@value='3134'
--2008R2
EXEC xp_regwrite @rootkey='HKEY_LOCAL_MACHINE', 
	@key='SOFTWARE\Microsoft\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQLServer\SuperSocketNetLib\Tcp\IPAll', 
	@value_name='TcpPort', 
	@type='REG_SZ',
	@value='3134'
GO

--修改TempDB库大小和增长方式
ALTER DATABASE tempdb MODIFY FILE (NAME='tempdev',SIZE=2000MB,FILEGROWTH=500MB)
ALTER DATABASE tempdb MODIFY FILE (NAME='templog',SIZE=1000MB,FILEGROWTH=500MB)
GO

-- 代理日志行数最大
-- 2005以上
EXEC msdb.dbo.sp_set_sqlagent_properties @jobhistory_max_rows=999999, 
		@jobhistory_max_rows_per_job=10000
GO
-- =================================================================================================

-- 数据库外部设置 ----------------------------------------------------------------------------------
-- 注销时删除运行列表
EXEC xp_regwrite @rootkey='HKEY_LOCAL_MACHINE', 
	@key='SOFTWARE\Microsoft\Windows\CurrentVersion\policies\Explorer', 
	@value_name='ClearRecentDocsonExit', 
	@type='REG_SZ',
	@value='1'
GO
-- 远程桌面端口改为33189
EXEC xp_regwrite @rootkey='HKEY_LOCAL_MACHINE', 
	@key='SYSTEM\CurrentControlSet\Control\Terminal Server\WinStations\RDP-Tcp', 
	@value_name='PortNumber', 
	@type='REG_DWORD',
	@value=33189
EXEC xp_regwrite @rootkey='HKEY_LOCAL_MACHINE', 
	@key='SYSTEM\CurrentControlSet\Control\Terminal Server\Wds\rdpwd\Tds\tcp', 
	@value_name='PortNumber', 
	@type='REG_DWORD',
	@value=33189
GO

-- ODBC数据源添加 local<Apq_DBA>
EXEC xp_regwrite @rootkey=N'HKEY_LOCAL_MACHINE',
	@key=N'SOFTWARE\ODBC\ODBC.INI\local<Apq_DBA>',
	@value_name=N'Driver',
	@type=N'REG_SZ',
	@value='C:\\WINDOWS\\system32\\SQLSRV32.dll'
EXEC xp_regwrite @rootkey=N'HKEY_LOCAL_MACHINE',
	@key=N'SOFTWARE\ODBC\ODBC.INI\local<Apq_DBA>',
	@value_name=N'Server',
	@type=N'REG_SZ',
	@value='(local)'
EXEC xp_regwrite @rootkey=N'HKEY_LOCAL_MACHINE',
	@key=N'SOFTWARE\ODBC\ODBC.INI\local<Apq_DBA>',
	@value_name=N'Database',
	@type=N'REG_SZ',
	@value='Apq_DBA'
EXEC xp_regwrite @rootkey=N'HKEY_LOCAL_MACHINE',
	@key=N'SOFTWARE\ODBC\ODBC.INI\ODBC Data Sources',
	@value_name=N'local<Apq_DBA>',
	@type=N'REG_SZ',
	@value='SQL Server'
GO
-- =================================================================================================
