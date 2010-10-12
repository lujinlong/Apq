
/*
CREATE TABLE USERS(
UID INT
,USERNAME VARCHAR(50)
,PARENTID INT
)
INSERT INTO USERS
SELECT 1,'用户A',0 UNION ALL
SELECT 2,'用户B',4 UNION ALL
SELECT 3,'用户C',1 UNION ALL
SELECT 4,'用户D',1 UNION ALL
SELECT 5,'用户E',0 UNION ALL
SELECT 6,'用户F',5 UNION ALL
SELECT 7,'用户G',3 UNION ALL
SELECT 8,'用户H',3
;
*/

DECLARE @Pre0 nvarchar(20) ;
SELECT  @Pre0 = Replicate('0',20) ;

--不使用竖线
WITH MU AS(
SELECT ID0 = CAST('·' AS nvarchar(max)),ID = CONVERT(nvarchar(max),RIGHT(@Pre0 + CAST(UID AS nvarchar(20)),20)),UID,USERNAME,LEVEL = 1
  FROM USERS
 WHERE PARENTID = 0 -- UID = 0
UNION ALL
SELECT REPLICATE('·',LEVEL) + CONVERT(nvarchar(max),'·'),MU.ID + '/' + RIGHT(@Pre0 + CAST(t.UID AS nvarchar(20)),20),t.UID,t.USERNAME,
	MU.LEVEL + 1
 FROM MU INNER JOIN USERS t ON MU.UID = t.PARENTID
)
SELECT UID,ID0 + USERNAME,ID,LEVEL
  FROM MU
 ORDER BY ID;

--使用竖线
WITH MU AS (
SELECT ID0 = CAST('|-·' AS nvarchar(max)),ID = CONVERT(nvarchar(max),RIGHT(@Pre0 + CAST(UID AS nvarchar(20)),20)),UID,USERNAME,LEVEL = 0
  FROM USERS
 WHERE PARENTID = 0
UNION ALL
SELECT '|-' + REPLICATE('--',LEVEL) + CONVERT(nvarchar(max),'|-·'),MU.ID + '/' + RIGHT(@Pre0 + CAST(t.UID AS nvarchar(20)),20),t.UID,t.USERNAME,
	MU.LEVEL + 1
  FROM MU INNER JOIN USERS t ON MU.UID = t.PARENTID
)
SELECT UID,ID0 + USERNAME,ID,LEVEL
  FROM MU
 ORDER BY ID;
