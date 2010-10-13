
-- 创建表 ------------------------------------------------------------------------------------------
CREATE TABLE [dbo].[DimDate](
	[PK_日期] [datetime] NOT NULL,
	[日期_名称] [nvarchar](50) NULL,
	[年] [datetime] NULL,
	[年_名称] [nvarchar](50) NULL,
	[季度] [datetime] NULL,
	[季度_名称] [nvarchar](50) NULL,
	[月份] [datetime] NULL,
	[月份_名称] [nvarchar](50) NULL,
	[周] [datetime] NULL,
	[周_名称] [nvarchar](50) NULL,
	[每年的某一日] [int] NULL,
	[每年的某一日_名称] [nvarchar](50) NULL,
	[每个季度的某一日] [int] NULL,
	[每个季度的某一日_名称] [nvarchar](50) NULL,
	[每月的某一日] [int] NULL,
	[每月的某一日_名称] [nvarchar](50) NULL,
	[每周的某一日] [int] NULL,
	[每周的某一日_名称] [nvarchar](50) NULL,
	[每年的某一周] [int] NULL,
	[每年的某一周_名称] [nvarchar](50) NULL,
	[每年的某一月] [int] NULL,
	[每年的某一月_名称] [nvarchar](50) NULL,
	[每个季度的某一月] [int] NULL,
	[每个季度的某一月_名称] [nvarchar](50) NULL,
	[每年的某一季度] [int] NULL,
	[每年的某一季度_名称] [nvarchar](50) NULL,
 CONSTRAINT [PK_DimDate] PRIMARY KEY CLUSTERED 
(
	[PK_日期] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

EXEC sys.sp_addextendedproperty @name=N'AllowGen', @value=N'True' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DimDate', @level2type=N'COLUMN',@level2name=N'PK_日期'
GO

EXEC sys.sp_addextendedproperty @name=N'DSVColumn', @value=N'日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DimDate', @level2type=N'COLUMN',@level2name=N'PK_日期'
GO

EXEC sys.sp_addextendedproperty @name=N'AllowGen', @value=N'True' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DimDate', @level2type=N'COLUMN',@level2name=N'日期_名称'
GO

EXEC sys.sp_addextendedproperty @name=N'DSVColumn', @value=N'日期_名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DimDate', @level2type=N'COLUMN',@level2name=N'日期_名称'
GO

EXEC sys.sp_addextendedproperty @name=N'AllowGen', @value=N'True' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DimDate', @level2type=N'COLUMN',@level2name=N'年'
GO

EXEC sys.sp_addextendedproperty @name=N'DSVColumn', @value=N'年' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DimDate', @level2type=N'COLUMN',@level2name=N'年'
GO

EXEC sys.sp_addextendedproperty @name=N'AllowGen', @value=N'True' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DimDate', @level2type=N'COLUMN',@level2name=N'年_名称'
GO

EXEC sys.sp_addextendedproperty @name=N'DSVColumn', @value=N'年_名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DimDate', @level2type=N'COLUMN',@level2name=N'年_名称'
GO

EXEC sys.sp_addextendedproperty @name=N'AllowGen', @value=N'True' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DimDate', @level2type=N'COLUMN',@level2name=N'季度'
GO

EXEC sys.sp_addextendedproperty @name=N'DSVColumn', @value=N'季度' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DimDate', @level2type=N'COLUMN',@level2name=N'季度'
GO

EXEC sys.sp_addextendedproperty @name=N'AllowGen', @value=N'True' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DimDate', @level2type=N'COLUMN',@level2name=N'季度_名称'
GO

EXEC sys.sp_addextendedproperty @name=N'DSVColumn', @value=N'季度_名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DimDate', @level2type=N'COLUMN',@level2name=N'季度_名称'
GO

EXEC sys.sp_addextendedproperty @name=N'AllowGen', @value=N'True' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DimDate', @level2type=N'COLUMN',@level2name=N'月份'
GO

EXEC sys.sp_addextendedproperty @name=N'DSVColumn', @value=N'月份' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DimDate', @level2type=N'COLUMN',@level2name=N'月份'
GO

EXEC sys.sp_addextendedproperty @name=N'AllowGen', @value=N'True' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DimDate', @level2type=N'COLUMN',@level2name=N'月份_名称'
GO

EXEC sys.sp_addextendedproperty @name=N'DSVColumn', @value=N'月份_名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DimDate', @level2type=N'COLUMN',@level2name=N'月份_名称'
GO

EXEC sys.sp_addextendedproperty @name=N'AllowGen', @value=N'True' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DimDate', @level2type=N'COLUMN',@level2name=N'周'
GO

EXEC sys.sp_addextendedproperty @name=N'DSVColumn', @value=N'周' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DimDate', @level2type=N'COLUMN',@level2name=N'周'
GO

EXEC sys.sp_addextendedproperty @name=N'AllowGen', @value=N'True' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DimDate', @level2type=N'COLUMN',@level2name=N'周_名称'
GO

EXEC sys.sp_addextendedproperty @name=N'DSVColumn', @value=N'周_名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DimDate', @level2type=N'COLUMN',@level2name=N'周_名称'
GO

EXEC sys.sp_addextendedproperty @name=N'AllowGen', @value=N'True' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DimDate', @level2type=N'COLUMN',@level2name=N'每年的某一日'
GO

EXEC sys.sp_addextendedproperty @name=N'DSVColumn', @value=N'每年的某一日' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DimDate', @level2type=N'COLUMN',@level2name=N'每年的某一日'
GO

EXEC sys.sp_addextendedproperty @name=N'AllowGen', @value=N'True' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DimDate', @level2type=N'COLUMN',@level2name=N'每年的某一日_名称'
GO

EXEC sys.sp_addextendedproperty @name=N'DSVColumn', @value=N'每年的某一日_名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DimDate', @level2type=N'COLUMN',@level2name=N'每年的某一日_名称'
GO

EXEC sys.sp_addextendedproperty @name=N'AllowGen', @value=N'True' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DimDate', @level2type=N'COLUMN',@level2name=N'每个季度的某一日'
GO

EXEC sys.sp_addextendedproperty @name=N'DSVColumn', @value=N'每个季度的某一日' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DimDate', @level2type=N'COLUMN',@level2name=N'每个季度的某一日'
GO

EXEC sys.sp_addextendedproperty @name=N'AllowGen', @value=N'True' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DimDate', @level2type=N'COLUMN',@level2name=N'每个季度的某一日_名称'
GO

EXEC sys.sp_addextendedproperty @name=N'DSVColumn', @value=N'每个季度的某一日_名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DimDate', @level2type=N'COLUMN',@level2name=N'每个季度的某一日_名称'
GO

EXEC sys.sp_addextendedproperty @name=N'AllowGen', @value=N'True' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DimDate', @level2type=N'COLUMN',@level2name=N'每月的某一日'
GO

EXEC sys.sp_addextendedproperty @name=N'DSVColumn', @value=N'每月的某一日' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DimDate', @level2type=N'COLUMN',@level2name=N'每月的某一日'
GO

EXEC sys.sp_addextendedproperty @name=N'AllowGen', @value=N'True' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DimDate', @level2type=N'COLUMN',@level2name=N'每月的某一日_名称'
GO

EXEC sys.sp_addextendedproperty @name=N'DSVColumn', @value=N'每月的某一日_名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DimDate', @level2type=N'COLUMN',@level2name=N'每月的某一日_名称'
GO

EXEC sys.sp_addextendedproperty @name=N'AllowGen', @value=N'True' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DimDate', @level2type=N'COLUMN',@level2name=N'每周的某一日'
GO

EXEC sys.sp_addextendedproperty @name=N'DSVColumn', @value=N'每周的某一日' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DimDate', @level2type=N'COLUMN',@level2name=N'每周的某一日'
GO

EXEC sys.sp_addextendedproperty @name=N'AllowGen', @value=N'True' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DimDate', @level2type=N'COLUMN',@level2name=N'每周的某一日_名称'
GO

EXEC sys.sp_addextendedproperty @name=N'DSVColumn', @value=N'每周的某一日_名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DimDate', @level2type=N'COLUMN',@level2name=N'每周的某一日_名称'
GO

EXEC sys.sp_addextendedproperty @name=N'AllowGen', @value=N'True' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DimDate', @level2type=N'COLUMN',@level2name=N'每年的某一周'
GO

EXEC sys.sp_addextendedproperty @name=N'DSVColumn', @value=N'每年的某一周' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DimDate', @level2type=N'COLUMN',@level2name=N'每年的某一周'
GO

EXEC sys.sp_addextendedproperty @name=N'AllowGen', @value=N'True' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DimDate', @level2type=N'COLUMN',@level2name=N'每年的某一周_名称'
GO

EXEC sys.sp_addextendedproperty @name=N'DSVColumn', @value=N'每年的某一周_名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DimDate', @level2type=N'COLUMN',@level2name=N'每年的某一周_名称'
GO

EXEC sys.sp_addextendedproperty @name=N'AllowGen', @value=N'True' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DimDate', @level2type=N'COLUMN',@level2name=N'每年的某一月'
GO

EXEC sys.sp_addextendedproperty @name=N'DSVColumn', @value=N'每年的某一月' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DimDate', @level2type=N'COLUMN',@level2name=N'每年的某一月'
GO

EXEC sys.sp_addextendedproperty @name=N'AllowGen', @value=N'True' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DimDate', @level2type=N'COLUMN',@level2name=N'每年的某一月_名称'
GO

EXEC sys.sp_addextendedproperty @name=N'DSVColumn', @value=N'每年的某一月_名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DimDate', @level2type=N'COLUMN',@level2name=N'每年的某一月_名称'
GO

EXEC sys.sp_addextendedproperty @name=N'AllowGen', @value=N'True' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DimDate', @level2type=N'COLUMN',@level2name=N'每个季度的某一月'
GO

EXEC sys.sp_addextendedproperty @name=N'DSVColumn', @value=N'每个季度的某一月' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DimDate', @level2type=N'COLUMN',@level2name=N'每个季度的某一月'
GO

EXEC sys.sp_addextendedproperty @name=N'AllowGen', @value=N'True' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DimDate', @level2type=N'COLUMN',@level2name=N'每个季度的某一月_名称'
GO

EXEC sys.sp_addextendedproperty @name=N'DSVColumn', @value=N'每个季度的某一月_名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DimDate', @level2type=N'COLUMN',@level2name=N'每个季度的某一月_名称'
GO

EXEC sys.sp_addextendedproperty @name=N'AllowGen', @value=N'True' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DimDate', @level2type=N'COLUMN',@level2name=N'每年的某一季度'
GO

EXEC sys.sp_addextendedproperty @name=N'DSVColumn', @value=N'每年的某一季度' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DimDate', @level2type=N'COLUMN',@level2name=N'每年的某一季度'
GO

EXEC sys.sp_addextendedproperty @name=N'AllowGen', @value=N'True' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DimDate', @level2type=N'COLUMN',@level2name=N'每年的某一季度_名称'
GO

EXEC sys.sp_addextendedproperty @name=N'DSVColumn', @value=N'每年的某一季度_名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DimDate', @level2type=N'COLUMN',@level2name=N'每年的某一季度_名称'
GO

EXEC sys.sp_addextendedproperty @name=N'AllowGen', @value=N'True' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DimDate'
GO

EXEC sys.sp_addextendedproperty @name=N'DSVTable', @value=N'DimDate' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DimDate'
GO

EXEC sys.sp_addextendedproperty @name=N'Project', @value=N'c8479882-99c6-4a28-8903-36c4d511c8c7' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DimDate'
GO

EXEC sys.sp_addextendedproperty @name=N'AllowGen', @value=N'True' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DimDate', @level2type=N'CONSTRAINT',@level2name=N'PK_DimDate'
GO
-- =================================================================================================

-- 添加数据 ----------------------------------------------------------------------------------------
DECLARE @PK datetime, @PKEnd datetime
	,@yyyy int, @MM int, @dd int,@weekday int, @week int, @Quarter int, @dy int
	,@年 datetime, @季度 datetime, @月份 datetime, @周 datetime;

SELECT @PK = '2007-01-01',@PKEnd = '2051-01-01';

DECLARE @p datetime;
SELECT @p = @PK;
WHILE(@p <= @PKEnd)
BEGIN
	IF(NOT EXISTS(SELECT TOP 1 1 FROM dbo.DimDate WHERE PK_日期 = @p))
	BEGIN
		SELECT @yyyy = year(@p),@MM = month(@p),@dd = day(@p), @weekday = datepart(dw,@p), @week = datepart(ww,@p), @Quarter = datepart(qq,@p), @dy = datepart(dy,@p);
		SELECT @年 = Convert(nvarchar(50),@yyyy) + '-01-01',
			@季度 = Convert(nvarchar(50),@yyyy) + '-' + Convert(nvarchar(50),@Quarter * 3 -2) + '-01',
			@月份 = Convert(nvarchar(50),@yyyy) + '-' + Convert(nvarchar(50),@MM) + '-01',
			@周 = Dateadd(dd,1-@weekday,@p);
		
		INSERT dbo.DimDate (PK_日期, 日期_名称, 
			年, 年_名称, 
			月份, 月份_名称, 
			季度, 季度_名称, 
			周, 周_名称, 
			每月的某一日, 每月的某一日_名称,
			每年的某一日, 每年的某一日_名称, 
			每个季度的某一日, 每个季度的某一日_名称, 
			每周的某一日, 每周的某一日_名称, 
			每年的某一周, 每年的某一周_名称, 
			每年的某一月, 每年的某一月_名称, 
			每个季度的某一月, 每个季度的某一月_名称, 
			每年的某一季度, 每年的某一季度_名称)
		VALUES  (@p,Convert(nvarchar(50),@yyyy) + '年' + Convert(nvarchar(50),@mm)+ '月'+Convert(nvarchar(50),@dd)+'日',
			@年,Convert(nvarchar(50),@yyyy) + '年',
			@月份,Convert(nvarchar(50),@yyyy) + '年' + Convert(nvarchar(50),@mm)+ '月',
			@季度,Convert(nvarchar(50),@yyyy) + '年' + Convert(nvarchar(50),@Quarter)+ '季度',
			@周,Convert(nvarchar(50),@yyyy) + '年' + Convert(nvarchar(50),@week)+ '周',
			@dd,'第'+Convert(nvarchar(50),@dd)+ '天',
			@dy,'第'+Convert(nvarchar(50),@dy)+ '天',
			Datediff(dd,@季度,@p)+1,'第'+Convert(nvarchar(50),Datediff(dd,@季度,@p)+1)+ '天',
			@weekday,'第'+Convert(nvarchar(50),@weekday)+ '天',
			@week,'第'+Convert(nvarchar(50),@week)+ '周',
			@mm,'第'+Convert(nvarchar(50),@mm)+ '月',
			Datediff(mm,@季度,@月份)+1,'第'+Convert(nvarchar(50),Datediff(mm,@季度,@月份)+1)+ '月',
			@Quarter,'第'+Convert(nvarchar(50),@Quarter)+ '季度'
			);
	END
	
	SELECT @p = Dateadd(dd,1,@p);
END
-- =================================================================================================
