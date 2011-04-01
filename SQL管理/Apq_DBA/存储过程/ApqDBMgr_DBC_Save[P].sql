IF ( object_id('dbo.ApqDBMgr_DBC_Save','P') IS NULL ) 
    EXEC sp_executesql N'CREATE PROCEDURE dbo.ApqDBMgr_DBC_Save AS BEGIN RETURN END' ;
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2011-04-01
-- 描述: 保存Sql实例
-- 示例:
EXEC dbo.ApqDBMgr_DBC_Save 1
	,-1,-1,'测试数据',0,-1,'172.16.0.20',1433,'apq', 'f'
-- =============================================
*/
ALTER PROC dbo.ApqDBMgr_DBC_Save
	 @SaveType		int				-- 1:UPDATE/INSERT,2:DELETE

	,@ComputerID	int
	,@SqlID			int
	,@DBID			int
	,@DBCType		int
	,@UseTrusted	tinyint
	,@DBName		nvarchar(50)
	,@UserId		nvarchar(50)
	,@PwdC			nvarchar(500)
	,@Mirror		nvarchar(1000)
	,@Option		nvarchar(1000)
AS
SET NOCOUNT ON ;

IF(@SaveType = 1)
BEGIN
	UPDATE dbo.DBC
	   SET ComputerID = @ComputerID, SqlID = @SqlID, DBCType = @DBCType, UseTrusted = @UseTrusted, DBName = @DBName
		,UserId = @UserId, PwdC = @PwdC, Mirror = @Mirror, [Option] = @Option
	 WHERE [DBID] = @DBID;
	IF(@@ROWCOUNT = 0)
	BEGIN
		INSERT dbo.DBC ( ComputerID, SqlID, [DBID], DBCType, UseTrusted, DBName, UserId, PwdC, Mirror, [Option] )
		VALUES ( @ComputerID,@SqlID,@DBID,@DBCType,@UseTrusted,@DBName,@UserId,@PwdC,@Mirror,@Option );
	END	
END

IF(@SaveType = 2)
BEGIN
	DELETE dbo.DBC WHERE [DBID] = @DBID;
END
GO
