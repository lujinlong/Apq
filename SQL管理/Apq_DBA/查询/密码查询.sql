
-- ²é¿´ÃÜÂë
--SELECT *
SELECT s.SrvID,s.SrvName,s.IPWan1,s.IPLan1,PwdType=CASE p.PwdType WHEN 1 THEN 'OS' WHEN 2 THEN 'DB' WHEN 3 THEN 'FTP' WHEN 4 THEN 'Serv-U' END
	,p.LoginName,p.SID,p.LoginPwd,p.Enabled
  FROM mgr.Server s LEFT JOIN mgr.SrvPwd p ON s.SrvID = p.SrvID
 
-- WHERE SrvName LIKE ''
 ORDER BY p.SrvID
 