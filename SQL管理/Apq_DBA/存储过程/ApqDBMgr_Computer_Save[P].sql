IF ( object_id('dbo.ApqDBMgr_Computer_Save','P') IS NULL ) 
    EXEC sp_executesql N'CREATE PROCEDURE dbo.ApqDBMgr_Computer_Save AS BEGIN RETURN END' ;
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2011-04-01
-- 描述: (UPDATE/INSERT)保存服务器
-- 示例:
EXEC dbo.ApqDBMgr_Computer_Save 1
	,-1,-1,'测试数据',0,-1,'172.16.0.20',1433,'apq', 'f'
-- =============================================
*/
ALTER PROC dbo.ApqDBMgr_Computer_Save
	 @ComputerID	int out
	,@ComputerName	nvarchar(50)
	,@ComputerType	int
AS
SET NOCOUNT ON ;

DECLARE @ExMsg nvarchar(max);

UPDATE dbo.Computer
   SET ComputerName = @ComputerName, ComputerType = @ComputerType
 WHERE ComputerID = @ComputerID;
IF(@@ROWCOUNT = 0)
BEGIN
	IF(@ComputerID >= 0)
	BEGIN
		DECLARE @DB_ComputerID int;
		EXEC dbo.Apq_Identifier @ExMsg out, 'dbo.Computer',1,@DB_ComputerID OUT;
		SELECT @ComputerID = @DB_ComputerID;
	END

	INSERT dbo.Computer ( ComputerID, ComputerName, ComputerType )
	VALUES ( @ComputerID,@ComputerName,@ComputerType );
END

SELECT ComputerID, ComputerName, ComputerType
  FROM dbo.Computer
 WHERE ComputerID = @ComputerID;
GO
