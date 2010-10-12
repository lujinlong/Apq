IF ( OBJECT_ID('dbo.ApqDBMgr_RSrv_Save','P') IS NULL ) 
    EXEC sp_executesql N'CREATE PROCEDURE dbo.ApqDBMgr_RSrv_Save AS BEGIN RETURN END' ;
GO
IF ( NOT EXISTS ( SELECT    *
                  FROM      fn_listextendedproperty(N'MS_Description',N'SCHEMA',N'dbo',N'PROCEDURE',N'ApqDBMgr_RSrv_Save',DEFAULT,DEFAULT) )
   ) 
    EXEC sys.sp_addextendedproperty @name = N'MS_Description',@value = N'',@level0type = N'SCHEMA',@level0name = N'dbo',@level1type = N'PROCEDURE',
        @level1name = N'ApqDBMgr_RSrv_Save'
EXEC sp_updateextendedproperty @name = N'MS_Description',@value = N'
-- 保存服务器
-- 作者: 黄宗银
-- 日期: 2010-03-27
-- 示例:
DECLARE @rtn int, @ExMsg nvarchar(max);
EXEC @rtn = dbo.ApqDBMgr_RSrv_Save @ExMsg out, 1, DEFAULT, ''127019'',''User1'',''User1'';
SELECT @rtn,@ExMsg;
',@level0type = N'SCHEMA',@level0name = N'dbo',@level1type = N'PROCEDURE',@level1name = N'ApqDBMgr_RSrv_Save' ;
GO
ALTER PROC dbo.ApqDBMgr_RSrv_Save
    @ID int
   ,@ParentID int
   ,@Name nvarchar(256)
   ,@UID nvarchar(256)
   ,@PwdC nvarchar(max)
   ,@Type int
   ,@IPLan nvarchar(500)
   ,@IPWan1 nvarchar(500)
   ,@IPWan2 nvarchar(500)
   ,@FTPPort int
   ,@FTPU nvarchar(50)
   ,@FTPPC nvarchar(max)
   ,@SqlPort int
AS 
SET NOCOUNT ON ;

IF ( @ID IS NULL ) 
    RETURN -1 ;

UPDATE  dbo.RSrvConfig
SET     ParentID = @ParentID,Name = ISNULL(@Name,Name),LSName = ISNULL(@Name,Name),UID = @UID,PwdC = @PwdC,Type = @Type,IPLan = @IPLan,IPWan1 = @IPWan1,IPWan2 = @IPWan2,
        FTPPort = ISNULL(@FTPPort,21),FTPU = @FTPU,FTPPC = @FTPPC,SqlPort = ISNULL(@SqlPort,1433)
WHERE   ID = @ID ;
IF ( @@ROWCOUNT = 0 ) 
    INSERT  dbo.RSrvConfig ( ID,ParentID,Name,LSName,UID,PwdC,Type,IPLan,IPWan1,IPWan2,FTPPort,FTPU,FTPPC,SqlPort )
    VALUES  ( @ID,@ParentID,@Name,@Name,@UID,@PwdC,@Type,@IPLan,@IPWan1,@IPWan2,@FTPPort,@FTPU,@FTPPC,@SqlPort )
RETURN 1 ;
GO
