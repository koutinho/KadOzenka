CREATE OR REPLACE FUNCTION schema1.fias_houses_addressobjecttree (
  a_houseguid varchar,
  a_enddate timestamp = '2079-06-06 00:00:00'::timestamp without time zone
)
RETURNS TABLE (
  rtf_guid varchar,
  rtf_currstatus integer,
  rtf_actstatus integer,
  rtf_aolevel integer,
  rtf_shorttypename varchar,
  rtf_addressobjectname varchar
) AS
$body$
DECLARE
    c_MaxEndDate CONSTANT TIMESTAMP:=TO_TIMESTAMP('2079-06-06','YYYY-MM-DD');
    c_ActualStatusCode CONSTANT INTEGER :=1;	
                              /* Признак актуальной записи адресного объекта */
    c_NotActualStatusCode CONSTANT INTEGER :=0; 
                              /* Значени кода актуальной записи */
    v_AOGUID VARCHAR(36); /* Глобальный уникальный */
                             /* идентификатор адресного объекта*/
    v_CurrStatus INTEGER; /* Статус актуальности КЛАДР 4: */
                             /* 0 - актуальный, */
                            /*	1-50 - исторический, */
                           /* т.е. объект был переименован, */
                           /* в данной записи приведено */
                           /* одно из прежних его наименований, */
                           /* 51 - переподчиненный*/
    v_Return_Error Integer :=0; /* Код возврата */
--*******************************************************************       
--*******************************************************************
 BEGIN
    SELECT INTO v_AOGUID,v_CurrStatus h.AOGUID,
                CASE WHEN 0 < ALL(SELECT iao.currstatus 
                                        FROM fias_addrobj iao 
                                        WHERE ao.aoguid = iao.aoguid)
                    THEN (SELECT MAX(iao.currstatus) 
                                        FROM fias_addrobj iao 
                                        WHERE ao.aoguid = iao.aoguid)
                    ELSE 0 END
        FROM fias_house h INNER JOIN fias_addrobj ao ON h.AOGUID=ao.AOGUID 
        WHERE h.HOUSEGUID=a_HOUSEGUID 
            AND h.ENDDATE=COALESCE(a_ENDDATE,c_MaxEndDate)
        ORDER BY h.ENDDATE DESC;
    RETURN QUERY SELECT * FROM fias_Houses_AddressObjectTree(
                                                        v_AOGUID,a_HOUSEGUID,
                                                        v_CurrStatus,a_ENDDATE);
END;
$body$
LANGUAGE 'plpgsql'
VOLATILE
CALLED ON NULL INPUT
SECURITY INVOKER
COST 100 ROWS 1000;

COMMENT ON FUNCTION schema1.fias_houses_addressobjecttree(a_houseguid varchar, a_enddate timestamp)
IS 'Возвращает дерево (список взаимосвязанных строк) с дома характеристиками и его адресного объекта';
