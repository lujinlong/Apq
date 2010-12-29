
1.功能:
	导入配置:
		bcp格式的文本入库(临时表)
	
2.主要流程:
	遍历Etl配置表(etl.EtlCfg),根据文件目录查找是否有新的文件,若有,将待导入文件插入导入队列,然后记录初始化日志.
		导入队列处理作业检查到需要执行导入的行后,根据该行配置执行导入动作,导入成功后删除文件.
	
3.主要表:
	etl.EtlCfg		导入配置
	etl.BcpInQueue	导入队列
	log.BcpInInit	初始化日志
	
4.存储过程:
	etl.Job_Etl_BcpIn_Init	确定需要执行导入的行,初始化导入
	etl.Job_Etl_BcpIn		执行导入
	
5.作业:
	Etl_BcpIn		导入作业
	
6.配置说明:
	6.1) etl.EtlCfg
	
	EtlName						Etl配置名(对某一配置,后续配置表与其一致时可构成完整过程,如无需完整过程,就不能同名)
	Folder						数据文件目录
	PeriodType					时期类型{1:年,2:半年,3:季度,4:月,5:周,6:日,7:时,8:分}
	FileName					文件名(前缀)(格式:FileName[时期][SrvID].txt),时期格式{1:yyyy,2:yyyy01/07,3:yyyy01/04/07/09,4:yyyyMM,5:yyyyww,6:yyyyMMdd,7:yyyyMMdd_hh,8:yyyyMMdd_hhmm}
	DBName, SchemaName, TName	目标表
	t							列分割串
	r							行分割串
