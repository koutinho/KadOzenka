using System;

public class CIPJSKO_798_Delete_View_And_Table_For_Reports_Which_Go_Around_Platform
{
	public void Up()
	{
		@"
			--удаление view all_reports_in_system
			DROP VIEW IF EXISTS all_reports_in_system;
			delete from core_register_attribute where registerid = 1000811
			delete from core_register where registerid = 1000811;

			--удаление таблицы COMMON_REPORT_FILES
			DROP TABLE IF EXISTS COMMON_REPORT_FILES;
			delete from core_register_attribute where registerid = 810;
			delete from core_register where registerid = 810;
			delete from core_register_relation where id=222;

			--добавление новой раскладки
			delete from core_layout where layoutid=1000811;
			delete from core_layout_details  where layoutid=1000811;
		";

		//		insert into core_layout ("layoutid", "layoutname", "layoutcomment", "registerid", "defaultsort", "preffered", "username", "createdate", "qsquery", "isdistinct", "ordertype", "iscommon", "internal_name", "enable_minicards_mode", "register_view_id", "as_domain_id", "column_width_type", "is_using_extended_editor") values
		//(10009365, 'Раскладка по умолчанию', 'Данная раскладка находится на поддержке и обновляется одновременно с установкой новой версии системы', 809, 1002700, NULL, NULL, TO_TIMESTAMP('2021.03.16 17:45:10', 'YYYY.MM.DD HH24:MI:SS'), '<?xml version="1.0" encoding="utf-16"?>
		//<QSQuery xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
		//  <MainRegisterID>809</MainRegisterID>
		//  <TDInstanceID>0</TDInstanceID>
		//  <Columns />
		//  <ActualDate>0001-01-01T00:00:00</ActualDate>
		//  <IsActual>false</IsActual>
		//  <Distinct>false</Distinct>
		//  <ManualJoin>false</ManualJoin>
		//  <PackageSize>0</PackageSize>
		//  <PackageIndex>0</PackageIndex>
		//  <OrderBy />
		//  <GroupBy />
		//  <RegisterLinks />
		//  <JoinType xsi:nil="true" />
		//  <Joins />
		//  <Parameters />
		//  <SubMapRegisters />
		//  <ExcludeLinks />
		//  <DefaultAlias>false</DefaultAlias>
		//  <AddPKColumn>true</AddPKColumn>
		//  <LoadRelations>false</LoadRelations>
		//</QSQuery>', 0, 'DESC', 1, NULL, 0, 'CreatedReports', NULL, NULL, 0)
		//on conflict (layoutid) do update set
		//"layoutname"='Раскладка по умолчанию', "layoutcomment"='Данная раскладка находится на поддержке и обновляется одновременно с установкой новой версии системы', "registerid"=809, "defaultsort"=1002700, "preffered"=NULL, "username"=NULL, "createdate"=TO_TIMESTAMP('2021.03.16 17:45:10', 'YYYY.MM.DD HH24:MI:SS'), "qsquery"='<?xml version="1.0" encoding="utf-16"?>
		//<QSQuery xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
		//  <MainRegisterID>809</MainRegisterID>
		//  <TDInstanceID>0</TDInstanceID>
		//  <Columns />
		//  <ActualDate>0001-01-01T00:00:00</ActualDate>
		//  <IsActual>false</IsActual>
		//  <Distinct>false</Distinct>
		//  <ManualJoin>false</ManualJoin>
		//  <PackageSize>0</PackageSize>
		//  <PackageIndex>0</PackageIndex>
		//  <OrderBy />
		//  <GroupBy />
		//  <RegisterLinks />
		//  <JoinType xsi:nil="true" />
		//  <Joins />
		//  <Parameters />
		//  <SubMapRegisters />
		//  <ExcludeLinks />
		//  <DefaultAlias>false</DefaultAlias>
		//  <AddPKColumn>true</AddPKColumn>
		//  <LoadRelations>false</LoadRelations>
		//</QSQuery>', "isdistinct"=0, "ordertype"='DESC', "iscommon"=1, "internal_name"=NULL, "enable_minicards_mode"=0, "register_view_id"='CreatedReports', "as_domain_id"=NULL, "column_width_type"=NULL, "is_using_extended_editor"=0;


		//		--<DO>--
		//insert into core_layout_details ("id", "layoutid", "detailtype", "ordinal", "attributeid", "sortbyattribute", "referenceid", "headertext", "headerwidth", "visible", "format", "datatype", "expression", "sqlexpression", "totaltext", "totaltype", "style", "enablestyle", "textalign", "qscolumn", "export_column_name") values
		//(1002699, 10009365, 0, 5, 95000400, NULL, NULL, 'Полное имя пользователя', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
		//<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
		//  <RowStyle>false</RowStyle>
		//  <Conditions />
		//</StyleConditionItemWrapper>', NULL, NULL, NULL, NULL)
		//on conflict (id) do update set
		//"layoutid"=10009365, "detailtype"=0, "ordinal"=5, "attributeid"=95000400, "sortbyattribute"=NULL, "referenceid"=NULL, "headertext"='Полное имя пользователя', "headerwidth"=NULL, "visible"=1, "format"=NULL, "datatype"=4, "expression"=NULL, "sqlexpression"=NULL, "totaltext"=NULL, "totaltype"=NULL, "style"='<?xml version="1.0" encoding="utf-16"?>
		//<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
		//  <RowStyle>false</RowStyle>
		//  <Conditions />
		//</StyleConditionItemWrapper>', "enablestyle"=NULL, "textalign"=NULL, "qscolumn"=NULL, "export_column_name"=NULL;

		//--<DO>--
		//insert into core_layout_details ("id", "layoutid", "detailtype", "ordinal", "attributeid", "sortbyattribute", "referenceid", "headertext", "headerwidth", "visible", "format", "datatype", "expression", "sqlexpression", "totaltext", "totaltype", "style", "enablestyle", "textalign", "qscolumn", "export_column_name") values
		//(1002700, 10009365, 0, 2, 80900800, NULL, NULL, 'Дата создания отчета', NULL, 1, NULL, 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
		//<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
		//  <RowStyle>false</RowStyle>
		//  <Conditions />
		//</StyleConditionItemWrapper>', NULL, NULL, NULL, NULL)
		//on conflict (id) do update set
		//"layoutid"=10009365, "detailtype"=0, "ordinal"=2, "attributeid"=80900800, "sortbyattribute"=NULL, "referenceid"=NULL, "headertext"='Дата создания отчета', "headerwidth"=NULL, "visible"=1, "format"=NULL, "datatype"=5, "expression"=NULL, "sqlexpression"=NULL, "totaltext"=NULL, "totaltype"=NULL, "style"='<?xml version="1.0" encoding="utf-16"?>
		//<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
		//  <RowStyle>false</RowStyle>
		//  <Conditions />
		//</StyleConditionItemWrapper>', "enablestyle"=NULL, "textalign"=NULL, "qscolumn"=NULL, "export_column_name"=NULL;

		//--<DO>--
		//insert into core_layout_details ("id", "layoutid", "detailtype", "ordinal", "attributeid", "sortbyattribute", "referenceid", "headertext", "headerwidth", "visible", "format", "datatype", "expression", "sqlexpression", "totaltext", "totaltype", "style", "enablestyle", "textalign", "qscolumn", "export_column_name") values
		//(1002701, 10009365, 0, 3, 80901300, NULL, NULL, 'Дата окончания', NULL, 1, NULL, 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
		//<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
		//  <RowStyle>false</RowStyle>
		//  <Conditions />
		//</StyleConditionItemWrapper>', NULL, NULL, NULL, NULL)
		//on conflict (id) do update set
		//"layoutid"=10009365, "detailtype"=0, "ordinal"=3, "attributeid"=80901300, "sortbyattribute"=NULL, "referenceid"=NULL, "headertext"='Дата окончания', "headerwidth"=NULL, "visible"=1, "format"=NULL, "datatype"=5, "expression"=NULL, "sqlexpression"=NULL, "totaltext"=NULL, "totaltype"=NULL, "style"='<?xml version="1.0" encoding="utf-16"?>
		//<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
		//  <RowStyle>false</RowStyle>
		//  <Conditions />
		//</StyleConditionItemWrapper>', "enablestyle"=NULL, "textalign"=NULL, "qscolumn"=NULL, "export_column_name"=NULL;

		//--<DO>--
		//insert into core_layout_details ("id", "layoutid", "detailtype", "ordinal", "attributeid", "sortbyattribute", "referenceid", "headertext", "headerwidth", "visible", "format", "datatype", "expression", "sqlexpression", "totaltext", "totaltype", "style", "enablestyle", "textalign", "qscolumn", "export_column_name") values
		//(1002702, 10009365, 0, 4, 80900400, NULL, NULL, 'Наименование отчета', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
		//<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
		//  <RowStyle>false</RowStyle>
		//  <Conditions />
		//</StyleConditionItemWrapper>', NULL, NULL, NULL, NULL)
		//on conflict (id) do update set
		//"layoutid"=10009365, "detailtype"=0, "ordinal"=4, "attributeid"=80900400, "sortbyattribute"=NULL, "referenceid"=NULL, "headertext"='Наименование отчета', "headerwidth"=NULL, "visible"=1, "format"=NULL, "datatype"=4, "expression"=NULL, "sqlexpression"=NULL, "totaltext"=NULL, "totaltype"=NULL, "style"='<?xml version="1.0" encoding="utf-16"?>
		//<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
		//  <RowStyle>false</RowStyle>
		//  <Conditions />
		//</StyleConditionItemWrapper>', "enablestyle"=NULL, "textalign"=NULL, "qscolumn"=NULL, "export_column_name"=NULL;

		//--<DO>--
		//insert into core_layout_details ("id", "layoutid", "detailtype", "ordinal", "attributeid", "sortbyattribute", "referenceid", "headertext", "headerwidth", "visible", "format", "datatype", "expression", "sqlexpression", "totaltext", "totaltype", "style", "enablestyle", "textalign", "qscolumn", "export_column_name") values
		//(1002703, 10009365, 3, 1, NULL, NULL, NULL, 'Статус', NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
		//<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
		//  <RowStyle>false</RowStyle>
		//  <Conditions />
		//</StyleConditionItemWrapper>', NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
		//<QSColumn xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xsi:type="QSColumnSwitch">
		//  <Alias>Колонка_1</Alias>
		//  <ValueToCompare xsi:type="QSColumnSimple">
		//    <Alias>Колонка_1</Alias>
		//    <AttributeID>80901200</AttributeID>
		//    <Type>Value</Type>
		//    <Level>0</Level>
		//  </ValueToCompare>
		//  <Blocks>
		//    <QSColumnSwitchBlock>
		//      <ValueToCompare xsi:type="QSColumnConstant">
		//        <Alias>Колонка_3</Alias>
		//        <Value xsi:type="xsd:long">0</Value>
		//      </ValueToCompare>
		//      <Result xsi:type="QSColumnConstant">
		//        <Alias>Колонка_4</Alias>
		//        <Value xsi:type="xsd:string">Создана</Value>
		//      </Result>
		//    </QSColumnSwitchBlock>
		//    <QSColumnSwitchBlock>
		//      <ValueToCompare xsi:type="QSColumnConstant">
		//        <Alias>Колонка_5</Alias>
		//        <Value xsi:type="xsd:long">1</Value>
		//      </ValueToCompare>
		//      <Result xsi:type="QSColumnConstant">
		//        <Alias>Колонка_6</Alias>
		//        <Value xsi:type="xsd:string">Запущена</Value>
		//      </Result>
		//    </QSColumnSwitchBlock>
		//    <QSColumnSwitchBlock>
		//      <ValueToCompare xsi:type="QSColumnConstant">
		//        <Alias>Колонка_7</Alias>
		//        <Value xsi:type="xsd:long">2</Value>
		//      </ValueToCompare>
		//      <Result xsi:type="QSColumnConstant">
		//        <Alias>Колонка_8</Alias>
		//        <Value xsi:type="xsd:string">Завершена</Value>
		//      </Result>
		//    </QSColumnSwitchBlock>
		//    <QSColumnSwitchBlock>
		//      <ValueToCompare xsi:type="QSColumnConstant">
		//        <Alias>Колонка_9</Alias>
		//        <Value xsi:type="xsd:long">3</Value>
		//      </ValueToCompare>
		//      <Result xsi:type="QSColumnConstant">
		//        <Alias>Колонка_10</Alias>
		//        <Value xsi:type="xsd:string">Ошибка</Value>
		//      </Result>
		//    </QSColumnSwitchBlock>
		//  </Blocks>
		//</QSColumn>', NULL)
		//on conflict (id) do update set
		//"layoutid"=10009365, "detailtype"=3, "ordinal"=1, "attributeid"=NULL, "sortbyattribute"=NULL, "referenceid"=NULL, "headertext"='Статус', "headerwidth"=NULL, "visible"=1, "format"=NULL, "datatype"=NULL, "expression"=NULL, "sqlexpression"=NULL, "totaltext"=NULL, "totaltype"=NULL, "style"='<?xml version="1.0" encoding="utf-16"?>
		//<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
		//  <RowStyle>false</RowStyle>
		//  <Conditions />
		//</StyleConditionItemWrapper>', "enablestyle"=NULL, "textalign"=NULL, "qscolumn"='<?xml version="1.0" encoding="utf-16"?>
		//<QSColumn xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xsi:type="QSColumnSwitch">
		//  <Alias>Колонка_1</Alias>
		//  <ValueToCompare xsi:type="QSColumnSimple">
		//    <Alias>Колонка_1</Alias>
		//    <AttributeID>80901200</AttributeID>
		//    <Type>Value</Type>
		//    <Level>0</Level>
		//  </ValueToCompare>
		//  <Blocks>
		//    <QSColumnSwitchBlock>
		//      <ValueToCompare xsi:type="QSColumnConstant">
		//        <Alias>Колонка_3</Alias>
		//        <Value xsi:type="xsd:long">0</Value>
		//      </ValueToCompare>
		//      <Result xsi:type="QSColumnConstant">
		//        <Alias>Колонка_4</Alias>
		//        <Value xsi:type="xsd:string">Создана</Value>
		//      </Result>
		//    </QSColumnSwitchBlock>
		//    <QSColumnSwitchBlock>
		//      <ValueToCompare xsi:type="QSColumnConstant">
		//        <Alias>Колонка_5</Alias>
		//        <Value xsi:type="xsd:long">1</Value>
		//      </ValueToCompare>
		//      <Result xsi:type="QSColumnConstant">
		//        <Alias>Колонка_6</Alias>
		//        <Value xsi:type="xsd:string">Запущена</Value>
		//      </Result>
		//    </QSColumnSwitchBlock>
		//    <QSColumnSwitchBlock>
		//      <ValueToCompare xsi:type="QSColumnConstant">
		//        <Alias>Колонка_7</Alias>
		//        <Value xsi:type="xsd:long">2</Value>
		//      </ValueToCompare>
		//      <Result xsi:type="QSColumnConstant">
		//        <Alias>Колонка_8</Alias>
		//        <Value xsi:type="xsd:string">Завершена</Value>
		//      </Result>
		//    </QSColumnSwitchBlock>
		//    <QSColumnSwitchBlock>
		//      <ValueToCompare xsi:type="QSColumnConstant">
		//        <Alias>Колонка_9</Alias>
		//        <Value xsi:type="xsd:long">3</Value>
		//      </ValueToCompare>
		//      <Result xsi:type="QSColumnConstant">
		//        <Alias>Колонка_10</Alias>
		//        <Value xsi:type="xsd:string">Ошибка</Value>
		//      </Result>
		//    </QSColumnSwitchBlock>
		//  </Blocks>
		//</QSColumn>', "export_column_name"=NULL;
	}

	public void Down()
	{
		@"	--восстановление таблицы COMMON_REPORT_FILES
			
			create table common_report_files
			(
			    id             bigint       not null
			        constraint reg_810_q_pk
			            primary key,
			    user_id        bigint       not null,
			    creation_date  timestamp    not null,
			    status         bigint       not null,
			    file_name      varchar(255) not null,
			    finish_date    timestamp,
			    file_extension varchar(255) not null
			);
			comment on table common_report_files is 'Таблица для созданных отчетов';
			alter table common_report_files owner to cipjs_kad_ozenka;

			INSERT INTO core_register (registerid, registername, registerdescription, allpri_table, object_table, quant_table, track_changes_column, storage_type, object_sequence, is_virtual, contains_quant_in_future, db_connection_name, track_changes_userid, track_changes_date, is_deleted, allpri_partitioning, main_register) VALUES (810, 'Common.ReportFiles', 'Таблица для созданных отчетов', null, null, 'COMMON_REPORT_FILES', null, 4, 'REG_OBJECT_SEQ', 0, 0, null, null, null, null, null, null);
			
			INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (81000100, 'Идентификатор', 810, 1, null, null, 'ID', null, null, 1, null, null, 'Id', 0, null, null, null, null, 2, '2020-12-24 10:43:17.735173', 0);
			INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (81000200, 'ИД пользователя', 810, 1, null, null, 'user_id', null, null, 0, null, null, 'UserId', 0, null, null, null, 0, 2, '2020-12-24 10:44:55.410758', 0);
			INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (81000800, 'Расширение файла', 810, 4, null, null, 'file_extension', null, null, 0, null, null, 'FileExtension', 0, null, null, null, 0, 2, '2020-12-24 15:10:47.348561', 0);
			INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (81000300, 'Дата создания', 810, 5, null, null, 'creation_date', null, null, 0, null, null, 'CreationDate', 0, null, null, null, 0, 2, '2020-12-24 10:48:16.873168', 0);
			INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (81000500, 'Статус', 810, 4, null, 802, null, 'status', null, 0, null, null, 'Status', 0, null, null, null, 0, 2, '2020-12-24 11:00:24.453443', 0);
			INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (81000600, 'Имя файла', 810, 4, null, null, 'file_name', null, null, 0, null, null, 'FileName', 0, null, null, null, 0, 2, '2020-12-24 12:13:23.174627', 0);
			INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (81000700, 'Дата Завершения', 810, 5, null, null, 'finish_date', null, null, 0, null, null, 'FinishDate', 1, null, null, null, 0, 2, '2020-12-24 12:26:04.790233', 0);

			INSERT INTO core_register_relation (id, name, parentregister, chieldregister, cardinality, kindid, parentregister_attribute_id, qscondition) VALUES (222, 'От созданных отчетов к пользователю', 950, 1000811, null, 81100200, null, null);


			--восстановление view all_reports_in_system

			CREATE VIEW public.all_reports_in_system (
			    id,
			    file_name,
			    creation_date,
			    finish_date,
			    user_id,
			    status,
			    is_platform)
			AS
			SELECT common_report_files.id,
			    common_report_files.file_name,
			    common_report_files.creation_date,
			    common_report_files.finish_date,
			    common_report_files.user_id,
			        CASE
			            WHEN common_report_files.status = 0 THEN 'Создана'::text
			            WHEN common_report_files.status = 1 THEN 'Запущена'::text
			            WHEN common_report_files.status = 2 THEN 'Завершена'::text
			            WHEN common_report_files.status = 3 THEN 'Ошибка'::text
			            ELSE NULL::text
			        END AS status,
			    (
			    SELECT false AS bool
			    ) AS is_platform
			FROM common_report_files
			UNION ALL
			SELECT fm_reports_savedreport.id,
			    fm_reports_savedreport.title AS file_name,
			    fm_reports_savedreport.create_date AS creation_date,
			    fm_reports_savedreport.end_date AS finish_date,
			    fm_reports_savedreport.user_id,
			        CASE
			            WHEN fm_reports_savedreport.status = 0 THEN 'Создана'::text
			            WHEN fm_reports_savedreport.status = 1 THEN 'Запущена'::text
			            WHEN fm_reports_savedreport.status = 2 THEN 'Завершена'::text
			            WHEN fm_reports_savedreport.status = 3 THEN 'Ошибка'::text
			            ELSE NULL::text
			        END AS status,
			    (
			    SELECT true AS bool
			    ) AS is_platform
			FROM fm_reports_savedreport;
			ALTER VIEW all_reports_in_system OWNER TO cipjs_kad_ozenka;

			INSERT INTO core_register (registerid, registername, registerdescription, allpri_table, object_table, quant_table, track_changes_column, storage_type, object_sequence, is_virtual, contains_quant_in_future, db_connection_name, track_changes_userid, track_changes_date, is_deleted, allpri_partitioning, main_register) VALUES (1000811, 'Common.AllReportsInSystemView', 'View со всеми отчетами в системе (платформенные + сгенерированные вручную через длительный процесс)', null, null, 'all_reports_in_system', null, 4, 'REG_OBJECT_SEQ', 0, 0, null, null, null, null, null, null);
			
			INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (81100300, 'Имя файла', 1000811, 4, null, null, 'file_name', null, null, 0, null, null, 'FileName', 0, null, null, null, 0, 2, '2020-12-25 00:00:00.000000', 0);
			INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (81100400, 'Дата создания', 1000811, 5, null, null, 'creation_date', null, null, 0, null, null, 'CreationDate', 0, null, null, null, 0, 2, '2020-12-25 00:00:00.000000', 0);
			INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (81100500, 'Дата завершения', 1000811, 5, null, null, 'finish_date', null, null, 0, null, null, 'FinishDate', 1, null, null, null, 0, 2, '2020-12-25 00:00:00.000000', 0);
			INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (81100700, 'Статус', 1000811, 4, null, null, 'status', null, null, 0, null, null, 'Status', 0, null, null, null, 0, 2, '2020-12-25 00:00:00.000000', 0);
			INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (81100600, 'Платформенный отчет', 1000811, 3, null, null, 'is_platform', null, null, 0, null, null, 'IsPlatformReport', 0, null, null, null, 0, 2, '2020-12-25 00:00:00.000000', 0);
			INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (81100100, 'Идентификатор', 1000811, 1, null, null, 'id', null, null, 1, null, null, 'Id', 0, null, null, null, 0, 2, '2020-12-25 00:00:00.000000', 0);
			INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (81100200, 'ИД пользователя', 1000811, 1, null, null, 'user_id', null, null, 0, null, null, 'UserId', 0, null, null, null, 0, 2, '2020-12-25 00:00:00.000000', 0);


			--удаление новой раскладки
			delete from core_layout where layoutid=10009365;
			delete from core_layout_details  where layoutid=10009365;
		";
	}
}
