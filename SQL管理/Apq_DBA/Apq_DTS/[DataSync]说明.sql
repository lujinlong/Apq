一.摘要
1.功能:
	数据同步
	
2.基本原理:
	将要同步的数据以字符串的形式放入队列,然后由作业轮询,按时间等条件出队,通过远程存储过程将数据同步至目标数据库.
	同时已处理以下方面:
	1.远程服务器网络连续断开一定次数后不再往该服务器发送数据.
	2.简单模拟Server Broker队列,对同一数据类型,同一数据所有者(DataOwner+DataKey)的数据按入队顺序出队.
	
3.主要表:
	DataSyncQueue			队列
	DataSyncQueue_His		队列历史
	DataSyncQueue_DataType	数据类型列表
	RDBConfig	远程数据库列表
	RSrvConfig	远程服务器列表
	
4.存储过程:
	Apq_DataSync_Enqueue	入队
	Apq_DataSync			同步(作业调用)
	
5.作业:
	命名:[Apq_DataSync]数据库名
	如,[Apq_DataSync]Passport
	暂为2分钟运行一次,运行一次即为一次批量出队(1000)
	
6.配置:
	对准备同步的数据,整理出以下两项,每个项目皆可有多项:
	1.目标服务器别名
	2.目标数据库名
	然后确定一个目标数据库用于接收数据的存储过程名,所有目标库须统一(格式:架构.SPName,参数列表:DataOwner,DataKey,OldValue,NewValue).
	
	1.添加别名[RSrvConfig]
		将RSrvConfig中不存在的别名添加到该表,对同一别名SrvID须全局一致(保证任意两数据库迁移至同一服务器时不冲突)
	2.添加远程数据库[RDBConfig]
		将RDBConfig中不存在的远程数据库添加到该表,每个远程数据库一行,注意RDBType(不能为0),GameID的值,远程数据库编号应该全局唯一,可采用分级分段分配
   *3.定义数据类型[DataSyncQueue_DataType][重点]
		数据类型决定了数据如何分发,一行表示一种分发方式,一个数据类型可存在多行,表示该类型数据具有多种分发方式
		重要列:RDBType,0表示此行代表的分发方式为只发送到RDBID表示的单个数据库,>0则表示发送至RDBConfig表该类型的所有库(但限于该玩家登录过的游戏)
		存储过程名也记录于该表.
		
7.入队:
	配置完成后,只需要将准备同步的数据传入入队存储过程(Apq_DataSync_Enqueue)即可.
	
二.基本意外处理
1.网络断开
	[RSrvConfig]连续断开达到指定次数后,该目标服务器的LSState被自动置为0,只需要恢复网络后将该值改为1即可.
