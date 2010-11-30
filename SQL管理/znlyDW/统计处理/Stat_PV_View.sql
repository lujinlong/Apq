
/*
TRUNCATE TABLE dbo.PV_Imei_LogType;
TRUNCATE TABLE dbo.PV_Imei;
*/

EXEC sp_spaceused
EXEC sp_helpfile

EXEC sp_updatestats

--11569506
SELECT COUNT(*) FROM dbo.PV_Imei_LogType(NOLOCK)
SELECT COUNT(*) FROM dbo.PV_Imei_LogType(NOLOCK) WHERE VisitCountTotal_Time = '20101101'
SELECT COUNT(*) FROM dbo.PV_Imei_LogType(NOLOCK) WHERE _LastImeiLogIDTemp_Time = '20101101'
SELECT COUNT(*) FROM dbo.PV_Imei_LogType(NOLOCK) WHERE _LastImeiLogID_Time = '20101101'

SELECT COUNT(*) FROM dbo.PV_Imei(NOLOCK)
SELECT COUNT(*) FROM dbo.PV_Imei(NOLOCK) WHERE VisitCount_Time = '20101101'
SELECT COUNT(*) FROM dbo.PV_Imei(NOLOCK) WHERE _LastPVTimeTemp_Time = '20101101'
SELECT COUNT(*) FROM dbo.PV_Imei(NOLOCK) WHERE _LastPVTime_Time = '20101101'
