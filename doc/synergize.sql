INSERT INTO `studyin`.`user`
(`identity`,`photofile`,

`syncdate`,`completed`,`licensetype`
)
VALUES
('','',

'2011/1/1','0','2');

select count(*) from `studyin`.`user`;
select * from `studyin`.`user` where token like 'ae1c476889a045d6b2d6b75d34d9769d';
select * from  `studyin`.`user` where length(identity)<44;
select * from  `studyin`.`user` where name like  '李柏均';
INSERT INTO `studyin`.`user`
(`identity`,

`syncdate`,`completed`,`licensetype`,`photofile`
)
VALUES
('',

'2011/1/1','0','2','');


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

