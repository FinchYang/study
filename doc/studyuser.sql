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
  `status` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`identity`),
  UNIQUE KEY `identity_UNIQUE` (`identity`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
