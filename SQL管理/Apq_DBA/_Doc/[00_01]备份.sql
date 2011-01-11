
1.功能:

	数据库备份:完整备份或日志备份.
	
2.主要流程:

	遍历当前数据库(除model,tempdb以外的所有数据库,并且:联机状态,非备用库,非历史库)
		在备份配置表(bak.BakCfg)查找对应配置(或默认配置,即ID=0)
			找到则以该配置入队(备份队列)
			否则,以程序默认值入队(目录D:\DBA\Bak2FTP\数据库名,一天完备一次,一小时日志备份一次)
			
	遍历备份队列表(bak.BakQueue)中未成功的备份请求,以队列中的配置启动备份操作并记录备份操作日志(BakQueue).
		完整备份文件删除:根据保存文件数删除历史文件,然后执行完整备份,先删除后备份.
		
	遍历发送配置表(bak.FTP_PutBak),检查备份目录,将未发送的备份文件存入FTP发送队列,然后记录最后文件名.
	
	★注意事项:
		为了保证备份文件的唯一连续性,请不要使用Management提供的界面手动执行备份操作,但可以使用SQL语句强制往队列中插入记录实现需要的临时备份.
			INSERT bak.BakQueue(DBName, BakFolder, FTPFolder, NeedTruncate, BakType, NeedRestore, RestoreFolder, DB_HisNum, Num_Full)
			VALUES(@DBName, @BakFolder, @FTPFolder, @NeedTruncate, 1, @NeedRestore, @RestoreFolder, @DB_HisNum, @Num_Full)
		但此操作可能会使在备份队列中同一数据库出现两条(或更多)记录,因此不推荐.
		
		由于过程中需要从备份队列(bak.BakQueue)中读取最后备份时间等信息,因此备份队列请不要完全清空,但可以仅保留最近一段时间的数据(如一周或一个月等,可根据当前数据库中的最大完整备份周期制订该表的清理计划).
		
	
3.主要表:

	bak.BakCfg				备份配置
	bak.BakQueue			备份队列
	log.Bak					备份操作日志
	
	bak.FTP_PutBak			发送配置
	
4.存储过程:

	bak.Job_Apq_Bak_Init_R1	备份作业调用,确定备份动作并记录在备份配置表
	bak.Job_Apq_Bak_R1		备份作业调用,启动相应备份动作
	bak.Apq_Bak_R1			执行备份
	
	bak.Job_Apq_Bak_FTP_Enqueue	备份文件发送作业调用,备份文件存入FTP发送队列
	
5.作业:

	Apq_Bak_R1				备份作业
	Apq_Bak_FTP_Enqueue		备份文件存入发送队列
	
6.配置说明:

	6.1) bak.BakCfg (ID=0的行表示默认配置,必须使用SQL打开标识插入,然后使用INSERT语句添加)
	
	DBName			数据库名
	FTPFolder		备份文件最终转入该目录以便FTP传送
	BakFolder		备份目录(需要高性能,可为共享目录)
	
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
