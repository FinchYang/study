--------------------------------------------------------
--  文件已创建 - 星期二-七月-11-2017   
--------------------------------------------------------
--------------------------------------------------------
--  DDL for Table HISTORY
--------------------------------------------------------

  CREATE TABLE "SIX"."HISTORY" 
   (	"ID" NUMBER, 
	"IDCARD" VARCHAR2(50 BYTE), 
	"LICENCE" VARCHAR2(5 BYTE), 
	"SREMARK" VARCHAR2(2 BYTE), 
	"PHONENUMBER" VARCHAR2(20 BYTE), 
	"DEDUCTPOINTS" VARCHAR2(10 BYTE), 
	"ZHIDUINUMBER" VARCHAR2(20 BYTE), 
	"ADDRESS" VARCHAR2(200 BYTE), 
	"NAME" VARCHAR2(20 BYTE), 
	"FILENAME" VARCHAR2(50 BYTE), 
	"LICENCENUMBER" VARCHAR2(20 BYTE), 
	"STATUS" VARCHAR2(20 BYTE), 
	"TIME" DATE, 
	"PHOTO" VARCHAR2(2 BYTE)
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 
 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS" ;

   COMMENT ON COLUMN "SIX"."HISTORY"."ID" IS '自增';
   COMMENT ON COLUMN "SIX"."HISTORY"."IDCARD" IS '身份证号';
   COMMENT ON COLUMN "SIX"."HISTORY"."LICENCE" IS '驾照级别';
   COMMENT ON COLUMN "SIX"."HISTORY"."SREMARK" IS '吸毒';
   COMMENT ON COLUMN "SIX"."HISTORY"."PHONENUMBER" IS '注册手机号';
   COMMENT ON COLUMN "SIX"."HISTORY"."DEDUCTPOINTS" IS '扣分';
   COMMENT ON COLUMN "SIX"."HISTORY"."ZHIDUINUMBER" IS '支队来的号码';
   COMMENT ON COLUMN "SIX"."HISTORY"."ADDRESS" IS '地址';
   COMMENT ON COLUMN "SIX"."HISTORY"."NAME" IS '姓名';
   COMMENT ON COLUMN "SIX"."HISTORY"."FILENAME" IS '照片文件名';
   COMMENT ON COLUMN "SIX"."HISTORY"."LICENCENUMBER" IS '驾驶证号码';
   COMMENT ON COLUMN "SIX"."HISTORY"."STATUS" IS '状态';
   COMMENT ON COLUMN "SIX"."HISTORY"."TIME" IS '日期';
   COMMENT ON COLUMN "SIX"."HISTORY"."PHOTO" IS '照片';
--------------------------------------------------------
--  DDL for Index HISTORY_PK
--------------------------------------------------------

  CREATE UNIQUE INDEX "SIX"."HISTORY_PK" ON "SIX"."HISTORY" ("ID") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  DDL for Trigger INSERTID
--------------------------------------------------------

  CREATE OR REPLACE TRIGGER "SIX"."INSERTID" 
   before insert on "SIX"."HISTORY" 
   for each row 
DECLARE
    NEXT_ID NUMBER;
begin  
   
    select SEQ.nextval into NEXT_ID from dual; 
    :NEW.ID :=NEXT_ID;
end;
/
ALTER TRIGGER "SIX"."INSERTID" ENABLE;
--------------------------------------------------------
--  Constraints for Table HISTORY
--------------------------------------------------------

  ALTER TABLE "SIX"."HISTORY" MODIFY ("TIME" NOT NULL ENABLE);
  ALTER TABLE "SIX"."HISTORY" ADD CONSTRAINT "HISTORY_PK" PRIMARY KEY ("ID")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS"  ENABLE;
  ALTER TABLE "SIX"."HISTORY" MODIFY ("ID" NOT NULL ENABLE);
