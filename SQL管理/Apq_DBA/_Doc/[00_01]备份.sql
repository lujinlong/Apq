
1.功能:
	数据库备份:完整备份或日志备份.
	
2.主要流程:
	遍历备份配置表(bak.BakCfg),将应该执行备份的数据库ReadAction设为1(完整备份)或2(日志备份).
	遍历备份配置表(bak.BakCfg)中ReadAction为1或2的行,执行相应备份动作.完成后ReadAction取消相应位.
		完整备份文件删除:根据保存文件数删除历史文件,然后执行完整备份.
	遍历发送配置表(bak.FTP_PutBak),将未发送的备份文件存入FTP发送队列,然后记录最后文件名.
	
3.主要表:
	bak.BakCfg				备份配置
	bak.FTP_PutBak			发送配置
	
4.存储过程:
	bak.Apq_Bak_Full		执行完整备份
	bak.Apq_Bak_Trn			执行日志备份
	bak.Job_Apq_Bak_Init	备份作业调用,确定备份动作并记录在备份配置表
	bak.Job_Apq_Bak			备份作业调用,启动相应备份动作
	bak.Job_Apq_Bak_FTP_Enqueue	备份文件发送作业调用,备份文件存入FTP发送队列
	
5.作业:
	Apq_Bak					备份作业
	Apq_Bak_FTP_Enqueue		备份文件存入发送队列
	
6.配置说明:
	6.1) bak.BakCfg
	
	DBName			数据库名
	FTPFolder		备份文件最终转入该目录以便FTP传送
	BakFolder		备份目录(需要高性能,可为共享目录)
	FTPFolderT		中转文件夹(性能不定,需要与FTPFolder位于同一分区)
	
	FullTime		首次完整备份时间(5分钟的倍数时间点)
	FullCycle		完整备份周期(天)
	TrnCycle		日志备份周期(分钟,5的倍数)
	NeedTruncate	是否需要截断日志	-- 2008以下版本有效,完整备份前执行
	
	NeedRestore		是否需要还原出历史库
	RestoreFolder	还原目录
	DB_HisNum		历史库保留个数
	
	Num_Full		完整备份文件保留个数(同时保留具有基础完整备份文件的日志备份文件)
	
	6.2) bak.FTP_PutBak
	
	DBName			数据库名
	Folder			备份文件目录
	
	FTPSrv			FTP服务器(IP)
	FTPPort			FTP端口
	U				用户名
	P				密码
	FTPFolder		FTP目录
	
	TransferIDCfg	指定传送作业编号(一般设为1)
	
	Num_Full		完整备份文件保留个数(同时保留具有基础完整备份文件的日志备份文件)
