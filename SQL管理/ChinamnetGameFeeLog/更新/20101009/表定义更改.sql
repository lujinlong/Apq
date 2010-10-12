ALTER TABLE dbo.T_Chuangyidaishou ADD Imei nvarchar(50);
ALTER TABLE dbo.T_Chuangyidaishou_history ADD Imei nvarchar(50);
ALTER TABLE dbo.T_Sohudaishou ADD Imei nvarchar(50);
ALTER TABLE dbo.T_Sohudaishou_history ADD Imei nvarchar(50);
ALTER TABLE dbo.UserChannelPay_Today ADD Imei nvarchar(50);

ALTER TABLE dbo.T_Chuangyidaishou ADD ipaddress nvarchar(30);
ALTER TABLE dbo.T_Chuangyidaishou_history ADD ipaddress nvarchar(30);
