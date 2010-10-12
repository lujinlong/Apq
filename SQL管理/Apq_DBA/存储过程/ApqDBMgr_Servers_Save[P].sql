IF ( OBJECT_ID('dbo.ApqDBMgr_Servers_Save','P') IS NULL ) 
    EXEC sp_executesql N'CREATE PROCEDURE dbo.ApqDBMgr_Servers_Save AS BEGIN RETURN END' ;
GO
IF ( NOT EXISTS ( SELECT    *
                  FROM      fn_listextendedproperty(N'MS_Description',N'SCHEMA',N'dbo',N'PROCEDURE',N'ApqDBMgr_Servers_Save',DEFAULT,DEFAULT) )
   ) 
    EXEC sys.sp_addextendedproperty @name = N'MS_Description',@value = N'',@level0type = N'SCHEMA',@level0name = N'dbo',@level1type = N'PROCEDURE',
        @level1name = N'ApqDBMgr_Servers_Save'
EXEC sp_updateextendedproperty @name = N'MS_Description',@value = N'
-- 保存服务器
-- 作者: 黄宗银
-- 日期: 2010-03-10
-- 示例:
DECLARE @rtn int, @ExMsg nvarchar(max);
EXEC @rtn = dbo.ApqDBMgr_Servers_Save @ExMsg out, 1, DEFAULT, ''127019'',''User1'',''User1'';
SELECT @rtn,@ExMsg;
',@level0type = N'SCHEMA',@level0name = N'dbo',@level1type = N'PROCEDURE',@level1name = N'ApqDBMgr_Servers_Save' ;
GO
ALTER PROC dbo.ApqDBMgr_Servers_Save
    @ID int
   ,@ParentID int
   ,@Name nvarchar(256)
   ,@SrvName nvarchar(256)
   ,@UID nvarchar(256)
   ,@PwdC nvarchar(max)
   ,@Type int
AS 
SET NOCOUNT ON ;

IF ( @ID IS NULL ) 
    RETURN -1 ;

UPDATE  dbo.RSrvConfig
SET     ParentID = @ParentID,Name = @Name,LSName = @SrvName,UID = @UID,PwdC = @PwdC,Type = @Type
WHERE   ID = @ID ;
IF ( @@ROWCOUNT = 0 ) 
    INSERT  dbo.RSrvConfig ( ID,ParentID,Name,LSName,UID,PwdC,Type )
    VALUES  ( @ID,@ParentID,@Name,@SrvName,@UID,@PwdC,@Type )
RETURN 1 ;
GO
