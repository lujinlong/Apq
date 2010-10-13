IF( OBJECT_ID('dbo.Apq_BuildSPParams', 'P') IS NULL )
	EXEC sp_executesql N'CREATE PROCEDURE dbo.Apq_BuildSPParams AS BEGIN RETURN END';
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2009-05-07
-- 描述: 创建存储过程参数列表(SQL Server 2008)
-- 示例:
DECLARE @rtn int, @ExMsg nvarchar(4000);
EXEC @rtn = dbo.Apq_BuildSPParams @ExMsg out, 'dbo.Apq_Def_EveryDB';
SELECT @rtn, @ExMsg;
-- =============================================
*/
ALTER PROC dbo.Apq_BuildSPParams
	@ExMsg nvarchar(4000) out
	
	,@SPName	nvarchar(512)
AS
SET NOCOUNT ON;

DECLARE @t_helpsp TABLE(
	ParamName	sysname,
	ParamType	sysname,
	max_length	smallint,
	Precision	int,
	Scale		tinyint,
	parameter_id	int,
	Collation	nvarchar(256),
	IsOut		tinyint
);

INSERT @t_helpsp(ParamName,ParamType,max_length,Precision,Scale,parameter_id,Collation,IsOut)
SELECT name, type_name(system_type_id), max_length
	,CASE WHEN type_name(system_type_id) = 'uniqueidentifier' THEN precision ELSE OdbcPrec(system_type_id, max_length, precision) END
	,OdbcScale(system_type_id, scale), parameter_id
	,convert(sysname, case when system_type_id in (35, 99, 167, 175, 231, 239) then ServerProperty('collation') end)
	,is_output
  FROM sys.all_parameters
 WHERE object_id = object_id(@SPName)
 ORDER BY parameter_id;

SELECT * FROM @t_helpsp;
SELECT @ExMsg = '操作成功';
RETURN 1;
GO
