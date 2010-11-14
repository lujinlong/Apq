
/*
DBCC SQLPERF(LOGSPACE)					-- 查看日志文件空间情况(百分比)
DBCC SRHINKFILE('LogFileGroupName')		-- 收缩日志并显示空间情况

-- 查看所有文件空间情况
SELECT DB_NAME() AS DbName,name AS FileName,size/128.0 AS CurrentSizeMB,size/128.0 - CAST(FILEPROPERTY(name, 'SpaceUsed') AS INT)/128.0 AS FreeSpaceMB
  FROM sys.database_files
*/

/*
-- 
EXEC sp_spaceused @updateusage = 'TRUE'
--sp_MSforeachdb	-- 对每个库
--sp_MSforeachtable	-- 对每个表
*/

-- 查看所有表空间使用情况
CREATE TABLE #T(
	name		nvarchar(256),
	rows		varchar(11),
	reserved	varchar(18),
	data		varchar(18),
	index_size	varchar(18),
	unused		varchar(18)
);
INSERT #T(name,rows,reserved,data,index_size,unused)
EXEC sp_MSforeachtable "EXEC sp_spaceused '?',true";
-- 游戏库
SELECT FGameDB.dbo.Apq_Ext_Get('',0,'区服名称'), * FROM #T ORDER BY Convert(int,SubString(reserved,1,Len(reserved)-3)) DESC;
-- 区库
SELECT FGameAreaDB.dbo.Apq_Ext_Get('',0,'区服名称'), * FROM #T ORDER BY Convert(int,SubString(reserved,1,Len(reserved)-3)) DESC;
-- 其余
SELECT * FROM #T ORDER BY Convert(int,SubString(reserved,1,Len(reserved)-3)) DESC;
DROP TABLE #T;
