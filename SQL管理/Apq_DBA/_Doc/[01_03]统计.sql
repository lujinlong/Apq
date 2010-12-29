
1.功能:
	执行统计
	
2.主要流程:
	遍历统计配置表(etl.StatCfg),对统计时间已到的行,检查数据到位情况(Detect),成功后执行统计代码(STMT),统计成功记录统计日志,更新统计配置表的最后执行时间.
	
3.主要表:
	etl.StatCfg		统计配置
	etl.Log_Stat	统计日志
	
4.存储过程:
	etl.Job_Etl_Stat	统计作业
	
5.作业:
	Etl_Stat		统计作业
	
6.配置说明:
	6.1) etl.StatCfg
	
	StatName					配置名(可与EtlName对应查看)
	Detect						检测数据到位情况,通过时返回1(参数:@StatName,@StartTime,@EndTime)
	STMT						统计存储过程或统计语句(参数:@StartTime,@EndTime)
	FirstStartTime				统计参数:开始时间初始值
	
	Cycle						统计周期(分钟)
	RTime						首次统计执行时间
