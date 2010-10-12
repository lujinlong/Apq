/****** Object:  Table [dbo].[UserChannelPayLimit]    Script Date: 10/09/2010 10:25:10 ******/
CREATE TABLE [dbo].[UserChannelPayLimit](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[LimitType] [tinyint] NOT NULL,
	[PayWay] [int] NOT NULL,
	[Limit] [money] NOT NULL,
	[_InTime] [datetime] NOT NULL,
	[_Time] [datetime] NOT NULL,
 CONSTRAINT [PK_UserChannelPayLimit_Month] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'限额类型{1:日限,2:月限}' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserChannelPayLimit', @level2type=N'COLUMN',@level2name=N'LimitType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'支付方式:{1:创意2:搜狐}' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserChannelPayLimit', @level2type=N'COLUMN',@level2name=N'PayWay'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'消费限额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserChannelPayLimit', @level2type=N'COLUMN',@level2name=N'Limit'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'行首次写入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserChannelPayLimit', @level2type=N'COLUMN',@level2name=N'_InTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserChannelPayLimit', @level2type=N'COLUMN',@level2name=N'_Time'
GO

ALTER TABLE [dbo].[UserChannelPayLimit] ADD  CONSTRAINT [DF_UserChannelPayLimit_Month_LimitType]  DEFAULT ((1)) FOR [LimitType]
GO

ALTER TABLE [dbo].[UserChannelPayLimit] ADD  CONSTRAINT [DF_UserChannelPayLimit_Month_Limit]  DEFAULT ((0)) FOR [Limit]
GO

ALTER TABLE [dbo].[UserChannelPayLimit] ADD  CONSTRAINT [DF_UserChannelPayLimit_Month__InTime]  DEFAULT (getdate()) FOR [_InTime]
GO

ALTER TABLE [dbo].[UserChannelPayLimit] ADD  CONSTRAINT [DF_UserChannelPayLimit_Month__Time]  DEFAULT (getdate()) FOR [_Time]
GO

/****** Object:  Table [dbo].[UserChannelPay_Month]    Script Date: 10/11/2010 10:36:14 ******/
CREATE TABLE [dbo].[UserChannelPay_Month](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[PayWay] [int] NOT NULL,
	[uid] [nvarchar](50) NOT NULL,
	[PayMonth] [money] NOT NULL,
	[_Time] [datetime] NOT NULL,
	[Imei] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_UserChannelPay_Month] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE NONCLUSTERED INDEX [IX_UserChannelPay_Month:uid] ON [dbo].[UserChannelPay_Month] 
(
	[uid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'支付方式:{1:创意2:搜狐}' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserChannelPay_Month', @level2type=N'COLUMN',@level2name=N'PayWay'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'uid' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserChannelPay_Month', @level2type=N'COLUMN',@level2name=N'uid'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'今日消费总额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserChannelPay_Month', @level2type=N'COLUMN',@level2name=N'PayMonth'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserChannelPay_Month', @level2type=N'COLUMN',@level2name=N'_Time'
GO

ALTER TABLE [dbo].[UserChannelPay_Month] ADD  CONSTRAINT [DF_UserChannelPay_Month_PayToday]  DEFAULT ((0)) FOR [PayMonth]
GO

ALTER TABLE [dbo].[UserChannelPay_Month] ADD  CONSTRAINT [DF_UserChannelPay_Month__Time]  DEFAULT (getdate()) FOR [_Time]
GO

ALTER TABLE [dbo].[UserChannelPay_Month] ADD  CONSTRAINT [DF_UserChannelPay_Month_Imei]  DEFAULT ('') FOR [Imei]
GO

/****** Object:  Table [dbo].[WangYouDaiShouMonitor]    Script Date: 10/11/2010 10:37:36 ******/
CREATE TABLE [dbo].[WangYouDaiShouMonitor](
	[pkid] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NULL,
	[interval] [int] NULL,
	[newdatacount] [int] NULL,
	[newdatatime] [int] NULL,
	[faildcount] [int] NULL,
	[faildtime] [int] NULL,
	[url] [varchar](250) NULL,
	[timeout] [int] NULL,
	[warnflg] [tinyint] NULL,
	[phonelist] [varchar](100) NULL,
 CONSTRAINT [PK_WangYouDaiShouMonitor] PRIMARY KEY CLUSTERED 
(
	[pkid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
