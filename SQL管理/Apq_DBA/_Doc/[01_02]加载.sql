
1.功能:
	数据从临时表加载到目标表
	
2.主要流程:
	遍历加载配置表(etl.LoadCfg),对周期已到的配置,执行加载动作.
		加载方法:目标表去掉索引,以每次转入1万行循环直到全部转入目标表,然后目标表恢复原有索引.
	
3.主要表:
	etl.LoadCfg		加载配置
	
4.存储过程:
	etl.Job_Etl_Load	确定需要执行加载的行,启动加载过程
	etl.Etl_Load		执行加载
	
5.作业:
	Etl_Load		加载作业
	
6.配置说明:
	6.1) etl.LoadCfg
	
	ID, EtlName, SrcFullTableName, DstFullTableName, Enabled, Cycle, LTime, PreLTime, _InTime, _Time
	EtlName				Etl配置名
	SrcFullTableName	临时表全名
	DstFullTableName	目标表全名
	Cycle				加载周期(分钟)
	LTime				首次加载时间	-- 可利用该值提前设置
