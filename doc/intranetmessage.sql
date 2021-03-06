use studyin;
CREATE TABLE `MESSAGE` (
 	`ID` int(11), 
	`HISTORYID` int(11), 
	`TIME` DATE, 
	`CONTENT` varchar(2000 ), 
	`SENT` varchar(1 ),
     PRIMARY KEY (`ID`),
  UNIQUE KEY `ordinal_UNIQUE` (`ID`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8;

  
/*

  ALTER TABLE `WYXX`.`MESSAGE` ADD CONSTRAINT `MESSAGE_PK` PRIMARY KEY (`ID`)
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE `USERS`  ENABLE;
  ALTER TABLE `WYXX`.`MESSAGE` MODIFY (`CONTENT` NOT NULL ENABLE);
  ALTER TABLE `WYXX`.`MESSAGE` MODIFY (`TIME` NOT NULL ENABLE);
  ALTER TABLE `WYXX`.`MESSAGE` MODIFY (`HISTORYID` NOT NULL ENABLE);
  ALTER TABLE `WYXX`.`MESSAGE` MODIFY (`ID` NOT NULL ENABLE);
*/