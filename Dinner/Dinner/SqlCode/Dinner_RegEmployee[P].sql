IF ( object_id('dbo.Dinner_RegEmployee','P') IS NULL ) 
    EXEC sp_executesql N'CREATE PROCEDURE dbo.Dinner_RegEmployee AS BEGIN RETURN END' ;
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2011-04-08
-- 描述: 登录
-- 示例:
EXEC dbo.Dinner_RegEmployee 'Apq', 0x123
-- =============================================
*/
ALTER PROC dbo.Dinner_RegEmployee
	 @ExMsg nvarchar(max) OUT
	 
	,@EmName nvarchar(50)
	,@LoginName	nvarchar(50)
	,@LoginPwd	varbinary(64)
	
	,@EmID bigint OUT
	,@LoginID bigint OUT
AS 
SET NOCOUNT ON ;

DECLARE @rtn int, @ExMsg1 nvarchar(max);

-- 重复检测
IF(EXISTS(SELECT TOP 1 1 FROM dbo.Logins WHERE LoginName = @LoginName))
BEGIN
	SELECT @ExMsg = '登录名重复,请重新输入';
	RETURN -1;
END

-- 登录信息录入
EXEC @rtn = dbo.Apq_Identifier @ExMsg1 OUT, 'dbo.Logins',1,@LoginID OUT
IF(@@ERROR = 0 AND @rtn = 1)
BEGIN
	INSERT dbo.Logins(LoginID,LoginName,LoginPwd)
	VALUES (@LoginID,@LoginName,@LoginPwd);
END

-- 员工信息录入
EXEC @rtn = dbo.Apq_Identifier @ExMsg1 OUT, 'dbo.Employee',1,@EmID OUT
IF(@@ERROR = 0 AND @rtn = 1)
BEGIN
	INSERT dbo.Employee(EmID,LoginID,EmName)
	VALUES (@EmID,@LoginID,@EmName);
END

GO
