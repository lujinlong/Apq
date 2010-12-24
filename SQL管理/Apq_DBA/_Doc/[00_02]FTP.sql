
1.功能:
	通过ncftp上传文件(未支持断点续传)
	
2.主要流程:
	遍历上传队列表(dbo.FTP_SendQueue),尝试上传所有未成功的行,上传过程中文件名以加前缀"_up"方式标识,上传成功后去掉该前缀.
		上传成功后,将该行移到历史表中(log.FTP_SendQueue).
	判断上传是否成功:上传完成后立即获取远程文件大小,与本地文件文件大小比较,相同则上传成功.
	
3.主要表:
	dbo.FTP_SendQueue		上传队列
	log.FTP_SendQueue		历史记录
	
4.存储过程:
	dbo.Job_FTP_Send		执行上传
	
5.作业:
	Apq_FTP_Send			FTP上传
	
6.队列说明:
	6.1) dbo.FTP_SendQueue
	
	Folder			本地文件目录
	FileName		文件名
	
	FTPSrv			FTP服务器(IP)
	FTPPort			FTP端口
	U				用户名
	P				密码
	FTPFolder		FTP目录
