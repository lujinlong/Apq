IF( OBJECT_ID('dbo.Apq_String_Get_tvp') IS NULL )
	EXEC sp_executesql N'CREATE FUNCTION dbo.Apq_String_Get_tvp() RETURNS @t table([ID] int) AS BEGIN RETURN; END';
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2009-03-11
-- 描述: 获取字符串,先搜索指定语言,然后搜索指定默认语言,最后搜索中文
-- 示例:
SELECT dbo.Apq_String_Get_tvp('FFFF::FFFF', 8);
-- =============================================
*/
ALTER FUNCTION dbo.Apq_String_Get_tvp(
	@tvp_StringIDs	[tvp:ID,lang] READONLY,
	@lang			nvarchar(10) = 'zh-cn'	-- 默认语言
)RETURNS @t TABLE(
	[ID1] [bigint],
	[ID] [bigint],
	[lang] [nvarchar](10),
	[str] [nvarchar](max),
	[_Time] [datetime],
	[_InTime] [datetime]
)
AS
BEGIN
	DECLARE @tvp_StringIDs1 [tvp:ID,lang];
	INSERT @tvp_StringIDs1(ID,lang) SELECT ID,lang FROM @tvp_StringIDs;

	INSERT @t(ID1,ID,lang,[str],_Time,_InTime)
	SELECT ID1,ID,lang,[str],_Time,_InTime
	  FROM Apq_String t
	 WHERE EXISTS(SELECT TOP 1 1 FROM @tvp_StringIDs1 ps WHERE ps.ID = t.ID AND ps.lang = t.lang)
	
	DELETE ps FROM @tvp_StringIDs1 ps WHERE EXISTS(SELECT TOP 1 1 FROM @t t WHERE t.ID = ps.ID)
	INSERT @t(ID1,ID,lang,[str],_Time,_InTime)
	SELECT ID1,ID,lang,[str],_Time,_InTime
	  FROM Apq_String t
	 WHERE EXISTS(SELECT TOP 1 1 FROM @tvp_StringIDs1 ps WHERE ps.ID = t.ID) AND t.lang = @lang;
	
	DELETE ps FROM @tvp_StringIDs1 ps WHERE EXISTS(SELECT TOP 1 1 FROM @t t WHERE t.ID = ps.ID)
	INSERT @t(ID1,ID,lang,[str],_Time,_InTime)
	SELECT ID1,ID,lang,[str],_Time,_InTime
	  FROM Apq_String t
	 WHERE EXISTS(SELECT TOP 1 1 FROM @tvp_StringIDs1 ps WHERE ps.ID = t.ID) AND t.lang = 'zh-cn';
	
	RETURN;
END
GO
