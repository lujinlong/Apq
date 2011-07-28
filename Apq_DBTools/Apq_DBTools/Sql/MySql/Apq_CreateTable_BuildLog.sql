CREATE TABLE IF NOT EXISTS `dbv_BuildLog` (
  `ID` bigint(20) NOT NULL AUTO_INCREMENT,
  `_InTime` datetime NOT NULL,
  `Type` int(11) DEFAULT NULL,
  `Severity` int(11) DEFAULT NULL,
  `Msg` longtext,
  `Titile` varchar(100) DEFAULT NULL,
  UNIQUE KEY `ID` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;