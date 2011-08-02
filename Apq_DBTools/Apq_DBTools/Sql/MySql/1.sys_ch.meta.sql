
/* -------------------------------------------------------------------------------------------------
该文件由Apq_DBTools自动生成

数据库创建
dbv表创建
元数据导入
基本存储过程（根据元数据无损构建数据库）

====================================================================================================
*/
CREATE DATABASE IF NOT EXISTS `remp_sys_ch` DEFAULT CHARACTER SET utf8;
USE `remp_sys_ch`;
SET FOREIGN_KEY_CHECKS=0;
-- 元数据表 -----------------------------------------------------------------------------------------
DROP TABLE IF EXISTS `dbv_table`;

CREATE TABLE `dbv_table` (
  `TID` INT NOT NULL AUTO_INCREMENT,
  `SchemaName` VARCHAR(192) DEFAULT NULL,
  `TableName` VARCHAR(192) DEFAULT NULL,
  `ENGINE` VARCHAR(192) DEFAULT NULL,
  `CREATE_OPTIONS` VARCHAR(765) DEFAULT NULL,
  `TABLE_COMMENT` VARCHAR(240) DEFAULT NULL,
  `PrimaryKeys` TEXT,
  PRIMARY KEY (`TID`)
) ENGINE=INNODB DEFAULT CHARSET=utf8;

DROP TABLE IF EXISTS `dbv_column`;

CREATE TABLE `dbv_column` (
  `CID` INT NOT NULL AUTO_INCREMENT,
  `TID` INT DEFAULT NULL,
  `ColName` VARCHAR(192) DEFAULT NULL,
  `DefaultValue` LONGTEXT,
  `NullAble` TINYINT DEFAULT NULL,
  `DATA_TYPE` VARCHAR(192) DEFAULT NULL,
  `CHARACTER_MAXIMUM_LENGTH` BIGINT DEFAULT NULL,
  `CHARACTER_OCTET_LENGTH` BIGINT DEFAULT NULL,
  `NUMERIC_PRECISION` BIGINT DEFAULT NULL,
  `NUMERIC_SCALE` BIGINT DEFAULT NULL,
  `CHARACTER_SET_NAME` VARCHAR(192) DEFAULT NULL,
  `COLLATION_NAME` VARCHAR(192) DEFAULT NULL,
  `COLUMN_TYPE` LONGTEXT,
  `COLUMN_KEY` VARCHAR(3) DEFAULT NULL,
  `is_auto_increment` TINYINT DEFAULT NULL,
  `COLUMN_COMMENT` VARCHAR(1024) DEFAULT NULL,
  `SchemaName` VARCHAR(192) DEFAULT NULL,
  `TableName` VARCHAR(192) DEFAULT NULL,
  PRIMARY KEY (`CID`)
) ENGINE=INNODB DEFAULT CHARSET=utf8;

DROP TABLE IF EXISTS `dbv_view`;

CREATE TABLE `dbv_view` (
  `VID` INT NOT NULL AUTO_INCREMENT,
  `SchemaName` VARCHAR(64) NOT NULL DEFAULT '',
  `TableName` VARCHAR(64) NOT NULL DEFAULT '',
  `VIEW_DEFINITION` LONGTEXT NOT NULL,
  PRIMARY KEY (`VID`)
) ENGINE=INNODB DEFAULT CHARSET=utf8;

DROP TABLE IF EXISTS `dbv_trigger`;

CREATE TABLE `dbv_trigger` (
  `TriID` INT NOT NULL AUTO_INCREMENT,
  `SchemaName` VARCHAR(64) NOT NULL DEFAULT '',
  `TriName` VARCHAR(64) NOT NULL DEFAULT '',
  `EVENT_MANIPULATION` VARCHAR(6) NOT NULL DEFAULT '',
  `TableName` VARCHAR(64) NOT NULL DEFAULT '',
  `ACTION_ORDER` BIGINT(4) NOT NULL DEFAULT '0',
  `ACTION_CONDITION` LONGTEXT,
  `ACTION_STATEMENT` LONGTEXT NOT NULL,
  `ACTION_ORIENTATION` VARCHAR(9) NOT NULL DEFAULT '',
  `ACTION_TIMING` VARCHAR(6) NOT NULL DEFAULT '',
  PRIMARY KEY (`TriID`)
) ENGINE=INNODB DEFAULT CHARSET=utf8;

DROP TABLE IF EXISTS `dbv_proc`;

CREATE TABLE `dbv_proc` (
  `PID` INT NOT NULL AUTO_INCREMENT,
  `SchemaName` VARCHAR(192) DEFAULT NULL,
  `ProcName` VARCHAR(192) DEFAULT NULL,
  `type` INT NOT NULL DEFAULT 2,
  `param_list` TEXT,
  `returns` TEXT,
  `body` LONGTEXT,
  `comment` VARCHAR(192) DEFAULT NULL,
  PRIMARY KEY (`PID`)
) ENGINE=INNODB DEFAULT CHARSET=utf8;

-- =================================================================================================

-- 基本存储过程 -------------------------------------------------------------------------------------

DROP PROCEDURE IF EXISTS `Apq_CreateNewTable`;

DELIMITER $$
CREATE PROCEDURE `Apq_CreateNewTable`(
	pTID	INT
)
BEGIN
	DECLARE minCID INT;
	DECLARE nCID INT;
	DECLARE maxCID INT;
	
	DECLARE vSchemaName VARCHAR(192);
	DECLARE vTableName VARCHAR(192);
	DECLARE vENGINE VARCHAR(192);
	DECLARE vCREATE_OPTIONS VARCHAR(765);
	DECLARE vTABLE_COMMENT VARCHAR(240);
	DECLARE vPrimaryKeys TEXT;
	
	DECLARE vColName VARCHAR(192);
	DECLARE vDefaultValue LONGTEXT;
	DECLARE vNullAble TINYINT;
	DECLARE vDATA_TYPE VARCHAR(192);
	DECLARE vCHARACTER_MAXIMUM_LENGTH BIGINT;
	DECLARE vCHARACTER_OCTET_LENGTH BIGINT;
	DECLARE vNUMERIC_PRECISION BIGINT;
	DECLARE vNUMERIC_SCALE BIGINT;
	DECLARE vCHARACTER_SET_NAME VARCHAR(192);
	DECLARE vCOLLATION_NAME VARCHAR(192);
	DECLARE vCOLUMN_TYPE LONGTEXT;
	DECLARE vCOLUMN_KEY VARCHAR(3);
	DECLARE vis_auto_increment TINYINT;
	DECLARE vCOLUMN_COMMENT VARCHAR(1024);
	
	SELECT SchemaName, TableName, `ENGINE`, CREATE_OPTIONS, TABLE_COMMENT, PrimaryKeys
	  INTO vSchemaName,vTableName,vENGINE,vCREATE_OPTIONS,vTABLE_COMMENT,vPrimaryKeys
	  FROM dbv_table
	 WHERE TID = pTID;
	
	SET @STMT = CONCAT('
CREATE TABLE IF NOT EXISTS `',vTableName,'`(
');
	 
	SELECT IFNULL(MIN(CID),0), IFNULL(MAX(CID),-1)
	  INTO minCID, maxCID
	  FROM dbv_column
	 WHERE TID = pTID;
	
	SET nCID = minCID;
	WHILE nCID <= maxCID DO
		SELECT  ColName, DefaultValue, NullAble, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH, CHARACTER_OCTET_LENGTH, NUMERIC_PRECISION, NUMERIC_SCALE,
			 CHARACTER_SET_NAME, COLLATION_NAME, COLUMN_TYPE, COLUMN_KEY, is_auto_increment, COLUMN_COMMENT
		  INTO vColName,vDefaultValue,vNullAble,vDATA_TYPE,vCHARACTER_MAXIMUM_LENGTH,vCHARACTER_OCTET_LENGTH,vNUMERIC_PRECISION,vNUMERIC_SCALE,
			vCHARACTER_SET_NAME,vCOLLATION_NAME,vCOLUMN_TYPE,vCOLUMN_KEY,vis_auto_increment,vCOLUMN_COMMENT
		  FROM dbv_column
		 WHERE TID = pTID AND CID = nCID;
		IF FOUND_ROWS() > 0 THEN
			SET @STMT = CONCAT(@STMT,'	`',vColName,'` ',vCOLUMN_TYPE);
			
			IF vNullAble = 0 THEN
				SET @STMT = CONCAT(@STMT,' NOT NULL');
			END IF;
			
			IF vDefaultValue IS NOT NULL THEN
				SET @STMT = CONCAT(@STMT,' DEFAULT ', vDefaultValue);
			END IF;
			
			IF nCID < maxCID THEN
				SET @STMT = CONCAT(@STMT,',
');
			END IF;
		END IF;
		
		SET nCID = nCID + 1;
	END WHILE;
	
	IF CHAR_LENGTH(vPrimaryKeys) > 0 THEN
		SET @STMT = CONCAT(@STMT,',
	PRIMARY KEY (',vPrimaryKeys,')');
	END IF;
	
	SET @STMT = CONCAT(@STMT,'
) ENGINE=',vENGINE,' DEFAULT CHARSET=utf8;');
	
	-- SELECT @STMT as `Sql`;
	
	PREPARE stmt FROM @STMT;
	EXECUTE stmt;
END$$

DELIMITER ;


-- =================================================================================================

-- 元数据 ------------------------------------------------------------------------------------------

-- 表:sys_application
INSERT INTO `dbv_table`(`TID`,`SchemaName`,`TableName`,`ENGINE`,`CREATE_OPTIONS`,`TABLE_COMMENT`,`PrimaryKeys`) VALUES(1,'remp_sys_ch','sys_application','InnoDB','','','`AppID`');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(1,'AppID',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','PRI',0,'业务编码','remp_sys_ch','sys_application');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(1,'AppName',NULL,0,'varchar',40,120,NULL,NULL,'utf8','utf8_general_ci','varchar(40)','UNI',0,'业务名称','remp_sys_ch','sys_application');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(1,'Description',NULL,1,'varchar',60,180,NULL,NULL,'utf8','utf8_general_ci','varchar(60)','',0,'业务描述','remp_sys_ch','sys_application');

-- 表:sys_area
INSERT INTO `dbv_table`(`TID`,`SchemaName`,`TableName`,`ENGINE`,`CREATE_OPTIONS`,`TABLE_COMMENT`,`PrimaryKeys`) VALUES(2,'remp_sys_ch','sys_area','InnoDB','','','`AreaID`');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(2,'AreaID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','PRI',1,'区域编码','remp_sys_ch','sys_area');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(2,'Name',NULL,0,'varchar',30,90,NULL,NULL,'utf8','utf8_general_ci','varchar(30)','',0,'区域名称','remp_sys_ch','sys_area');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(2,'ParentID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'上级区域编码','remp_sys_ch','sys_area');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(2,'Path',NULL,0,'varchar',30,90,NULL,NULL,'utf8','utf8_general_ci','varchar(30)','',0,'访问路径','remp_sys_ch','sys_area');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(2,'EnglishName',NULL,0,'varchar',20,60,NULL,NULL,'utf8','utf8_general_ci','varchar(20)','',0,'拼音字母','remp_sys_ch','sys_area');

-- 表:sys_category
INSERT INTO `dbv_table`(`TID`,`SchemaName`,`TableName`,`ENGINE`,`CREATE_OPTIONS`,`TABLE_COMMENT`,`PrimaryKeys`) VALUES(3,'remp_sys_ch','sys_category','InnoDB','','','`CategoryID`');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(3,'CategoryID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','PRI',1,'','remp_sys_ch','sys_category');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(3,'CategoryName',NULL,0,'varchar',50,150,NULL,NULL,'utf8','utf8_general_ci','varchar(50)','',0,'','remp_sys_ch','sys_category');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(3,'CategoryDesc',NULL,1,'varchar',255,765,NULL,NULL,'utf8','utf8_general_ci','varchar(255)','',0,'','remp_sys_ch','sys_category');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(3,'Type',NULL,1,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_category');

-- 表:sys_categoryitem
INSERT INTO `dbv_table`(`TID`,`SchemaName`,`TableName`,`ENGINE`,`CREATE_OPTIONS`,`TABLE_COMMENT`,`PrimaryKeys`) VALUES(4,'remp_sys_ch','sys_categoryitem','InnoDB','','','`CategoryItemId`');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(4,'CategoryItemId',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','PRI',1,'','remp_sys_ch','sys_categoryitem');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(4,'CategoryItemValue',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','MUL',0,'','remp_sys_ch','sys_categoryitem');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(4,'CategoryItemName',NULL,0,'varchar',50,150,NULL,NULL,'utf8','utf8_general_ci','varchar(50)','',0,'','remp_sys_ch','sys_categoryitem');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(4,'CategoryID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'','remp_sys_ch','sys_categoryitem');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(4,'OrderNo','0',0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_categoryitem');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(4,'IsDefault','0',0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_categoryitem');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(4,'Type','0',0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_categoryitem');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(4,'Invalid','0',0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_categoryitem');

-- 表:sys_collectorinfo
INSERT INTO `dbv_table`(`TID`,`SchemaName`,`TableName`,`ENGINE`,`CREATE_OPTIONS`,`TABLE_COMMENT`,`PrimaryKeys`) VALUES(5,'remp_sys_ch','sys_collectorinfo','InnoDB','','采集器信息表','`CollectorID`');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(5,'CollectorID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','PRI',1,'采集器编码','remp_sys_ch','sys_collectorinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(5,'CollectorCode',NULL,0,'varchar',30,90,NULL,NULL,'utf8','utf8_general_ci','varchar(30)','UNI',0,'采集器物理编号','remp_sys_ch','sys_collectorinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(5,'CollectorModelID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'','remp_sys_ch','sys_collectorinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(5,'CollectorTypeID',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_collectorinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(5,'BlueTooth',NULL,0,'varchar',30,90,NULL,NULL,'utf8','utf8_general_ci','varchar(30)','',0,'蓝牙地址','remp_sys_ch','sys_collectorinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(5,'ImeiAddr',NULL,1,'varchar',40,120,NULL,NULL,'utf8','utf8_general_ci','varchar(40)','',0,'IMEI地址','remp_sys_ch','sys_collectorinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(5,'Description',NULL,1,'varchar',60,180,NULL,NULL,'utf8','utf8_general_ci','varchar(60)','',0,'采集器描述','remp_sys_ch','sys_collectorinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(5,'Status',NULL,1,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'采集器状态','remp_sys_ch','sys_collectorinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(5,'OrgID',NULL,1,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','MUL',0,'所属组织','remp_sys_ch','sys_collectorinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(5,'Creator',NULL,0,'varchar',30,90,NULL,NULL,'utf8','utf8_general_ci','varchar(30)','',0,'创建人','remp_sys_ch','sys_collectorinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(5,'Invalid',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'数据是否有效','remp_sys_ch','sys_collectorinfo');

-- 表:sys_collectormodel
INSERT INTO `dbv_table`(`TID`,`SchemaName`,`TableName`,`ENGINE`,`CREATE_OPTIONS`,`TABLE_COMMENT`,`PrimaryKeys`) VALUES(6,'remp_sys_ch','sys_collectormodel','InnoDB','','采集器类型表','`CollectorModelID`');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(6,'CollectorModelID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','PRI',0,'采集器类型编码','remp_sys_ch','sys_collectormodel');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(6,'CollectorModelName',NULL,0,'varchar',30,90,NULL,NULL,'utf8','utf8_general_ci','varchar(30)','',0,'采集器类型名称','remp_sys_ch','sys_collectormodel');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(6,'CollectorType',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_collectormodel');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(6,'NumOfChannels',NULL,1,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'通道总数','remp_sys_ch','sys_collectormodel');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(6,'PassiveMode',NULL,1,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'移动终端是否被采集器连接','remp_sys_ch','sys_collectormodel');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(6,'ProtocolType',NULL,1,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'蓝牙协议类型','remp_sys_ch','sys_collectormodel');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(6,'PairingCode',NULL,1,'varchar',30,90,NULL,NULL,'utf8','utf8_general_ci','varchar(30)','',0,'蓝牙设备配对码','remp_sys_ch','sys_collectormodel');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(6,'AppID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'业务类型','remp_sys_ch','sys_collectormodel');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(6,'HeartBeatInterval',NULL,1,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'心跳间隔','remp_sys_ch','sys_collectormodel');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(6,'SoftVersion','1.0.0',0,'varchar',20,60,NULL,NULL,'utf8','utf8_general_ci','varchar(20)','',0,'','remp_sys_ch','sys_collectormodel');

-- 表:sys_combodetailsinfo
INSERT INTO `dbv_table`(`TID`,`SchemaName`,`TableName`,`ENGINE`,`CREATE_OPTIONS`,`TABLE_COMMENT`,`PrimaryKeys`) VALUES(7,'remp_sys_ch','sys_combodetailsinfo','InnoDB','','','`SCID`,`ServiceItemID`');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(7,'SCID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','PRI',0,'套餐编号','remp_sys_ch','sys_combodetailsinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(7,'ServiceItemID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','PRI',0,'项目编号','remp_sys_ch','sys_combodetailsinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(7,'Frequency',NULL,1,'varchar',60,180,NULL,NULL,'utf8','utf8_general_ci','varchar(60)','',0,'服务频次','remp_sys_ch','sys_combodetailsinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(7,'Standard',NULL,1,'varchar',256,768,NULL,NULL,'utf8','utf8_general_ci','varchar(256)','',0,'服务标准','remp_sys_ch','sys_combodetailsinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(7,'Price',NULL,1,'varchar',60,180,NULL,NULL,'utf8','utf8_general_ci','varchar(60)','',0,'服务价格','remp_sys_ch','sys_combodetailsinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(7,'Content',NULL,1,'varchar',512,1536,NULL,NULL,'utf8','utf8_general_ci','varchar(512)','',0,'服务内容','remp_sys_ch','sys_combodetailsinfo');

-- 表:sys_dbversion
INSERT INTO `dbv_table`(`TID`,`SchemaName`,`TableName`,`ENGINE`,`CREATE_OPTIONS`,`TABLE_COMMENT`,`PrimaryKeys`) VALUES(8,'remp_sys_ch','sys_dbversion','InnoDB','','','`ID`');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(8,'ID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','PRI',1,'','remp_sys_ch','sys_dbversion');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(8,'AppName',NULL,0,'varchar',30,90,NULL,NULL,'utf8','utf8_general_ci','varchar(30)','',0,'应用程序名称','remp_sys_ch','sys_dbversion');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(8,'OriginalVersion',NULL,0,'varchar',20,60,NULL,NULL,'utf8','utf8_general_ci','varchar(20)','',0,'','remp_sys_ch','sys_dbversion');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(8,'CurrentVersion',NULL,0,'varchar',20,60,NULL,NULL,'utf8','utf8_general_ci','varchar(20)','',0,'','remp_sys_ch','sys_dbversion');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(8,'ModifyDate',NULL,0,'date',NULL,NULL,NULL,NULL,NULL,NULL,'date','',0,'','remp_sys_ch','sys_dbversion');

-- 表:sys_deviceupgradeinfo
INSERT INTO `dbv_table`(`TID`,`SchemaName`,`TableName`,`ENGINE`,`CREATE_OPTIONS`,`TABLE_COMMENT`,`PrimaryKeys`) VALUES(9,'remp_sys_ch','sys_deviceupgradeinfo','InnoDB','','','`InfoID`');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(9,'InfoID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','PRI',1,'','remp_sys_ch','sys_deviceupgradeinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(9,'CollectorModelID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'设备型号编码','remp_sys_ch','sys_deviceupgradeinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(9,'UpgradeType',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'升级类型 0-手机,1-Pad,2采集器
','remp_sys_ch','sys_deviceupgradeinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(9,'AppId',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'业务类型编码','remp_sys_ch','sys_deviceupgradeinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(9,'SoftVersion',NULL,0,'varchar',20,60,NULL,NULL,'utf8','utf8_general_ci','varchar(20)','',0,'软件版本','remp_sys_ch','sys_deviceupgradeinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(9,'FileName',NULL,1,'varchar',50,150,NULL,NULL,'utf8','utf8_general_ci','varchar(50)','',0,'升级包文件名','remp_sys_ch','sys_deviceupgradeinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(9,'FileSize',NULL,1,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'数据包大小','remp_sys_ch','sys_deviceupgradeinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(9,'Mark',NULL,1,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,' 升级标志 0,可升级可不升级;1,强制升级

 ','remp_sys_ch','sys_deviceupgradeinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(9,'PublishTime',NULL,1,'datetime',NULL,NULL,NULL,NULL,NULL,NULL,'datetime','',0,'发布时间','remp_sys_ch','sys_deviceupgradeinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(9,'MD5',NULL,1,'text',65535,65535,NULL,NULL,'utf8','utf8_general_ci','text','',0,'','remp_sys_ch','sys_deviceupgradeinfo');

-- 表:sys_doctorinfo
INSERT INTO `dbv_table`(`TID`,`SchemaName`,`TableName`,`ENGINE`,`CREATE_OPTIONS`,`TABLE_COMMENT`,`PrimaryKeys`) VALUES(10,'remp_sys_ch','sys_doctorinfo','InnoDB','','医护人员信息表','`DrID`');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(10,'DrID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','PRI',1,'医护人员编码','remp_sys_ch','sys_doctorinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(10,'DrName',NULL,0,'varchar',30,90,NULL,NULL,'utf8','utf8_general_ci','varchar(30)','',0,'姓名','remp_sys_ch','sys_doctorinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(10,'DrAccount',NULL,0,'varchar',30,90,NULL,NULL,'utf8','utf8_general_ci','varchar(30)','UNI',0,'登陆帐号','remp_sys_ch','sys_doctorinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(10,'Password',NULL,0,'varchar',64,192,NULL,NULL,'utf8','utf8_general_ci','varchar(64)','',0,'登录密码','remp_sys_ch','sys_doctorinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(10,'WorkID',NULL,1,'varchar',30,90,NULL,NULL,'utf8','utf8_general_ci','varchar(30)','',0,'工号','remp_sys_ch','sys_doctorinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(10,'DrSex',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'性别','remp_sys_ch','sys_doctorinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(10,'Birthday',NULL,1,'date',NULL,NULL,NULL,NULL,NULL,NULL,'date','',0,'出生日期','remp_sys_ch','sys_doctorinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(10,'CredNO',NULL,1,'varchar',20,60,NULL,NULL,'utf8','utf8_general_ci','varchar(20)','',0,'证件号码','remp_sys_ch','sys_doctorinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(10,'CredType',NULL,1,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'证件类型','remp_sys_ch','sys_doctorinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(10,'MaritalStatus',NULL,1,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'婚姻状况','remp_sys_ch','sys_doctorinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(10,'Nation',NULL,1,'varchar',20,60,NULL,NULL,'utf8','utf8_general_ci','varchar(20)','',0,'民族','remp_sys_ch','sys_doctorinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(10,'BornPlace',NULL,1,'varchar',20,60,NULL,NULL,'utf8','utf8_general_ci','varchar(20)','',0,'籍贯','remp_sys_ch','sys_doctorinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(10,'Education',NULL,1,'varchar',7,21,NULL,NULL,'utf8','utf8_general_ci','varchar(7)','',0,'','remp_sys_ch','sys_doctorinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(10,'Address',NULL,1,'varchar',100,300,NULL,NULL,'utf8','utf8_general_ci','varchar(100)','',0,'','remp_sys_ch','sys_doctorinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(10,'Phone',NULL,1,'varchar',30,90,NULL,NULL,'utf8','utf8_general_ci','varchar(30)','',0,'','remp_sys_ch','sys_doctorinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(10,'Mobile',NULL,1,'varchar',30,90,NULL,NULL,'utf8','utf8_general_ci','varchar(30)','',0,'','remp_sys_ch','sys_doctorinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(10,'OrgID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','MUL',0,'','remp_sys_ch','sys_doctorinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(10,'WorkPhone',NULL,1,'varchar',30,90,NULL,NULL,'utf8','utf8_general_ci','varchar(30)','',0,'','remp_sys_ch','sys_doctorinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(10,'Email',NULL,1,'varchar',30,90,NULL,NULL,'utf8','utf8_general_ci','varchar(30)','',0,'','remp_sys_ch','sys_doctorinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(10,'Position',NULL,1,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_doctorinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(10,'Brief',NULL,1,'varchar',300,900,NULL,NULL,'utf8','utf8_general_ci','varchar(300)','',0,'','remp_sys_ch','sys_doctorinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(10,'Creator',NULL,0,'varchar',30,90,NULL,NULL,'utf8','utf8_general_ci','varchar(30)','',0,'','remp_sys_ch','sys_doctorinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(10,'IsEnable',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_doctorinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(10,'Invalid',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_doctorinfo');

-- 表:sys_doctorinfoq
INSERT INTO `dbv_table`(`TID`,`SchemaName`,`TableName`,`ENGINE`,`CREATE_OPTIONS`,`TABLE_COMMENT`,`PrimaryKeys`) VALUES(11,'remp_sys_ch','sys_doctorinfoq','InnoDB','','','`DrID`');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(11,'DrID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','PRI',0,'','remp_sys_ch','sys_doctorinfoq');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(11,'DrName',NULL,0,'varchar',30,90,NULL,NULL,'utf8','utf8_general_ci','varchar(30)','',0,'','remp_sys_ch','sys_doctorinfoq');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(11,'OrgID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'','remp_sys_ch','sys_doctorinfoq');

-- 表:sys_doctororgrel
INSERT INTO `dbv_table`(`TID`,`SchemaName`,`TableName`,`ENGINE`,`CREATE_OPTIONS`,`TABLE_COMMENT`,`PrimaryKeys`) VALUES(12,'remp_sys_ch','sys_doctororgrel','InnoDB','','','`DrOrgID`');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(12,'DrOrgID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','PRI',1,'','remp_sys_ch','sys_doctororgrel');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(12,'DrID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','MUL',0,'','remp_sys_ch','sys_doctororgrel');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(12,'OrgID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'','remp_sys_ch','sys_doctororgrel');

-- 表:sys_doctorpatientmsg
INSERT INTO `dbv_table`(`TID`,`SchemaName`,`TableName`,`ENGINE`,`CREATE_OPTIONS`,`TABLE_COMMENT`,`PrimaryKeys`) VALUES(13,'remp_sys_ch','sys_doctorpatientmsg','InnoDB','','','`DrMsgID`');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(13,'DrMsgID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','PRI',1,'','remp_sys_ch','sys_doctorpatientmsg');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(13,'DrID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'','remp_sys_ch','sys_doctorpatientmsg');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(13,'DrName',NULL,0,'varchar',30,90,NULL,NULL,'utf8','utf8_general_ci','varchar(30)','',0,'','remp_sys_ch','sys_doctorpatientmsg');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(13,'PatientID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'','remp_sys_ch','sys_doctorpatientmsg');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(13,'PatientName',NULL,0,'varchar',60,180,NULL,NULL,'utf8','utf8_general_ci','varchar(60)','',0,'','remp_sys_ch','sys_doctorpatientmsg');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(13,'SourceMsgID','0',1,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'','remp_sys_ch','sys_doctorpatientmsg');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(13,'MsgContent',NULL,0,'varchar',200,600,NULL,NULL,'utf8','utf8_general_ci','varchar(200)','',0,'','remp_sys_ch','sys_doctorpatientmsg');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(13,'AppID',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_doctorpatientmsg');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(13,'MsgType',NULL,1,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_doctorpatientmsg');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(13,'SendTime',NULL,0,'datetime',NULL,NULL,NULL,NULL,NULL,NULL,'datetime','',0,'','remp_sys_ch','sys_doctorpatientmsg');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(13,'Status',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'网页端用户是否读取的状态标识','remp_sys_ch','sys_doctorpatientmsg');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(13,'ReadTime',NULL,1,'datetime',NULL,NULL,NULL,NULL,NULL,NULL,'datetime','',0,'对于医生发送给患者的信息，该时间是记录患者第一次下载的时间；对于患者发给医生的信息，次时间记录医生第一次读取的时间','remp_sys_ch','sys_doctorpatientmsg');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(13,'SendSource',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_doctorpatientmsg');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(13,'DrDeleted','0',0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'医生删除标志，默认为0，删除为1
','remp_sys_ch','sys_doctorpatientmsg');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(13,'PatientDeleted','0',0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'患者删除标志，默认为0，删除为1','remp_sys_ch','sys_doctorpatientmsg');

-- 表:sys_doctorpatientmsghis
INSERT INTO `dbv_table`(`TID`,`SchemaName`,`TableName`,`ENGINE`,`CREATE_OPTIONS`,`TABLE_COMMENT`,`PrimaryKeys`) VALUES(14,'remp_sys_ch','sys_doctorpatientmsghis','InnoDB','','','`DrMsgID`');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(14,'DrMsgID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','PRI',0,'','remp_sys_ch','sys_doctorpatientmsghis');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(14,'DrID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'','remp_sys_ch','sys_doctorpatientmsghis');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(14,'DrName',NULL,0,'varchar',30,90,NULL,NULL,'utf8','utf8_general_ci','varchar(30)','',0,'','remp_sys_ch','sys_doctorpatientmsghis');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(14,'PatientID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'','remp_sys_ch','sys_doctorpatientmsghis');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(14,'PatientName',NULL,0,'varchar',60,180,NULL,NULL,'utf8','utf8_general_ci','varchar(60)','',0,'','remp_sys_ch','sys_doctorpatientmsghis');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(14,'SourceMsgID','0',1,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'','remp_sys_ch','sys_doctorpatientmsghis');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(14,'MsgContent',NULL,0,'varchar',200,600,NULL,NULL,'utf8','utf8_general_ci','varchar(200)','',0,'','remp_sys_ch','sys_doctorpatientmsghis');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(14,'AppID',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_doctorpatientmsghis');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(14,'MsgType',NULL,1,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_doctorpatientmsghis');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(14,'SendTime',NULL,0,'datetime',NULL,NULL,NULL,NULL,NULL,NULL,'datetime','',0,'','remp_sys_ch','sys_doctorpatientmsghis');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(14,'Status',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_doctorpatientmsghis');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(14,'ReadTime',NULL,1,'datetime',NULL,NULL,NULL,NULL,NULL,NULL,'datetime','',0,'','remp_sys_ch','sys_doctorpatientmsghis');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(14,'SendSource',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_doctorpatientmsghis');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(14,'DrDeleted','0',0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_doctorpatientmsghis');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(14,'PatientDeleted','0',0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_doctorpatientmsghis');

-- 表:sys_functioninfo
INSERT INTO `dbv_table`(`TID`,`SchemaName`,`TableName`,`ENGINE`,`CREATE_OPTIONS`,`TABLE_COMMENT`,`PrimaryKeys`) VALUES(15,'remp_sys_ch','sys_functioninfo','InnoDB','','','`FuncID`');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(15,'FuncID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','PRI',0,'','remp_sys_ch','sys_functioninfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(15,'ParentID',NULL,1,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'','remp_sys_ch','sys_functioninfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(15,'FuncName',NULL,0,'varchar',60,180,NULL,NULL,'utf8','utf8_general_ci','varchar(60)','',0,'','remp_sys_ch','sys_functioninfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(15,'FuncDescription',NULL,1,'varchar',200,600,NULL,NULL,'utf8','utf8_general_ci','varchar(200)','',0,'','remp_sys_ch','sys_functioninfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(15,'FuncURL',NULL,1,'varchar',200,600,NULL,NULL,'utf8','utf8_general_ci','varchar(200)','',0,'','remp_sys_ch','sys_functioninfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(15,'FuncOrder',NULL,1,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_functioninfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(15,'FuncCode',NULL,1,'varchar',40,120,NULL,NULL,'utf8','utf8_general_ci','varchar(40)','',0,'','remp_sys_ch','sys_functioninfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(15,'AppID',NULL,1,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_functioninfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(15,'FuncType',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_functioninfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(15,'FuncKind',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_functioninfo');

-- 表:sys_issueinfo
INSERT INTO `dbv_table`(`TID`,`SchemaName`,`TableName`,`ENGINE`,`CREATE_OPTIONS`,`TABLE_COMMENT`,`PrimaryKeys`) VALUES(16,'remp_sys_ch','sys_issueinfo','InnoDB','','','`IssueId`');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(16,'IssueId',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','PRI',1,'','remp_sys_ch','sys_issueinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(16,'DataID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'','remp_sys_ch','sys_issueinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(16,'NotifyStr',NULL,0,'varchar',2000,6000,NULL,NULL,'utf8','utf8_general_ci','varchar(2000)','',0,'','remp_sys_ch','sys_issueinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(16,'NotifyTime',NULL,0,'datetime',NULL,NULL,NULL,NULL,NULL,NULL,'datetime','',0,'','remp_sys_ch','sys_issueinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(16,'Status',NULL,1,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_issueinfo');

-- 表:sys_loginlog
INSERT INTO `dbv_table`(`TID`,`SchemaName`,`TableName`,`ENGINE`,`CREATE_OPTIONS`,`TABLE_COMMENT`,`PrimaryKeys`) VALUES(17,'remp_sys_ch','sys_loginlog','InnoDB','','','`LoginID`');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(17,'LoginID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','PRI',1,'','remp_sys_ch','sys_loginlog');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(17,'UserID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'','remp_sys_ch','sys_loginlog');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(17,'SourceIP',NULL,0,'varchar',20,60,NULL,NULL,'utf8','utf8_general_ci','varchar(20)','',0,'','remp_sys_ch','sys_loginlog');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(17,'LoginMsg',NULL,0,'varchar',20,60,NULL,NULL,'utf8','utf8_general_ci','varchar(20)','',0,'','remp_sys_ch','sys_loginlog');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(17,'LogIntime',NULL,0,'datetime',NULL,NULL,NULL,NULL,NULL,NULL,'datetime','',0,'','remp_sys_ch','sys_loginlog');

-- 表:sys_loginloghis
INSERT INTO `dbv_table`(`TID`,`SchemaName`,`TableName`,`ENGINE`,`CREATE_OPTIONS`,`TABLE_COMMENT`,`PrimaryKeys`) VALUES(18,'remp_sys_ch','sys_loginloghis','InnoDB','','','`LoginID`');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(18,'LoginID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','PRI',0,'','remp_sys_ch','sys_loginloghis');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(18,'UserID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'','remp_sys_ch','sys_loginloghis');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(18,'SourceIP',NULL,0,'varchar',20,60,NULL,NULL,'utf8','utf8_general_ci','varchar(20)','',0,'','remp_sys_ch','sys_loginloghis');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(18,'LoginMsg',NULL,0,'varchar',20,60,NULL,NULL,'utf8','utf8_general_ci','varchar(20)','',0,'','remp_sys_ch','sys_loginloghis');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(18,'LogIntime',NULL,0,'datetime',NULL,NULL,NULL,NULL,NULL,NULL,'datetime','',0,'','remp_sys_ch','sys_loginloghis');

-- 表:sys_mailhisinfo
INSERT INTO `dbv_table`(`TID`,`SchemaName`,`TableName`,`ENGINE`,`CREATE_OPTIONS`,`TABLE_COMMENT`,`PrimaryKeys`) VALUES(19,'remp_sys_ch','sys_mailhisinfo','InnoDB','','','`MailId`');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(19,'MailId',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','PRI',1,'','remp_sys_ch','sys_mailhisinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(19,'SendType',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_mailhisinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(19,'AppID','0',0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_mailhisinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(19,'SourceUserID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'','remp_sys_ch','sys_mailhisinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(19,'SourceName',NULL,1,'varchar',60,180,NULL,NULL,'utf8','utf8_general_ci','varchar(60)','',0,'','remp_sys_ch','sys_mailhisinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(19,'Subject',NULL,1,'varchar',200,600,NULL,NULL,'utf8','utf8_general_ci','varchar(200)','',0,'','remp_sys_ch','sys_mailhisinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(19,'Content',NULL,1,'varchar',2000,6000,NULL,NULL,'utf8','utf8_general_ci','varchar(2000)','',0,'','remp_sys_ch','sys_mailhisinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(19,'SendTime',NULL,1,'datetime',NULL,NULL,NULL,NULL,NULL,NULL,'datetime','',0,'','remp_sys_ch','sys_mailhisinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(19,'Status',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_mailhisinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(19,'LastSendTime',NULL,1,'datetime',NULL,NULL,NULL,NULL,NULL,NULL,'datetime','',0,'','remp_sys_ch','sys_mailhisinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(19,'FailedCount',NULL,1,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'','remp_sys_ch','sys_mailhisinfo');

-- 表:sys_mailinfo
INSERT INTO `dbv_table`(`TID`,`SchemaName`,`TableName`,`ENGINE`,`CREATE_OPTIONS`,`TABLE_COMMENT`,`PrimaryKeys`) VALUES(20,'remp_sys_ch','sys_mailinfo','InnoDB','','','`MailId`');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(20,'MailId',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','PRI',1,'','remp_sys_ch','sys_mailinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(20,'SendType',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_mailinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(20,'AppID','0',0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_mailinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(20,'SourceUserID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'','remp_sys_ch','sys_mailinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(20,'SourceName',NULL,1,'varchar',60,180,NULL,NULL,'utf8','utf8_general_ci','varchar(60)','',0,'','remp_sys_ch','sys_mailinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(20,'Subject',NULL,1,'varchar',200,600,NULL,NULL,'utf8','utf8_general_ci','varchar(200)','',0,'','remp_sys_ch','sys_mailinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(20,'Content',NULL,1,'varchar',2000,6000,NULL,NULL,'utf8','utf8_general_ci','varchar(2000)','',0,'','remp_sys_ch','sys_mailinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(20,'SendTime',NULL,1,'datetime',NULL,NULL,NULL,NULL,NULL,NULL,'datetime','',0,'','remp_sys_ch','sys_mailinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(20,'Status',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_mailinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(20,'LastSendTime',NULL,1,'datetime',NULL,NULL,NULL,NULL,NULL,NULL,'datetime','',0,'','remp_sys_ch','sys_mailinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(20,'FailedCount',NULL,1,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'','remp_sys_ch','sys_mailinfo');

-- 表:sys_mailsendtohisinfo
INSERT INTO `dbv_table`(`TID`,`SchemaName`,`TableName`,`ENGINE`,`CREATE_OPTIONS`,`TABLE_COMMENT`,`PrimaryKeys`) VALUES(21,'remp_sys_ch','sys_mailsendtohisinfo','InnoDB','','','`MailSendID`');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(21,'MailSendID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','PRI',0,'','remp_sys_ch','sys_mailsendtohisinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(21,'MailId',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'','remp_sys_ch','sys_mailsendtohisinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(21,'SendTo',NULL,0,'varchar',60,180,NULL,NULL,'utf8','utf8_general_ci','varchar(60)','',0,'','remp_sys_ch','sys_mailsendtohisinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(21,'UserID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'','remp_sys_ch','sys_mailsendtohisinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(21,'OrgID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'','remp_sys_ch','sys_mailsendtohisinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(21,'UserName',NULL,0,'varchar',30,90,NULL,NULL,'utf8','utf8_general_ci','varchar(30)','',0,'','remp_sys_ch','sys_mailsendtohisinfo');

-- 表:sys_mailsendtoinfo
INSERT INTO `dbv_table`(`TID`,`SchemaName`,`TableName`,`ENGINE`,`CREATE_OPTIONS`,`TABLE_COMMENT`,`PrimaryKeys`) VALUES(22,'remp_sys_ch','sys_mailsendtoinfo','InnoDB','','','`MailSendID`');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(22,'MailSendID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','PRI',1,'','remp_sys_ch','sys_mailsendtoinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(22,'MailId',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'','remp_sys_ch','sys_mailsendtoinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(22,'SendTo',NULL,0,'varchar',60,180,NULL,NULL,'utf8','utf8_general_ci','varchar(60)','',0,'','remp_sys_ch','sys_mailsendtoinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(22,'UserID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'','remp_sys_ch','sys_mailsendtoinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(22,'OrgID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'','remp_sys_ch','sys_mailsendtoinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(22,'UserName',NULL,0,'varchar',30,90,NULL,NULL,'utf8','utf8_general_ci','varchar(30)','',0,'','remp_sys_ch','sys_mailsendtoinfo');

-- 表:sys_masssendmsg
INSERT INTO `dbv_table`(`TID`,`SchemaName`,`TableName`,`ENGINE`,`CREATE_OPTIONS`,`TABLE_COMMENT`,`PrimaryKeys`) VALUES(23,'remp_sys_ch','sys_masssendmsg','InnoDB','','','`MsgID`');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(23,'MsgID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','PRI',1,'流水号','remp_sys_ch','sys_masssendmsg');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(23,'AppID',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'业务类型','remp_sys_ch','sys_masssendmsg');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(23,'OrgID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'组织ID','remp_sys_ch','sys_masssendmsg');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(23,'PatientID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'患者编码','remp_sys_ch','sys_masssendmsg');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(23,'DrID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'医生编码','remp_sys_ch','sys_masssendmsg');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(23,'UCNum',NULL,0,'varchar',30,90,NULL,NULL,'utf8','utf8_general_ci','varchar(30)','',0,'用户账号','remp_sys_ch','sys_masssendmsg');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(23,'SendTime',NULL,0,'datetime',NULL,NULL,NULL,NULL,NULL,NULL,'datetime','',0,'发送时间','remp_sys_ch','sys_masssendmsg');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(23,'Content',NULL,0,'varchar',700,2100,NULL,NULL,'utf8','utf8_general_ci','varchar(700)','',0,'发送内容','remp_sys_ch','sys_masssendmsg');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(23,'Status',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'登记状态，0待发送，1发送中，2成功，3失败','remp_sys_ch','sys_masssendmsg');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(23,'CreateTime',NULL,0,'datetime',NULL,NULL,NULL,NULL,NULL,NULL,'datetime','',0,'创建时间','remp_sys_ch','sys_masssendmsg');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(23,'IsDelay','0',0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'发送方式，0否，1 延迟','remp_sys_ch','sys_masssendmsg');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(23,'GWTime',NULL,0,'datetime',NULL,NULL,NULL,NULL,NULL,NULL,'datetime','',0,'回调时间','remp_sys_ch','sys_masssendmsg');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(23,'GWRet',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'0成功，<0失败码，-1接收失败','remp_sys_ch','sys_masssendmsg');

-- 表:sys_masssendmsgr
INSERT INTO `dbv_table`(`TID`,`SchemaName`,`TableName`,`ENGINE`,`CREATE_OPTIONS`,`TABLE_COMMENT`,`PrimaryKeys`) VALUES(24,'remp_sys_ch','sys_masssendmsgr','InnoDB','','','`MsgTelID`');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(24,'MsgTelID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','PRI',1,'流水号','remp_sys_ch','sys_masssendmsgr');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(24,'MsgID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'关联短信记录ID','remp_sys_ch','sys_masssendmsgr');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(24,'RecvType',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'0发送给会员|1非会员|2医生','remp_sys_ch','sys_masssendmsgr');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(24,'RecvDrID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'接收方医生编码','remp_sys_ch','sys_masssendmsgr');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(24,'RecvPatientID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'接收方会员编码','remp_sys_ch','sys_masssendmsgr');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(24,'RecvTel','接收手机号码',0,'varchar',30,90,NULL,NULL,'utf8','utf8_general_ci','varchar(30)','',0,'','remp_sys_ch','sys_masssendmsgr');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(24,'Status',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'登记状态，1发送中，2成功，3失败','remp_sys_ch','sys_masssendmsgr');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(24,'GWTime',NULL,0,'datetime',NULL,NULL,NULL,NULL,NULL,NULL,'datetime','',0,'回调时间','remp_sys_ch','sys_masssendmsgr');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(24,'GWRet',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'0成功，<0失败码，-1接收失败','remp_sys_ch','sys_masssendmsgr');

-- 表:sys_memberserviceinfo
INSERT INTO `dbv_table`(`TID`,`SchemaName`,`TableName`,`ENGINE`,`CREATE_OPTIONS`,`TABLE_COMMENT`,`PrimaryKeys`) VALUES(25,'remp_sys_ch','sys_memberserviceinfo','InnoDB','','','`ServiceID`');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(25,'ServiceID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','PRI',1,'','remp_sys_ch','sys_memberserviceinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(25,'PatientID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','MUL',0,'','remp_sys_ch','sys_memberserviceinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(25,'BeginDate',NULL,1,'date',NULL,NULL,NULL,NULL,NULL,NULL,'date','',0,'','remp_sys_ch','sys_memberserviceinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(25,'EndDate',NULL,1,'date',NULL,NULL,NULL,NULL,NULL,NULL,'date','',0,'','remp_sys_ch','sys_memberserviceinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(25,'Period',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'服务时长','remp_sys_ch','sys_memberserviceinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(25,'Status',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'0 未开通 默认
1开通
2已结束
','remp_sys_ch','sys_memberserviceinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(25,'OrgID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'','remp_sys_ch','sys_memberserviceinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(25,'SCID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'套餐编号','remp_sys_ch','sys_memberserviceinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(25,'Status2','0',0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_memberserviceinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(25,'IntervalPeriod',NULL,1,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_memberserviceinfo');

-- 表:sys_memberserviceitem
INSERT INTO `dbv_table`(`TID`,`SchemaName`,`TableName`,`ENGINE`,`CREATE_OPTIONS`,`TABLE_COMMENT`,`PrimaryKeys`) VALUES(26,'remp_sys_ch','sys_memberserviceitem','InnoDB','','','`ServiceItemID`');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(26,'ServiceItemID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','PRI',1,'','remp_sys_ch','sys_memberserviceitem');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(26,'ServiceID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'','remp_sys_ch','sys_memberserviceitem');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(26,'ItemID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'','remp_sys_ch','sys_memberserviceitem');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(26,'ServiceType',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_memberserviceitem');

-- 表:sys_memberserviceorg
INSERT INTO `dbv_table`(`TID`,`SchemaName`,`TableName`,`ENGINE`,`CREATE_OPTIONS`,`TABLE_COMMENT`,`PrimaryKeys`) VALUES(27,'remp_sys_ch','sys_memberserviceorg','InnoDB','','','`ServiceOrgID`');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(27,'ServiceOrgID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','PRI',1,'','remp_sys_ch','sys_memberserviceorg');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(27,'ServiceID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','MUL',0,'','remp_sys_ch','sys_memberserviceorg');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(27,'OrgID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'','remp_sys_ch','sys_memberserviceorg');

-- 表:sys_memberserviceorgchg
INSERT INTO `dbv_table`(`TID`,`SchemaName`,`TableName`,`ENGINE`,`CREATE_OPTIONS`,`TABLE_COMMENT`,`PrimaryKeys`) VALUES(28,'remp_sys_ch','sys_memberserviceorgchg','InnoDB','','','`ServiceChgID`');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(28,'ServiceChgID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','PRI',1,'','remp_sys_ch','sys_memberserviceorgchg');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(28,'ServiceID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'','remp_sys_ch','sys_memberserviceorgchg');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(28,'OrgID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'','remp_sys_ch','sys_memberserviceorgchg');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(28,'SCID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'套餐编号','remp_sys_ch','sys_memberserviceorgchg');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(28,'BeginDate',NULL,1,'date',NULL,NULL,NULL,NULL,NULL,NULL,'date','',0,'','remp_sys_ch','sys_memberserviceorgchg');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(28,'Status',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_memberserviceorgchg');

-- 表:sys_menufunction
INSERT INTO `dbv_table`(`TID`,`SchemaName`,`TableName`,`ENGINE`,`CREATE_OPTIONS`,`TABLE_COMMENT`,`PrimaryKeys`) VALUES(29,'remp_sys_ch','sys_menufunction','InnoDB','','','`MenuFuncID`');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(29,'MenuFuncID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','PRI',1,'','remp_sys_ch','sys_menufunction');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(29,'MenuID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'','remp_sys_ch','sys_menufunction');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(29,'FuncID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'','remp_sys_ch','sys_menufunction');

-- 表:sys_menuinfo
INSERT INTO `dbv_table`(`TID`,`SchemaName`,`TableName`,`ENGINE`,`CREATE_OPTIONS`,`TABLE_COMMENT`,`PrimaryKeys`) VALUES(30,'remp_sys_ch','sys_menuinfo','InnoDB','','','`MenuID`');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(30,'MenuID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','PRI',1,'','remp_sys_ch','sys_menuinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(30,'MenuName',NULL,0,'varchar',50,150,NULL,NULL,'utf8','utf8_general_ci','varchar(50)','',0,'','remp_sys_ch','sys_menuinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(30,'ParentID','0',0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'','remp_sys_ch','sys_menuinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(30,'MenuURL',NULL,1,'varchar',200,600,NULL,NULL,'utf8','utf8_general_ci','varchar(200)','',0,'','remp_sys_ch','sys_menuinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(30,'MenuOrder',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_menuinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(30,'MenuIcon',NULL,1,'varchar',200,600,NULL,NULL,'utf8','utf8_general_ci','varchar(200)','',0,'','remp_sys_ch','sys_menuinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(30,'MenuType',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_menuinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(30,'IsSystem',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_menuinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(30,'Invalid',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_menuinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(30,'IsMember',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_menuinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(30,'FuncKind',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_menuinfo');

-- 表:sys_nation
INSERT INTO `dbv_table`(`TID`,`SchemaName`,`TableName`,`ENGINE`,`CREATE_OPTIONS`,`TABLE_COMMENT`,`PrimaryKeys`) VALUES(31,'remp_sys_ch','sys_nation','InnoDB','','','`NationID`');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(31,'NationID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','PRI',1,'????','remp_sys_ch','sys_nation');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(31,'NationName',NULL,0,'varchar',20,60,NULL,NULL,'utf8','utf8_general_ci','varchar(20)','UNI',0,'????','remp_sys_ch','sys_nation');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(31,'EnglishName',NULL,0,'varchar',40,120,NULL,NULL,'utf8','utf8_general_ci','varchar(40)','UNI',0,'','remp_sys_ch','sys_nation');

-- 表:sys_operatelog
INSERT INTO `dbv_table`(`TID`,`SchemaName`,`TableName`,`ENGINE`,`CREATE_OPTIONS`,`TABLE_COMMENT`,`PrimaryKeys`) VALUES(32,'remp_sys_ch','sys_operatelog','InnoDB','','','`LogID`');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(32,'LogID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','PRI',1,'','remp_sys_ch','sys_operatelog');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(32,'UserID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'','remp_sys_ch','sys_operatelog');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(32,'FuncID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'','remp_sys_ch','sys_operatelog');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(32,'Operate',NULL,0,'varchar',200,600,NULL,NULL,'utf8','utf8_general_ci','varchar(200)','',0,'','remp_sys_ch','sys_operatelog');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(32,'LogTime',NULL,0,'datetime',NULL,NULL,NULL,NULL,NULL,NULL,'datetime','',0,'','remp_sys_ch','sys_operatelog');

-- 表:sys_operateloghis
INSERT INTO `dbv_table`(`TID`,`SchemaName`,`TableName`,`ENGINE`,`CREATE_OPTIONS`,`TABLE_COMMENT`,`PrimaryKeys`) VALUES(33,'remp_sys_ch','sys_operateloghis','InnoDB','','','`LogID`');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(33,'LogID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','PRI',0,'','remp_sys_ch','sys_operateloghis');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(33,'UserID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'','remp_sys_ch','sys_operateloghis');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(33,'FuncID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'','remp_sys_ch','sys_operateloghis');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(33,'Operate',NULL,0,'varchar',200,600,NULL,NULL,'utf8','utf8_general_ci','varchar(200)','',0,'','remp_sys_ch','sys_operateloghis');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(33,'LogTime',NULL,0,'datetime',NULL,NULL,NULL,NULL,NULL,NULL,'datetime','',0,'','remp_sys_ch','sys_operateloghis');

-- 表:sys_organizationinfo
INSERT INTO `dbv_table`(`TID`,`SchemaName`,`TableName`,`ENGINE`,`CREATE_OPTIONS`,`TABLE_COMMENT`,`PrimaryKeys`) VALUES(34,'remp_sys_ch','sys_organizationinfo','InnoDB','','组织信息表','`OrgID`');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(34,'OrgID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','PRI',1,'组织编码','remp_sys_ch','sys_organizationinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(34,'OrgName',NULL,0,'varchar',60,180,NULL,NULL,'utf8','utf8_general_ci','varchar(60)','',0,'组织名称','remp_sys_ch','sys_organizationinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(34,'OrgFullName',NULL,0,'varchar',100,300,NULL,NULL,'utf8','utf8_general_ci','varchar(100)','',0,'','remp_sys_ch','sys_organizationinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(34,'OrgType',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'组织类型','remp_sys_ch','sys_organizationinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(34,'ParentOrgID','0',0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','MUL',0,'上级组织','remp_sys_ch','sys_organizationinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(34,'Address',NULL,1,'varchar',100,300,NULL,NULL,'utf8','utf8_general_ci','varchar(100)','',0,'联系地址','remp_sys_ch','sys_organizationinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(34,'Phone',NULL,1,'varchar',30,90,NULL,NULL,'utf8','utf8_general_ci','varchar(30)','',0,'联系电话','remp_sys_ch','sys_organizationinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(34,'Contact',NULL,1,'varchar',20,60,NULL,NULL,'utf8','utf8_general_ci','varchar(20)','',0,'联系人员','remp_sys_ch','sys_organizationinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(34,'FaxNO',NULL,1,'varchar',30,90,NULL,NULL,'utf8','utf8_general_ci','varchar(30)','',0,'传真号码','remp_sys_ch','sys_organizationinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(34,'Email',NULL,1,'varchar',30,90,NULL,NULL,'utf8','utf8_general_ci','varchar(30)','',0,'电子邮件','remp_sys_ch','sys_organizationinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(34,'Description',NULL,1,'varchar',200,600,NULL,NULL,'utf8','utf8_general_ci','varchar(200)','',0,'组织描述','remp_sys_ch','sys_organizationinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(34,'Logofile',NULL,1,'varchar',200,600,NULL,NULL,'utf8','utf8_general_ci','varchar(200)','',0,'组织logo存放路径','remp_sys_ch','sys_organizationinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(34,'AreaID',NULL,1,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'行政区域','remp_sys_ch','sys_organizationinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(34,'DispOrder',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'显示顺序，同级组织机构节点的显示顺序','remp_sys_ch','sys_organizationinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(34,'Invalid',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'数据是否有效','remp_sys_ch','sys_organizationinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(34,'OrgPath',NULL,0,'varchar',300,900,NULL,NULL,'utf8','utf8_general_ci','varchar(300)','MUL',0,'访问路径','remp_sys_ch','sys_organizationinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(34,'BannerFile',NULL,1,'varchar',200,600,NULL,NULL,'utf8','utf8_general_ci','varchar(200)','',0,'Banner的存放路径','remp_sys_ch','sys_organizationinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(34,'OrgKind',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'单位类型','remp_sys_ch','sys_organizationinfo');

-- 表:sys_patientinfo
INSERT INTO `dbv_table`(`TID`,`SchemaName`,`TableName`,`ENGINE`,`CREATE_OPTIONS`,`TABLE_COMMENT`,`PrimaryKeys`) VALUES(35,'remp_sys_ch','sys_patientinfo','InnoDB','','','`PatientID`');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(35,'PatientID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','PRI',1,'','remp_sys_ch','sys_patientinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(35,'PatientName',NULL,0,'varchar',60,180,NULL,NULL,'utf8','utf8_general_ci','varchar(60)','',0,'','remp_sys_ch','sys_patientinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(35,'FirstName',NULL,1,'varchar',30,90,NULL,NULL,'utf8','utf8_general_ci','varchar(30)','',0,'','remp_sys_ch','sys_patientinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(35,'LastName',NULL,1,'varchar',30,90,NULL,NULL,'utf8','utf8_general_ci','varchar(30)','',0,'','remp_sys_ch','sys_patientinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(35,'MemberID',NULL,1,'varchar',30,90,NULL,NULL,'utf8','utf8_general_ci','varchar(30)','',0,'','remp_sys_ch','sys_patientinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(35,'TreatmentID',NULL,1,'varchar',30,90,NULL,NULL,'utf8','utf8_general_ci','varchar(30)','',0,'','remp_sys_ch','sys_patientinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(35,'Password',NULL,0,'varchar',64,192,NULL,NULL,'utf8','utf8_general_ci','varchar(64)','',0,'','remp_sys_ch','sys_patientinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(35,'PatientSex',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_patientinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(35,'Birthday',NULL,1,'date',NULL,NULL,NULL,NULL,NULL,NULL,'date','',0,'','remp_sys_ch','sys_patientinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(35,'MaritalStatus',NULL,1,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_patientinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(35,'Nationality',NULL,0,'varchar',20,60,NULL,NULL,'utf8','utf8_general_ci','varchar(20)','',0,'','remp_sys_ch','sys_patientinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(35,'AreaID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'','remp_sys_ch','sys_patientinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(35,'Nation',NULL,1,'varchar',20,60,NULL,NULL,'utf8','utf8_general_ci','varchar(20)','',0,'','remp_sys_ch','sys_patientinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(35,'BornPlace',NULL,1,'varchar',20,60,NULL,NULL,'utf8','utf8_general_ci','varchar(20)','',0,'','remp_sys_ch','sys_patientinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(35,'Education',NULL,1,'varchar',10,30,NULL,NULL,'utf8','utf8_general_ci','varchar(10)','',0,'','remp_sys_ch','sys_patientinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(35,'CredNO',NULL,1,'varchar',20,60,NULL,NULL,'utf8','utf8_general_ci','varchar(20)','MUL',0,'','remp_sys_ch','sys_patientinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(35,'CredType',NULL,1,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_patientinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(35,'MemberLevel',NULL,1,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_patientinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(35,'MemberType',NULL,1,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_patientinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(35,'MemberGroup',NULL,1,'varchar',30,90,NULL,NULL,'utf8','utf8_general_ci','varchar(30)','',0,'','remp_sys_ch','sys_patientinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(35,'Address',NULL,1,'varchar',100,300,NULL,NULL,'utf8','utf8_general_ci','varchar(100)','',0,'','remp_sys_ch','sys_patientinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(35,'Postalcode',NULL,1,'varchar',8,24,NULL,NULL,'utf8','utf8_general_ci','varchar(8)','',0,'','remp_sys_ch','sys_patientinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(35,'Urgency1',NULL,1,'varchar',20,60,NULL,NULL,'utf8','utf8_general_ci','varchar(20)','',0,'','remp_sys_ch','sys_patientinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(35,'UrgentPhone1',NULL,1,'varchar',20,60,NULL,NULL,'utf8','utf8_general_ci','varchar(20)','',0,'','remp_sys_ch','sys_patientinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(35,'Urgency2',NULL,1,'varchar',20,60,NULL,NULL,'utf8','utf8_general_ci','varchar(20)','',0,'','remp_sys_ch','sys_patientinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(35,'UrgentPhone2',NULL,1,'varchar',20,60,NULL,NULL,'utf8','utf8_general_ci','varchar(20)','',0,'','remp_sys_ch','sys_patientinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(35,'Phone',NULL,1,'varchar',30,90,NULL,NULL,'utf8','utf8_general_ci','varchar(30)','',0,'','remp_sys_ch','sys_patientinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(35,'Mobile',NULL,1,'varchar',30,90,NULL,NULL,'utf8','utf8_general_ci','varchar(30)','',0,'','remp_sys_ch','sys_patientinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(35,'Phone2',NULL,1,'varchar',30,90,NULL,NULL,'utf8','utf8_general_ci','varchar(30)','',0,'联系电话','remp_sys_ch','sys_patientinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(35,'WorkPhone',NULL,1,'varchar',20,60,NULL,NULL,'utf8','utf8_general_ci','varchar(20)','',0,'','remp_sys_ch','sys_patientinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(35,'Email',NULL,1,'varchar',30,90,NULL,NULL,'utf8','utf8_general_ci','varchar(30)','',0,'','remp_sys_ch','sys_patientinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(35,'WorkOrg',NULL,1,'varchar',50,150,NULL,NULL,'utf8','utf8_general_ci','varchar(50)','',0,'','remp_sys_ch','sys_patientinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(35,'OrgAddress',NULL,1,'varchar',128,384,NULL,NULL,'utf8','utf8_general_ci','varchar(128)','',0,'单位地址','remp_sys_ch','sys_patientinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(35,'Postalcode2',NULL,1,'varchar',8,24,NULL,NULL,'utf8','utf8_general_ci','varchar(8)','',0,'单位邮编','remp_sys_ch','sys_patientinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(35,'Job',NULL,1,'varchar',20,60,NULL,NULL,'utf8','utf8_general_ci','varchar(20)','',0,'','remp_sys_ch','sys_patientinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(35,'ContractNO',NULL,1,'varchar',40,120,NULL,NULL,'utf8','utf8_general_ci','varchar(40)','',0,'合同编号','remp_sys_ch','sys_patientinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(35,'Creator',NULL,0,'varchar',30,90,NULL,NULL,'utf8','utf8_general_ci','varchar(30)','',0,'','remp_sys_ch','sys_patientinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(35,'Brief',NULL,1,'varchar',512,1536,NULL,NULL,'utf8','utf8_general_ci','varchar(512)','',0,'备注','remp_sys_ch','sys_patientinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(35,'PayType',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_patientinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(35,'IsEnable',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_patientinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(35,'Invalid',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_patientinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(35,'RegDate',NULL,0,'date',NULL,NULL,NULL,NULL,NULL,NULL,'date','',0,'','remp_sys_ch','sys_patientinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(35,'CharTrait',NULL,1,'varchar',30,90,NULL,NULL,'utf8','utf8_general_ci','varchar(30)','',0,'性格特征','remp_sys_ch','sys_patientinfo');

-- 表:sys_patientinfoq
INSERT INTO `dbv_table`(`TID`,`SchemaName`,`TableName`,`ENGINE`,`CREATE_OPTIONS`,`TABLE_COMMENT`,`PrimaryKeys`) VALUES(36,'remp_sys_ch','sys_patientinfoq','InnoDB','','','`PatientID`');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(36,'PatientID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','PRI',0,'','remp_sys_ch','sys_patientinfoq');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(36,'PatientName',NULL,0,'varchar',60,180,NULL,NULL,'utf8','utf8_general_ci','varchar(60)','',0,'','remp_sys_ch','sys_patientinfoq');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(36,'PatientSex',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_patientinfoq');

-- 表:sys_patientorgrel
INSERT INTO `dbv_table`(`TID`,`SchemaName`,`TableName`,`ENGINE`,`CREATE_OPTIONS`,`TABLE_COMMENT`,`PrimaryKeys`) VALUES(37,'remp_sys_ch','sys_patientorgrel','InnoDB','','','`PatientOrgID`');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(37,'PatientOrgID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','PRI',1,'','remp_sys_ch','sys_patientorgrel');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(37,'PatientID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'','remp_sys_ch','sys_patientorgrel');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(37,'OrgID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'','remp_sys_ch','sys_patientorgrel');

-- 表:sys_patientphonehisinfo
INSERT INTO `dbv_table`(`TID`,`SchemaName`,`TableName`,`ENGINE`,`CREATE_OPTIONS`,`TABLE_COMMENT`,`PrimaryKeys`) VALUES(38,'remp_sys_ch','sys_patientphonehisinfo','InnoDB','','','`PatientPhoneID`');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(38,'PatientPhoneID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','PRI',0,'','remp_sys_ch','sys_patientphonehisinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(38,'PatientID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'','remp_sys_ch','sys_patientphonehisinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(38,'PhoneID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'','remp_sys_ch','sys_patientphonehisinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(38,'StartDate',NULL,0,'date',NULL,NULL,NULL,NULL,NULL,NULL,'date','',0,'','remp_sys_ch','sys_patientphonehisinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(38,'EndDate',NULL,0,'date',NULL,NULL,NULL,NULL,NULL,NULL,'date','',0,'','remp_sys_ch','sys_patientphonehisinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(38,'Creator',NULL,0,'varchar',30,90,NULL,NULL,'utf8','utf8_general_ci','varchar(30)','',0,'','remp_sys_ch','sys_patientphonehisinfo');

-- 表:sys_patientphoneinfo
INSERT INTO `dbv_table`(`TID`,`SchemaName`,`TableName`,`ENGINE`,`CREATE_OPTIONS`,`TABLE_COMMENT`,`PrimaryKeys`) VALUES(39,'remp_sys_ch','sys_patientphoneinfo','InnoDB','','','`PatientPhoneID`');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(39,'PatientPhoneID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','PRI',1,'','remp_sys_ch','sys_patientphoneinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(39,'PatientID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'','remp_sys_ch','sys_patientphoneinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(39,'PhoneID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'','remp_sys_ch','sys_patientphoneinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(39,'StartDate',NULL,0,'date',NULL,NULL,NULL,NULL,NULL,NULL,'date','',0,'','remp_sys_ch','sys_patientphoneinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(39,'Creator',NULL,0,'varchar',30,90,NULL,NULL,'utf8','utf8_general_ci','varchar(30)','',0,'','remp_sys_ch','sys_patientphoneinfo');

-- 表:sys_patientrelationinfo
INSERT INTO `dbv_table`(`TID`,`SchemaName`,`TableName`,`ENGINE`,`CREATE_OPTIONS`,`TABLE_COMMENT`,`PrimaryKeys`) VALUES(40,'remp_sys_ch','sys_patientrelationinfo','InnoDB','','','`RelationtID`');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(40,'RelationtID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','PRI',1,'','remp_sys_ch','sys_patientrelationinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(40,'PatientID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'','remp_sys_ch','sys_patientrelationinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(40,'PatientRelID',NULL,1,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'','remp_sys_ch','sys_patientrelationinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(40,'PatientRelName',NULL,0,'varchar',30,90,NULL,NULL,'utf8','utf8_general_ci','varchar(30)','',0,'','remp_sys_ch','sys_patientrelationinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(40,'PatientRelation',NULL,1,'varchar',10,30,NULL,NULL,'utf8','utf8_general_ci','varchar(10)','',0,'','remp_sys_ch','sys_patientrelationinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(40,'Mobile',NULL,0,'varchar',20,60,NULL,NULL,'utf8','utf8_general_ci','varchar(20)','',0,'','remp_sys_ch','sys_patientrelationinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(40,'Phone',NULL,1,'varchar',20,60,NULL,NULL,'utf8','utf8_general_ci','varchar(20)','',0,'','remp_sys_ch','sys_patientrelationinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(40,'CheckStatus',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'审核标志','remp_sys_ch','sys_patientrelationinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(40,'Invalid',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_patientrelationinfo');

-- 表:sys_phonecollectorhisinfo
INSERT INTO `dbv_table`(`TID`,`SchemaName`,`TableName`,`ENGINE`,`CREATE_OPTIONS`,`TABLE_COMMENT`,`PrimaryKeys`) VALUES(41,'remp_sys_ch','sys_phonecollectorhisinfo','InnoDB','','','`PhoneColID`');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(41,'PhoneColID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','PRI',0,'','remp_sys_ch','sys_phonecollectorhisinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(41,'PhoneID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'','remp_sys_ch','sys_phonecollectorhisinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(41,'CollectorID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'','remp_sys_ch','sys_phonecollectorhisinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(41,'AppID',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_phonecollectorhisinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(41,'StartDate',NULL,0,'date',NULL,NULL,NULL,NULL,NULL,NULL,'date','',0,'','remp_sys_ch','sys_phonecollectorhisinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(41,'EndDate',NULL,0,'date',NULL,NULL,NULL,NULL,NULL,NULL,'date','',0,'','remp_sys_ch','sys_phonecollectorhisinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(41,'Creator',NULL,0,'varchar',30,90,NULL,NULL,'utf8','utf8_general_ci','varchar(30)','',0,'','remp_sys_ch','sys_phonecollectorhisinfo');

-- 表:sys_phonecollectorinfo
INSERT INTO `dbv_table`(`TID`,`SchemaName`,`TableName`,`ENGINE`,`CREATE_OPTIONS`,`TABLE_COMMENT`,`PrimaryKeys`) VALUES(42,'remp_sys_ch','sys_phonecollectorinfo','InnoDB','','','`PhoneColID`');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(42,'PhoneColID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','PRI',1,'','remp_sys_ch','sys_phonecollectorinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(42,'PhoneID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'','remp_sys_ch','sys_phonecollectorinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(42,'CollectorID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'','remp_sys_ch','sys_phonecollectorinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(42,'AppID',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_phonecollectorinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(42,'StartDate',NULL,0,'date',NULL,NULL,NULL,NULL,NULL,NULL,'date','',0,'','remp_sys_ch','sys_phonecollectorinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(42,'Creator',NULL,0,'varchar',30,90,NULL,NULL,'utf8','utf8_general_ci','varchar(30)','',0,'','remp_sys_ch','sys_phonecollectorinfo');

-- 表:sys_phoneinfo
INSERT INTO `dbv_table`(`TID`,`SchemaName`,`TableName`,`ENGINE`,`CREATE_OPTIONS`,`TABLE_COMMENT`,`PrimaryKeys`) VALUES(43,'remp_sys_ch','sys_phoneinfo','InnoDB','','','`PhoneID`');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(43,'PhoneID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','PRI',1,'','remp_sys_ch','sys_phoneinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(43,'PhoneCode',NULL,0,'varchar',30,90,NULL,NULL,'utf8','utf8_general_ci','varchar(30)','UNI',0,'','remp_sys_ch','sys_phoneinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(43,'Type',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_phoneinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(43,'BlueTooth',NULL,1,'varchar',30,90,NULL,NULL,'utf8','utf8_general_ci','varchar(30)','',0,'','remp_sys_ch','sys_phoneinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(43,'ImeiAddr',NULL,1,'varchar',40,120,NULL,NULL,'utf8','utf8_general_ci','varchar(40)','',0,'','remp_sys_ch','sys_phoneinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(43,'Description',NULL,1,'varchar',60,180,NULL,NULL,'utf8','utf8_general_ci','varchar(60)','',0,'','remp_sys_ch','sys_phoneinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(43,'Status',NULL,1,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_phoneinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(43,'OrgID',NULL,1,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','MUL',0,'','remp_sys_ch','sys_phoneinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(43,'Creator',NULL,0,'varchar',30,90,NULL,NULL,'utf8','utf8_general_ci','varchar(30)','',0,'','remp_sys_ch','sys_phoneinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(43,'Invalid',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_phoneinfo');

-- 表:sys_phonenotify
INSERT INTO `dbv_table`(`TID`,`SchemaName`,`TableName`,`ENGINE`,`CREATE_OPTIONS`,`TABLE_COMMENT`,`PrimaryKeys`) VALUES(44,'remp_sys_ch','sys_phonenotify','InnoDB','','','`NotifyId`');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(44,'NotifyId',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','PRI',1,'','remp_sys_ch','sys_phonenotify');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(44,'UserID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'','remp_sys_ch','sys_phonenotify');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(44,'AppId',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_phonenotify');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(44,'NotifyType',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_phonenotify');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(44,'Content',NULL,0,'varchar',300,900,NULL,NULL,'utf8','utf8_general_ci','varchar(300)','',0,'','remp_sys_ch','sys_phonenotify');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(44,'LastUpdate',NULL,0,'datetime',NULL,NULL,NULL,NULL,NULL,NULL,'datetime','',0,'','remp_sys_ch','sys_phonenotify');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(44,'Creator',NULL,1,'varchar',30,90,NULL,NULL,'utf8','utf8_general_ci','varchar(30)','',0,'','remp_sys_ch','sys_phonenotify');

-- 表:sys_roledrinfo
INSERT INTO `dbv_table`(`TID`,`SchemaName`,`TableName`,`ENGINE`,`CREATE_OPTIONS`,`TABLE_COMMENT`,`PrimaryKeys`) VALUES(45,'remp_sys_ch','sys_roledrinfo','InnoDB','','','`RoleDrID`');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(45,'RoleDrID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','PRI',1,'','remp_sys_ch','sys_roledrinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(45,'RoleID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','MUL',0,'','remp_sys_ch','sys_roledrinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(45,'DrID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'','remp_sys_ch','sys_roledrinfo');

-- 表:sys_rolefuncinfo
INSERT INTO `dbv_table`(`TID`,`SchemaName`,`TableName`,`ENGINE`,`CREATE_OPTIONS`,`TABLE_COMMENT`,`PrimaryKeys`) VALUES(46,'remp_sys_ch','sys_rolefuncinfo','InnoDB','avg_row_length=50','','`RoleFuncID`');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(46,'RoleFuncID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','PRI',1,'','remp_sys_ch','sys_rolefuncinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(46,'RoleID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','MUL',0,'','remp_sys_ch','sys_rolefuncinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(46,'FuncID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','MUL',0,'','remp_sys_ch','sys_rolefuncinfo');

-- 表:sys_roleinfo
INSERT INTO `dbv_table`(`TID`,`SchemaName`,`TableName`,`ENGINE`,`CREATE_OPTIONS`,`TABLE_COMMENT`,`PrimaryKeys`) VALUES(47,'remp_sys_ch','sys_roleinfo','InnoDB','','','`RoleID`');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(47,'RoleID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','PRI',1,'','remp_sys_ch','sys_roleinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(47,'RoleName',NULL,0,'varchar',20,60,NULL,NULL,'utf8','utf8_general_ci','varchar(20)','MUL',0,'','remp_sys_ch','sys_roleinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(47,'RoleDescription',NULL,1,'varchar',200,600,NULL,NULL,'utf8','utf8_general_ci','varchar(200)','',0,'','remp_sys_ch','sys_roleinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(47,'OrgID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'','remp_sys_ch','sys_roleinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(47,'IsSystem',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_roleinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(47,'RoleType',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_roleinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(47,'Creator',NULL,0,'varchar',30,90,NULL,NULL,'utf8','utf8_general_ci','varchar(30)','',0,'','remp_sys_ch','sys_roleinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(47,'Invalid',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_roleinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(47,'FuncKind',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_roleinfo');

-- 表:sys_rolemenuinfo
INSERT INTO `dbv_table`(`TID`,`SchemaName`,`TableName`,`ENGINE`,`CREATE_OPTIONS`,`TABLE_COMMENT`,`PrimaryKeys`) VALUES(48,'remp_sys_ch','sys_rolemenuinfo','InnoDB','','','`RoleMenuID`');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(48,'RoleMenuID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','PRI',1,'','remp_sys_ch','sys_rolemenuinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(48,'RoleID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'','remp_sys_ch','sys_rolemenuinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(48,'MenuID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'','remp_sys_ch','sys_rolemenuinfo');

-- 表:sys_rolepatientinfo
INSERT INTO `dbv_table`(`TID`,`SchemaName`,`TableName`,`ENGINE`,`CREATE_OPTIONS`,`TABLE_COMMENT`,`PrimaryKeys`) VALUES(49,'remp_sys_ch','sys_rolepatientinfo','InnoDB','','','`RolePatientID`');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(49,'RolePatientID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','PRI',1,'','remp_sys_ch','sys_rolepatientinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(49,'RoleID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'','remp_sys_ch','sys_rolepatientinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(49,'PatientID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'','remp_sys_ch','sys_rolepatientinfo');

-- 表:sys_roleserviceinfo
INSERT INTO `dbv_table`(`TID`,`SchemaName`,`TableName`,`ENGINE`,`CREATE_OPTIONS`,`TABLE_COMMENT`,`PrimaryKeys`) VALUES(50,'remp_sys_ch','sys_roleserviceinfo','InnoDB','','','`RoleServiceID`');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(50,'RoleServiceID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','PRI',1,'关系编码','remp_sys_ch','sys_roleserviceinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(50,'RoleID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'角色编码','remp_sys_ch','sys_roleserviceinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(50,'ServiceItemID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'监护类型编码','remp_sys_ch','sys_roleserviceinfo');

-- 表:sys_servicecomboinfo
INSERT INTO `dbv_table`(`TID`,`SchemaName`,`TableName`,`ENGINE`,`CREATE_OPTIONS`,`TABLE_COMMENT`,`PrimaryKeys`) VALUES(51,'remp_sys_ch','sys_servicecomboinfo','InnoDB','','','`SCID`');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(51,'SCID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','PRI',1,'套餐编号','remp_sys_ch','sys_servicecomboinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(51,'SCName',NULL,0,'varchar',30,90,NULL,NULL,'utf8','utf8_general_ci','varchar(30)','',0,'套餐名称','remp_sys_ch','sys_servicecomboinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(51,'CreateDate',NULL,0,'date',NULL,NULL,NULL,NULL,NULL,NULL,'date','',0,'创建时间','remp_sys_ch','sys_servicecomboinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(51,'Creator',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'创建人','remp_sys_ch','sys_servicecomboinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(51,'Brief',NULL,1,'varchar',1024,3072,NULL,NULL,'utf8','utf8_general_ci','varchar(1024)','',0,'备注','remp_sys_ch','sys_servicecomboinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(51,'Status',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'状态','remp_sys_ch','sys_servicecomboinfo');

-- 表:sys_servicedetailsinfo
INSERT INTO `dbv_table`(`TID`,`SchemaName`,`TableName`,`ENGINE`,`CREATE_OPTIONS`,`TABLE_COMMENT`,`PrimaryKeys`) VALUES(52,'remp_sys_ch','sys_servicedetailsinfo','InnoDB','','','`ServiceItemID`');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(52,'ServiceItemID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','PRI',1,'项目编号','remp_sys_ch','sys_servicedetailsinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(52,'ServiceLevelID',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'服务大类编号','remp_sys_ch','sys_servicedetailsinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(52,'ServiceLevelII',NULL,0,'varchar',60,180,NULL,NULL,'utf8','utf8_general_ci','varchar(60)','',0,'服务小类名称','remp_sys_ch','sys_servicedetailsinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(52,'Frequency',NULL,1,'varchar',60,180,NULL,NULL,'utf8','utf8_general_ci','varchar(60)','',0,'服务频次','remp_sys_ch','sys_servicedetailsinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(52,'Standard',NULL,1,'varchar',256,768,NULL,NULL,'utf8','utf8_general_ci','varchar(256)','',0,'服务标准','remp_sys_ch','sys_servicedetailsinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(52,'Price',NULL,1,'varchar',60,180,NULL,NULL,'utf8','utf8_general_ci','varchar(60)','',0,'服务价格','remp_sys_ch','sys_servicedetailsinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(52,'Content',NULL,1,'varchar',512,1536,NULL,NULL,'utf8','utf8_general_ci','varchar(512)','',0,'服务内容','remp_sys_ch','sys_servicedetailsinfo');

-- 表:sys_smsauth
INSERT INTO `dbv_table`(`TID`,`SchemaName`,`TableName`,`ENGINE`,`CREATE_OPTIONS`,`TABLE_COMMENT`,`PrimaryKeys`) VALUES(53,'remp_sys_ch','sys_smsauth','InnoDB','','','`AuthID`');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(53,'AuthID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','PRI',1,'流水号','remp_sys_ch','sys_smsauth');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(53,'RegTime',NULL,0,'datetime',NULL,NULL,NULL,NULL,NULL,NULL,'datetime','',0,'登记时间','remp_sys_ch','sys_smsauth');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(53,'UCNum',NULL,0,'varchar',30,90,NULL,NULL,'utf8','utf8_general_ci','varchar(30)','',0,'短信账户','remp_sys_ch','sys_smsauth');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(53,'ConnID',NULL,0,'varchar',30,90,NULL,NULL,'utf8','utf8_general_ci','varchar(30)','',0,'连接ID','remp_sys_ch','sys_smsauth');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(53,'Status','0',0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'登记状态，0设置中，1生效，2失效，3异常','remp_sys_ch','sys_smsauth');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(53,'GWTime',NULL,0,'datetime',NULL,NULL,NULL,NULL,NULL,NULL,'datetime','',0,'回调时间','remp_sys_ch','sys_smsauth');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(53,'GWRet',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'0成功，<0失败码，-1接收失败','remp_sys_ch','sys_smsauth');

-- 表:sys_smshisinfo
INSERT INTO `dbv_table`(`TID`,`SchemaName`,`TableName`,`ENGINE`,`CREATE_OPTIONS`,`TABLE_COMMENT`,`PrimaryKeys`) VALUES(54,'remp_sys_ch','sys_smshisinfo','InnoDB','','','`SMSId`');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(54,'SMSId',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','PRI',0,'','remp_sys_ch','sys_smshisinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(54,'SendType',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_smshisinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(54,'AppID',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_smshisinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(54,'Content',NULL,1,'varchar',200,600,NULL,NULL,'utf8','utf8_general_ci','varchar(200)','',0,'','remp_sys_ch','sys_smshisinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(54,'SendTime',NULL,1,'datetime',NULL,NULL,NULL,NULL,NULL,NULL,'datetime','',0,'','remp_sys_ch','sys_smshisinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(54,'Status',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_smshisinfo');

-- 表:sys_smsinfo
INSERT INTO `dbv_table`(`TID`,`SchemaName`,`TableName`,`ENGINE`,`CREATE_OPTIONS`,`TABLE_COMMENT`,`PrimaryKeys`) VALUES(55,'remp_sys_ch','sys_smsinfo','InnoDB','','','`SMSId`');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(55,'SMSId',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','PRI',1,'','remp_sys_ch','sys_smsinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(55,'SendType',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_smsinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(55,'AppID',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_smsinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(55,'Content',NULL,1,'varchar',200,600,NULL,NULL,'utf8','utf8_general_ci','varchar(200)','',0,'','remp_sys_ch','sys_smsinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(55,'SendTime',NULL,1,'datetime',NULL,NULL,NULL,NULL,NULL,NULL,'datetime','',0,'','remp_sys_ch','sys_smsinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(55,'Status',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_smsinfo');

-- 表:sys_smsreceive
INSERT INTO `dbv_table`(`TID`,`SchemaName`,`TableName`,`ENGINE`,`CREATE_OPTIONS`,`TABLE_COMMENT`,`PrimaryKeys`) VALUES(56,'remp_sys_ch','sys_smsreceive','InnoDB','','','`RecvID`');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(56,'RecvID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','PRI',1,'流水号','remp_sys_ch','sys_smsreceive');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(56,'SendTime',NULL,0,'datetime',NULL,NULL,NULL,NULL,NULL,NULL,'datetime','',0,'发送时间','remp_sys_ch','sys_smsreceive');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(56,'SendTel',NULL,0,'varchar',30,90,NULL,NULL,'utf8','utf8_general_ci','varchar(30)','',0,'发送者手机号','remp_sys_ch','sys_smsreceive');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(56,'Content',NULL,0,'varchar',700,2100,NULL,NULL,'utf8','utf8_general_ci','varchar(700)','',0,'发送内容','remp_sys_ch','sys_smsreceive');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(56,'UCNum',NULL,0,'varchar',30,90,NULL,NULL,'utf8','utf8_general_ci','varchar(30)','',0,'短信账户','remp_sys_ch','sys_smsreceive');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(56,'GWTime',NULL,0,'datetime',NULL,NULL,NULL,NULL,NULL,NULL,'datetime','',0,'回调时间','remp_sys_ch','sys_smsreceive');

-- 表:sys_smssendtohisinfo
INSERT INTO `dbv_table`(`TID`,`SchemaName`,`TableName`,`ENGINE`,`CREATE_OPTIONS`,`TABLE_COMMENT`,`PrimaryKeys`) VALUES(57,'remp_sys_ch','sys_smssendtohisinfo','InnoDB','','','`SMSSendID`');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(57,'SMSSendID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','PRI',1,'','remp_sys_ch','sys_smssendtohisinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(57,'SMSId',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'','remp_sys_ch','sys_smssendtohisinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(57,'SendTo',NULL,0,'varchar',30,90,NULL,NULL,'utf8','utf8_general_ci','varchar(30)','',0,'','remp_sys_ch','sys_smssendtohisinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(57,'UserID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'','remp_sys_ch','sys_smssendtohisinfo');

-- 表:sys_smssendtoinfo
INSERT INTO `dbv_table`(`TID`,`SchemaName`,`TableName`,`ENGINE`,`CREATE_OPTIONS`,`TABLE_COMMENT`,`PrimaryKeys`) VALUES(58,'remp_sys_ch','sys_smssendtoinfo','InnoDB','','','`SMSSendID`');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(58,'SMSSendID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','PRI',1,'','remp_sys_ch','sys_smssendtoinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(58,'SMSId',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'','remp_sys_ch','sys_smssendtoinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(58,'SendTo',NULL,0,'varchar',30,90,NULL,NULL,'utf8','utf8_general_ci','varchar(30)','',0,'','remp_sys_ch','sys_smssendtoinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(58,'UserID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'','remp_sys_ch','sys_smssendtoinfo');

-- 表:sys_smstask
INSERT INTO `dbv_table`(`TID`,`SchemaName`,`TableName`,`ENGINE`,`CREATE_OPTIONS`,`TABLE_COMMENT`,`PrimaryKeys`) VALUES(59,'remp_sys_ch','sys_smstask','InnoDB','','','`TaskID`');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(59,'TaskID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','PRI',1,'流水号','remp_sys_ch','sys_smstask');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(59,'TaskName',NULL,0,'varchar',30,90,NULL,NULL,'utf8','utf8_general_ci','varchar(30)','',0,'规则名称','remp_sys_ch','sys_smstask');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(59,'TemplateType',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'模板类型','remp_sys_ch','sys_smstask');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(59,'StartDate',NULL,0,'datetime',NULL,NULL,NULL,NULL,NULL,NULL,'datetime','',0,'起始时间','remp_sys_ch','sys_smstask');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(59,'EndDate',NULL,0,'datetime',NULL,NULL,NULL,NULL,NULL,NULL,'datetime','',0,'结束时间','remp_sys_ch','sys_smstask');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(59,'OrgID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'组织编号','remp_sys_ch','sys_smstask');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(59,'StartHour',NULL,0,'varchar',20,60,NULL,NULL,'utf8','utf8_general_ci','varchar(20)','',0,'当天起始时间段，10:30 ','remp_sys_ch','sys_smstask');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(59,'EndHour',NULL,0,'varchar',20,60,NULL,NULL,'utf8','utf8_general_ci','varchar(20)','',0,'当天结束时间段，20:30 ','remp_sys_ch','sys_smstask');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(59,'CurrentTemplateCode',NULL,0,'varchar',20,60,NULL,NULL,'utf8','utf8_general_ci','varchar(20)','',0,'当前模板编号','remp_sys_ch','sys_smstask');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(59,'CurrentRank',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'当前排序号，大于等于0','remp_sys_ch','sys_smstask');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(59,'CurrentTime',NULL,1,'datetime',NULL,NULL,NULL,NULL,NULL,NULL,'datetime','',0,'当前执行时间','remp_sys_ch','sys_smstask');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(59,'IsCyc',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'是否循环（0否，1循环）','remp_sys_ch','sys_smstask');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(59,'IntervalType',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'间隔类型（0时，1天，2周，3月，4年）','remp_sys_ch','sys_smstask');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(59,'IntervalAmount',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'间隔周期','remp_sys_ch','sys_smstask');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(59,'Invalid',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'数据是否有效','remp_sys_ch','sys_smstask');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(59,'Creator',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'创建人编码','remp_sys_ch','sys_smstask');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(59,'CreateTime',NULL,0,'datetime',NULL,NULL,NULL,NULL,NULL,NULL,'datetime','',0,'创建时间','remp_sys_ch','sys_smstask');

-- 表:sys_smstaskpatient
INSERT INTO `dbv_table`(`TID`,`SchemaName`,`TableName`,`ENGINE`,`CREATE_OPTIONS`,`TABLE_COMMENT`,`PrimaryKeys`) VALUES(60,'remp_sys_ch','sys_smstaskpatient','InnoDB','','','`TaskPatientID`');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(60,'TaskPatientID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','PRI',1,'流水号','remp_sys_ch','sys_smstaskpatient');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(60,'TaskID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'关联任务编码','remp_sys_ch','sys_smstaskpatient');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(60,'MemberID',NULL,0,'varchar',30,90,NULL,NULL,'utf8','utf8_general_ci','varchar(30)','',0,'会员编码','remp_sys_ch','sys_smstaskpatient');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(60,'ItemID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'1.	就医陪同
2.	预约等	服务项目编码
','remp_sys_ch','sys_smstaskpatient');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(60,'TaskPatientType',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'关系类型（0表示会员卡号，1表示服务项目ID）','remp_sys_ch','sys_smstaskpatient');

-- 表:sys_smstasktemplate
INSERT INTO `dbv_table`(`TID`,`SchemaName`,`TableName`,`ENGINE`,`CREATE_OPTIONS`,`TABLE_COMMENT`,`PrimaryKeys`) VALUES(61,'remp_sys_ch','sys_smstasktemplate','InnoDB','','','`TaskTemplateID`');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(61,'TaskTemplateID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','PRI',1,'流水号','remp_sys_ch','sys_smstasktemplate');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(61,'TaskID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'关联任务规则编码','remp_sys_ch','sys_smstasktemplate');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(61,'StartTemplateCode',NULL,0,'varchar',20,60,NULL,NULL,'utf8','utf8_general_ci','varchar(20)','',0,'起始模板编号Code','remp_sys_ch','sys_smstasktemplate');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(61,'EndTemplateCode',NULL,0,'varchar',20,60,NULL,NULL,'utf8','utf8_general_ci','varchar(20)','',0,'结束模板编号Code','remp_sys_ch','sys_smstasktemplate');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(61,'Rank',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'排序号，大于等于0','remp_sys_ch','sys_smstasktemplate');

-- 表:sys_smstemplate
INSERT INTO `dbv_table`(`TID`,`SchemaName`,`TableName`,`ENGINE`,`CREATE_OPTIONS`,`TABLE_COMMENT`,`PrimaryKeys`) VALUES(62,'remp_sys_ch','sys_smstemplate','InnoDB','','','`TemplateID`');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(62,'TemplateID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','PRI',1,'流水号','remp_sys_ch','sys_smstemplate');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(62,'TemplateType',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'关联sys_SMSTemplateType模板类型表','remp_sys_ch','sys_smstemplate');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(62,'TemplateName',NULL,0,'varchar',30,90,NULL,NULL,'utf8','utf8_general_ci','varchar(30)','',0,'模板名称','remp_sys_ch','sys_smstemplate');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(62,'Code',NULL,0,'varchar',20,60,NULL,NULL,'utf8','utf8_general_ci','varchar(20)','',0,'用户自定义编号','remp_sys_ch','sys_smstemplate');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(62,'Content',NULL,0,'varchar',700,2100,NULL,NULL,'utf8','utf8_general_ci','varchar(700)','',0,'模板内容','remp_sys_ch','sys_smstemplate');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(62,'Creator',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'创建人编码 医生编码','remp_sys_ch','sys_smstemplate');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(62,'CheckID',NULL,1,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'审核人编码','remp_sys_ch','sys_smstemplate');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(62,'CreateTime',NULL,0,'datetime',NULL,NULL,NULL,NULL,NULL,NULL,'datetime','',0,'创建时间','remp_sys_ch','sys_smstemplate');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(62,'CheckTime',NULL,1,'datetime',NULL,NULL,NULL,NULL,NULL,NULL,'datetime','',0,'审核日期','remp_sys_ch','sys_smstemplate');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(62,'CheckStatus',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'审核状态','remp_sys_ch','sys_smstemplate');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(62,'Remark',NULL,1,'varchar',200,600,NULL,NULL,'utf8','utf8_general_ci','varchar(200)','',0,'备注','remp_sys_ch','sys_smstemplate');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(62,'Invalid',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'数据是否有效','remp_sys_ch','sys_smstemplate');

-- 表:sys_smstemplatetype
INSERT INTO `dbv_table`(`TID`,`SchemaName`,`TableName`,`ENGINE`,`CREATE_OPTIONS`,`TABLE_COMMENT`,`PrimaryKeys`) VALUES(63,'remp_sys_ch','sys_smstemplatetype','InnoDB','','','`TemplateTypeID`');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(63,'TemplateTypeID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','PRI',1,'流水号','remp_sys_ch','sys_smstemplatetype');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(63,'TypeCode',NULL,0,'varchar',30,90,NULL,NULL,'utf8','utf8_general_ci','varchar(30)','',0,'模板类型编码','remp_sys_ch','sys_smstemplatetype');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(63,'TypeName',NULL,0,'varchar',30,90,NULL,NULL,'utf8','utf8_general_ci','varchar(30)','',0,'模板类型名称','remp_sys_ch','sys_smstemplatetype');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(63,'Invalid',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'0有效数据 默认值
1删除，无效
2 可执行物理删除	数据是否有效
','remp_sys_ch','sys_smstemplatetype');

-- 表:sys_subapprel
INSERT INTO `dbv_table`(`TID`,`SchemaName`,`TableName`,`ENGINE`,`CREATE_OPTIONS`,`TABLE_COMMENT`,`PrimaryKeys`) VALUES(64,'remp_sys_ch','sys_subapprel','InnoDB','','','`SubAppID`');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(64,'SubAppID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','PRI',1,'','remp_sys_ch','sys_subapprel');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(64,'SubID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'','remp_sys_ch','sys_subapprel');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(64,'AppID',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_subapprel');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(64,'WarnTypeID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'','remp_sys_ch','sys_subapprel');

-- 表:sys_subdrrel
INSERT INTO `dbv_table`(`TID`,`SchemaName`,`TableName`,`ENGINE`,`CREATE_OPTIONS`,`TABLE_COMMENT`,`PrimaryKeys`) VALUES(65,'remp_sys_ch','sys_subdrrel','InnoDB','','','`SubDrID`');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(65,'SubDrID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','PRI',1,'','remp_sys_ch','sys_subdrrel');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(65,'SubID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'','remp_sys_ch','sys_subdrrel');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(65,'DrID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'','remp_sys_ch','sys_subdrrel');

-- 表:sys_suborgrel
INSERT INTO `dbv_table`(`TID`,`SchemaName`,`TableName`,`ENGINE`,`CREATE_OPTIONS`,`TABLE_COMMENT`,`PrimaryKeys`) VALUES(66,'remp_sys_ch','sys_suborgrel','InnoDB','','','`SubOrgID`');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(66,'SubOrgID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','PRI',1,'','remp_sys_ch','sys_suborgrel');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(66,'SubID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'','remp_sys_ch','sys_suborgrel');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(66,'OrgID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'','remp_sys_ch','sys_suborgrel');

-- 表:sys_subscriptioninfo
INSERT INTO `dbv_table`(`TID`,`SchemaName`,`TableName`,`ENGINE`,`CREATE_OPTIONS`,`TABLE_COMMENT`,`PrimaryKeys`) VALUES(67,'remp_sys_ch','sys_subscriptioninfo','InnoDB','','','`SubID`');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(67,'SubID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','PRI',1,'','remp_sys_ch','sys_subscriptioninfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(67,'OrgID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'','remp_sys_ch','sys_subscriptioninfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(67,'SubName',NULL,0,'varchar',32,96,NULL,NULL,'utf8','utf8_general_ci','varchar(32)','',0,'','remp_sys_ch','sys_subscriptioninfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(67,'SubDescription',NULL,1,'varchar',64,192,NULL,NULL,'utf8','utf8_general_ci','varchar(64)','',0,'','remp_sys_ch','sys_subscriptioninfo');

-- 表:sys_usertoken
INSERT INTO `dbv_table`(`TID`,`SchemaName`,`TableName`,`ENGINE`,`CREATE_OPTIONS`,`TABLE_COMMENT`,`PrimaryKeys`) VALUES(68,'remp_sys_ch','sys_usertoken','InnoDB','','','`UserTokenID`');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(68,'UserTokenID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','PRI',1,'','remp_sys_ch','sys_usertoken');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(68,'UserID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'','remp_sys_ch','sys_usertoken');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(68,'RandCode',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'','remp_sys_ch','sys_usertoken');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(68,'TokenType',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_usertoken');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(68,'CreateTime',NULL,0,'datetime',NULL,NULL,NULL,NULL,NULL,NULL,'datetime','',0,'','remp_sys_ch','sys_usertoken');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(68,'LastActive',NULL,0,'datetime',NULL,NULL,NULL,NULL,NULL,NULL,'datetime','',0,'','remp_sys_ch','sys_usertoken');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(68,'Status',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_usertoken');

-- 表:sys_wardpara
INSERT INTO `dbv_table`(`TID`,`SchemaName`,`TableName`,`ENGINE`,`CREATE_OPTIONS`,`TABLE_COMMENT`,`PrimaryKeys`) VALUES(69,'remp_sys_ch','sys_wardpara','InnoDB','','','`UpdateID`');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(69,'UpdateID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','PRI',1,'','remp_sys_ch','sys_wardpara');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(69,'PatientID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'','remp_sys_ch','sys_wardpara');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(69,'AppID',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_wardpara');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(69,'UpdateTime',NULL,0,'datetime',NULL,NULL,NULL,NULL,NULL,NULL,'datetime','',0,'','remp_sys_ch','sys_wardpara');

-- 表:sys_warnconfig
INSERT INTO `dbv_table`(`TID`,`SchemaName`,`TableName`,`ENGINE`,`CREATE_OPTIONS`,`TABLE_COMMENT`,`PrimaryKeys`) VALUES(70,'remp_sys_ch','sys_warnconfig','InnoDB','','','`CFGID`');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(70,'CFGID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','PRI',1,'','remp_sys_ch','sys_warnconfig');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(70,'AppID',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_warnconfig');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(70,'WarnTypeID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'','remp_sys_ch','sys_warnconfig');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(70,'WarnLevel',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_warnconfig');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(70,'WarnTimeInterval',NULL,1,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_warnconfig');

-- 表:sys_warndrrel
INSERT INTO `dbv_table`(`TID`,`SchemaName`,`TableName`,`ENGINE`,`CREATE_OPTIONS`,`TABLE_COMMENT`,`PrimaryKeys`) VALUES(71,'remp_sys_ch','sys_warndrrel','InnoDB','','','`WarnDrID`');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(71,'WarnDrID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','PRI',1,'','remp_sys_ch','sys_warndrrel');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(71,'DrID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'','remp_sys_ch','sys_warndrrel');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(71,'WarnID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'','remp_sys_ch','sys_warndrrel');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(71,'WarnTimeInterval','0',1,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_warndrrel');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(71,'Status',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_warndrrel');

-- 表:sys_warndrrelhis
INSERT INTO `dbv_table`(`TID`,`SchemaName`,`TableName`,`ENGINE`,`CREATE_OPTIONS`,`TABLE_COMMENT`,`PrimaryKeys`) VALUES(72,'remp_sys_ch','sys_warndrrelhis','InnoDB','','','`WarnDrID`');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(72,'WarnDrID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','PRI',0,'','remp_sys_ch','sys_warndrrelhis');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(72,'DrID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'','remp_sys_ch','sys_warndrrelhis');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(72,'WarnID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'','remp_sys_ch','sys_warndrrelhis');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(72,'WarnTimeInterval','0',1,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_warndrrelhis');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(72,'Status',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_warndrrelhis');

-- 表:sys_warnhisinfo
INSERT INTO `dbv_table`(`TID`,`SchemaName`,`TableName`,`ENGINE`,`CREATE_OPTIONS`,`TABLE_COMMENT`,`PrimaryKeys`) VALUES(73,'remp_sys_ch','sys_warnhisinfo','InnoDB','','','`WarnID`');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(73,'WarnID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','PRI',0,'','remp_sys_ch','sys_warnhisinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(73,'PatientID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'','remp_sys_ch','sys_warnhisinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(73,'PatientName',NULL,0,'varchar',60,180,NULL,NULL,'utf8','utf8_general_ci','varchar(60)','',0,'','remp_sys_ch','sys_warnhisinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(73,'CreatTime',NULL,0,'datetime',NULL,NULL,NULL,NULL,NULL,NULL,'datetime','',0,'','remp_sys_ch','sys_warnhisinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(73,'WarnContent',NULL,0,'varchar',256,768,NULL,NULL,'utf8','utf8_general_ci','varchar(256)','',0,'','remp_sys_ch','sys_warnhisinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(73,'WarnTypeID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'','remp_sys_ch','sys_warnhisinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(73,'AppID',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_warnhisinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(73,'WarmLevel',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_warnhisinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(73,'WarnTimeInterval','0',1,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_warnhisinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(73,'ProcessTime',NULL,1,'datetime',NULL,NULL,NULL,NULL,NULL,NULL,'datetime','',0,'','remp_sys_ch','sys_warnhisinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(73,'DrID',NULL,1,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'','remp_sys_ch','sys_warnhisinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(73,'DrName',NULL,1,'varchar',30,90,NULL,NULL,'utf8','utf8_general_ci','varchar(30)','',0,'','remp_sys_ch','sys_warnhisinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(73,'ProcessWay',NULL,1,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_warnhisinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(73,'ProcessResult',NULL,1,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_warnhisinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(73,'InsertTime',NULL,1,'datetime',NULL,NULL,NULL,NULL,NULL,NULL,'datetime','',0,'','remp_sys_ch','sys_warnhisinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(73,'BusCustomID',NULL,1,'varchar',512,1536,NULL,NULL,'utf8','utf8_general_ci','varchar(512)','',0,'','remp_sys_ch','sys_warnhisinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(73,'DataUrl',NULL,1,'varchar',256,768,NULL,NULL,'utf8','utf8_general_ci','varchar(256)','',0,'','remp_sys_ch','sys_warnhisinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(73,'ResultRemark',NULL,1,'varchar',1024,3072,NULL,NULL,'utf8','utf8_general_ci','varchar(1024)','',0,'处理结果说明','remp_sys_ch','sys_warnhisinfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(73,'EventUrl',NULL,1,'varchar',256,768,NULL,NULL,'utf8','utf8_general_ci','varchar(256)','',0,'事件URL','remp_sys_ch','sys_warnhisinfo');

-- 表:sys_warninfo
INSERT INTO `dbv_table`(`TID`,`SchemaName`,`TableName`,`ENGINE`,`CREATE_OPTIONS`,`TABLE_COMMENT`,`PrimaryKeys`) VALUES(74,'remp_sys_ch','sys_warninfo','InnoDB','','','`WarnID`');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(74,'WarnID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','PRI',1,'','remp_sys_ch','sys_warninfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(74,'PatientID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','MUL',0,'','remp_sys_ch','sys_warninfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(74,'PatientName',NULL,0,'varchar',60,180,NULL,NULL,'utf8','utf8_general_ci','varchar(60)','',0,'','remp_sys_ch','sys_warninfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(74,'CreatTime',NULL,0,'datetime',NULL,NULL,NULL,NULL,NULL,NULL,'datetime','',0,'','remp_sys_ch','sys_warninfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(74,'WarnContent',NULL,0,'varchar',256,768,NULL,NULL,'utf8','utf8_general_ci','varchar(256)','',0,'','remp_sys_ch','sys_warninfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(74,'WarnTypeID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'','remp_sys_ch','sys_warninfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(74,'AppID',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_warninfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(74,'ProcessTime',NULL,1,'datetime',NULL,NULL,NULL,NULL,NULL,NULL,'datetime','',0,'','remp_sys_ch','sys_warninfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(74,'DrID',NULL,1,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','',0,'','remp_sys_ch','sys_warninfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(74,'DrName',NULL,1,'varchar',30,90,NULL,NULL,'utf8','utf8_general_ci','varchar(30)','',0,'','remp_sys_ch','sys_warninfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(74,'ProcessWay',NULL,1,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_warninfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(74,'ProcessResult',NULL,1,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_warninfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(74,'InsertTime',NULL,1,'datetime',NULL,NULL,NULL,NULL,NULL,NULL,'datetime','',0,'','remp_sys_ch','sys_warninfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(74,'BusCustomID',NULL,1,'varchar',512,1536,NULL,NULL,'utf8','utf8_general_ci','varchar(512)','',0,'','remp_sys_ch','sys_warninfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(74,'WarmLevel',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_warninfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(74,'WarnTimeInterval','0',1,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','',0,'','remp_sys_ch','sys_warninfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(74,'DataUrl',NULL,1,'varchar',256,768,NULL,NULL,'utf8','utf8_general_ci','varchar(256)','',0,'','remp_sys_ch','sys_warninfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(74,'ResultRemark',NULL,1,'varchar',1024,3072,NULL,NULL,'utf8','utf8_general_ci','varchar(1024)','',0,'处理结果说明','remp_sys_ch','sys_warninfo');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(74,'EventUrl',NULL,1,'varchar',256,768,NULL,NULL,'utf8','utf8_general_ci','varchar(256)','',0,'事件URL','remp_sys_ch','sys_warninfo');

-- 表:sys_warntype
INSERT INTO `dbv_table`(`TID`,`SchemaName`,`TableName`,`ENGINE`,`CREATE_OPTIONS`,`TABLE_COMMENT`,`PrimaryKeys`) VALUES(75,'remp_sys_ch','sys_warntype','InnoDB','','','`WarnTypeID`,`AppID`');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(75,'WarnTypeID',NULL,0,'int',NULL,NULL,10,0,NULL,NULL,'int(11)','PRI',0,'信息类型编码','remp_sys_ch','sys_warntype');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(75,'WarnTypeName',NULL,0,'varchar',40,120,NULL,NULL,'utf8','utf8_general_ci','varchar(40)','',0,'信息类型名称','remp_sys_ch','sys_warntype');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(75,'AppID',NULL,0,'tinyint',NULL,NULL,3,0,NULL,NULL,'tinyint(4)','PRI',0,'所属业务编码','remp_sys_ch','sys_warntype');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(75,'Description',NULL,1,'varchar',60,180,NULL,NULL,'utf8','utf8_general_ci','varchar(60)','',0,'信息类型描述','remp_sys_ch','sys_warntype');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(75,'DataUrl',NULL,0,'varchar',256,768,NULL,NULL,'utf8','utf8_general_ci','varchar(256)','',0,'','remp_sys_ch','sys_warntype');
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES(75,'EventUrl',NULL,1,'varchar',256,768,NULL,NULL,'utf8','utf8_general_ci','varchar(256)','',0,'事件URL','remp_sys_ch','sys_warntype');

-- 存储过程:proc_CallCenter_ListPager_MemberSearch
INSERT INTO `dbv_proc`(`SchemaName`,`ProcName`,`type`,`param_list`,`returns`,`body`,`comment`) VALUES('remp_sys_ch','proc_CallCenter_ListPager_MemberSearch',2,'
        IN vPatientName VARCHAR(60),
        IN vMemberID VARCHAR(30),
        IN vPhoneNum VARCHAR(20),
        IN vCredNO VARCHAR(20),
        IN iOrgID INTEGER,
        IN iServiceStatus INTEGER,
        IN iPageSize INTEGER,
        IN iCurrentPageIndex INTEGER,
        IN iSortField INTEGER,
        IN iSortType INTEGER,
        INOUT iRowCount INTEGER
    ','','labelbegin:BEGIN
	DECLARE vOrgPath VARCHAR(300);
  	DECLARE vQuery VARCHAR(300);
    DECLARE vSQL VARCHAR(1000);
    DECLARE vSQLCount VARCHAR(1000);
    DECLARE vSort VARCHAR(60);
    DECLARE vLimit VARCHAR(30);
    DECLARE stmt VARCHAR(2000);
    
    SET vSQL='''';
    SET vSQLCount='''';
    SET vSort='' order by a.orgid asc'';
    SET vOrgPath='''';
    SET vQuery='''';
    
    CASE iSortField  
  
	WHEN 1 THEN 
    	SET vSort=CONCAT(vSort,''  , PatientName'');
    WHEN 2 THEN 
    	SET vSort=CONCAT(vSort,''  , MemberID'');
    WHEN 3 THEN 
    	SET vSort=CONCAT(vSort,''  , Mobile'');
    WHEN 4 THEN 
    	SET vSort=CONCAT(vSort,''  , CredNO'');
    END CASE;
    
    IF iSortField<>-1 AND iSortType=0    THEN
    	SET vSort=CONCAT(vSort,'' asc'');
    END IF;
    IF iSortField<>-1 AND iSortType=1    THEN
       	SET vSort=CONCAT(vSort,'' desc'');
    END IF;
    
    IF iPageSize=-1 OR iPageSize=0 THEN
    	SET vLimit='''';
    ELSE	
    	SET vLimit=CONCAT('' limit '',iCurrentPageIndex*iPageSize,'' , '',iPageSize);
    END IF;
    
    
    IF vPatientName<>'''' THEN
    
		SET vQuery=CONCAT(vQuery,'' and patientname like '''''',vPatientName,''%'''''');
    END IF;
    
    IF vMemberID<>'''' THEN
    
		SET vQuery=CONCAT(vQuery,'' and MemberID like '''''',vMemberID,''%'''''');

    END IF;
  
    IF vCredNO<>'''' THEN
    
		SET vQuery=CONCAT(vQuery,'' and CredNO like '''''',vCredNO,''%'''''');
    END IF;
 
    
  	IF vPhoneNum<>'''' THEN
    
		SET vQuery=CONCAT(vQuery,'' and (Mobile like '''''',vPhoneNum,''%'''''');
   		SET vQuery=CONCAT(vQuery,'' or b.phone like '''''',vPhoneNum,''%'''''');
   		SET vQuery=CONCAT(vQuery,'' or b.phone2 like '''''',vPhoneNum,''%'''''','')'');
    END IF;
    
    IF iOrgID<>-1 THEN
		SELECT IFNULL(OrgPath,'''') INTO vOrgPath  FROM  `sys_OrganizationInfo` 
        WHERE OrgID=iOrgID AND InValid=0 AND OrgType=1 ;
        IF vOrgPath='''' THEN
        	SET iRowCount=0;
            LEAVE labelbegin;
        END IF;
        
        IF iServiceStatus=-1 THEN
        
        	SET vSQL=CONCAT(vSQL,''SELECT b.PatientID as PatientID,b.PatientName as PatientName,b.MemberID as MemberID,'');
            SET vSQL=CONCAT(vSQL,''b.PatientSex as PatientSex,b.CredNO as CredNO,b.Mobile as Mobile,b.IsEnable as IsEnable,'');
            SET vSQL=CONCAT(vSQL,''a.OrgName as OrgName,a.status as Status,b.Birthday as Birthday FROM  sys_PatientInfo b,'');
            SET vSQL=CONCAT(vSQL,''( select  a.patientID as patientID,b.orgname as OrgName,a.status as Status,b.orgid as OrgID from   sys_MemberServiceInfo a, '');
            SET vSQL=CONCAT(vSQL,'' sys_OrganizationInfo b, (select patientID ,Max(serviceid) as serviceid from  sys_MemberServiceInfo  '');
            SET vSQL=CONCAT(vSQL,'' group by patientID) c  where  a.PatientID=c.patientid and a.ServiceID=c.serviceid and    '');
            SET vSQL=CONCAT(vSQL,'' a.orgid=b.orgid and b.invalid=0 and b.OrgPath like '''''',vOrgPath,''%'''''','' group by a.patientID) a  where b.patientID=a.PatientID and   b.invalid=0 '');
        	SET vSQLCount=CONCAT(vSQLCount,''SELECT count(*) into @iCount '');
            SET vSQLCount=CONCAT(vSQLCount,'' FROM  sys_PatientInfo b,'');
            SET vSQLCount=CONCAT(vSQLCount,''( select  a.patientID as patientID,b.orgname as OrgName from   sys_MemberServiceInfo a, '');
            SET vSQLCount=CONCAT(vSQLCount,'' sys_OrganizationInfo b, (select patientID ,Max(serviceid) as serviceid from  sys_MemberServiceInfo  '');
            SET vSQLCount=CONCAT(vSQLCount,'' group by patientID) c  where  a.PatientID=c.patientid and a.ServiceID=c.serviceid and    '');
            SET vSQLCount=CONCAT(vSQLCount,'' a.orgid=b.orgid and b.invalid=0 and b.OrgPath like '''''',vOrgPath,''%'''''','' group by a.patientID) c  where b.patientID=c.PatientID and   b.invalid=0 '');
        ELSE
        	SET vSQL=CONCAT(vSQL,''SELECT b.PatientID as PatientID,b.PatientName as PatientName,b.MemberID as MemberID,'');
            SET vSQL=CONCAT(vSQL,''b.PatientSex as PatientSex,b.CredNO as CredNO,b.Mobile as Mobile,b.IsEnable as IsEnable,'');
            SET vSQL=CONCAT(vSQL,''a.OrgName as OrgName,a.status as Status,b.Birthday as Birthday FROM  sys_PatientInfo b,'');
            SET vSQL=CONCAT(vSQL,''( select  a.patientID as patientID,b.orgname as OrgName,a.status as Status,b.orgid as OrgID from   sys_MemberServiceInfo a, '');
            SET vSQL=CONCAT(vSQL,'' sys_OrganizationInfo b, (select patientID ,Max(serviceid) as serviceid from  sys_MemberServiceInfo  '');
            SET vSQL=CONCAT(vSQL,'' group by patientID) c  where  a.PatientID=c.patientid and a.ServiceID=c.serviceid and  a.status='',iServiceStatus,'' and '');
            SET vSQL=CONCAT(vSQL,'' a.orgid=b.orgid and b.invalid=0 and b.OrgPath like '''''',vOrgPath,''%'''''','' group by a.patientID) a  where b.patientID=a.PatientID and   b.invalid=0 '');
        	SET vSQLCount=CONCAT(vSQLCount,''SELECT count(*) into @iCount '');
            SET vSQLCount=CONCAT(vSQLCount,'' FROM  sys_PatientInfo b,'');
            SET vSQLCount=CONCAT(vSQLCount,''( select  a.patientID as patientID,b.orgname as OrgName from   sys_MemberServiceInfo a, '');
            SET vSQLCount=CONCAT(vSQLCount,'' sys_OrganizationInfo b, (select patientID ,Max(serviceid) as serviceid from  sys_MemberServiceInfo  '');
            SET vSQLCount=CONCAT(vSQLCount,'' group by patientID) c  where  a.PatientID=c.patientid and a.ServiceID=c.serviceid and   a.status='',iServiceStatus,'' and '');
            SET vSQLCount=CONCAT(vSQLCount,'' a.orgid=b.orgid and b.invalid=0 and b.OrgPath like '''''',vOrgPath,''%'''''','' group by a.patientID) c  where b.patientID=c.PatientID and   b.invalid=0 '');
        
        END IF;
       
    END IF;
  IF iOrgID=-1  THEN
      
      	IF iServiceStatus=-1  THEN
      	
        	SET vSQL=CONCAT(vSQL,''SELECT   b.PatientID as PatientID,  b.PatientName as PatientName,'');
        	SET vSQL=CONCAT(vSQL,''b.MemberID as MemberID,b.PatientSex as PatientSex,b.CredNO as CredNO,'');
        	SET vSQL=CONCAT(vSQL,''b.Mobile as Mobile,  b.IsEnable as IsEnable,a.OrgName as OrgName,c.status as Status '');
        	SET vSQL=CONCAT(vSQL,'' ,b.Birthday as Birthday FROM sys_OrganizationInfo a, sys_PatientInfo b,sys_MemberServiceInfo c '');
        	SET vSQL=CONCAT(vSQL,''  where a.orgid=c.orgid  and a.Invalid=0 and '');
        	SET vSQL=CONCAT(vSQL,''  b.patientID=c.PatientID and b.invalid=0 '');
        
        	SET vSQLCount=CONCAT(vSQLCount,''SELECT   count(*) into @iCount '');
        	SET vSQLCount=CONCAT(vSQLCount,'' FROM sys_OrganizationInfo a, sys_PatientInfo b,sys_MemberServiceInfo c '');
        	SET vSQLCount=CONCAT(vSQLCount,''  where a.orgid=c.orgid  and a.Invalid=0 and '');
       	 	SET vSQLCount=CONCAT(vSQLCount,''  b.patientID=c.PatientID and b.invalid=0 '');
        ELSE
        	SET vSQL=CONCAT(vSQL,''SELECT b.PatientID as PatientID,b.PatientName as PatientName,b.MemberID as MemberID,'');
            SET vSQL=CONCAT(vSQL,''b.PatientSex as PatientSex,b.CredNO as CredNO,b.Mobile as Mobile,b.IsEnable as IsEnable,'');
            SET vSQL=CONCAT(vSQL,''a.OrgName as OrgName,a.status as Status,b.Birthday as Birthday FROM  sys_PatientInfo b,'');
            SET vSQL=CONCAT(vSQL,''( select  a.patientID as patientID,b.orgname as OrgName,a.status as Status,b.orgid as OrgID from   sys_MemberServiceInfo a, '');
            SET vSQL=CONCAT(vSQL,'' sys_OrganizationInfo b, (select patientID ,Max(serviceid) as serviceid from  sys_MemberServiceInfo  '');
            SET vSQL=CONCAT(vSQL,'' group by patientID) c  where  a.PatientID=c.patientid and a.ServiceID=c.serviceid and  a.status='',iServiceStatus,'' and '');
            SET vSQL=CONCAT(vSQL,'' a.orgid=b.orgid and b.invalid=0  group by a.patientID) a  where b.patientID=a.PatientID and   b.invalid=0 '');

        	SET vSQLCount=CONCAT(vSQLCount,''SELECT count(*) into @iCount '');
            SET vSQLCount=CONCAT(vSQLCount,'' FROM  sys_PatientInfo b,'');
            SET vSQLCount=CONCAT(vSQLCount,''( select  a.patientID as patientID,b.orgname as OrgName from   sys_MemberServiceInfo a, '');
            SET vSQLCount=CONCAT(vSQLCount,'' sys_OrganizationInfo b, (select patientID ,Max(serviceid) as serviceid from  sys_MemberServiceInfo  '');
            SET vSQLCount=CONCAT(vSQLCount,'' group by patientID) c  where  a.PatientID=c.patientid and a.ServiceID=c.serviceid and   a.status='',iServiceStatus,'' and '');
            SET vSQLCount=CONCAT(vSQLCount,'' a.orgid=b.orgid and b.invalid=0 group by a.patientID) c  where b.patientID=c.PatientID and   b.invalid=0 '');
        
      END IF;
       
  END IF;
      IF vQuery<>''''   THEN
          SET vSQL=CONCAT(vSQL,vQuery);
          SET vSQLCount=CONCAT(vSQLCount,vQuery);
      END IF;
            
      SET vSQL=CONCAT(vSQL,vSort);
      
      
      
            
      SET vSQL=CONCAT(vSQL,vLimit);
      
      SET @sqltext:=vSQL;
	  PREPARE stmt FROM @sqltext;
      EXECUTE stmt;
	  
	  DEALLOCATE PREPARE stmt;
      
      IF iRowCount=-1 THEN
	      SET @sqltext:=vSQLCount;
		  PREPARE stmt FROM @sqltext;
	      EXECUTE stmt;
		  SET iRowCount=@iCount;
      	  DEALLOCATE PREPARE stmt;
     
      END IF;
      
END labelbegin','');

-- 存储过程:proc_DoctorAdvancedSearch
INSERT INTO `dbv_proc`(`SchemaName`,`ProcName`,`type`,`param_list`,`returns`,`body`,`comment`) VALUES('remp_sys_ch','proc_DoctorAdvancedSearch',2,'
        IN vDoctorName VARCHAR(30),
        IN vWorkID VARCHAR(30),
        IN tPosition TINYINT,
        IN tIsInclude TINYINT,
        IN iOrgID INTEGER,
        IN iDeptID INTEGER,
        IN iPageSize INTEGER,
        IN iCurrentPageIndex INTEGER,
        IN iSortField INTEGER,
        IN iSortType INTEGER,
        INOUT iRowCount INTEGER
    ','','labelbegin:BEGIN
	DECLARE vOrgPath VARCHAR(300);
  	DECLARE vQuery VARCHAR(300);
    DECLARE vSQL VARCHAR(1000);
    DECLARE vSQLCount VARCHAR(1000);
    DECLARE vSort VARCHAR(60);
    DECLARE vLimit VARCHAR(30);
    DECLARE stmt VARCHAR(2000);
    
    
    SET vSQL='''';
    SET vSQLCount='''';
    SET vSort='' order by a.orgid asc '';
    SET vQuery='''';
    
    CASE iSortField  
    
    WHEN 1 THEN
    	SET vSort=CONCAT(vSort,''  ,b.DrID'');
	WHEN 2 THEN 
    	SET vSort=CONCAT(vSort,''  , DrName'');
    WHEN 3 THEN 
    	SET vSort=CONCAT(vSort,''  , WorkID'');
    WHEN 4 THEN 
    	SET vSort=CONCAT(vSort,''  , Position'');
    WHEN 6 THEN 
    	SET vSort=CONCAT(vSort,''  , IsEnable'');
    
    END CASE;
    
    IF iSortField<>-1 AND iSortType=0    THEN
    	SET vSort=CONCAT(vSort,'' asc'');
    END IF;
    IF iSortField<>-1 AND iSortType=1    THEN
       	SET vSort=CONCAT(vSort,'' desc'');
    END IF;
    
    IF iPageSize=-1 OR iPageSize=0 THEN
    	SET vLimit='''';
    ELSE	
    	SET vLimit=CONCAT('' limit '',iCurrentPageIndex*iPageSize,'' , '',iPageSize);
    END IF;
    
    
    
    IF vDoctorName<>'''' THEN
    
		SET vQuery=CONCAT(vQuery,'' and drname like '''''',vDoctorName,''%'''''');
    END IF;
    
    IF vWorkID<>'''' THEN
    
		SET vQuery=CONCAT(vQuery,'' and WorkID like '''''',vWorkID,''%'''''');
    END IF;
     
	IF tPosition<>-1 THEN
    
		SET vQuery=CONCAT(vQuery,'' and Position='',tPosition);
    END IF;
    
    IF tIsInclude =1 THEN
		SELECT IFNULL(OrgPath,'''') INTO vOrgPath  FROM  `sys_OrganizationInfo` 
        WHERE OrgID=iOrgID AND InValid=0 AND OrgType=1 ;
        IF vOrgPath=''''  THEN
        	SET iRowCount=0;
            LEAVE labelbegin;
        END IF;
        
        
        	SET vSQL=CONCAT(vSQL,''SELECT b.drID as DoctorID,b.DrName as DoctorName,b.WorkID as WorkID,'');
            SET vSQL=CONCAT(vSQL,''b.position as Position,b.isenable as IsEnable,a.OrgName as OrgName FROM sys_OrganizationInfo a, '');
            SET vSQL=CONCAT(vSQL,''sys_DoctorInfo b where   a.orgid=b.OrgID and a.Invalid=0 and b.invalid=0 and '');
            SET vSQL=CONCAT(vSQL,'' a.OrgPath like '''''',vOrgPath,''%'''''' );
        	SET vSQLCount=CONCAT(vSQLCount,''SELECT count(*)  into @iCount '');
            SET vSQLCount=CONCAT(vSQLCount,'' FROM sys_OrganizationInfo a, '');
            SET vSQLCount=CONCAT(vSQLCount,''sys_DoctorInfo b where  a.orgid=b.OrgID and a.Invalid=0 and b.invalid=0  and '');
            SET vSQLCount=CONCAT(vSQLCount,'' a.OrgPath like '''''',vOrgPath,''%'''''' );
            
    END IF;
  IF tIsInclude =0 THEN
       IF iDeptID=-1 THEN
       	    SET vSQL=CONCAT(vSQL,''SELECT b.drID as DoctorID,b.DrName as DoctorName,b.WorkID as WorkID,'');
       	    SET vSQL=CONCAT(vSQL,''b.position as Position,b.isenable as IsEnable,a.OrgName as OrgName FROM sys_OrganizationInfo a, '');
       	    SET vSQL=CONCAT(vSQL,''sys_DoctorInfo b,sys_DoctorOrgRel c where  a.orgid=b.OrgID and a.Invalid=0 and b.invalid=0  '');
       	    SET vSQL=CONCAT(vSQL,'' and a.Orgid='',iorgid,''  '');
        
        	SET vSQLCount=CONCAT(vSQLCount,''SELECT count(*) into @iCount '');
        	SET vSQLCount=CONCAT(vSQLCount,'' FROM sys_OrganizationInfo a, '');
        	SET vSQLCount=CONCAT(vSQLCount,''sys_DoctorInfo b,sys_DoctorOrgRel c where  a.orgid=b.OrgID and a.Invalid=0 and b.invalid=0  '');
        	SET vSQLCount=CONCAT(vSQLCount,'' and a.Orgid='',iorgid,''  '');
       ELSE
       	    SET vSQL=CONCAT(vSQL,''SELECT b.drID as DoctorID,b.DrName as DoctorName,b.WorkID as WorkID,'');
       	    SET vSQL=CONCAT(vSQL,''b.position as Position,b.isenable as IsEnable,a.OrgName as OrgName FROM sys_OrganizationInfo a, '');
       	    SET vSQL=CONCAT(vSQL,''sys_DoctorInfo b,sys_DoctorOrgRel c where  a.orgid=b.OrgID and a.Invalid=0 and b.invalid=0  '');
       	    SET vSQL=CONCAT(vSQL,'' and a.Orgid='',iorgid,'' AND c.drid=b.drid and c.OrgID='',ideptID);
        
        	SET vSQLCount=CONCAT(vSQLCount,''SELECT count(*) into @iCount '');
        	SET vSQLCount=CONCAT(vSQLCount,'' FROM sys_OrganizationInfo a, '');
        	SET vSQLCount=CONCAT(vSQLCount,''sys_DoctorInfo b,sys_DoctorOrgRel c where  a.orgid=b.OrgID and a.Invalid=0 and b.invalid=0  '');
        	SET vSQLCount=CONCAT(vSQLCount,'' and a.Orgid='',iorgid,'' AND c.drid=b.drid and c.OrgID='',ideptID);
        
       END IF;
       
  END IF;
      IF vQuery<>''''   THEN
          SET vSQL=CONCAT(vSQL,vQuery);
          SET vSQLCount=CONCAT(vSQLCount,vQuery);
      END IF;
            
      SET vSQL=CONCAT(vSQL,vSort);
            
      SET vSQL=CONCAT(vSQL,vLimit);
      
      SET @sqltext:=vSQL;
	  PREPARE stmt FROM @sqltext;
      EXECUTE stmt;
	  DEALLOCATE PREPARE stmt;
	  
      IF iRowCount=-1  THEN
      	SET @sqltext:=vSQLCount;
	  	PREPARE stmt FROM @sqltext;
      	EXECUTE stmt;
      	SET iRowCount=@iCount;
		DEALLOCATE PREPARE stmt;
      END IF;
      
END labelbegin','');

-- 存储过程:proc_GetOrgDeleteStatus
INSERT INTO `dbv_proc`(`SchemaName`,`ProcName`,`type`,`param_list`,`returns`,`body`,`comment`) VALUES('remp_sys_ch','proc_GetOrgDeleteStatus',2,'
    IN iOrgID INTEGER (11),
    OUT iStatus TINYINT
','','labelbegin :
BEGIN
    DECLARE iCount INT ;
    DECLARE iMark INT ;
    SET iMark = 0 ;
    
    SELECT COUNT(*) INTO iCount FROM `sys_OrganizationInfo` WHERE ParentOrgID = iOrgID AND invalid = 0 ;
    IF iCount > 0 THEN
	    SET iMark = 1 ;
	    SET iStatus = 2 ;
		LEAVE labelbegin ;
    END IF ;
    

    
    SELECT COUNT(*) INTO iCount FROM `sys_doctororgrel` WHERE OrgID = iOrgID ;
    IF iCount > 0 THEN
		SET iMark = 1 ;
		SET iStatus = 3 ;
		LEAVE labelbegin ;
    END IF ;
    
    SELECT COUNT(a.patientid) INTO iCount FROM `sys_PatientInfo` a, `sys_MemberServiceInfo` b 
    WHERE a.patientid = b.PatientID AND a.invalid = 0 AND b.orgid = iOrgID ;
    IF iCount > 0 THEN
		SET iMark = 1 ;
		SET iStatus = 4 ;
		LEAVE labelbegin ;
    END IF ;
    
    SELECT COUNT(*) INTO iCount FROM `sys_CollectorInfo` WHERE OrgID = iOrgID AND invalid = 0 ;
    IF iCount > 0 THEN
		SET iMark = 1 ;
		SET iStatus = 5 ;
		LEAVE labelbegin ;
    END IF ;
    
    SELECT COUNT(*) INTO iCount FROM `sys_PhoneInfo` WHERE OrgID = iOrgID AND invalid = 0 ;
    IF iCount > 0 THEN
		SET iMark = 1 ;
		SET iStatus = 6 ;
		LEAVE labelbegin ;
    END IF ;
    
    IF iMark = 0 THEN
		SET iStatus = 1 ;
    END IF ;
END labelbegin','');

-- 存储过程:proc_GetSubDepartment
INSERT INTO `dbv_proc`(`SchemaName`,`ProcName`,`type`,`param_list`,`returns`,`body`,`comment`) VALUES('remp_sys_ch','proc_GetSubDepartment',2,'
        IN iOrgID INTEGER(11)
    ','','BEGIN
	SELECT orgid,orgname,ParentOrgID,a.OrgPath FROM sys_OrganizationInfo a,
(SELECT orgpath FROM sys_OrganizationInfo WHERE parentorgid=iOrgID AND orgtype=2) b
WHERE orgtype=2 AND LOCATE(b.orgpath,a.`OrgPath`)>0;
END','');

-- 存储过程:proc_PatientAdvancedSearch
INSERT INTO `dbv_proc`(`SchemaName`,`ProcName`,`type`,`param_list`,`returns`,`body`,`comment`) VALUES('remp_sys_ch','proc_PatientAdvancedSearch',2,'
        IN vPatientName VARCHAR(60),
        IN vMemberID VARCHAR(30),
        IN vCredNO VARCHAR(20),
        IN iSex INTEGER,
        IN vPhoneNum VARCHAR(20),
        IN tIsInclude TINYINT,
        IN iOrgID INTEGER,
        IN iDeptID INTEGER,
        IN iPageSize INTEGER,
        IN iCurrentPageIndex INTEGER,
        IN iSortField INTEGER,
        IN iSortType INTEGER,
        IN tIsFilterStatus TINYINT,
        INOUT iRowCount INTEGER
    ','','labelbegin:BEGIN
	declare vOrgPath varchar(300);
  	declare vQuery varchar(300);
    declare vSQL varchar(1000);
    declare vSQLCount varchar(1000);
    declare vSort varchar(60);
    declare vLimit varchar(30);
    declare stmt varchar(2000);
    
    set vSQL='''';
    set vSQLCount='''';
    set vSort='' order by a.orgid asc'';
    set vOrgPath='''';
    set vQuery='''';
    
    CASE iSortField  
    WHEN 1 THEN
    	set vSort=concat(vSort,''  , PatientId'');
	WHEN 2 THEN 
    	set vSort=concat(vSort,''  , PatientName'');
    WHEN 3 THEN 
    	set vSort=concat(vSort,''  , PatientSex'');
    WHEN 4 THEN 
    	set vSort=concat(vSort,''  , Mobile'');
    WHEN 5 THEN 
    	set vSort=concat(vSort,''  , CredNO'');
    WHEN 6 THEN 
    	set vSort=concat(vSort,''  , MemberID'');
    WHEN 7 THEN 
    	set vSort=concat(vSort,''  , IsEnable'');
    
    END case;
    
    if iSortField<>-1 and iSortType=0    then
    	set vSort=concat(vSort,'' asc'');
    end if;
    if iSortField<>-1 and iSortType=1    then
       	set vSort=concat(vSort,'' desc'');
    end if;
    
    if iPageSize=-1 or iPageSize=0 then
    	set vLimit='''';
    else	
    	set vLimit=concat('' limit '',iCurrentPageIndex*iPageSize,'' , '',iPageSize);
    end if;
    
    
	
    
    if vPatientName<>'''' then
    
		set vQuery=CONCAT(vQuery,'' and patientname like '''''',vPatientName,''%'''''');
    end if;
    
    if vMemberID<>'''' then
    
		set vQuery=CONCAT(vQuery,'' and MemberID like '''''',vMemberID,''%'''''');
    end if;
    if vCredNO<>'''' then
    
		set vQuery=CONCAT(vQuery,'' and CredNO like '''''',vCredNO,''%'''''');
    end if;
    
	if iSex<>-1 then
    
		set vQuery=CONCAT(vQuery,'' and patientsex='',isex);
    end if;
    
  	if vPhoneNum<>'''' then
    
		set vQuery=CONCAT(vQuery,'' and Mobile like '''''',vPhoneNum,''%'''''');
    end if;
    
    if tIsInclude =1 then
		select ifnull(OrgPath,'''') into vOrgPath  FROM  `sys_OrganizationInfo` 
        where OrgID=iOrgID and InValid=0 and OrgType=1 ;
        if vOrgPath='''' then
        	set iRowCount=0;
            leave labelbegin;
        end if;
        
        if tIsFilterStatus=0 then
        
 
        	set vSQL=CONCAT(vSQL,''SELECT b.PatientID as PatientID,b.PatientName as PatientName,b.MemberID as MemberID,b.PatientSex as PatientSex,'');
           	set vSQL=CONCAT(vSQL,'' b.CredNO as CredNO,b.Mobile as Mobile,b.IsEnable as IsEnable,c.OrgName as OrgName,a.status as Status,'');
           	set vSQL=CONCAT(vSQL,'' b.Birthday as Birthday FROM  sys_PatientInfo b,sys_MemberServiceInfo a,'');
            set vSQL=CONCAT(vSQL,''  sys_OrganizationInfo c  where b.patientID=a.PatientID and a.orgid=c.orgid '');
            set vSQL=CONCAT(vSQL,'' and c.invalid=0 and a.status2=1 and c.OrgPath like '''''',vOrgPath,''%'''''','' and  b.invalid=0 '');
            
        	set vSQLCount=CONCAT(vSQLCount,''SELECT count(*) into @iCount '');
           	set vSQLCount=CONCAT(vSQLCount,''  FROM  sys_PatientInfo b,sys_MemberServiceInfo a,'');
            set vSQLCount=CONCAT(vSQLCount,''  sys_OrganizationInfo c  where b.patientID=a.PatientID and a.orgid=c.orgid '');
            set vSQLCount=CONCAT(vSQLCount,'' and c.invalid=0 and a.status2=1 and c.OrgPath like '''''',vOrgPath,''%'''''','' and  b.invalid=0 '');

            
        end if;
        if tIsFilterStatus=1 then
			
       	set vSQL=CONCAT(vSQL,''SELECT b.PatientID as PatientID,b.PatientName as PatientName,b.MemberID as MemberID,b.PatientSex as PatientSex,'');
           	set vSQL=CONCAT(vSQL,'' b.CredNO as CredNO,b.Mobile as Mobile,b.IsEnable as IsEnable,c.OrgName as OrgName,a.status as Status,'');
           	set vSQL=CONCAT(vSQL,'' b.Birthday as Birthday FROM  sys_PatientInfo b,sys_MemberServiceInfo a,'');
            set vSQL=CONCAT(vSQL,''  sys_OrganizationInfo c  where b.patientID=a.PatientID and a.orgid=c.orgid '');
            set vSQL=CONCAT(vSQL,'' and c.invalid=0 and a.status=1 and a.status2=1 and c.OrgPath like '''''',vOrgPath,''%'''''','' and  b.invalid=0 '');
            
        	set vSQLCount=CONCAT(vSQLCount,''SELECT count(*) into @iCount '');
           	set vSQLCount=CONCAT(vSQLCount,''  FROM  sys_PatientInfo b,sys_MemberServiceInfo a,'');
            set vSQLCount=CONCAT(vSQLCount,''  sys_OrganizationInfo c  where b.patientID=a.PatientID and a.orgid=c.orgid '');
            set vSQLCount=CONCAT(vSQLCount,'' and  a.status=1 and a.status2=1 and c.invalid=0 and c.OrgPath like '''''',vOrgPath,''%'''''','' and  b.invalid=0 '');

        end if;
        
    end if;
  if tIsInclude =0 then
      if iDeptID<>-1 then
      
      	if tIsFilterStatus=0 then
      	
        	set vSQL=CONCAT(vSQL,''SELECT   b.PatientID as PatientID,  b.PatientName as PatientName,'');
        	set vSQL=CONCAT(vSQL,''b.MemberID as MemberID,b.PatientSex as PatientSex,b.CredNO as CredNO,'');
        	set vSQL=CONCAT(vSQL,''b.Mobile as Mobile,  b.IsEnable as IsEnable,a.OrgName as OrgName,c.status as Status '');
        	set vSQL=concat(vSQL,'' ,b.Birthday as Birthday FROM sys_OrganizationInfo a, sys_PatientInfo b,sys_MemberServiceInfo c,'');
        	set vSQL=concat(vSQL,''sys_MemberServiceOrg d  where a.orgid=c.orgid  and a.Invalid=0 and '');
        	set vSQL=concat(vSQL,'' c.OrgID='',iOrgID,'' and b.patientID=c.PatientID and b.invalid=0 and '');
        	set vSQL=concat(vSQL,'' d.ServiceID=c.serviceid and d.orgid='',iDeptID);
        
        	set vSQLCount=CONCAT(vSQLCount,''SELECT   count(*) into @iCount '');
        	set vSQLCount=concat(vSQLCount,'' FROM sys_OrganizationInfo a, sys_PatientInfo b,sys_MemberServiceInfo c,'');
        	set vSQLCount=concat(vSQLCount,''sys_MemberServiceOrg d  where a.orgid=c.orgid  and a.Invalid=0 and '');
       	 	set vSQLCount=concat(vSQLCount,'' c.OrgID='',iOrgID,'' and b.patientID=c.PatientID and b.invalid=0 and '');
        	set vSQLCount=concat(vSQLCount,'' d.ServiceID=c.serviceid and d.orgid='',iDeptID);
        
	      end if;
      	if tIsFilterStatus=1 then
     
  	    	set vSQL=CONCAT(vSQL,''SELECT   b.PatientID as PatientID,  b.PatientName as PatientName,'');
        	set vSQL=CONCAT(vSQL,''b.MemberID as MemberID,b.PatientSex as PatientSex,b.CredNO as CredNO,'');
        	set vSQL=CONCAT(vSQL,''b.Mobile as Mobile,  b.IsEnable as IsEnable,a.OrgName as OrgName,c.status as Status '');
        	set vSQL=concat(vSQL,'' ,b.Birthday as Birthday FROM sys_OrganizationInfo a, sys_PatientInfo b,sys_MemberServiceInfo c,'');
        	set vSQL=concat(vSQL,''sys_MemberServiceOrg d  where b.patientID=c.PatientID and d.ServiceID=c.serviceid and a.orgid=c.orgid  and a.Invalid=0 and '');
        	set vSQL=concat(vSQL,'' c.OrgID='',iOrgID,'' and  '');
        	set vSQL=concat(vSQL,''  d.orgid='',iDeptID);
        	set vSQL=concat(vSQL,'' and b.invalid=0 and c.status=1'' );
  	    	set vSQLCount=CONCAT(vSQLCount,''SELECT   count(*) into @iCount '');
        	set vSQLCount=concat(vSQLCount,'' FROM sys_OrganizationInfo a, sys_PatientInfo b,sys_MemberServiceInfo c,'');
        	set vSQLCount=concat(vSQLCount,''sys_MemberServiceOrg d  where a.orgid=c.orgid  and a.Invalid=0 and '');
        	set vSQLCount=concat(vSQLCount,'' c.OrgID='',iOrgID,'' and b.patientID=c.PatientID and '');
        	set vSQLCount=concat(vSQLCount,'' d.ServiceID=c.serviceid and d.orgid='',iDeptID);
        	set vSQLCount=concat(vSQLCount,'' and b.invalid=0 and c.status=1'' );
     
      	end if;
        
       else
        
      	if tIsFilterStatus=0 then
      	
        	set vSQL=CONCAT(vSQL,''SELECT   b.PatientID as PatientID,  b.PatientName as PatientName,'');
        	set vSQL=CONCAT(vSQL,''b.MemberID as MemberID,b.PatientSex as PatientSex,b.CredNO as CredNO,'');
        	set vSQL=CONCAT(vSQL,''b.Mobile as Mobile,  b.IsEnable as IsEnable,a.OrgName as OrgName,c.status as Status '');
        	set vSQL=concat(vSQL,'',b.Birthday as Birthday FROM sys_OrganizationInfo a, sys_PatientInfo b,sys_MemberServiceInfo c'');
        	set vSQL=concat(vSQL,''  where a.orgid=c.orgid  and a.Invalid=0 and '');
        	set vSQL=concat(vSQL,'' c.OrgID='',iOrgID,'' and b.patientID=c.PatientID and b.invalid=0 '');
        
        	set vSQLCount=CONCAT(vSQLCount,''SELECT   count(*) into @iCount '');
        	set vSQLCount=concat(vSQLCount,'' FROM sys_OrganizationInfo a, sys_PatientInfo b,sys_MemberServiceInfo c '');
        	set vSQLCount=concat(vSQLCount,'' where a.orgid=c.orgid  and a.Invalid=0 and '');
       	 	set vSQLCount=concat(vSQLCount,'' c.OrgID='',iOrgID,'' and b.patientID=c.PatientID and b.invalid=0 '');
        
	      end if;
      	if tIsFilterStatus=1 then
     
  	    	set vSQL=CONCAT(vSQL,''SELECT   b.PatientID as PatientID,  b.PatientName as PatientName,'');
        	set vSQL=CONCAT(vSQL,''b.MemberID as MemberID,b.PatientSex as PatientSex,b.CredNO as CredNO,'');
        	set vSQL=CONCAT(vSQL,''b.Mobile as Mobile,  b.IsEnable as IsEnable,a.OrgName as OrgName,c.status as Status '');
        	set vSQL=concat(vSQL,'',b.Birthday as Birthday FROM sys_OrganizationInfo a, sys_PatientInfo b,sys_MemberServiceInfo c '');
        	set vSQL=concat(vSQL,''  where a.orgid=c.orgid  and a.Invalid=0 and '');
        	set vSQL=concat(vSQL,'' c.OrgID='',iOrgID,'' and b.patientID=c.PatientID '');
        	set vSQL=concat(vSQL,'' and b.invalid=0 and c.status=1'' );
  	    	set vSQLCount=CONCAT(vSQLCount,''SELECT   count(*) into @iCount '');
        	set vSQLCount=concat(vSQLCount,'' FROM sys_OrganizationInfo a, sys_PatientInfo b,sys_MemberServiceInfo c '');
        	set vSQLCount=concat(vSQLCount,''  where a.orgid=c.orgid  and a.Invalid=0 and '');
        	set vSQLCount=concat(vSQLCount,'' c.OrgID='',iOrgID,'' and b.patientID=c.PatientID '');
        	set vSQLCount=concat(vSQLCount,'' and b.invalid=0 and c.status=1'' );
     
      	end if;
       end if;
       
  end if;
      if vQuery<>''''   then
          set vSQL=CONCAT(vSQL,vQuery);
          set vSQLCount=CONCAT(vSQLCount,vQuery);
      end if;
            
      set vSQL=CONCAT(vSQL,vSort);
      
      
      
            
      set vSQL=concat(vSQL,vLimit);
      
      
      set @sqltext:=vSQL;
	  prepare stmt from @sqltext;
      execute stmt;
	  
	  DEALLOCATE PREPARE stmt;
      
      if iRowCount=-1 then
	      set @sqltext:=vSQLCount;
		  prepare stmt from @sqltext;
	      execute stmt;
		  set iRowCount=@iCount;
      	  DEALLOCATE PREPARE stmt;
     
      end if;
      
END labelbegin','');

-- 存储过程:proc_WarnInfoSearch
INSERT INTO `dbv_proc`(`SchemaName`,`ProcName`,`type`,`param_list`,`returns`,`body`,`comment`) VALUES('remp_sys_ch','proc_WarnInfoSearch',2,'
        IN iOrgID INTEGER,
        IN vPatientName VARCHAR(60),
        IN dDate1 VARCHAR(25),
        IN dDate2 VARCHAR(25),
        IN tAppID TINYINT,
        IN iWarnTypeID INTEGER,
        IN tResult TINYINT,
        IN iPageSize INTEGER,
        IN iCurrentPageIndex INTEGER,
        INOUT iRowCount INTEGER
    ','','labelbegin:BEGIN
	DECLARE vOrgPath VARCHAR(300);
  	DECLARE vQuery VARCHAR(300);
    DECLARE vSQL VARCHAR(1000);
    DECLARE vSQLCount VARCHAR(1000);
    DECLARE vSort VARCHAR(60);
    DECLARE vLimit VARCHAR(30);
    DECLARE stmt VARCHAR(2000);
    
    
    SET vSQL='''';
    SET vSQLCount='''';
    SET vSort='' order by c.warnid desc '';
    SET vQuery='''';
    
     
    IF iPageSize=-1 OR iPageSize=0 THEN
    	SET vLimit='''';
    ELSE	
    	SET vLimit=CONCAT('' limit '',iCurrentPageIndex*iPageSize,'' , '',iPageSize);
    END IF;
    
     IF vPatientName<>'''' THEN
    
		SET vQuery=CONCAT(vQuery,'' and PatientName ='''''',vPatientName,'''''''');
    END IF;
    
    IF ddate1<>''1990-01-01'' THEN
    
		SET vQuery=CONCAT(vQuery,'' and c.creattime>='''''',ddate1,'''''''');
    END IF;
    
    IF ddate2<>''1990-01-01'' THEN
    
		SET vQuery=CONCAT(vQuery,'' and c.creattime<='''''',ddate2,'''''''');
    END IF;
    
    
    
    IF tAppID<>-1 THEN
    
		SET vQuery=CONCAT(vQuery,'' and c.appid ='',tAppID);
    END IF;
     
	IF iWarnTypeID<>-1 THEN
    
		SET vQuery=CONCAT(vQuery,'' and WarnTypeID='',iWarnTypeID);
    END IF;
    
    IF tResult<>-1 THEN
    
		SET vQuery=CONCAT(vQuery,'' and ProcessResult='',tResult);
    END IF;
    
    
		SELECT IFNULL(OrgPath,'''') INTO vOrgPath  FROM  `sys_OrganizationInfo`  
        	WHERE OrgID=iOrgID  AND InValid=0 ;
        IF vOrgPath=''''  THEN
        	SET iRowCount=0;
            LEAVE labelbegin;
        END IF;
        
        	SET vSQL=CONCAT(vSQL,''select c.* from `sys_OrganizationInfo` a,`sys_MemberServiceInfo` b,`sys_WarnHisInfo` c'');
            SET vSQL=CONCAT(vSQL,''   where a.`OrgID`=b.`OrgID` and a.orgpath like '''''',vOrgPath,''%'''''' );
            SET vSQL=CONCAT(vSQL,'' 	   and  a.invalid=0 and b.status=1  and c.`PatientID`=b.`PatientID` '');
        	SET vSQLCount=CONCAT(vSQLCount,''select count(*)  into @iCount from `sys_OrganizationInfo` a,`sys_MemberServiceInfo` b,`sys_WarnHisInfo` c'');
            SET vSQLCount=CONCAT(vSQLCount,'' where a.`OrgID`=b.`OrgID` and a.orgpath like '''''',vOrgPath,''%'''''' );
            SET vSQLCount=CONCAT(vSQLCount,'' 	   and  a.invalid=0 and b.status=1 and c.`PatientID`=b.`PatientID` '');
        
        
      IF vQuery<>''''   THEN
          SET vSQL=CONCAT(vSQL,vQuery);
          SET vSQLCount=CONCAT(vSQLCount,vQuery);
      END IF;
            
      SET vSQL=CONCAT(vSQL,vSort);
            
      SET vSQL=CONCAT(vSQL,vLimit);
      
      
      SET @sqltext:=vSQL;
	  PREPARE stmt FROM @sqltext;
      EXECUTE stmt;
	  DEALLOCATE PREPARE stmt;
	  
      IF iRowCount=-1  THEN
      	SET @sqltext:=vSQLCount;
	  	PREPARE stmt FROM @sqltext;
      	EXECUTE stmt;
      	SET iRowCount=@iCount;
		DEALLOCATE PREPARE stmt;
      END IF;
      
END labelbegin','');

-- 触发器:TRI_Dr_I
INSERT INTO `dbv_trigger`(`SchemaName`,`TriName`,`EVENT_MANIPULATION`,`TableName`,`ACTION_ORDER`,`ACTION_CONDITION`,`ACTION_STATEMENT`,`ACTION_ORIENTATION`,`ACTION_TIMING`) VALUES('remp_sys_ch','TRI_Dr_I','INSERT','sys_doctorinfo',0,NULL,'BEGIN
	INSERT INTO 
  `sys_DoctorInfoQ`
(
  `DrID`,
  `DrName`,
  `OrgID`
) 
VALUE (
  NEW.DRID,
  NEW.DrName,
  NEW.OrgID
);
END','ROW','AFTER');

-- 触发器:TRI_Dr_U
INSERT INTO `dbv_trigger`(`SchemaName`,`TriName`,`EVENT_MANIPULATION`,`TableName`,`ACTION_ORDER`,`ACTION_CONDITION`,`ACTION_STATEMENT`,`ACTION_ORIENTATION`,`ACTION_TIMING`) VALUES('remp_sys_ch','TRI_Dr_U','UPDATE','sys_doctorinfo',0,NULL,'BEGIN
UPDATE 
  `sys_DoctorInfoQ`  
SET 
  `DrName` = NEW.DrName,
  `OrgID` = NEW.OrgID
 
WHERE 
  `DrID` = old.DrID
;
END','ROW','AFTER');

-- 触发器:TRI_Dr_D
INSERT INTO `dbv_trigger`(`SchemaName`,`TriName`,`EVENT_MANIPULATION`,`TableName`,`ACTION_ORDER`,`ACTION_CONDITION`,`ACTION_STATEMENT`,`ACTION_ORIENTATION`,`ACTION_TIMING`) VALUES('remp_sys_ch','TRI_Dr_D','DELETE','sys_doctorinfo',0,NULL,'BEGIN
	DELETE FROM 
  	`sys_DoctorInfoQ` 
	WHERE 
	  `DrID` = old.DrID
;
END','ROW','AFTER');

-- 触发器:TRI_Pati_I
INSERT INTO `dbv_trigger`(`SchemaName`,`TriName`,`EVENT_MANIPULATION`,`TableName`,`ACTION_ORDER`,`ACTION_CONDITION`,`ACTION_STATEMENT`,`ACTION_ORIENTATION`,`ACTION_TIMING`) VALUES('remp_sys_ch','TRI_Pati_I','INSERT','sys_patientinfo',0,NULL,'BEGIN
INSERT INTO 
  `sys_PatientInfoQ`
(
  `PatientID`,
  `PatientName`,
  `PatientSex`
) 
VALUE (
  new.PatientID,
  new.PatientName,
  new.PatientSex
);
END','ROW','AFTER');

-- 触发器:TRI_Pati_U
INSERT INTO `dbv_trigger`(`SchemaName`,`TriName`,`EVENT_MANIPULATION`,`TableName`,`ACTION_ORDER`,`ACTION_CONDITION`,`ACTION_STATEMENT`,`ACTION_ORIENTATION`,`ACTION_TIMING`) VALUES('remp_sys_ch','TRI_Pati_U','UPDATE','sys_patientinfo',0,NULL,'BEGIN
UPDATE 
  `sys_PatientInfoQ`  
SET 
  `PatientName` = new.PatientName,
  `PatientSex` = new.PatientSex
 
WHERE 
  `PatientID` = old.PatientID
;
END','ROW','AFTER');

-- 触发器:TRI_Pati_D
INSERT INTO `dbv_trigger`(`SchemaName`,`TriName`,`EVENT_MANIPULATION`,`TableName`,`ACTION_ORDER`,`ACTION_CONDITION`,`ACTION_STATEMENT`,`ACTION_ORIENTATION`,`ACTION_TIMING`) VALUES('remp_sys_ch','TRI_Pati_D','DELETE','sys_patientinfo',0,NULL,'BEGIN
DELETE FROM 
  `sys_PatientInfoQ` 
WHERE 
  `PatientID` = old.PatientID
;
END','ROW','AFTER');
-- =================================================================================================

COMMIT;
