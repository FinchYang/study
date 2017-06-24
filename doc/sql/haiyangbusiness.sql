--------------------------------------------------------
--  �ļ��Ѵ��� - ������-����-28-2017   
--------------------------------------------------------
--------------------------------------------------------
--  DDL for Table haiyangbusiness
--------------------------------------------------------

  CREATE TABLE "CITY"."haiyangbusiness" 
   (	"ID" NUMBER, 
	"TYPE" NUMBER, 
	"START_TIME" VARCHAR2(10 BYTE), 
	"END_TIME" VARCHAR2(10 BYTE), 
	"STATUS" NUMBER, 
	"QUEUE_NUM" NVARCHAR2(10), 
	"ID_NUM" NVARCHAR2(25), 
	"ADDRESS" NVARCHAR2(100), 
	"SERIAL_NUM" VARCHAR2(20 BYTE), 
	"REJECT_REASON" VARCHAR2(100 BYTE), 
	"NAME" VARCHAR2(100 BYTE), 
	"PHONE_NUM" VARCHAR2(50 BYTE), 
	"PROCESS_USER" VARCHAR2(50 BYTE), 
	"FILE_RECV_USER" VARCHAR2(50 BYTE), 
	"TRANSFER_STATUS" NUMBER DEFAULT 0, 
	"UPLOADER" VARCHAR2(50 BYTE), 
	"COMPLETE_PAY_USER" VARCHAR2(50 BYTE), 
	"ATTENTION" VARCHAR2(100 BYTE), 
	"UNLOAD_TASK_NUM" NVARCHAR2(20), 
	"COUNTYCODE" VARCHAR2(20 BYTE)
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS" ;

   COMMENT ON COLUMN "CITY"."haiyangbusiness"."ID" IS 'ҵ���ID';
   COMMENT ON COLUMN "CITY"."haiyangbusiness"."TYPE" IS 'ҵ�����ͣ�
1.��������
2.����׼�ݳ�������
....';
   COMMENT ON COLUMN "CITY"."haiyangbusiness"."START_TIME" IS 'ҵ���ϴ���ʱ��';
   COMMENT ON COLUMN "CITY"."haiyangbusiness"."END_TIME" IS 'ҵ����ɵ�ʱ��';
   COMMENT ON COLUMN "CITY"."haiyangbusiness"."STATUS" IS 'ҵ���״̬
1.��ɨ����ɲ��ϴ�
2.�ϴ���һ���ֵ�����
3.���ڴ��������
4.�Ѿܾ����������
5.����ɵ�δ�ɷ�
6.�޷����������
7.�ѽɷ�
8.����ȡ��֤ 9.����ύ';
   COMMENT ON COLUMN "CITY"."haiyangbusiness"."QUEUE_NUM" IS '�ŶӺ�';
   COMMENT ON COLUMN "CITY"."haiyangbusiness"."ID_NUM" IS '���֤��';
   COMMENT ON COLUMN "CITY"."haiyangbusiness"."ADDRESS" IS '������ַ';
   COMMENT ON COLUMN "CITY"."haiyangbusiness"."SERIAL_NUM" IS '����һƽ̨��ˮ��';
   COMMENT ON COLUMN "CITY"."haiyangbusiness"."REJECT_REASON" IS '�ܾ�ԭ��ֻ�����񱻾ܾ�ʱ��Ч��';
   COMMENT ON COLUMN "CITY"."haiyangbusiness"."PHONE_NUM" IS '�绰����';
   COMMENT ON COLUMN "CITY"."haiyangbusiness"."PROCESS_USER" IS '������';
   COMMENT ON COLUMN "CITY"."haiyangbusiness"."FILE_RECV_USER" IS '���յ������û������ڵ����ƽ�';
   COMMENT ON COLUMN "CITY"."haiyangbusiness"."TRANSFER_STATUS" IS '�����ƽ�״̬
0.δ�ƽ�
1.�ѷ����ƽ������Է�δ����
2.�ѽ���';
   COMMENT ON COLUMN "CITY"."haiyangbusiness"."UPLOADER" IS '�����ϴ��˺�';
   COMMENT ON COLUMN "CITY"."haiyangbusiness"."COMPLETE_PAY_USER" IS '��ɽɷ��û�';
   COMMENT ON COLUMN "CITY"."haiyangbusiness"."ATTENTION" IS '�ص��ע��ҵ�񣬹�עԭ��';
   COMMENT ON COLUMN "CITY"."haiyangbusiness"."UNLOAD_TASK_NUM" IS '��Ϣ�ɼ�ϵͳ�ϴ���ҵ����';
--------------------------------------------------------
--  DDL for Index haiyangbusiness_PK
--------------------------------------------------------

  CREATE UNIQUE INDEX "CITY"."haiyangbusiness_PK" ON "CITY"."haiyangbusiness" ("ID") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  Constraints for Table haiyangbusiness
--------------------------------------------------------

  ALTER TABLE "CITY"."haiyangbusiness" MODIFY ("QUEUE_NUM" NOT NULL ENABLE);
  ALTER TABLE "CITY"."haiyangbusiness" MODIFY ("STATUS" NOT NULL ENABLE);
  ALTER TABLE "CITY"."haiyangbusiness" MODIFY ("START_TIME" NOT NULL ENABLE);
  ALTER TABLE "CITY"."haiyangbusiness" MODIFY ("TYPE" NOT NULL ENABLE);
  ALTER TABLE "CITY"."haiyangbusiness" MODIFY ("ID" NOT NULL ENABLE);
  ALTER TABLE "CITY"."haiyangbusiness" MODIFY ("COUNTYCODE" NOT NULL ENABLE);
  ALTER TABLE "CITY"."haiyangbusiness" ADD CONSTRAINT "haiyangbusiness_PK" PRIMARY KEY ("ID")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS"  ENABLE;
