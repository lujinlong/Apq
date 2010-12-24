
1.功能:
	数据库还原:完整/日志
		用于备用库或历史数据库
	
2.主要流程:
	遍历还原配置表(bak.RestoreFromFolder),根据文件目录查找是否有新的备份文件,若有,根据文件类型和还原类型执行相应还原操作.还原完成后记录最后文件名.
		过期的历史库和备份文件处理:完整还原后删除过期历史库和过期备份文件.
		当还原类型(RestoreType)为0时,表示不还原数据库,仅删除过期备份文件;备份机仅用于保存备份文件时指定0以自动删除过期备份文件.
	
3.主要表:
	bak.RestoreFromFolder	还原配置
	
4.存储过程:
	bak.Apq_Restore					执行还原操作
	bak.Job_Apq_RestoreFromFolder	遍历还原配置表,启动还原
	
5.作业:
	Apq_Bak					备份作业
	Apq_Bak_FTP_Enqueue		备份文件存入发送队列
	
6.配置说明:
	6.1) bak.RestoreFromFolder
	
	DBName			数据库名
	RestoreType		还原类型{0x1:历史,0x2:备用}
	BakFolder		备份文件目录
	RestoreFolder	还原目录
	
	RunnerIDCfg		指定还原作业编号(一般设为1)
	
	DB_HisNum		历史库保留个数
	Num_Full		完整备份文件保留个数(同时保留具有基础完整备份文件的日志备份文件)
