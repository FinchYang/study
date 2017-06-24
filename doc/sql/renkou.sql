--------------------------------------------------------
--  文件已创建 - 星期五-四月-14-2017   
--------------------------------------------------------
--------------------------------------------------------
--  DDL for Table POPULATION
--------------------------------------------------------

  CREATE TABLE "CITY"."POPULATION" 
   (	"NAME" VARCHAR2(200 BYTE), 
	"SEX" VARCHAR2(5 BYTE), 
	"NATION" VARCHAR2(10 BYTE), 
	"BORN" VARCHAR2(15 BYTE), 
	"ADDRESS" VARCHAR2(260 BYTE), 
	"POSTCODE" VARCHAR2(10 BYTE), 
	"POSTADDRESS" VARCHAR2(260 BYTE), 
	"MOBILE" VARCHAR2(15 BYTE), 
	"TELEPHONE" VARCHAR2(20 BYTE), 
	"EMAIL" VARCHAR2(50 BYTE), 
	"IDNUM" VARCHAR2(50 BYTE), 
	"FIRSTFINGER" VARCHAR2(345 BYTE), 
	"SECONDFINGER" VARCHAR2(345 BYTE), 
	"LEFTEYE" VARCHAR2(2100 BYTE), 
	"RIGHTEYE" VARCHAR2(2100 BYTE)
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS" ;

   COMMENT ON COLUMN "CITY"."POPULATION"."NAME" IS '1名称';
   COMMENT ON COLUMN "CITY"."POPULATION"."SEX" IS '2性别';
   COMMENT ON COLUMN "CITY"."POPULATION"."NATION" IS '3民族';
   COMMENT ON COLUMN "CITY"."POPULATION"."BORN" IS '4出生年月';
   COMMENT ON COLUMN "CITY"."POPULATION"."ADDRESS" IS '5身份证地址';
   COMMENT ON COLUMN "CITY"."POPULATION"."POSTCODE" IS '6邮政编码';
   COMMENT ON COLUMN "CITY"."POPULATION"."POSTADDRESS" IS '7邮寄地址';
   COMMENT ON COLUMN "CITY"."POPULATION"."MOBILE" IS '8移动电话';
   COMMENT ON COLUMN "CITY"."POPULATION"."TELEPHONE" IS '9座机';
   COMMENT ON COLUMN "CITY"."POPULATION"."EMAIL" IS '10电子信箱';
   COMMENT ON COLUMN "CITY"."POPULATION"."IDNUM" IS '11身份证号码或是其他证件号码';
   COMMENT ON COLUMN "CITY"."POPULATION"."FIRSTFINGER" IS '12拇指指纹';
   COMMENT ON COLUMN "CITY"."POPULATION"."SECONDFINGER" IS '13食指指纹';
   COMMENT ON COLUMN "CITY"."POPULATION"."LEFTEYE" IS '14左眼';
   COMMENT ON COLUMN "CITY"."POPULATION"."RIGHTEYE" IS '15右眼';
   COMMENT ON TABLE "CITY"."POPULATION"  IS '此表为身份证数据表，存放所有在车驾管业务管理系统刷过的身份证信息';
REM INSERTING into CITY.POPULATION
SET DEFINE OFF;
--------------------------------------------------------
--  DDL for Index POPULATION_PK
--------------------------------------------------------

  CREATE UNIQUE INDEX "CITY"."POPULATION_PK" ON "CITY"."POPULATION" ("IDNUM") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  Constraints for Table POPULATION
--------------------------------------------------------

  ALTER TABLE "CITY"."POPULATION" ADD CONSTRAINT "POPULATION_PK" PRIMARY KEY ("IDNUM")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS"  ENABLE;
  ALTER TABLE "CITY"."POPULATION" MODIFY ("IDNUM" NOT NULL ENABLE);
