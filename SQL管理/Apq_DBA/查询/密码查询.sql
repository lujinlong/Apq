
-- ²é¿´ÃÜÂë
--SELECT *
SELECT s.SrvID,s.SrvName,s.IPWan1,s.IPLan1,PwdType=pt.Description
	,p.LoginName,p.LoginPwd,p.SID,p.Enabled
	,s.RdpPort,s.SqlPort,s.FTPPort
  FROM mgr.Server s FULL JOIN mgr.SrvPwd p ON s.SrvID = p.SrvID LEFT JOIN dic.PwdType pt ON p.PwdType = pt.PwdType
 
-- WHERE SrvName LIKE ''
 ORDER BY s.SrvID DESC, LoginName
 