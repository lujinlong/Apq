IF ( object_id('dbo.ApqDBMgr_DBC_Save','P') IS NULL ) 
    EXEC sp_executesql N'CREATE PROCEDURE dbo.ApqDBMgr_DBC_Save AS BEGIN RETURN END' ;
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2011-04-01
-- 描述: (UPDATE/INSERT)保存数据库
-- 示例:
EXEC dbo.ApqDBMgr_DBC_Save 1
	,-1,-1,'测试数据',0,-1,'172.16.0.20',1433,'apq', 'f'
-- =============================================
*/
ALTER PROC dbo.ApqDBMgr_DBC_Save
	 @SqlID			int
	,@DBID			int out
	,@DBCType		int
	,@UseTrusted	tinyint
	,@DBName		nvarchar(50)
	,@UserId		nvarchar(50)
	,@PwdC			nvarchar(500)
	,@Mirror		nvarchar(1000)
	,@Option		nvarchar(1000)
AS
SET NOCOUNT ON ;

DECLARE @ExMsg nvarchar(max);

UPDATE dbo.DBC
   SET SqlID = @SqlID, DBCType = @DBCType, UseTrusted = @UseTrusted, DBName = @DBName
	,UserId = @UserId, PwdC = @PwdC, Mirror = @Mirror, [Option] = @Option
 WHERE [DBID] = @DBID;
IF(@@ROWCOUNT = 0)
BEGIN
	IF(@DBID >= 0)
	BEGIN
		DECLARE @DB_DBID int;
		EXEC dbo.Apq_Identifier @ExMsg out, 'dbo.DBC',1,@DB_DBID OUT;
		SELECT @DBID = @DB_DBID;
	END

	INSERT dbo.DBC ( SqlID, [DBID], DBCType, UseTrusted, DBName, UserId, PwdC, Mirror, [Option] )
	VALUES ( @SqlID,@DBID,@DBCType,@UseTrusted,@DBName,@UserId,@PwdC,@Mirror,@Option );
	
END

SELECT s.ComputerID, d.SqlID, [DBID], DBCType, UseTrusted, DBName, d.UserId, d.PwdC, Mirror, [Option]
  FROM dbo.DBC d LEFT JOIN dbo.SqlInstance s ON d.SqlID = s.SqlID
 WHERE [DBID] = @DBID;
GO
