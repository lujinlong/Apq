
/*
EXEC sp_configure 'default trace enabled',1
RECONFIGURE;
*/

SELECT * 
FROM fn_trace_gettable
('D:\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\LOG\log.trc', default)
