CREATE OR REPLACE FUNCTION schema1.fias_houses_treeactualname (
  a_houseguid varchar,
  a_maskarray varchar [] = '{ZC,TP,LM,LP,ST,HS,BY,BG}'::character varying[]
)
RETURNS varchar AS
$body$
DECLARE
    c_MaxEndDate CONSTANT TIMESTAMP:=TO_TIMESTAMP('2079-06-06','YYYY-MM-DD');
    v_AOGUID VARCHAR(36); /* Идентификтор адресного объекта */
    v_TreeAddressObjectName VARCHAR(1000); /* Полное в иерархии название объекта*/ 
    v_Return_Error Integer :=0; /* Код возврата */
--**********************************************************       
--**********************************************************
BEGIN
    SELECT INTO v_AOGUID h.AOGUID	
        FROM fias_house h 
            INNER JOIN fias_addrobj ao ON h.AOGUID=ao.AOGUID 
        WHERE h.HOUSEGUID=a_HOUSEGUID AND h.ENDDATE=c_MaxEndDate
        ORDER BY h.ENDDATE DESC;
    v_TreeAddressObjectName:=fias_Houses_TreeActualName
                                        (v_AOGUID,a_HOUSEGUID,a_MaskArray);
    RETURN 	v_TreeAddressObjectName;
END;
$body$
LANGUAGE 'plpgsql'
VOLATILE
CALLED ON NULL INPUT
SECURITY INVOKER
COST 100;

COMMENT ON FUNCTION schema1.fias_houses_treeactualname(a_houseguid varchar, a_maskarray varchar [])
IS 'Возвращает строку с адресом дома в соответствии с массивом масок';
