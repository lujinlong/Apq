
DECLARE @db_id int, @tbl_id int
SET @db_id = DB_ID('Apq')
SET @tbl_id = OBJECT_ID('Apq..Apq_Ext')
DBCC PINTABLE (@db_id, @tbl_id)

SELECT OBJECTPROPERTY(OBJECT_ID('Apq..Apq_Ext'),'TableIsPinned')
