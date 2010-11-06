IF( OBJECT_ID('dbo.Job_BcpOut_Model', 'P') IS NULL )
	EXEC sp_executesql N'CREATE PROCEDURE dbo.Job_BcpOut_Model AS BEGIN RETURN END';
GO
ALTER PROC dbo.Job_BcpOut_Model
AS
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-11-05
-- 描述: 机型厂商对应表
-- 示例:
DECLARE @rtn int;
EXEC @rtn = dbo.Job_BcpOut_Model;
SELECT @rtn;
-- =============================================
*/
SET NOCOUNT ON;

DECLARE @Now datetime, @Today datetime;
SELECT @Now = getdate();
SELECT @Today = dateadd(dd,0,datediff(dd,0,@Now));

DECLARE @rtn int, @cmd nvarchar(4000);

SELECT @cmd = 'bcp "BaseBusinessDb.dbo.T_Factory" out "D:\Apq_DBA\Factory.txt" -c -t\t -r\n -T';
EXEC @rtn = xp_cmdshell @cmd;
IF(@@ERROR = 0 AND @rtn = 0)
BEGIN
	INSERT Apq_DBA.dbo.FTP_SendQueue ( Folder,FileName,FTPSrv,U,P,FTPFolder,FTPFolderTmp)
	VALUES  ('D:\Apq_DBA\', 'Factory.txt','192.168.1.86 33021','SPFTP','yipfjEnF3TRDJG49','/Model_Cp/','/')
END

SELECT @cmd = 'bcp "BaseBusinessDb.dbo.T_Model" out "D:\Apq_DBA\Model_Factory.txt" -c -t\t -r\n -T';
EXEC @rtn = xp_cmdshell @cmd;
IF(@@ERROR = 0 AND @rtn = 0)
BEGIN
	INSERT Apq_DBA.dbo.FTP_SendQueue ( Folder,FileName,FTPSrv,U,P,FTPFolder,FTPFolderTmp)
	VALUES  ('D:\Apq_DBA\', 'Model_Factory.txt','192.168.1.86 33021','SPFTP','yipfjEnF3TRDJG49','/Model_Cp/','/')
END

RETURN 1;
GO
