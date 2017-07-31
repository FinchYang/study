CREATE TABLE `request` (
  `ordinal` int(11) NOT NULL AUTO_INCREMENT,
  `ip` varchar(45) DEFAULT NULL,
  `content` varchar(4500) DEFAULT NULL,
  `method` varchar(45) DEFAULT NULL,
  `time` datetime NOT NULL,
  PRIMARY KEY (`ordinal`),
  UNIQUE KEY `ordinal_UNIQUE` (`ordinal`)
) ENGINE=InnoDB AUTO_INCREMENT=23513 DEFAULT CHARSET=utf8;
