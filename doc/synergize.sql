INSERT INTO `studyin`.`user`
(`identity`,

`syncdate`,`studylog`,`completed`,`drivinglicense`
)
VALUES
('F5zbBKTCHE92KQLCiTLxhCf4V5Zs7FaqGxAQBBYH4/Y=',

'2011/1/1','abc,34234234,234234-jakdflkj,323523,234234','0','akdjf45345');

SELECT * FROM studyin.user;

UPDATE `studyin`.`user` SET
`completed` = '1'
WHERE `identity` = 'F5zbBKTCHE92KQLCiTLxhCf4V5Zs7FaqGxAQBBYH4/Y=';

UPDATE `studyin`.`user` SET
`drivinglicense` = '驾驶证号'
WHERE `identity` = '370681199211250014';
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
  PRIMARY KEY (`ordinal`),
  UNIQUE KEY `ordinal_UNIQUE` (`ordinal`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8;
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
  PRIMARY KEY (`identity`),
  UNIQUE KEY `identity_UNIQUE` (`identity`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
