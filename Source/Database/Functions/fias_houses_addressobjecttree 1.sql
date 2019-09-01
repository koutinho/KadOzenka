CREATE OR REPLACE FUNCTION schema1.fias_houses_addressobjecttree (
  a_aoguid varchar,
  a_houseguid varchar,
  a_currstatus integer = 0,
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
    c_HouseAOLevel CONSTANT INTEGER:=8;
    c_HouseShortTypeName CONSTANT VARCHAR(10):='д.';
    c_BuildShortTypeName CONSTANT VARCHAR(10):='корп.';
    c_StructShortTypeName CONSTANT VARCHAR(10):='стр.';
    c_StatusActual CONSTANT INTEGER:=1;	/* Признак актуальности записи */
    c_StatusNotActual CONSTANT INTEGER:=0; /* Признак неактальной записи записи */
    c_MAXENDDATE CONSTANT TIMESTAMP:=to_timestamp('2079-06-06 00:00:00', 
                                                                                      'YYYY-MM-DD');
    v_HouseActStatus 	INTEGER;	/* Признак актуальности для здания*/
    v_HouseCurrStatus INTEGER;	/* Признак актуальности для здания */
    v_ENDDATE TIMESTAMP;	/* Окончание действия записи */
    v_HOUSEGUID VARCHAR(36);	/* Глобальный уникальный идентификатор дома */
    v_HOUSENUM VARCHAR(10);	/* Номер дома */
    v_BUILDNUM VARCHAR(10);	/* Номер корпуса */
    v_STRUCNUM VARCHAR(10);	/* Номер строения */
    v_Return_Error Integer :=0;	/* Код возврата */
--************************************************************       
--************************************************************
 BEGIN
    RETURN QUERY SELECT * FROM fias_AddressObjects_AddressObjectTree
                                                        (a_AOGUID,a_CurrStatus);
    IF a_ENDDATE IS NULL THEN 
        SELECT INTO v_ENDDATE MAX(ENDDATE) 
                FROM fias_house WHERE AOGUID=a_AOGUID AND HOUSEGUID=a_HOUSEGUID;
    ELSE
        v_ENDDATE:=a_ENDDATE;
    END IF;
    SELECT INTO v_HOUSENUM,v_BUILDNUM,v_STRUCNUM,
                            v_ENDDATE,v_HouseCurrStatus
                    h.HOUSENUM,h.BUILDNUM,h.STRUCNUM,
                            h.ENDDATE,ah.HouseCurrStatus
        FROM fias_house h
            INNER JOIN (SELECT AOGUID,HOUSEGUID,ENDDATE, 
                           RANK() OVER (PARTITION BY AOGUID,
                           HOUSEGUID ORDER BY ENDDATE ASC) AS HouseCurrStatus
                        FROM fias_house insh  WHERE insh.AOGUID=a_AOGUID AND
                                                insh.HOUSEGUID=a_HOUSEGUID) as ah
				ON h.AOGUID=ah.AOGUID AND h.HOUSEGUID=ah.HOUSEGUID 
                                    AND h.ENDDATE=ah.ENDDATE
        WHERE h.ENDDATE=v_ENDDATE;					
    v_HouseActStatus:=CASE WHEN COALESCE(v_ENDDATE,c_MAXENDDATE)=
                    c_MAXENDDATE THEN c_StatusActual ELSE c_StatusNotActual END;
    v_HouseCurrStatus:=CASE WHEN COALESCE(v_ENDDATE,c_MAXENDDATE)=
                    c_MAXENDDATE THEN 0 ELSE v_HouseCurrStatus END;
    IF v_HOUSENUM IS NOT NULL OR v_HOUSENUM != '' THEN
        RETURN QUERY SELECT a_HOUSEGUID,v_HouseCurrStatus,v_HouseActStatus,
                    c_HouseAOLevel,c_HouseShortTypeName,v_HOUSENUM;
    END IF;
    IF v_BUILDNUM IS NOT NULL OR v_BUILDNUM != '' THEN
        RETURN QUERY SELECT a_HOUSEGUID,v_HouseCurrStatus,v_HouseActStatus,
                                    c_HouseAOLevel,c_BuildShortTypeName,v_BUILDNUM;
    END IF;
    IF v_STRUCNUM IS NOT NULL OR v_STRUCNUM != '' THEN
        RETURN QUERY SELECT a_HOUSEGUID,v_HouseCurrStatus,v_HouseActStatus,
                                 c_HouseAOLevel,c_StructShortTypeName,v_STRUCNUM;
    END IF;
  END;
$body$
LANGUAGE 'plpgsql'
VOLATILE
CALLED ON NULL INPUT
SECURITY INVOKER
COST 100 ROWS 1000;

COMMENT ON FUNCTION schema1.fias_houses_addressobjecttree(a_aoguid varchar, a_houseguid varchar, a_currstatus integer, a_enddate timestamp)
IS 'Возвращает дерево (список взаимосвязанных строк) с дома характеристиками и его адресного объекта';
