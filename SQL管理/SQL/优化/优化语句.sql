
1.程序超时设置,一般30秒到2分钟
2.SET ARITHABORT ON -- 溢出或被零除时终止查询,OFF则以NULL为结果继续.
3.合理使用 NOLOCK,READPAST 锁提示.
4.查询开销限制(秒)
	SET QUERY_GOVERNOR_COST_LIMIT 60	-- 1分钟
5.指定语句等待锁释放的毫秒数
	SET LOCK_TIMEOUT -1	-- 默认-1无限等待,可设置为30秒(30000)

事务相关
6.SET CURSOR_CLOSE_ON_COMMIT ON -- 在提交或回滚时关闭所有打开的游标,一般设置为OFF
7.SET XACT_ABORT ON	-- 运行错误时,自动回滚当前事务,当前查询有事务时一般设置为ON

8.SET DATEFIRST 7	-- 星期日为每周的第一天

9.设置死锁优先级
	SET DEADLOCK_PRIORITY @n	-- @n:[-10,10],发生死锁时越小越可能被牺牲{LOW:-5,NORMAL:0,HIGH:5}
	
10.SET FMTONLY ON	-- 只将元数据返回给客户端,不会实际执行查询

11.分析语句功能实现
	SET PARSEONLY ON	-- 只分析不编译
	SET NOEXEC ON	-- 只编译不执行,可用于实现类似"分析"的功能.
	
12.查询统计信息
	SET STATISTICS IO ON	-- 显示有关磁盘活动量的信息
	SET STATISTICS TIME ON	-- 显示分析、编译和执行各语句所需的毫秒数
	

使用上述有关语句可做优化:
SET QUERY_GOVERNOR_COST_LIMIT 30
SET LOCK_TIMEOUT 750
SET DEADLOCK_PRIORITY @n	-- 后台读:-5,后台写:-1
SET XACT_ABORT ON	-- 有事务时使用该语句
