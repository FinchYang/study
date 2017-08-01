
use studyin;
CREATE TABLE `HUJI` (
   	`SFZH` varchar(50 ), 
	`MINZU` varchar(20 ), 
	`ZHUZHI1` varchar(100 ), 
	`ZHUZHI2` varchar(100 ), 
	`QIXIAN1` varchar(20 ), 
	`QIXIAN2` varchar(30 ), 
	`JIGUAN` varchar(100 ), 
	`TIME` DATE,
    PRIMARY KEY (`SFZH`),
  UNIQUE KEY `ordinal_UNIQUE` (`SFZH`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8;


/*
  ALTER TABLE `WYXX`.`HUJI` MODIFY (`SFZH` NOT NULL ENABLE);
  */
