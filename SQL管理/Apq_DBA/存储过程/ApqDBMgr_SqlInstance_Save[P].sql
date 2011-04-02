IF ( object_id('dbo.ApqDBMgr_SqlInstance_Save','P') IS NULL ) 
    EXEC sp_executesql N'CREATE PROCEDURE dbo.ApqDBMgr_SqlInstance_Save AS BEGIN RETURN END' ;
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2011-04-01
-- 描述: (UPDATE/INSERT)保存Sql实例
-- 示例:
EXEC dbo.ApqDBMgr_SqlInstance_Save 1
	,-1,-1,'测试数据',0,-1,'172.16.0.20',1433,'apq', 'f'
-- =============================================
*/
ALTER PROC dbo.ApqDBMgr_SqlInstance_Save
	 @ComputerID	int
	,@SqlID			int out
	,@SqlName		nvarchar(50)
	,@ParentID		int
	,@SqlType		int
	,@IP			nvarchar(50)
	,@SqlPort		int
	,@UserId		nvarchar(50)
	,@PwdC			nvarchar(500)
AS
SET NOCOUNT ON ;

DECLARE @ExMsg nvarchar(max);

UPDATE dbo.SqlInstance
   SET ComputerID = @ComputerID, SqlName = @SqlName, ParentID = @ParentID, SqlType = @SqlType, IP = @IP, SqlPort = @SqlPort, UserId = @UserId, PwdC = @PwdC
 WHERE SqlID = @SqlID;
IF(@@ROWCOUNT = 0)
BEGIN
	IF(@SqlID >= 0)
	BEGIN
		DECLARE @DB_SqlID int;
		EXEC dbo.Apq_Identifier @ExMsg out, 'dbo.SqlInstance',1,@DB_SqlID OUT;
		SELECT @SqlID = @DB_SqlID;
	END

	INSERT dbo.SqlInstance ( ComputerID,SqlID,SqlName,ParentID,SqlType,IP,SqlPort,UserId,PwdC )
	VALUES ( @ComputerID,@SqlID,@SqlName,@ParentID,@SqlType,@IP,@SqlPort,@UserId,@PwdC );
END

SELECT ComputerID,SqlID,SqlName,ParentID,SqlType,IP,SqlPort,UserId,PwdC
  FROM dbo.SqlInstance
 WHERE SqlID = @SqlID;
GO
