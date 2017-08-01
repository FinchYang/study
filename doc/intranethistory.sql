use studyin;
CREATE TABLE `iHISTORY` (
 	
    `ID` int(11) ,
	`IDCARD` varchar(50 ), 
	`LICENCE` varchar(5 ), 
	`SREMARK` varchar(2 ), 
	`PHONENUMBER` varchar(20 ), 
	`DEDUCTPOINTS` varchar(10 ), 
	`ZHIDUINUMBER` varchar(20 ), 
	`ADDRESS` varchar(200 ), 
	`NAME` varchar(20 ), 
	`FILENAME` varchar(50 ), 
	`LICENCENUMBER` varchar(20 ), 
	`STATUS` varchar(20 ), 
	`TIME` DATE, 
	`PHOTO` varchar(2 ), 
	`PRINTED` varchar(1 ) DEFAULT 0, 
	`PROCESSED` varchar(1 ) DEFAULT 0, 
	`MESSAGED` varchar(1 ) DEFAULT 0, 
	`STUDYLOG` varchar(500 ), 
	`FAILURE` varchar(1 ) DEFAULT 0, 
	`SYYXQZ` DATE, 
	`ORDINAL` int(11) , 
	`DABH` varchar(30 ), 
	`COUNTY` varchar(5 ),
    PRIMARY KEY (`ID`),
  UNIQUE KEY `identity_UNIQUE` (`ID`)
  ) ENGINE=InnoDB DEFAULT CHARSET=utf8;
  



/*
  ALTER TABLE `WYXX`.`HISTORY` MODIFY (`TIME` NOT NULL ENABLE);
  ALTER TABLE `WYXX`.`HISTORY` ADD CONSTRAINT `HISTORY_PK` PRIMARY KEY (`ID`)
  
  ALTER TABLE `WYXX`.`HISTORY` MODIFY (`ID` NOT NULL ENABLE);
*/