IF ( object_id('dbo.Dinner_Login','P') IS NULL ) 
    EXEC sp_executesql N'CREATE PROCEDURE dbo.Dinner_Login AS BEGIN RETURN END' ;
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2011-04-08
-- 描述: 登录
-- 示例:
EXEC dbo.Dinner_Login 'Apq', 0x123
-- =============================================
*/
ALTER PROC dbo.Dinner_Login
	 @ExMsg nvarchar(max) OUT
	 
	,@LoginName	nvarchar(50)
	,@LoginPwd	varbinary(64)
AS 
SET NOCOUNT ON ;

DECLARE @LoginID bigint, @LoginPwdDB varbinary(64), @PwdExpire datetime, @LoginStatus int, @EmStatus int;
SELECT @LoginID = l.LoginID, @LoginPwdDB = LoginPwd, @LoginStatus = LoginStatus, @EmStatus = EmStatus
  FROM dbo.Logins l LEFT JOIN dbo.Employee e ON l.LoginID = e.LoginID
 WHERE LoginName = @LoginName;
IF(@@ROWCOUNT = 0)
BEGIN
	SELECT @ExMsg = '登录名或密码错误';
	RETURN -1;
END
IF(@LoginPwd <> @LoginPwdDB)
BEGIN
	SELECT @ExMsg = '登录名或密码错误';
	RETURN -1;
END
IF((@LoginStatus&1) = 0)
BEGIN
	SELECT @ExMsg = '登录未激活';
	RETURN -1;
END
IF((@LoginStatus&2) > 0)
BEGIN
	SELECT @ExMsg = '登录已禁用';
	RETURN -1;
END
IF((@LoginStatus&4) > 0)
BEGIN
	SELECT @ExMsg = '登录已封号';
	RETURN -1;
END
IF((@EmStatus&1) > 0)
BEGIN
	SELECT @ExMsg = '员工已禁用';
	RETURN -1;
END

SELECT l.LoginID, LoginName, LoginPwd, PwdExpire, LoginStatus, RegTime, e.EmID, e.IsAdmin, e.EmName, e.EmStatus
  FROM dbo.Logins l LEFT JOIN dbo.Employee e ON l.LoginID = e.LoginID
 WHERE l.LoginID = @LoginID;
RETURN 1;
GO
