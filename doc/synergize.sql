INSERT INTO `studyin`.`user`
(`identity`,

`syncdate`,`completed`,`licensetype`
)
VALUES
('370686199102070710',

'2011/1/1','0','2');

INSERT INTO `studyin`.`user`
(`identity`,

`syncdate`,`completed`,`licensetype`,`photofile`
)
VALUES
('RDN7P+4C2VxxP+e8qWCixxCGI8Fybfd1jrzvy2iK9Hs=',

'2011/1/1','0','2','8c44a398e813497391b71e92c1e6fe7c');

UPDATE `studyin`.`user` SET
`completed` = '1'
WHERE `identity` = '370686199102070710';

UPDATE `studyin`.`user` SET
`studylog` = '',`completed` = '0',`firstsigned` = '0',`token` = ''
WHERE `identity` =  'RjvYSXUXE0K+WSETt5OY0LoSzcofuqUumGj0IlfiZcM=';

UPDATE `studyin`.`user` SET
`firstsigned` = '0'
WHERE `identity` = '370686199011302512';

UPDATE `studyin`.`user` SET
`studylog` = 'abc,234234,234234-def,243234234,3423442'
WHERE `identity` = '370686199102070710';

UPDATE `studyin`.`user` SET
`firstsigned` = '0'
WHERE `identity` = 'F5zbBKTCHE92KQLCiTLxhCf4V5Zs7FaqGxAQBBYH4/Y=';

UPDATE `studyin`.`user` SET
`completed` = '1'
WHERE `identity` = 'F5zbBKTCHE92KQLCiTLxhCf4V5Zs7FaqGxAQBBYH4/Y=';

UPDATE `studyin`.`user` SET
`drugrelated` = ''
WHERE `identity` = 'F5zbBKTCHE92KQLCiTLxhCf4V5Zs7FaqGxAQBBYH4/Y=';

UPDATE `studyin`.`user` SET
`studylog` = ''
WHERE `identity` = 'F5zbBKTCHE92KQLCiTLxhCf4V5Zs7FaqGxAQBBYH4/Y=';

UPDATE `studyin`.`user` SET
`studylog` = 'abc,234234,234234-def,243234234,3423442'
WHERE `identity` = 'F5zbBKTCHE92KQLCiTLxhCf4V5Zs7FaqGxAQBBYH4/Y=';

UPDATE `studyin`.`user` SET
`drivinglicense` = '驾驶证号'
WHERE `identity` = '370681199211250014';

update `studyin`.`history` set finishdate = '2017/07/07' where  ordinal<33;
CREATE TABLE `history` (
  `identity` varchar(45) NOT NULL,
  `drugrelated` varchar(1) DEFAULT NULL,
  `inspect` varchar(1) DEFAULT '1',
  `fullmark` varchar(1) DEFAULT NULL,
  `licensetype` varchar(1) DEFAULT NULL,
  `name` varchar(45) DEFAULT NULL,
  `noticedate` datetime DEFAULT NULL,
  `phone` varchar(45) DEFAULT NULL,
  `stoplicense` varchar(1) DEFAULT NULL,
  `completed` varchar(1) DEFAULT NULL,
  `studylog` varchar(500) DEFAULT NULL,
  `startdate` datetime DEFAULT NULL,
  `syncdate` datetime NOT NULL,
  `wechat` varchar(45) DEFAULT NULL,
  `ordinal` int(11) NOT NULL AUTO_INCREMENT,
  `finishdate` datetime NOT NULL,
  `syncphone` varchar(45) DEFAULT NULL,
  `completelog` varchar(80) DEFAULT NULL,
  `signed` varchar(1) DEFAULT NULL,
  `photostatus` varchar(1) DEFAULT NULL,
  `firstsigned` varchar(1) DEFAULT NULL,
  `postaladdress` varchar(100) DEFAULT NULL,
  `drivinglicense` varchar(45) DEFAULT NULL,
  `deductedmarks` int(11) DEFAULT NULL,
  `photofile` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`ordinal`),
  UNIQUE KEY `ordinal_UNIQUE` (`ordinal`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8;
CREATE TABLE `user` (
  `identity` varchar(45) NOT NULL,
  `drugrelated` varchar(1) DEFAULT NULL,
  `inspect` varchar(1) DEFAULT '1',
  `fullmark` varchar(1) DEFAULT NULL,
  `licensetype` varchar(1) DEFAULT NULL,
  `name` varchar(45) DEFAULT NULL,
  `noticedate` datetime DEFAULT NULL,
  `phone` varchar(45) DEFAULT NULL,
  `stoplicense` varchar(1) DEFAULT NULL,
  `completed` varchar(1) DEFAULT NULL,
  `studylog` varchar(500) DEFAULT NULL,
  `startdate` datetime DEFAULT NULL,
  `syncdate` datetime NOT NULL,
  `wechat` varchar(45) DEFAULT NULL,
  `syncphone` varchar(45) DEFAULT NULL,
  `completelog` varchar(80) DEFAULT NULL,
  `signed` varchar(1) DEFAULT NULL,
  `photostatus` varchar(1) DEFAULT NULL,
  `firstsigned` varchar(1) DEFAULT NULL,
  `postaladdress` varchar(100) DEFAULT NULL,
  `drivinglicense` varchar(45) DEFAULT NULL,
  `deductedmarks` int(11) DEFAULT NULL,
  `photofile` varchar(45) DEFAULT NULL,
  `token` varchar(45) DEFAULT NULL,
  `lasttoken` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`identity`),
  UNIQUE KEY `identity_UNIQUE` (`identity`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `request` (
  `ordinal` int(11) NOT NULL AUTO_INCREMENT,
  `ip` varchar(45) DEFAULT NULL,
  `content` varchar(4500) DEFAULT NULL,
  `method` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`ordinal`),
  UNIQUE KEY `ordinal_UNIQUE` (`ordinal`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;

