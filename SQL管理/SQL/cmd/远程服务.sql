
-- 查看服务
EXEC xp_cmdshell 'sc query TermService'

-- 禁用启用[不太管用]
EXEC xp_cmdshell 'reg delete "HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Terminal Server" /v fDenyTSConnections /f'
EXEC xp_cmdshell 'reg add "HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Terminal Server" /v fDenyTSConnections /t REG_DWORD /d 0'
EXEC xp_cmdshell 'reg query "HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Terminal Server"'

-- 远程桌面端口改为40896
EXEC xp_regwrite @rootkey='HKEY_LOCAL_MACHINE', 
	@key='SYSTEM\CurrentControlSet\Control\Terminal Server\WinStations\RDP-Tcp', 
	@value_name='PortNumber', 
	@type='REG_DWORD',
	@value=40896
EXEC xp_regwrite @rootkey='HKEY_LOCAL_MACHINE', 
	@key='SYSTEM\CurrentControlSet\Control\Terminal Server\Wds\rdpwd\Tds\tcp', 
	@value_name='PortNumber', 
	@type='REG_DWORD',
	@value=40896
GO
