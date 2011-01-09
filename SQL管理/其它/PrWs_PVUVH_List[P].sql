ALTER PROCEDURE dbo.PrWs_PVUVH_List
	 @ExMsg nvarchar(max) = '' OUT
	
	,@BeginDay		datetime = NULL
	,@EndDay		datetime = NULL
	,@BeginHour		int = NULL
	,@EndHour		int = NULL
    ,@AppName		varchar(500) = NULL
	,@PlatForm		varchar(500) = NULL
	,@Model			varchar(500) = NULL
	,@Resolution	varchar(500) = NULL
	,@Area			varchar(500) = NULL
	,@Provider		varchar(500) = NULL
	,@Source		int = NULL
AS
/* =============================================
-- 作者: 黄宗银
-- 日期: 2011-01-07
-- 描述: 列表PV,UV
-- 示例:
DECLARE @rtn int,@ExMsg nvarchar(max)
	,@BeginDay		datetime
	,@EndDay		datetime
	,@BeginHour		int
	,@EndHour		int
    ,@AppName		varchar(500)
	,@PlatForm		varchar(500)
	,@Model			varchar(500)
	,@Resolution	varchar(500)
	,@Area			varchar(500)
	,@Provider		varchar(500)
	,@Source		int
	;
SELECT @BeginDay = '2010-10-01'
	,@EndDay = '2010-10-01'
	,@BeginHour = 0
	,@EndHour = 20
    ,@AppName = NULL
	,@PlatForm = NULL--'''mtk6223'''
	,@Model = '''r53x608jc'''
	,@Resolution = NULL
	,@Area = '''山东'''
	,@Provider = NULL
	,@Source = NULL
EXEC @rtn = dbo.PrWs_PVUVH_List @ExMsg out, @BeginDay,@EndDay,@BeginHour,@EndHour,@AppName,@PlatForm,@Model,@Resolution,@Area,@Provider,@Source
SELECT @rtn,@ExMsg;
-- =============================================
*/
SET NOCOUNT ON;

SELECT @BeginDay = ISNULL(@BeginDay,getdate());
SELECT @EndDay = ISNULL(@EndDay,getdate());
SELECT @BeginHour = ISNULL(@BeginHour,0);
SELECT @EndHour = ISNULL(@EndHour,23);

SELECT @BeginDay = Dateadd(dd,0,datediff(dd,0,@BeginDay));
SELECT @EndDay = Dateadd(dd,0,datediff(dd,0,@EndDay));

DECLARE @sql nvarchar(4000), @cmd nvarchar(4000)
	,@i int, @pB int, @pE int
	,@h_BeginTime datetime
	,@h_EndTime datetime
	
	,@cols_rt nvarchar(2000)
	
	,@cols_l nvarchar(2000)
	,@cd_l nvarchar(2000)
	
	,@jo_uvh nvarchar(2000)
	,@jo_uvhd nvarchar(2000)
	
	,@cd_pv nvarchar(2000)
	,@cd_uvh nvarchar(2000)
	,@cd_uvhd nvarchar(2000)
	;

SELECT @cols_rt = ''

	,@cols_l = ''
	,@cd_l = ''
	
	,@jo_uvh = ''
	,@jo_uvhd = ''
	
	,@cd_pv = ''
	,@cd_uvh = ''
	,@cd_uvhd = ''
	;
	
SELECT @h_BeginTime = dateadd(hh,@BeginHour,@BeginDay)
	,@h_EndTime = dateadd(hh,@EndHour+1,@EndDay)
	;
PRINT Convert(nvarchar(50),@h_BeginTime,120)
PRINT Convert(nvarchar(50),@h_EndTime,120)

SELECT @cd_pv = CASE WHEN @AppName IS NULL THEN '' ELSE ' AND pv.AppName IN (' + @AppName + ')' END
	+ CASE WHEN @PlatForm IS NULL THEN '' ELSE ' AND pv.PlatForm IN (' + @PlatForm + ')' END
	+ CASE WHEN @Model IS NULL THEN '' ELSE ' AND pv.Model IN (' + @Model + ')' END
	+ CASE WHEN @Resolution IS NULL THEN '' ELSE ' AND pv.Resolution IN (' + @Resolution + ')' END
	+ CASE WHEN @Area IS NULL THEN '' ELSE ' AND pv.Area IN (' + @Area + ')' END
	+ CASE WHEN @Provider IS NULL THEN '' ELSE ' AND pv.Provider IN (' + @Provider + ')' END
	+ CASE WHEN @Source IS NULL THEN '' ELSE ' AND pv.Source = ' + Convert(nvarchar(20),@Source) END
	;
SELECT @cd_uvh = CASE WHEN @Model IS NULL THEN ' AND uvh.Model IS NULL' ELSE ' AND uvh.Model IN (' + @Model + ')' END
	+ CASE WHEN @Area IS NULL THEN ' AND uvh.Area IS NULL' ELSE ' AND uvh.Area IN (' + @Area + ')' END
	+ CASE WHEN @Source IS NULL THEN ' AND uvh.Source IS NULL' ELSE ' AND uvh.Source = ' + Convert(nvarchar(20),@Source) END
	;
SELECT @cd_uvhd = CASE WHEN @Model IS NULL THEN ' AND uvhd.Model IS NULL' ELSE ' AND uvhd.Model IN (' + @Model + ')' END
	+ CASE WHEN @Area IS NULL THEN ' AND uvhd.Area IS NULL' ELSE ' AND uvhd.Area IN (' + @Area + ')' END
	+ CASE WHEN @Source IS NULL THEN ' AND uvhd.Source IS NULL' ELSE ' AND uvhd.Source = ' + Convert(nvarchar(20),@Source) END
	;
SELECT @cd_l = REplace(@cd_pv,'pv.','l.');

IF(@AppName IS NULL AND @PlatForm IS NULL AND @Resolution IS NULL AND @Provider IS NULL)
BEGIN
	SELECT @cols_rt = CASE WHEN @Model IS NULL THEN '' ELSE ', Model = ISnull(pv.Model,ISNULL(uvh.Model,uvhd.Model))' END
		+ CASE WHEN @Area IS NULL THEN '' ELSE ', Area = ISnull(pv.Area,ISNULL(uvh.Area,uvhd.Area))' END
		+ CASE WHEN @Source IS NULL THEN '' ELSE ', Source = ISnull(pv.Source,ISNULL(uvh.Source,uvhd.Source))' END;
		
	SELECT @cols_l = CASE WHEN @Model IS NULL THEN '' ELSE ', l.Model' END
		+ CASE WHEN @Area IS NULL THEN '' ELSE ', l.Area' END
		+ CASE WHEN @Source IS NULL THEN '' ELSE ', l.Source' END;
	
	SELECT @jo_uvh = CASE WHEN @Model IS NULL THEN '' ELSE ' AND pv.Model =  uvh.Model' END
		+ CASE WHEN @Area IS NULL THEN '' ELSE ' AND pv.Area =  uvh.Area' END
		+ CASE WHEN @Source IS NULL THEN '' ELSE ' AND pv.Source =  uvh.Source' END;
	
	SELECT @jo_uvhd = CASE WHEN @Model IS NULL THEN '' ELSE ' AND pv.Model =  uvhd.Model' END
		+ CASE WHEN @Area IS NULL THEN '' ELSE ' AND pv.Area =  uvhd.Area' END
		+ CASE WHEN @Source IS NULL THEN '' ELSE ' AND pv.Source =  uvhd.Source' END;
	
	SELECT @sql = '
SELECT PV=ISNULL(pv.PV,0),UV=ISNULL(uvh.UV,0),UVD=ISNULL(uvhd.UV,0), d_h.PK_Hour' + @cols_rt + '
  FROM dbo.DimHour d_h(NOLOCK) LEFT JOIN
	(SELECT PV=ISNULL(Sum(l.PV),0),d_h1.PK_Hour' + @cols_l + '
	   FROM dbo.DimHour d_h1(NOLOCK) LEFT JOIN dbo.PV_M_SG l(NOLOCK) ON d_h1.PK_Hour = l.FK_Hour
	  WHERE d_h1.PK_Hour >= @h_BeginTime AND d_h1.PK_Hour < @h_EndTime AND d_h1.Hour BETWEEN @BeginHour AND @EndHour
		'+ @cd_l + '
	  GROUP BY d_h1.PK_Hour' + @cols_l + '
	) pv ON d_h.PK_Hour = pv.PK_Hour
	LEFT JOIN dbo.UV_Hour_SG uvh(NOLOCK) ON d_h.PK_Hour = uvh.FK_Hour' + @jo_uvh + '
	LEFT JOIN dbo.UVD_Hour_SG uvhd(NOLOCK) ON d_h.PK_Hour = uvhd.FK_Hour' + @jo_uvhd + '
 WHERE d_h.PK_Hour >= @h_BeginTime AND d_h.PK_Hour < @h_EndTime AND d_h.Hour BETWEEN @BeginHour AND @EndHour
	' + @cd_pv + @cd_uvh + @cd_uvhd + '
 ORDER BY d_h.PK_Hour DESC
';
END
ELSE
BEGIN
	SELECT @sql = '
SELECT PV=ISNULL(pv.PV,0),UV=ISNULL(uvh.UV,0),UVD=ISNULL(uvhd.UVD,0), d_h.PK_Hour
  FROM dbo.DimHour d_h(NOLOCK) LEFT JOIN
	(SELECT PV=ISNULL(Sum(l.PV),0),d_h1.PK_Hour
	   FROM dbo.DimHour d_h1(NOLOCK) LEFT JOIN dbo.PV_M_SG l(NOLOCK) ON d_h1.PK_Hour = l.FK_Hour
	  WHERE d_h1.PK_Hour >= @h_BeginTime AND d_h1.PK_Hour < @h_EndTime AND d_h1.Hour BETWEEN @BeginHour AND @EndHour
		' + @cd_l + '
	  GROUP BY d_h1.PK_Hour
	) pv ON d_h.PK_Hour = pv.PK_Hour LEFT JOIN
	(SELECT UV=ISNULL(Count(distinct Imei),0), d_h2.PK_Hour
	   FROM dbo.DimHour d_h2(NOLOCK) LEFT JOIN log.SG l(NOLOCK) ON d_h2.PK_Hour = l.FK_Hour
	  WHERE d_h2.PK_Hour >= @h_BeginTime AND d_h2.PK_Hour < @h_EndTime AND d_h2.Hour BETWEEN @BeginHour AND @EndHour
		AND l.DownLoadPage = 0 AND l.Source>0 AND LEFT(l.ScStatus,1) IN(2,3)' + @cd_l + '
	  GROUP BY d_h2.PK_Hour
	) uvh ON d_h.PK_Hour = uvh.PK_Hour LEFT JOIN
	(SELECT UVD=ISNULL(Count(distinct Imei),0), d_h3.PK_Hour
	   FROM dbo.DimHour d_h3(NOLOCK) LEFT JOIN log.SG l(NOLOCK) ON d_h3.PK_Hour = l.FK_Hour
	  WHERE d_h3.PK_Hour >= @h_BeginTime AND d_h3.PK_Hour < @h_EndTime AND d_h3.Hour BETWEEN @BeginHour AND @EndHour
		AND l.DownLoadPage > 0 AND l.Source>0 AND LEFT(l.ScStatus,1) IN(2,3)' + @cd_l + '
	  GROUP BY d_h3.PK_Hour
	) uvhd ON d_h.PK_Hour = uvhd.PK_Hour
 WHERE d_h.PK_Hour >= @h_BeginTime AND d_h.PK_Hour < @h_EndTime AND d_h.Hour BETWEEN @BeginHour AND @EndHour
 ORDER BY d_h.PK_Hour DESC
';
END

PRINT @sql;
EXEC sp_executesql @sql, N'@h_BeginTime datetime, @h_EndTime datetime, @BeginHour int, @EndHour int'
	,@h_BeginTime = @h_BeginTime, @h_EndTime = @h_EndTime, @BeginHour = @BeginHour, @EndHour = @EndHour;

RETURN 1;
