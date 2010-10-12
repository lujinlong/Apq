-- ≈‰÷√ -------------------------------------------------------------------------------------------
DECLARE @AreaID int, @ServerID int, @Today datetime, @TransTime datetime;
SELECT @AreaID = GameDB.dbo.Apq_Ext_Get('',0,'AreaID'),@ServerID = GameDB.dbo.Apq_Ext_Get('',0,'ServerID');
SELECT @Today = DateAdd(dd,0,DateDiff(dd,0,getdate()));
SELECT @TransTime = DateAdd(ss,(@AreaID*100 + @ServerID)* 30,DateAdd(hh,1,@Today))
SELECT @AreaID, @ServerID, @Today, @TransTime;

INSERT Apq_DBA..DTSConfig(TransName,Enabled,TransMethod,STMT,TransCycle,TransTime,SrvName,DBName,SchemaName,SPTName,U,P,LastID,LastTime,STMTMax)
SELECT 'GameActor', 1, 1
	,'SELECT UserID,ActorID,ActorName,' + Convert(nvarchar,@AreaID) + ',' + Convert(nvarchar,@ServerID)
	+',Occupation,ActorLevel,Expvalue,HoldMoney,WareMoney,WriteTime,HasWarePwd=CASE WHEN WarePassword<>'''' THEN 1 Else 0 END'
	+',ActorSex,LoginTime,SMLevel=GameDB.dbo.Apq_RConvertVarBinary8X_BigInt(SubString(Buffer1,31,4))'
 	+' FROM GameDB.dbo.GameActor(NOLOCK)'
	+' WHERE ActorID > 1000 AND ActorID > ^LastID$ AND ActorID <= ^MaxID$ OR LoginTime > ^LastTime$ AND LoginTime <= ^MaxTime$'
	,1,@TransTime,'HX2StatDbCenter','Stat_Hx2','bcp','GameActor_Bcp','BcpUser','5se7TYGsdu#@4dzx',0,'19900101'
	,'SELECT @MaxID = ISNULL(Max(ActorID),0), @MaxTime = ISNULL(Max(LoginTime),''20081218'') FROM GameDB.dbo.GameActor(NOLOCK);'
UNION SELECT 'GameActor_DelBak', 1, 1
	,'SELECT ActorID,' + Convert(nvarchar,@AreaID) + ',' + Convert(nvarchar,@ServerID)
 	+' FROM GameDB.dbo.GameActor_DelBak(NOLOCK)'
	+' WHERE ActorID > 1000 AND DelTime BETWEEN ^LastTime$ AND ^MaxTime$'
	,1,@TransTime,'HX2StatDbCenter','Stat_Hx2','bcp','GameActor_DelBak_Bcp','BcpUser','5se7TYGsdu#@4dzx',0,'19900101'
	,'SELECT @MaxTime = ISNULL(Max(DelTime),''20081218'') FROM GameDB.dbo.GameActor_DelBak(NOLOCK);'
-- ================================================================================================
