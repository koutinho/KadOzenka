CREATE OR REPLACE FUNCTION schema1.fias_houses_treeactualname (
  a_aoguid varchar,
  a_houseguid varchar,
  a_maskarray varchar [] = '{ZC,TP,LM,LP,ST,HS,BY,BG}'::character varying[]
)
RETURNS varchar AS
$body$
DECLARE
    c_HouseMaskArray CONSTANT VARCHAR(2)[3]:='{HS,BY,BG}'; /* Массив масок по умолчанию*/
    c_PostIndexMask	 CONSTANT  VARCHAR(2)[1] :='{ZC}'; /* Маска почтовый индекс */
    c_HouseNoMask    CONSTANT  VARCHAR(2)[1] :='{HS}';
    c_BodyNoMask     CONSTANT  VARCHAR(2)[1] :='{BY}'; /* Маска корпуса*/
    c_BuildingNoMask	  CONSTANT VARCHAR(2)[1] :='{BG}';/* Маска строения*/
    c_HouseShortTypeName  CONSTANT VARCHAR(10):='д.';
    c_BuildShortTypeName  CONSTANT VARCHAR(10):='корп.';
    c_StructShortTypeName CONSTANT VARCHAR(10):='стр.';
    v_ENDDATE  TIMESTAMP;     /* Окончание действия записи */
    v_HOUSENUM VARCHAR(10);   /* Номер дома */
    v_BUILDNUM VARCHAR(10);	  /* Номер корпуса */
    v_STRUCNUM VARCHAR(10);	  /* Номер строения */
    v_POSTALCODE VARCHAR(10); /* Почтовый индекс */
    v_TreeAddressObjectName VARCHAR(1000); /* Полное в иерархии название объекта*/ 
    v_Return_Error Integer :=0; /* Код возврата */
--*******************************************************       
--*******************************************************
BEGIN
    v_TreeAddressObjectName:=fias_AddressObjects_TreeActualName
                                   (a_AOGUID,a_MaskArray);
    SELECT INTO v_ENDDATE MAX(ENDDATE) 
        FROM fias_house
        WHERE AOGUID=a_AOGUID AND HOUSEGUID=a_HOUSEGUID;
    SELECT INTO v_HOUSENUM,v_BUILDNUM,v_STRUCNUM, v_POSTALCODE 
    			HOUSENUM, BUILDNUM, STRUCNUM, POSTALCODE  
        FROM fias_house 
        WHERE AOGUID=a_AOGUID AND HOUSEGUID=a_HOUSEGUID
                    AND ENDDATE=v_ENDDATE;
    IF  c_HouseNoMask <@ a_MaskArray 
            AND COALESCE(TRIM(v_HOUSENUM),'')<>'' THEN
        v_TreeAddressObjectName:=v_TreeAddressObjectName||
                    CASE WHEN v_TreeAddressObjectName='' THEN '' 
                                ELSE ', ' ||c_HouseShortTypeName||' '||v_HOUSENUM 
                    END;
    END IF;			
    IF  c_BodyNoMask <@ a_MaskArray 
            AND COALESCE(TRIM(v_BUILDNUM),'')<>'' THEN
        v_TreeAddressObjectName:=v_TreeAddressObjectName||
                CASE WHEN v_TreeAddressObjectName='' THEN '' 
                        ELSE ', ' ||	c_BuildShortTypeName||' '||v_BUILDNUM 
                END;
    END IF;							
    IF  c_BuildingNoMask <@ a_MaskArray 
            AND COALESCE(TRIM(v_STRUCNUM),'')<>'' THEN
        v_TreeAddressObjectName:=v_TreeAddressObjectName||
                CASE WHEN v_TreeAddressObjectName='' THEN '' 
                        ELSE ', ' ||	c_StructShortTypeName||' '||v_STRUCNUM 
                 END;
    END IF;	
    /* Почтоый индекс */
    IF  c_PostIndexMask <@ a_MaskArray 
            AND COALESCE(TRIM(v_POSTALCODE),'') <> '' THEN
            v_TreeAddressObjectName:=
                CASE WHEN v_TreeAddressObjectName='' THEN '' 
                        ELSE v_POSTALCODE || ', '
                 END || v_TreeAddressObjectName;
    END IF;	
    						
    RETURN 	v_TreeAddressObjectName;
 END;
$body$
LANGUAGE 'plpgsql'
VOLATILE
CALLED ON NULL INPUT
SECURITY INVOKER
COST 100;

COMMENT ON FUNCTION schema1.fias_houses_treeactualname(a_aoguid varchar, a_houseguid varchar, a_maskarray varchar [])
IS 'Возвращает строку с адресом дома в соответствии с массивом масок';
