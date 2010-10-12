
-- 用户映射 ----------------------------------------------------------------------------------------
IF(user_id('Web') IS NULL) CREATE USER Web FOR LOGIN Web;
ALTER USER Web WITH LOGIN = Web

IF(user_id('Web_Bg') IS NULL) CREATE USER Web_Bg FOR LOGIN Web_Bg;
ALTER USER Web_Bg WITH LOGIN = Web_Bg
