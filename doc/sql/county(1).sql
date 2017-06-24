--------------------------------------------------------
--  文件已创建 - 星期五-四月-14-2017   
--------------------------------------------------------
--------------------------------------------------------
--  DDL for Table COUNTY
--------------------------------------------------------

  CREATE TABLE "CITY"."COUNTY" 
   (	"COUNTYCODE" VARCHAR2(20 BYTE), 
	"NAME" VARCHAR2(20 BYTE)
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS" ;
REM INSERTING into CITY.COUNTY
SET DEFINE OFF;
Insert into CITY.COUNTY (COUNTYCODE,NAME) values ('haiyang','海阳');
--------------------------------------------------------
--  DDL for Index COUNTY_PK
--------------------------------------------------------

  CREATE UNIQUE INDEX "CITY"."COUNTY_PK" ON "CITY"."COUNTY" ("COUNTYCODE") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  Constraints for Table COUNTY
--------------------------------------------------------

  ALTER TABLE "CITY"."COUNTY" ADD CONSTRAINT "COUNTY_PK" PRIMARY KEY ("COUNTYCODE")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS"  ENABLE;
  ALTER TABLE "CITY"."COUNTY" MODIFY ("NAME" NOT NULL ENABLE);
  ALTER TABLE "CITY"."COUNTY" MODIFY ("COUNTYCODE" NOT NULL ENABLE);
