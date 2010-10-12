
-- 查询执行计划
SELECT plan_handle, st.text
  FROM sys.dm_exec_cached_plans 
	CROSS APPLY sys.dm_exec_sql_text(plan_handle) AS st
 WHERE text LIKE N'SELECT * FROM Person.Address%';


DBCC FREEPROCCACHE([plan_handle]/'default');	-- 指定(或所有)存储过程重新编译,重新生成执行计划
DBCC DROPCLEANBUFFERS;		-- 清空所有缓存(写入硬盘)
DBCC FREESYSTEMCACHE('ALL')	-- 清空系统缓存(存储过程&批处理)
