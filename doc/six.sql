  CREATE TABLE "history" 
   (	"IDCARD" VARCHAR2(50 BYTE), 
	"SNAME" VARCHAR2(20 BYTE), 
	"LICENCE" VARCHAR2(10 BYTE), 
	"TIME" DATE, 
	"SREMARK" VARCHAR2(2 BYTE) DEFAULT 0, 
	"PHOTO" VARCHAR2(2 BYTE) DEFAULT 1, 
	"PHONENUMBER" VARCHAR2(20 BYTE), 
	"DEDUCTPOINTS" VARCHAR2(10 BYTE), 
	"LICENCENUMBER" VARCHAR2(30 BYTE), 
	"FILENAME" VARCHAR2(50 BYTE), 
	"STATUS" VARCHAR2(10 BYTE)
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 
 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS" ;

   COMMENT ON COLUMN "history"."IDCARD" IS '身份证';
   COMMENT ON COLUMN "history"."SNAME" IS '姓名';
   COMMENT ON COLUMN "history"."LICENCE" IS '驾驶证级别';
   COMMENT ON COLUMN "history"."TIME" IS '存入时间';
   COMMENT ON COLUMN "history"."SREMARK" IS '0：未吸毒 1：吸毒';
   COMMENT ON COLUMN "history"."PHOTO" IS '0:无照片 1：有照片';
   COMMENT ON COLUMN "history"."PHONENUMBER" IS '手机号';
   COMMENT ON COLUMN "history"."DEDUCTPOINTS" IS '积分';
   COMMENT ON COLUMN "history"."LICENCENUMBER" IS '驾照号';
   COMMENT ON COLUMN "history"."FILENAME" IS '文件名';
   COMMENT ON COLUMN "history"."STATUS" IS '状态';