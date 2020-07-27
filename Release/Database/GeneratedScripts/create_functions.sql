﻿
-- XII. Загрузка функций
--<DO>--
--fias_addressobjects_addressobjecttree
DO $$
BEGIN
    CREATE OR REPLACE FUNCTION public.fias_addressobjects_addressobjecttree(a_aoguid character varying, a_currstatus integer DEFAULT NULL::integer)
 RETURNS TABLE(rtf_aoguid character varying, rtf_currstatus integer, rtf_actstatus integer, rtf_aolevel integer, rtf_shorttypename character varying, rtf_addressobjectname character varying)
 LANGUAGE plpgsql
AS $function$
DECLARE
 c_ActualStatusCode CONSTANT INTEGER :=1;    /* Признак актуальной записи адресообразующего элемента */
 c_NotActualStatusCode CONSTANT INTEGER :=0; /* Значение кода актуальной записи */
 v_AOGUID        VARCHAR(36); /* ИД адресообразующего элемента */
 v_ParentGUID    VARCHAR(36); /* Идентификатор родительского элемента */
 v_CurrStatus    INTEGER;     /* Статус актуальности КЛАДР 4*/
 v_ActStatus     INTEGER;     /* Статус актуальности адресообразующего элемента ФИАС. */
 v_AOLevel       INTEGER;     /* Уровень адресообразующего элемента  */
 v_ShortName     VARCHAR(10); /* Краткое наименование типа элемента */
 v_FormalName    VARCHAR(120);/* Формализованное наименование элемента */
 v_Return_Error  INTEGER;     /* Код возврата */
--***********************************************************************
--***********************************************************************
 BEGIN
 IF a_CurrStatus IS NOT NULL THEN
    SELECT INTO  v_AOGUID,v_ParentGUID,v_CurrStatus,v_ActStatus,v_AOLevel,
                              v_ShortName, v_FormalName
                               ao.AOGUID,ao.ParentGUID,ao.CurrStatus,ao.ActStatus,ao.AOLevel,
                              ao.ShortName, ao.FormalName
                  FROM fias_addrobj ao
	WHERE ao.AOGUID=a_AOGUID AND ao.CurrStatus=a_CurrStatus;
 ELSE
    SELECT INTO v_AOGUID,v_ParentGUID,v_CurrStatus,v_ActStatus,v_AOLevel,
                              v_ShortName, v_FormalName
                              ao.AOGUID,ao.ParentGUID,ao.CurrStatus,ao.ActStatus,ao.AOLevel,
                              ao.ShortName, ao.FormalName
                   FROM fias_addrobj ao
	WHERE ao.AOGUID=a_AOGUID AND ao.ActStatus=c_ActualStatusCode;
   IF NOT FOUND THEN
      SELECT INTO v_AOGUID,v_ParentGUID,v_CurrStatus,v_ActStatus,v_AOLevel,
                               v_ShortName, v_FormalName
                                  ao.AOGUID,ao.ParentGUID,ao.CurrStatus,ao.ActStatus,ao.AOLevel,
                                ao.ShortName, ao.FormalName
              FROM fias_addrobj ao
              WHERE ao.AOGUID=a_AOGUID 
                       AND ao.ActStatus=c_NotActualStatusCode
                      AND ao.currstatus = (SELECT MAX(iao.currstatus) 
                                                                 FROM fias_addrobj iao 
                                                                 WHERE ao.aoguid = iao.aoguid);
    END IF;
 END IF;
 
 IF v_AOGUID IS NOT NULL THEN
 	RETURN QUERY SELECT v_AOGUID,v_CurrStatus,v_ActStatus,v_AOLevel,
                                               v_ShortName,v_FormalName;
 END IF;
 
 WHILE v_ParentGUID IS NOT NULL LOOP
     SELECT INTO v_AOGUID,v_ParentGUID,v_CurrStatus,v_ActStatus,v_AOLevel,
                              v_ShortName, v_FormalName
                           ao.AOGUID,ao.ParentGUID,ao.CurrStatus,ao.ActStatus,ao.AOLevel,
                             ao.ShortName,ao.FormalName
         FROM fias_addrobj ao
         WHERE ao.AOGUID=v_ParentGUID AND ao.ActStatus=c_ActualStatusCode;
          IF NOT FOUND THEN   
             SELECT INTO v_AOGUID,v_ParentGUID,v_CurrStatus,v_ActStatus,v_AOLevel,
                                        v_ShortName,v_FormalName
                             ao.AOGUID,ao.ParentGUID,ao.CurrStatus,ao.ActStatus,ao.AOLevel,
                                        ao.ShortName, ao.FormalName
                 FROM fias_addrobj ao
                 WHERE ao.AOGUID=v_ParentGUID 
                               AND ao.ActStatus=c_NotActualStatusCode
                              AND ao.currstatus = (SELECT MAX(iao.currstatus) 
                                                               FROM fias_addrobj iao 
                                                               WHERE ao.aoguid = iao.aoguid);
          END IF;
          
          IF v_AOGUID IS NOT NULL THEN
             RETURN QUERY SELECT v_AOGUID, v_CurrStatus, v_ActStatus,
             					 v_AOLevel, v_ShortName, v_FormalName;
          END IF;
          
 END LOOP;
END $function$;

END $$;
--<DO>--
--fias_houses_addressobjecttree
DO $$
BEGIN
    CREATE OR REPLACE FUNCTION public.fias_houses_addressobjecttree(a_houseguid character varying, a_enddate timestamp without time zone DEFAULT '2079-06-06 00:00:00'::timestamp without time zone)
 RETURNS TABLE(rtf_guid character varying, rtf_currstatus integer, rtf_actstatus integer, rtf_aolevel integer, rtf_shorttypename character varying, rtf_addressobjectname character varying)
 LANGUAGE plpgsql
AS $function$
DECLARE
    c_MaxEndDate CONSTANT TIMESTAMP:=TO_TIMESTAMP('2079-06-06','YYYY-MM-DD');
    c_ActualStatusCode CONSTANT INTEGER :=1;	
                              /* Признак актуальной записи адресного объекта */
    c_NotActualStatusCode CONSTANT INTEGER :=0; 
    						 /* Значени кода актуальной записи */
    v_AOGUID VARCHAR(36);    /* Глобальный уникальный */
                             /* идентификатор адресного объекта*/
    v_CurrStatus INTEGER;    /* Статус актуальности КЛАДР 4: */
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
            AND h.ENDDATE=COALESCE(a_ENDDATE, c_MaxEndDate)
        ORDER BY h.ENDDATE DESC;
    RETURN QUERY SELECT * FROM fias_Houses_AddressObjectTree(v_AOGUID, a_HOUSEGUID, v_CurrStatus, a_ENDDATE);
END $function$;

END $$;
--<DO>--
--kladr_to_fias
DO $$
BEGIN
    CREATE OR REPLACE FUNCTION public.kladr_to_fias(a_codekladr character varying)
 RETURNS character varying
 LANGUAGE plpgsql
AS $function$
DECLARE
	v_aoguid    VARCHAR(36);	 /* ИД адресообразующего элемента */
    v_houseGuid VARCHAR(36);	 /* ИД здания */
    v_buildingNum NUMERIC;  	 /* Счетчик записей домов для КЛАДР 4 */
    v_buildingNumString VARCHAR(36);
BEGIN
  IF char_length(a_codekladr) < 15 THEN
    LOOP
      a_codekladr:= a_codekladr || '0';
      EXIT WHEN char_length(a_codekladr) = 15;
	END LOOP;
  END IF;

  SELECT INTO v_aoguid f.aoguid from fias_addrobj f
  	WHERE currstatus = 0 AND
      	  f.plaincode = SUBSTRING(a_codekladr from 1 for 15);
  
  v_buildingNumString:= SUBSTRING(a_codekladr from 16);
          
  IF v_buildingNumString != '' THEN
  		v_buildingNum:= CAST(v_buildingNumString AS INTEGER);
  END IF;
  
  IF v_buildingNum IS NOT NULL THEN
  		SELECT INTO v_houseGuid h.houseguid from fias_house h
  			WHERE h.counter = v_buildingNum and h.aoguid = v_aoguid;
  END IF;          

RETURN v_houseGuid;
END $function$;

END $$;
--<DO>--
--core_updstru_checkexist_primarykey
DO $$
BEGIN
    CREATE OR REPLACE FUNCTION public.core_updstru_checkexist_primarykey(character varying)
 RETURNS boolean
 LANGUAGE plpgsql
AS $function$
DECLARE
	tablename ALIAS FOR $1;
    result int = 0;
BEGIN
	SELECT COUNT(*)
    INTO result
	FROM information_schema.table_constraints as tc
	WHERE lower(tc.table_name) = lower(tablename) AND tc.constraint_type = 'PRIMARY KEY' AND tc.table_schema = 'public';

    if(result > 0)then
    	return true;
    else
    	return false;
    end if;
END $function$;

END $$;
--<DO>--
--fias_houses_treeactualname
DO $$
BEGIN
    CREATE OR REPLACE FUNCTION public.fias_houses_treeactualname(a_aoguid character varying, a_houseguid character varying, a_maskarray character varying[] DEFAULT '{ZC,TP,LM,LP,ST,HS,BY,BG}'::character varying[])
 RETURNS character varying
 LANGUAGE plpgsql
AS $function$
DECLARE
    c_HouseMaskArray 	  CONSTANT VARCHAR(2)[3]:='{HS,BY,BG}'; /* Массив масок по умолчанию*/
    c_PostIndexMask	 	  CONSTANT VARCHAR(2)[1] :='{ZC}'; /* Маска почтовый индекс */
    c_HouseNoMask    	  CONSTANT VARCHAR(2)[1] :='{HS}';
    c_BodyNoMask     	  CONSTANT VARCHAR(2)[1] :='{BY}'; /* Маска корпуса*/
    c_BuildingNoMask	  CONSTANT VARCHAR(2)[1] :='{BG}';/* Маска строения*/
    c_HouseShortTypeName  CONSTANT VARCHAR(10):='д.';
    c_BuildShortTypeName  CONSTANT VARCHAR(10):='корп.';
    c_StructShortTypeName CONSTANT VARCHAR(10):='стр.';
    v_ENDDATE    TIMESTAMP;   /* Окончание действия записи */
    v_HOUSENUM   VARCHAR(10); /* Номер дома */
    v_BUILDNUM   VARCHAR(10); /* Номер корпуса */
    v_STRUCNUM   VARCHAR(10); /* Номер строения */
    v_POSTALCODE VARCHAR(10); /* Почтовый индекс */
    v_TreeAddressObjectName VARCHAR(1000); /* Полное в иерархии название объекта*/ 
    v_Return_Error INTEGER :=0; /* Код возврата */
--*******************************************************       
--*******************************************************
BEGIN
    v_TreeAddressObjectName:= fias_AddressObjects_TreeActualName(a_AOGUID, a_MaskArray);
    
    SELECT INTO v_ENDDATE MAX(ENDDATE) 
        FROM fias_house
        WHERE AOGUID = a_AOGUID AND HOUSEGUID = a_HOUSEGUID;
        
    SELECT INTO v_HOUSENUM, v_BUILDNUM, v_STRUCNUM, v_POSTALCODE 
    			HOUSENUM, BUILDNUM, STRUCNUM, POSTALCODE  
        FROM fias_house 
        WHERE AOGUID=a_AOGUID AND HOUSEGUID=a_HOUSEGUID
                    AND ENDDATE=v_ENDDATE;
                    
    IF  c_HouseNoMask <@ a_MaskArray 
            AND COALESCE(TRIM(v_HOUSENUM), '') <> '' THEN
        v_TreeAddressObjectName:= v_TreeAddressObjectName||
                    CASE WHEN v_TreeAddressObjectName = '' THEN '' 
                         ELSE ', ' || c_HouseShortTypeName ||' '|| v_HOUSENUM 
                    END;
    END IF;	
    		
    IF  c_BodyNoMask <@ a_MaskArray 
            AND COALESCE(TRIM(v_BUILDNUM), '' ) <> '' THEN
        v_TreeAddressObjectName:=v_TreeAddressObjectName||
                CASE WHEN v_TreeAddressObjectName = '' THEN '' 
                     ELSE ', ' ||	c_BuildShortTypeName||' '||v_BUILDNUM 
                END;
    END IF;	
    						
    IF  c_BuildingNoMask <@ a_MaskArray 
            AND COALESCE(TRIM(v_STRUCNUM), '') <> '' THEN
        v_TreeAddressObjectName:=v_TreeAddressObjectName ||
                CASE WHEN v_TreeAddressObjectName = '' THEN '' 
                     ELSE ', ' || c_StructShortTypeName||' '||v_STRUCNUM 
                 END;
    END IF;	
    
    /* Почтоый индекс */
    IF  c_PostIndexMask <@ a_MaskArray 
            AND COALESCE(TRIM(v_POSTALCODE),'') <> '' THEN
            v_TreeAddressObjectName:=
                CASE WHEN v_TreeAddressObjectName = '' THEN '' 
                     ELSE v_POSTALCODE || ', '
                 END || v_TreeAddressObjectName;
    END IF;	
    						
    RETURN 	v_TreeAddressObjectName;
 END $function$;

END $$;
--<DO>--
--fias_fill_insur_address
DO $$
BEGIN
    CREATE OR REPLACE FUNCTION public.fias_fill_insur_address(a_emp_id bigint)
 RETURNS bigint
 LANGUAGE plpgsql
AS $function$
DECLARE
	v_houseguid 		VARCHAR(255);	/* guid-ссылка на таблицу fias_house */
  	v_codekladr 		VARCHAR(255);   /* код КЛАДР */
    v_tempHouseGuid 	VARCHAR(255);
    v_addressId     	BIGINT;      	/* идентификатор адреса здания */
    v_address       	VARCHAR(500); 	/* адрес здания */
    v_shortAddress      VARCHAR(500); 	/* короткий адрес здания */
    v_cadastrNum    	VARCHAR(255); 	/* кадастровый номер*/
    v_linkBtiFsks    	BIGINT;
    v_linkEgrnBild    	BIGINT;
    v_postalCode		VARCHAR(10); 
    v_guidRegion 	 	VARCHAR(55);
    v_typeRegion 	 	VARCHAR(25);
    v_region			VARCHAR(55);
    v_typeAvtonomnyyOkrug	VARCHAR(25);
    v_avtonomnyyOkrug       VARCHAR(255);
    v_typeUrbanTerritory	VARCHAR(25);
    v_urbanTerritory        VARCHAR(255);
    v_typeDistrict		VARCHAR(25);
    v_district			VARCHAR(4000); 
    v_cityguid 			VARCHAR(255);
    v_typecity			VARCHAR(55);
    v_city				VARCHAR(4000); 
	v_urbanDistrict		VARCHAR(306);  
    v_sovietVillage		VARCHAR(266);
    v_typeLocality 		VARCHAR(25);
	v_locality			VARCHAR(4000); 
    v_streetguid 		VARCHAR(55);
    v_typestreet 		VARCHAR(55);
    v_street			VARCHAR(4000); 
    v_level1        	VARCHAR(306);
    v_level2   			VARCHAR(306); 
    v_level3    		VARCHAR(306);
    v_propertyTypeName	VARCHAR(255);
    v_typehouse 		VARCHAR(55);
    v_houseNumber		VARCHAR(255); 
    v_typeCorpus		VARCHAR(255); 
    v_corpusNumber		VARCHAR(255);
    v_structureTypeName	VARCHAR(255); 
    v_structureNumber	VARCHAR(255);
    v_source_address		VARCHAR(10);
    v_source_address_code	BIGINT;
BEGIN
/*
	a_emp_id 	- идентификатор здания в таблице insur_building
*/
	IF a_emp_id IS NOT NULL THEN    
    	SELECT INTO v_houseguid, v_codekladr, v_linkBtiFsks, v_linkEgrnBild 
                    lower(guid_fias_mkd) AS guid_fias_house, code_kladr, 
                    link_bti_fsks, link_egrn_bild  
               FROM insur_building_q
        	   WHERE emp_id = a_emp_id AND actual = 1;            

        v_tempHouseGuid := v_houseguid;
        
        IF v_tempHouseGuid IS NULL AND v_codekladr IS NOT NULL THEN    
            SELECT INTO v_tempHouseGuid kladr_to_fias(v_codekladr);  
            
            IF v_tempHouseGuid IS NULL THEN
            	SELECT INTO v_tempHouseGuid h.houseguid
                  FROM ehd_build_parcel_q bp
                       join ehd_egrp_q e on e.num_cadnum = bp.object_id
                       join fias_addrobj o on rpad(o.plaincode, 15, '0') = rpad(substr(e.addr_str_cd, 1, 15), 15, '0')
                       join fias_house h on h.aoguid = o.aoguid
                       join insur_building_q b on b.link_egrn_bild = bp.emp_id
                  WHERE b.actual = 1 AND b.link_bti_fsks is null AND
                        bp.emp_id = v_linkEgrnBild AND
                        coalesce(e.addr_level1_num, '') = coalesce(h.housenum, '') AND
                        coalesce(e.addr_level2_num, '') = coalesce(h.buildnum, '') AND
                        coalesce(e.addr_level3_num, '') = coalesce(h.strucnum, '') AND
                        h.eststatus in (0,1,2,5);  
            END IF;   
        END IF;   
        
        SELECT INTO v_addressId emp_id FROM insur_address
            WHERE guid_fias_house = v_tempHouseGuid;   
                    
        IF v_addressId IS NULL AND v_tempHouseGuid IS NOT NULL THEN                        
            SELECT INTO v_address, v_guidRegion, v_typeRegion, v_region,  
                        v_typeAvtonomnyyOkrug, v_avtonomnyyOkrug, v_typeDistrict, v_district,  
                        v_typeUrbanTerritory, v_urbanTerritory, v_typeLocality, v_locality,
            			v_cityguid, v_typecity, v_city, 
            			v_streetguid, v_typestreet, v_street,
  						v_typehouse, v_houseNumber, 
                        v_typecorpus, v_corpusNumber, 
                        v_structureTypeName, v_structureNumber, v_postalcode
                        fulladdress, guidregion, typeregion, region,  
  						typeavtonomnyyokrug, avtonomnyyokrug, typedistrict, district,  
  						typeurbanterritory, urbanterritory, typeLocality, locality,
                        cityguid, typecity, city, 
                        streetguid, typestreet, street, 
                        typehouse, house, 
                        typecorpus, corpus, 
                        typestructure, structure, postalcode	
            FROM fias_get_address(a_emp_id); 
        
            IF v_address IS NOT NULL AND v_address != '' THEN   
            	v_address:= validate_address(v_address);
                 
            	v_shortAddress:= regexp_replace(v_address, '(г. Москва|\d{6}, г. Москва),\s', '', 'gi');
                v_shortAddress:= TRIM(v_shortAddress);
                      
                INSERT INTO insur_address(emp_id, full_address, short_address, guid_fias_house, guid_fias_street,
  										  type_city, city, type_urban_territory, urban_territory,
                                          type_district, district, type_street, street,
                                          type_house, house, type_corpus, corpus,
                                          type_structure, structure, guid_region, type_region,                                          
                                          region, type_avtonomnyy_okrug, avtonomnyy_okrug,                                           
                                          type_locality, locality, postal_code, source_address, source_address_code)
				VALUES (nextval('insur_address_seq'), 
                		v_address, v_shortAddress, v_tempHouseGuid, v_streetguid,                 
                		v_typecity, v_city, v_typeUrbanTerritory, v_urbanTerritory, 
                        v_typeDistrict, v_district, v_typestreet, v_street, 
                        v_typehouse, v_houseNumber, v_typecorpus, v_corpusNumber, 
                        v_structureTypeName, v_structureNumber, v_guidRegion, v_typeRegion,
                        v_region, v_typeAvtonomnyyOkrug, v_avtonomnyyOkrug, 
                        v_typeLocality, v_locality, v_postalcode, 'ФИАС', 1);
                
                v_addressId := currval('insur_address_seq'); 
            END IF;                       
        END IF;     
                
        IF v_addressId IS NULL THEN
            v_address:= '';
            v_shortAddress:= '';
            v_typehouse:= NULL;
    		v_houseNumber:= NULL;
            
            IF v_linkBtiFsks IS NOT NULL THEN	
                SELECT INTO v_address, v_houseNumber a.full_name, a.house_number
                         FROM bti_building_q bti
                    LEFT JOIN bti_addrlink_q al on al.building_id = bti.emp_id and al.actual = 1 and al.address_status_id = 685
                    LEFT JOIN bti_address_q a on a.emp_id = al.address_id and a.actual = 1
                        WHERE bti.emp_id = v_linkBtiFsks and bti.actual = 1;  
               
            	v_address:= validate_address(v_address);
                
                IF v_address IS NOT NULL AND position('Москва' in v_address) = 1 THEN
                   	v_address:= 'г. ' || v_address;
                ELSEIF position('г. Москва' in v_address) = 0 THEN 
                	v_address:= replace(v_address, 'Москва, ', 'г. Москва, ');
                END IF;                            
                
                v_shortAddress:= regexp_replace(v_address, '(г. Москва|\d{6}, г. Москва),\s', '', 'gi');                           
                        
                v_source_address:= 'БТИ';
                v_source_address_code:= 2;
            ELSEIF v_linkEgrnBild IS NOT NULL THEN
                SELECT INTO v_postalCode, v_region, v_district, v_city,
                            v_urbanDistrict, v_sovietVillage,
                            v_locality, v_street,
                            v_level1, v_level2, v_level3 
                            l.postal_code, l.region, l.district, l.city,
                            l.urban_district, l.soviet_village,
                            l.locality, l.street,
                            l.level1, l.level2, l.level3
                        FROM ehd_build_parcel_q bp
                    JOIN ehd_location_q l on l.building_parcel_id = bp.emp_id
                        WHERE bp.emp_id = v_linkEgrnBild;
                        
                IF v_postalCode IS NOT NULL THEN 
                    v_address:= v_postalCode;
                END IF; 
                    
                IF v_region IS NOT NULL THEN 
                	IF v_region = 'Москва' THEN                     
                        v_region:= 'г. ' || v_region;
                    END IF;  
                    
                    v_address:= v_address || CASE WHEN v_address = '' THEN '' ELSE ', ' END || v_region;
                END IF;  
                
                IF v_district IS NOT NULL THEN 
                    v_address:= v_address || CASE WHEN v_address = '' THEN '' ELSE ', ' END || v_district;
                    v_shortAddress:= v_shortAddress || CASE WHEN v_shortAddress = '' THEN '' ELSE ', ' END || v_district;
                END IF; 
                
                IF v_city IS NOT NULL THEN 
                    v_address:= v_address || CASE WHEN v_address = '' THEN '' ELSE ', ' END || v_city;
                    v_shortAddress:= v_shortAddress || CASE WHEN v_shortAddress = '' THEN '' ELSE ', ' END || v_city;
                END IF; 
                    
                IF v_urbanDistrict IS NOT NULL THEN 
                    v_address:= v_address || CASE WHEN v_address = '' THEN '' ELSE ', ' END || v_urbanDistrict;
                    v_shortAddress:= v_shortAddress || CASE WHEN v_shortAddress = '' THEN '' ELSE ', ' END || v_urbanDistrict;
                END IF;  
                
                IF v_sovietVillage IS NOT NULL THEN 
                    v_address:= v_address || CASE WHEN v_address = '' THEN '' ELSE ', ' END || v_sovietVillage;
                    v_shortAddress:= v_shortAddress || CASE WHEN v_shortAddress = '' THEN '' ELSE ', ' END || v_sovietVillage;
                END IF; 
                
                IF v_locality IS NOT NULL THEN 
                    v_address:= v_address || CASE WHEN v_address = '' THEN '' ELSE ', ' END || v_locality;
                    v_shortAddress:= v_shortAddress || CASE WHEN v_shortAddress = '' THEN '' ELSE ', ' END || v_locality;
                END IF; 
                    
                IF v_street IS NOT NULL THEN 
                	v_street:= validate_address(v_street);
                	v_street:= regexp_replace(v_street, '(кв-л|ст\.|просек|платф\.|пр-д|пл\.|ш\.|наб\.|тер.|пер\.|лн\.|ал\.|ул\.|б-р|пр-кт|туп\.|км)(.+?)', '\2 \1', 'gi');                         

                    v_address:= v_address || CASE WHEN v_address = '' THEN '' ELSE ', ' END || v_street;
                    v_shortAddress:= v_shortAddress || CASE WHEN v_shortAddress = '' THEN '' ELSE ', ' END || v_street;
                END IF; 
                
                IF v_level1 IS NOT NULL THEN 
                    v_address:= v_address || CASE WHEN v_address = '' THEN '' ELSE ', ' END || v_level1;
                    v_shortAddress:= v_shortAddress || CASE WHEN v_shortAddress = '' THEN '' ELSE ', ' END || v_level1;
                    
                    IF position('Дом' in v_level1) > 0 THEN
                    	v_houseNumber:= regexp_replace(v_level1, 'Дом ', '', 'gi');
                    END IF; 
                END IF; 
                    
                IF v_level2 IS NOT NULL THEN 
                    v_address:= v_address || CASE WHEN v_address = '' THEN '' ELSE ', ' END || v_level2;
                    v_shortAddress:= v_shortAddress || CASE WHEN v_shortAddress = '' THEN '' ELSE ', ' END || v_level2;
                END IF;  
                
                IF v_level3 IS NOT NULL THEN 
                    v_address:= v_address || CASE WHEN v_address = '' THEN '' ELSE ', ' END || v_level3;
                    v_shortAddress:= v_shortAddress || CASE WHEN v_shortAddress = '' THEN '' ELSE ', ' END || v_level3;
                END IF; 
                
                v_address:= validate_address(v_address);
                v_shortAddress:= validate_address(v_shortAddress);
                v_shortAddress:= TRIM(v_shortAddress);
                            
                v_source_address:= 'ЕГРН';
                v_source_address_code:= 3;
            END IF;
            
            IF v_address IS NOT NULL AND v_address != '' THEN   
            	IF v_houseNumber IS NOT NULL AND v_houseNumber != '' THEN
                	v_typehouse:= 'д.';
                END IF; 
                     	                            
                INSERT INTO insur_address (emp_id, full_address, short_address, type_house, house, region, postal_code, source_address, source_address_code)
                VALUES (nextval('insur_address_seq'), v_address, v_shortAddress, v_typehouse, v_houseNumber, v_region, v_postalCode, v_source_address, v_source_address_code); 
                
                v_addressId := currval('insur_address_seq');  
            END IF; 
        END IF;
    
    END IF;
    
    RETURN v_addressId;                             
END $function$;

END $$;
--<DO>--
--insur_building_accrued_opl
DO $$
BEGIN
    CREATE OR REPLACE FUNCTION public.insur_building_accrued_opl(building_id bigint, month_count integer)
 RETURNS numeric
 LANGUAGE plpgsql
AS $function$
/*Сумма площадей по начислениям*/
DECLARE
  v_opl NUMERIC; /*Сумма площадей*/
BEGIN
  SELECT INTO v_opl SUM(n.opl)
  FROM insur_flat_q fl
  JOIN insur_fsp_q f on fl.emp_id = f.obj_id and f.actual = 1 and f.obj_reestr_id = 317
  JOIN insur_input_nach n on n.fsp_id = f.emp_id
  WHERE fl.link_object_mkd = building_id AND fl.actual = 1 AND
        n.period_reg_date = date_trunc('month', current_timestamp - month_count * interval '1 month') AND
        n.type_source_code = 12121001;
  RETURN v_opl;
END $function$;

END $$;
--<DO>--
--insur_building_credited_opl
DO $$
BEGIN
    CREATE OR REPLACE FUNCTION public.insur_building_credited_opl(building_id bigint, month_count integer)
 RETURNS numeric
 LANGUAGE plpgsql
AS $function$
/*Сумма площадей по выплатам*/
DECLARE
  v_opl NUMERIC; /*Сумма площадей*/
BEGIN
  SELECT INTO v_opl SUM(p.opl)
  FROM insur_flat_q fl
  JOIN insur_fsp_q f on fl.emp_id = f.obj_id and f.actual = 1 and f.obj_reestr_id = 317
  JOIN insur_input_plat p on p.fsp_id = f.emp_id
  WHERE fl.link_object_mkd = building_id AND fl.actual = 1 AND
        p.period_reg_date = date_trunc('month', current_timestamp - month_count * interval '1 month') AND
        p.type_source_code = 12121001;
  RETURN v_opl;
END $function$;

END $$;
--<DO>--
--insur_building_credited_sum
DO $$
BEGIN
    CREATE OR REPLACE FUNCTION public.insur_building_credited_sum(building_id bigint, month_count integer)
 RETURNS numeric
 LANGUAGE plpgsql
AS $function$
/*Сумма выплат*/
DECLARE
  v_sum NUMERIC; /*Сумма выплат*/
BEGIN
  SELECT INTO v_sum SUM(p.sum_opl)
  FROM insur_flat_q fl
  JOIN insur_fsp_q f on fl.emp_id = f.obj_id and f.actual = 1 and f.obj_reestr_id = 317
  JOIN insur_input_plat p on p.fsp_id = f.emp_id
  WHERE fl.link_object_mkd = building_id AND fl.actual = 1 AND
        p.period_reg_date = date_trunc('month', current_timestamp - month_count * interval '1 month') AND
        p.type_source_code = 12121001;
  RETURN v_sum;
END $function$;

END $$;
--<DO>--
--pckg_mfc_criteria_set_nach_unomaddressmismatch
DO $$
BEGIN
    CREATE OR REPLACE FUNCTION public.pckg_mfc_criteria_set_nach_unomaddressmismatch(p_input_file_ids bigint[])
 RETURNS void
 LANGUAGE plpgsql
AS $function$
BEGIN
  create temp table if not exists temp_insur_input_nach as
      select emp_id, unom, adres_t
      from insur_input_nach;

  update insur_input_nach iin
  set    criteria_json =  
	coalesce(criteria_json::jsonb -
      		(select i
            from generate_series(0, jsonb_array_length(criteria_json::jsonb) - 1) AS i
       		where criteria_json::jsonb -> i ->> 'Id' = '12166004'), coalesce(criteria_json::jsonb,'[]'))::jsonb
            || concat('{ "Id":12166004, "Value":', case when 
            	exists (select 1 from temp_insur_input_nach tiin 
                	where tiin.unom = iin.unom and tiin.adres_t <> iin.adres_t) 
            then 1 else 0 end, '}')::jsonb
  where link_id_file = any(p_input_file_ids);
  
  drop table temp_insur_input_nach;
END $function$;

END $$;
--<DO>--
--core_numerator_sregnomdecrement
DO $$
BEGIN
    CREATE OR REPLACE FUNCTION public.core_numerator_sregnomdecrement(p_numeratorid bigint, p_regnomtype bigint, p_par0 character varying, p_par1 character varying, p_par2 character varying, p_par3 character varying, p_par4 character varying, p_par5 character varying, p_par6 character varying, p_par7 character varying, p_par8 character varying, p_par9 character varying, p_decrementstep bigint, OUT p_sequenceid bigint, OUT p_numdecrement bigint)
 RETURNS record
 LANGUAGE plpgsql
AS $function$
DECLARE
  v_sequenceid      bigint default -1;
  v_newsequenceid   bigint default -1;
  v_currentsequence bigint default 0;
BEGIN
  lock table core_regnom_sequences in access exclusive mode;
  p_numdecrement := 0;
    begin
      select rsq.id
        into strict v_sequenceid
        from core_regnom_sequences rsq
       where (rsq.numeratorid = p_numeratorid and
             rsq.regnomtype = p_regnomtype)
         and (case coalesce(rsq.par0, '') when coalesce(p_par0, '') then 1 else 0 end) = 1
         and (case coalesce(rsq.par1, '') when coalesce(p_par1, '') then 1 else 0 end) = 1
         and (case coalesce(rsq.par2, '') when coalesce(p_par2, '') then 1 else 0 end) = 1
         and (case coalesce(rsq.par3, '') when coalesce(p_par3, '') then 1 else 0 end) = 1
         and (case coalesce(rsq.par4, '') when coalesce(p_par4, '') then 1 else 0 end) = 1
         and (case coalesce(rsq.par5, '') when coalesce(p_par5, '') then 1 else 0 end) = 1
         and (case coalesce(rsq.par6, '') when coalesce(p_par6, '') then 1 else 0 end) = 1
         and (case coalesce(rsq.par7, '') when coalesce(p_par7, '') then 1 else 0 end) = 1
         and (case coalesce(rsq.par8, '') when coalesce(p_par8, '') then 1 else 0 end) = 1
         and (case coalesce(rsq.par9, '') when coalesce(p_par9, '') then 1 else 0 end) = 1;
    exception
      when no_data_found then
        v_sequenceid := -1;
    end;

    if coalesce(v_sequenceid, -1) <> -1 then
      select currentincrement
        into v_currentsequence
        from core_regnom_sequences
       where id = v_sequenceid;
       
      update core_regnom_sequences
        set currentincrement = core_regnom_sequences.currentincrement - p_decrementstep
      where id = v_sequenceid
      returning currentincrement into p_numdecrement;
      p_sequenceid := v_sequenceid;          
    end if;
END $function$;

END $$;
--<DO>--
--core_updstru_getcolumntype
DO $$
BEGIN
    CREATE OR REPLACE FUNCTION public.core_updstru_getcolumntype(character varying, character varying)
 RETURNS character varying
 LANGUAGE plpgsql
AS $function$
    declare
	    tableName ALIAS FOR $1;
        columnName ALIAS FOR $2;
	    columnType varchar(100);
	begin
      	SELECT c.data_type
        INTO columnType
        FROM information_schema.columns c
        --TABLE information_schema.columns
        WHERE c.table_name = lower(tableName) AND c.column_name = lower(columnName) AND c.table_schema NOT IN ('pg_catalog', 'information_schema');

        return columnType;
	END $function$;

END $$;
--<DO>--
--fias_houses_addressobjecttree
DO $$
BEGIN
    CREATE OR REPLACE FUNCTION public.fias_houses_addressobjecttree(a_aoguid character varying, a_houseguid character varying, a_currstatus integer DEFAULT 0, a_enddate timestamp without time zone DEFAULT '2079-06-06 00:00:00'::timestamp without time zone)
 RETURNS TABLE(rtf_guid character varying, rtf_currstatus integer, rtf_actstatus integer, rtf_aolevel integer, rtf_shorttypename character varying, rtf_addressobjectname character varying)
 LANGUAGE plpgsql
AS $function$
DECLARE
    c_HouseAOLevel CONSTANT INTEGER:=8;
    c_PostalCodeTypeName  CONSTANT VARCHAR(10):='индекс';
    c_HouseShortTypeName  CONSTANT VARCHAR(10):='д.';
    c_BuildShortTypeName  CONSTANT VARCHAR(10):='корп.';
    c_StructShortTypeName CONSTANT VARCHAR(10):='стр.';
    c_StatusActual    CONSTANT INTEGER:=1; /* Признак актуальности записи */
    c_StatusNotActual CONSTANT INTEGER:=0; /* Признак неактальной записи записи */
    c_MAXENDDATE CONSTANT TIMESTAMP:=to_timestamp('2079-06-06 00:00:00', 'YYYY-MM-DD');
    v_HouseActStatus  INTEGER;	/* Признак актуальности для здания*/
    v_HouseCurrStatus INTEGER;	/* Признак актуальности для здания */
    v_ENDDATE   TIMESTAMP;	    /* Окончание действия записи */
    v_HOUSEGUID VARCHAR(36);	/* Глобальный уникальный идентификатор дома */
    v_HOUSENUM  VARCHAR(10);	/* Номер дома */
    v_BUILDNUM  VARCHAR(10);	/* Номер корпуса */
    v_STRUCNUM  VARCHAR(10);	/* Номер строения */
    v_POSTALCODE   VARCHAR(10);	/* Почтовый индекс */
    v_Return_Error INTEGER :=0;	/* Код возврата */
--************************************************************       
--************************************************************
 BEGIN    
    IF a_ENDDATE IS NULL THEN 
        SELECT INTO v_ENDDATE MAX(ENDDATE) 
                FROM fias_house WHERE AOGUID=a_AOGUID AND HOUSEGUID=a_HOUSEGUID;
    ELSE
        v_ENDDATE:=a_ENDDATE;
    END IF;
        
    SELECT INTO v_HOUSENUM, v_BUILDNUM, v_STRUCNUM, 
    			v_POSTALCODE, v_ENDDATE, v_HouseCurrStatus
                h.HOUSENUM,h.BUILDNUM,h.STRUCNUM,
                h.POSTALCODE, h.ENDDATE,ah.HouseCurrStatus
        FROM fias_house h
            INNER JOIN (SELECT AOGUID,HOUSEGUID,ENDDATE, 
                           RANK() OVER (PARTITION BY AOGUID,
                           HOUSEGUID ORDER BY ENDDATE ASC) AS HouseCurrStatus
                        FROM fias_house insh  WHERE insh.AOGUID = a_AOGUID AND
                                                insh.HOUSEGUID = a_HOUSEGUID) as ah
				ON h.AOGUID=ah.AOGUID AND h.HOUSEGUID=ah.HOUSEGUID AND h.ENDDATE = ah.ENDDATE
        WHERE h.ENDDATE=v_ENDDATE;         
        				
    v_HouseActStatus:= CASE WHEN COALESCE(v_ENDDATE,c_MAXENDDATE) =
                       c_MAXENDDATE THEN c_StatusActual ELSE c_StatusNotActual END;
                    
    v_HouseCurrStatus:= CASE WHEN COALESCE(v_ENDDATE,c_MAXENDDATE) =
                        c_MAXENDDATE THEN 0 ELSE v_HouseCurrStatus END;
    
    IF v_POSTALCODE IS NOT NULL OR v_POSTALCODE != '' THEN                    
    	RETURN QUERY SELECT a_HOUSEGUID, v_HouseCurrStatus, v_HouseActStatus,
        					c_HouseAOLevel, c_PostalCodeTypeName, v_POSTALCODE;
    END IF;
    
    RETURN QUERY SELECT * FROM fias_AddressObjects_AddressObjectTree(a_AOGUID, a_CurrStatus);                      
                    
    IF v_HOUSENUM IS NOT NULL OR v_HOUSENUM != '' THEN
        RETURN QUERY SELECT a_HOUSEGUID,v_HouseCurrStatus,v_HouseActStatus,
                    c_HouseAOLevel, c_HouseShortTypeName, v_HOUSENUM;
    END IF;
    IF v_BUILDNUM IS NOT NULL OR v_BUILDNUM != '' THEN
        RETURN QUERY SELECT a_HOUSEGUID, v_HouseCurrStatus, v_HouseActStatus,
                            c_HouseAOLevel, c_BuildShortTypeName, v_BUILDNUM;
    END IF;
    IF v_STRUCNUM IS NOT NULL OR v_STRUCNUM != '' THEN
        RETURN QUERY SELECT a_HOUSEGUID,v_HouseCurrStatus,v_HouseActStatus,
                                 c_HouseAOLevel,c_StructShortTypeName,v_STRUCNUM;
    END IF;
  END $function$;

END $$;
--<DO>--
--fias_houses_treeactualname
DO $$
BEGIN
    CREATE OR REPLACE FUNCTION public.fias_houses_treeactualname(a_houseguid character varying, a_maskarray character varying[] DEFAULT '{ZC,TP,LM,LP,ST,HS,BY,BG}'::character varying[])
 RETURNS character varying
 LANGUAGE plpgsql
AS $function$
  DECLARE
      c_MaxEndDate CONSTANT TIMESTAMP:=TO_TIMESTAMP('2079-06-06','YYYY-MM-DD');
      v_AOGUID VARCHAR(36); /* Идентификтор адресного объекта */
      v_TreeAddressObjectName VARCHAR(1000); /* Полное в иерархии название объекта*/ 
      v_Return_Error INTEGER :=0; /* Код возврата */
  --**********************************************************       
  --**********************************************************
  BEGIN
      SELECT INTO v_AOGUID h.AOGUID	
          FROM fias_house h 
              INNER JOIN fias_addrobj ao ON h.AOGUID=ao.AOGUID 
          WHERE h.HOUSEGUID=a_HOUSEGUID AND h.ENDDATE=c_MaxEndDate
          ORDER BY h.ENDDATE DESC;
          
      v_TreeAddressObjectName:= fias_Houses_TreeActualName(v_AOGUID,a_HOUSEGUID,a_MaskArray);
      
      RETURN v_TreeAddressObjectName;
  END $function$;

END $$;
--<DO>--
--mfc_otch_get_nach_stat
DO $$
BEGIN
    CREATE OR REPLACE FUNCTION public.mfc_otch_get_nach_stat(p_okrug_id bigint, p_district_id bigint, p_period timestamp without time zone)
 RETURNS mfc_otch_type
 LANGUAGE plpgsql
AS $function$
    declare
     rval mfc_otch_type;
    begin
with s as (
select first_value(ir.tariff) over (
	order by ir.date_begin desc
    rows between unbounded preceding and unbounded  following
) tariff
from insur_insur_rate ir 
where ir.date_begin <= to_date('01.09.2018','dd.mm.yyyy')
), s1 as (
select max(tariff) tariff from s
) 
select 
	count(case when iin.sum_nach > 0 and iin.sum_nach < round(iin.opl * s1.tariff,2) then 1 end),
	count(case when iin.sum_nach > 0 and iin.sum_nach = round(iin.opl * s1.tariff,2) then 1 end),
	count(case when iin.sum_nach > 0 and iin.sum_nach > round(iin.opl * s1.tariff,2) then 1 end),
    count(case when iin.sum_nach = 0 then 1 end),
    count(*), 
	sum(iin.sum_nach),
    round(sum(iin.sum_nach) * 0.07,2)
into rval
from insur_okrug io
join insur_district id on id.okrug_id = io.id
join insur_input_file iif on iif.district_id = id.id
join insur_input_nach iin on iin.link_id_file = iif.emp_id
join s1 on 1=1
where iif.period_reg_date = p_period
and (io.id = p_okrug_id or p_okrug_id is NULL)
and (id.id = p_district_id or p_district_id is null);
return rval;
END $function$;

END $$;
--<DO>--
--core_srd_usersettings_trgi_fnc
DO $$
BEGIN
    CREATE OR REPLACE FUNCTION public.core_srd_usersettings_trgi_fnc()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
       begin
	     execute core_cachmanagment_updateTimeStamp('core.srd.user', NEW.USERID);
         if TG_OP = 'DELETE' then return OLD;
         else return NEW;
         end if;
       END $function$;

END $$;
--<DO>--
--core_srd_role_attr_trgi_fnc
DO $$
BEGIN
    CREATE OR REPLACE FUNCTION public.core_srd_role_attr_trgi_fnc()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
	   declare
	   	cur record;
       begin
  		for cur in (select ur.*
                from core_srd_user_role ur
                join core_srd_role_register rr on rr.role_id = ur.role_id
               where rr.id = NEW.rule_id) loop
    		execute core_cachmanagment_updateTimeStamp('core.srd.user', cur.USER_ID);
		end loop;
         if TG_OP = 'DELETE' then return OLD;
         else return NEW;
         end if;
       END $function$;

END $$;
--<DO>--
--core_register_cache_trg
DO $$
BEGIN
    CREATE OR REPLACE FUNCTION public.core_register_cache_trg()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
       begin
	     execute core_cachmanagment_updateTimeStamp('core.registers');
       
         if TG_OP = 'DELETE' then return OLD;
           else return NEW;
         end if;
       
       END $function$;

END $$;
--<DO>--
--core_register_rel_cache_trg
DO $$
BEGIN
    CREATE OR REPLACE FUNCTION public.core_register_rel_cache_trg()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
       begin
	     execute core_cachmanagment_updateTimeStamp('core.registers');
       
         if TG_OP = 'DELETE' then return OLD;
           else return NEW;
         end if;
       
       END $function$;

END $$;
--<DO>--
--core_srd_user_role_trgd_fnc
DO $$
BEGIN
    CREATE OR REPLACE FUNCTION public.core_srd_user_role_trgd_fnc()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
       begin
	     execute core_cachmanagment_updateTimeStamp('core.srd.user',OLD.user_id);
         if TG_OP = 'DELETE' then return OLD;
         else return NEW;
         end if;
       END $function$;

END $$;
--<DO>--
--core_srd_user_role_trgi_fnc
DO $$
BEGIN
    CREATE OR REPLACE FUNCTION public.core_srd_user_role_trgi_fnc()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
       begin
	     execute core_cachmanagment_updateTimeStamp('core.srd.user', NEW.user_id);
         if TG_OP = 'DELETE' then return OLD;
         else return NEW;
         end if;
       END $function$;

END $$;
--<DO>--
--core_srd_user_trgi_fnc
DO $$
BEGIN
    CREATE OR REPLACE FUNCTION public.core_srd_user_trgi_fnc()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
       begin
	     execute core_cachmanagment_updateTimeStamp('core.srd.user', NEW.ID);
	     execute core_cachmanagment_updateTimeStamp('core.srd.common');
         if TG_OP = 'DELETE' then return OLD;
         else return NEW;
         end if;
       END $function$;

END $$;
--<DO>--
--core_srd_function_trgi_fnc
DO $$
BEGIN
    CREATE OR REPLACE FUNCTION public.core_srd_function_trgi_fnc()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
	   declare
	   	cur record;
       begin
  		for cur in (select DISTINCT r.user_id
                  from core_srd_role_function f
                  join core_srd_user_role r
                    on r.role_id = f.role_id
                 where f.function_id = NEW.ID) loop
    		execute core_cachmanagment_updateTimeStamp('core.srd.user', cur.USER_ID);
		end loop;
         if TG_OP = 'DELETE' then return OLD;
         else return NEW;
         end if;
       END $function$;

END $$;
--<DO>--
--core_srd_department_trgi_fnc
DO $$
BEGIN
    CREATE OR REPLACE FUNCTION public.core_srd_department_trgi_fnc()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
       begin
	   	 execute core_cachmanagment_updateTimeStamp('core.srd.common');
         if TG_OP = 'DELETE' then return OLD;
         else return NEW;
         end if;
       END $function$;

END $$;
--<DO>--
--core_srd_role_function_trgi_fnc
DO $$
BEGIN
    CREATE OR REPLACE FUNCTION public.core_srd_role_function_trgi_fnc()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
	   declare
	   	cur record;
       begin
  		for cur in (select ur.*
                from core_srd_user_role ur
               where ur.role_id = NEW.Role_id) loop
    		execute core_cachmanagment_updateTimeStamp('core.srd.user', cur.USER_ID);
		end loop;
         if TG_OP = 'DELETE' then return OLD;
         else return NEW;
         end if;
       END $function$;

END $$;
--<DO>--
--core_srd_role_function_trgd_fnc
DO $$
BEGIN
    CREATE OR REPLACE FUNCTION public.core_srd_role_function_trgd_fnc()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
	   declare
	   	cur record;
       begin
  		for cur in (select ur.*
                from core_srd_user_role ur
               where ur.role_id = OLD.Role_id) loop
    		execute core_cachmanagment_updateTimeStamp('core.srd.user', cur.USER_ID);
		end loop;
         if TG_OP = 'DELETE' then return OLD;
         else return NEW;
         end if;
       END $function$;

END $$;
--<DO>--
--core_srd_role_trgi_fnc
DO $$
BEGIN
    CREATE OR REPLACE FUNCTION public.core_srd_role_trgi_fnc()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
	   declare
	   	cur record;
       begin
  		for cur in (select ur.*
                from core_srd_user_role ur
               where ur.role_id = NEW.id) loop
    		execute core_cachmanagment_updateTimeStamp('core.srd.user', cur.USER_ID);
		end loop;
         if TG_OP = 'DELETE' then return OLD;
         else return NEW;
         end if;
       END $function$;

END $$;
--<DO>--
--core_srd_role_register_trgi_fnc
DO $$
BEGIN
    CREATE OR REPLACE FUNCTION public.core_srd_role_register_trgi_fnc()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
	   declare
	   	cur record;
       begin
  		for cur in (select ur.*
                from core_srd_user_role ur
               where ur.role_id = NEW.Role_id) loop

    		execute core_cachmanagment_updateTimeStamp('core.srd.user', cur.USER_ID);
		end loop;
       
	   if TG_OP = 'DELETE' then return OLD;
         else return NEW;
         end if;
       END $function$;

END $$;
--<DO>--
--core_register_attr_cache_trg
DO $$
BEGIN
    CREATE OR REPLACE FUNCTION public.core_register_attr_cache_trg()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
       begin
	     execute core_cachmanagment_updateTimeStamp('core.registers');
       
         if TG_OP = 'DELETE' then return OLD;
           else return NEW;
         end if;
       
       END $function$;

END $$;
--<DO>--
--getsizeofrelation
DO $$
BEGIN
    CREATE OR REPLACE FUNCTION public.getsizeofrelation(character varying)
 RETURNS bigint
 LANGUAGE plpgsql
AS $function$
declare
	relName ALIAS FOR $1;
    result bigint;
begin
	SELECT pg_total_relation_size(relName) INTO result;
    return result;
exception
	WHEN others THEN
		return 0;
END $function$;

END $$;
--<DO>--
--core_updstru_checkexisttype
DO $$
BEGIN
    CREATE OR REPLACE FUNCTION public.core_updstru_checkexisttype(character varying)
 RETURNS boolean
 LANGUAGE plpgsql
AS $function$
    declare
    	sName ALIAS FOR $1;
        nCnt numeric;
    begin
        SELECT COUNT(*)
        INTO nCnt
        FROM pg_catalog.pg_type t
        --TABLE pg_catalog.pg_type
        WHERE t.typname = lower(sName);

        if (nCnt = 0) then
   	        return false;
		else
   	        return true;
        end if;
    END $function$;

END $$;
--<DO>--
--core_updstru_checkexisttable
DO $$
BEGIN
    CREATE OR REPLACE FUNCTION public.core_updstru_checkexisttable(character varying)
 RETURNS boolean
 LANGUAGE plpgsql
AS $function$
	declare
        sName ALIAS FOR $1;
        nCnt numeric;
    begin
        SELECT COUNT(*)
        INTO nCnt
        FROM pg_catalog.pg_tables t
        --TABLE pg_catalog.pg_tables
        WHERE t.tablename = lower(sName); -- AND t.tablespace NOT IN ('pg_catalog', 'information_schema');

        if (nCnt = 0) then
   	        return false;
		else
   	        return true;
        end if;
    END $function$;

END $$;
--<DO>--
--core_numerator_sregnomincrement
DO $$
BEGIN
    CREATE OR REPLACE FUNCTION public.core_numerator_sregnomincrement(p_numeratorid bigint, p_regnomtype bigint, p_par0 character varying, p_par1 character varying, p_par2 character varying, p_par3 character varying, p_par4 character varying, p_par5 character varying, p_par6 character varying, p_par7 character varying, p_par8 character varying, p_par9 character varying, p_minval bigint, p_maxval bigint, p_incrementstep bigint, OUT p_sequenceid bigint, OUT p_numincrement bigint)
 RETURNS record
 LANGUAGE plpgsql
AS $function$DECLARE
  v_sequenceid      bigint default - 1;
  v_newsequenceid   bigint default - 1;
  v_currentsequence bigint default 0;
BEGIN
  lock table core_regnom_sequences in access exclusive mode;
  p_numincrement := 0;
    begin
      select rsq.id
        into strict v_sequenceid
        from core_regnom_sequences rsq
       where (rsq.numeratorid = p_numeratorid and
             rsq.regnomtype = p_regnomtype)
         and (case coalesce(rsq.par0,'') when coalesce(p_par0,'') then 1 else 0 end) = 1
         and (case coalesce(rsq.par1,'') when coalesce(p_par1,'') then 1 else 0 end) = 1
         and (case coalesce(rsq.par2,'') when coalesce(p_par2,'') then 1 else 0 end) = 1
         and (case coalesce(rsq.par3,'') when coalesce(p_par3,'') then 1 else 0 end) = 1
         and (case coalesce(rsq.par4,'') when coalesce(p_par4,'') then 1 else 0 end) = 1
         and (case coalesce(rsq.par5,'') when coalesce(p_par5,'') then 1 else 0 end) = 1
         and (case coalesce(rsq.par6,'') when coalesce(p_par6,'') then 1 else 0 end) = 1
         and (case coalesce(rsq.par7,'') when coalesce(p_par7,'') then 1 else 0 end) = 1
         and (case coalesce(rsq.par8,'') when coalesce(p_par8,'') then 1 else 0 end) = 1
         and (case coalesce(rsq.par9,'') when coalesce(p_par9,'') then 1 else 0 end) = 1;
    exception
      when no_data_found then
        v_sequenceid := -1;
    end;

    if coalesce(v_sequenceid, -1) <> -1 then
      select currentincrement
        into v_currentsequence
        from core_regnom_sequences
       where id = v_sequenceid;
      if v_currentsequence = p_maxval then
      	begin
          update core_regnom_sequences
             set currentincrement = p_minval
           where id = v_sequenceid
          returning v_currentsequence into p_numincrement;
        end;
      else
      	begin
          update core_regnom_sequences
             set currentincrement = core_regnom_sequences.currentincrement +
                                                    p_incrementstep
           where id = v_sequenceid
          returning currentincrement into p_numincrement;
          p_sequenceid := v_sequenceid;
        end;
      end if;
    else
      select coalesce(max(id) + 1, 1) into v_newsequenceid from core_regnom_sequences;
      insert into core_regnom_sequences
        (id,
         numeratorid,
         regnomtype,
         par0,
         par1,
         par2,
         par3,
         par4,
         par5,
         par6,
         par7,
         par8,
         par9,
         currentincrement)
      values
        (v_newsequenceid,
         p_numeratorid,
         p_regnomtype,
         p_par0,
         p_par1,
         p_par2,
         p_par3,
         p_par4,
         p_par5,
         p_par6,
         p_par7,
         p_par8,
         p_par9,
         p_minval)
      returning p_minval into p_numincrement;
      p_sequenceid := v_newsequenceid;
    end if;
END $function$;

END $$;
--<DO>--
--core_updstru_checkexisttrigger
DO $$
BEGIN
    CREATE OR REPLACE FUNCTION public.core_updstru_checkexisttrigger(character varying)
 RETURNS boolean
 LANGUAGE plpgsql
AS $function$
	declare
    	sTriggerName ALIAS FOR $1;
        nCnt numeric;
    begin
		SELECT COUNT(*)
        INTO nCnt
        FROM information_schema.triggers as t
        --TABLE information_schema.triggers
        WHERE lower(t.trigger_name) = lower(sTriggerName) AND t.trigger_schema NOT IN ('pg_catalog', 'information_schema');

        if(nCnt = 0)then
        	return false;
        else
        	return true;
        end if;

    END $function$;

END $$;
--<DO>--
--core_updstru_checkexistview
DO $$
BEGIN
    CREATE OR REPLACE FUNCTION public.core_updstru_checkexistview(character varying)
 RETURNS boolean
 LANGUAGE plpgsql
AS $function$
    declare
	    sName ALIAS FOR $1;
        nCnt numeric;
    begin
	    SELECT COUNT(*)
        INTO nCnt
        FROM information_schema.views as v
        --TABLE information_schema.views
        WHERE lower(v.table_name) = lower(sName) AND v.table_schema NOT IN ('pg_catalog', 'information_schema');

        if(nCnt = 0)then
        	return false;
        else
        	return true;
        end if;

    END $function$;

END $$;
--<DO>--
--fias_addressobjects_objectgroup
DO $$
BEGIN
    CREATE OR REPLACE FUNCTION public.fias_addressobjects_objectgroup(a_aoguid character varying, a_currstatus integer DEFAULT NULL::integer)
 RETURNS character varying
 LANGUAGE plpgsql
AS $function$
DECLARE
	c_CountryGroupValue      CONSTANT VARCHAR(50):='Country';
	c_RegionGroupValue	     CONSTANT VARCHAR(50):='Region';
	c_CityGroupValue         CONSTANT VARCHAR(50):='City';
	c_TerritoryGroupValue    CONSTANT VARCHAR(50):='Territory';
	c_LocalityGroupValue     CONSTANT VARCHAR(50):='Locality';
	c_MotorRoadValue         CONSTANT VARCHAR(50):='MotorRoad';
	c_RailWayObjectValue     CONSTANT VARCHAR(50):='RailWayObject';
	c_VillageCouncilValue    CONSTANT VARCHAR(50):='VillageCouncil';
	c_StreetGroupValue       CONSTANT VARCHAR(50):='Street';
	c_AddlTerritoryValue     CONSTANT VARCHAR(50):='AddlTerritory';
	c_PartAddlTerritoryValue CONSTANT VARCHAR(50):='PartAddlTerritory';
	v_ShortTypeName      VARCHAR(10);   /* Тип адресообразующего элемента */ 
	v_AddressObjectName  VARCHAR(100);  /* Название адресообразующего элемента */ 
	v_AOLevel            INTEGER;       /* Уровень адресообразующего элемента*/	
	v_CurrStatus         INTEGER;       /* Текущий статус адресообразующего элемента*/
	v_ObjectGroup        VARCHAR(50);   /* Группа адресообразующего элемента	*/
 	v_Return_Error		 INTEGER :=0;	/* Код возврата */
--**************************************************************************       
--**************************************************************************
 BEGIN
     SELECT INTO v_CurrStatus COALESCE(a_CurrStatus,MIN(addrobj.currstatus)) 
                     FROM fias_addrobj addrobj WHERE addrobj.AOGUID=a_AOGUID;
     SELECT INTO v_ShortTypeName,v_AddressObjectName,v_AOLevel
                                 ShortName,FormalName,AOLevel 
                     FROM fias_addrobj addrobj   
                     WHERE addrobj.AOGUID=a_AOGUID AND addrobj.currstatus = v_CurrStatus 	
                     LIMIT 1;
     IF v_AOLevel = 1 AND UPPER(v_ShortTypeName) <> 'Г' THEN /*  уровень региона */ 
          v_ObjectGroup:=c_RegionGroupValue;
     ELSIF v_AOLevel = 1 AND UPPER(v_ShortTypeName) =  'Г' THEN /*  уровень города как региона  */ 
          v_ObjectGroup:=c_CityGroupValue;
     ELSIF v_AOLevel = 3 THEN /* уровень района */
          v_ObjectGroup:=c_TerritoryGroupValue;
      ELSIF (v_AOLevel = 4 AND UPPER(v_ShortTypeName) NOT IN ('С/С','С/А','С/О','С/МО')) 
                OR (v_AOLevel = 1 AND UPPER(v_ShortTypeName) <> 'Г')  THEN /* уровень города */ 
          v_ObjectGroup:=c_CityGroupValue;
      ELSIF v_AOLevel IN (4,6)  AND UPPER(v_ShortTypeName) IN ('С/С','С/А','С/О','С/МО') 
               AND UPPER(v_ShortTypeName) NOT LIKE ('Ж/Д%') THEN /* уровень сельсовета */ 
          v_ObjectGroup:=c_VillageCouncilValue;	
      ELSIF v_AOLevel = 6 AND UPPER(v_ShortTypeName) NOT IN ('С/С','С/А','С/О','С/МО',
                                                      'САД','СНТ','ТЕР',
                                                      'АВТОДОРОГА',
                                                      'ПРОМЗОНА',
                                                     'ДП','МКР')
               AND UPPER(v_ShortTypeName) NOT LIKE ('Ж/Д%') THEN   /* уровень населенного */
                                                      /* пункта */ 
           v_ObjectGroup:=c_LocalityGroupValue;
       ELSIF  UPPER(v_ShortTypeName) IN ('АВТОДОРОГА') THEN /* уровень */
                                                     /* автомобильной дороги */ 
           v_ObjectGroup:=c_MotorRoadValue;
       ELSIF  v_AOLevel IN (6,7) AND UPPER(v_ShortTypeName) LIKE ('Ж/Д%') THEN 
                                                     /* уровень элемент */
                                                     /* на железной дороге */ 
           v_ObjectGroup:=c_RailWayObjectValue;	
       ELSIF v_AOLevel = 7 AND UPPER(v_ShortTypeName) NOT LIKE ('Ж/Д%') 
                    AND UPPER(v_ShortTypeName) NOT IN ('УЧ-К','ГСК','ПЛ-КА','СНТ','ТЕР') 
                    OR (v_AOLevel = 6 AND UPPER(v_ShortTypeName) IN ('МКР') )  THEN 
                                                      /* уровень улицы */
          v_ObjectGroup:=c_StreetGroupValue;
      ELSIF v_AOLevel = 90 OR v_AOLevel = 6 AND UPPER(v_ShortTypeName) IN ('САД',
                                                      'СНТ','ТЕР','ПРОМЗОНА','ДП')
                  OR v_AOLevel = 7 
                 AND UPPER(v_ShortTypeName) IN ('УЧ-К','ГСК','ПЛ-КА','СНТ','ТЕР')  THEN
                                                      /*  уровень дополнительных */
                                                      /* территорий */
           v_ObjectGroup:=c_AddlTerritoryValue;
      ELSIF v_AOLevel = 91 THEN  /* уровень подчиненных дополнительным территориям */
                                                 /* объектов */ 
           v_ObjectGroup:=c_PartAddlTerritoryValue;
     END IF;	
     RETURN v_ObjectGroup;
  END $function$;

END $$;
--<DO>--
--fias_addressobjects_treeactualname
DO $$
BEGIN
    CREATE OR REPLACE FUNCTION public.fias_addressobjects_treeactualname(a_aoguid character varying DEFAULT NULL::character varying, a_maskarray character varying[] DEFAULT '{TP,LM,LP,ST}'::character varying[])
 RETURNS character varying
 LANGUAGE plpgsql
AS $function$
DECLARE
	c_CountryGroupValue	 CONSTANT VARCHAR(50):='Country'; /* Признак группы - Страна*/	
	c_RegionGroupValue	 CONSTANT VARCHAR(50):='Region';  /* Признак группы - Регион*/	
	c_CityGroupValue	 CONSTANT VARCHAR(50):='City';	  /* Признак группы - Основной населенный пункт*/	
	c_TerritoryGroupValue  CONSTANT VARCHAR(50):='Territory'; /* Признак группы - район */	
	c_LocalityGroupValue   CONSTANT VARCHAR(50):='Locality';  /* Признак группы - населенный  пункт, */
    														  /* подчиненный основному */	
	c_MotorRoadValue      CONSTANT VARCHAR(50):='MotorRoad';  /* Признак группы - автомобильная дорога */	
	c_RailWayObjectValue  CONSTANT VARCHAR(50):='RailWayObject';  /* Признак группы - железная дорога */	
	c_VillageCouncilValue CONSTANT VARCHAR(50):='VillageCouncil'; /* Признак группы - сельсовет */
	c_StreetGroupValue	  CONSTANT VARCHAR(50):='Street';   	  /* Признак группы - улица в населенном пункте */	
	c_AddlTerritoryValue	 CONSTANT VARCHAR(50):='AddlTerritory';     /* Признак группы - дополнительная территория*/	
	c_PartAddlTerritoryValue CONSTANT VARCHAR(50):='PartAddlTerritory'; /* Признак группы - часть дополнительной территории*/	
	c_StreetMask	 	CONSTANT  VARCHAR(2)[1] :='{ST}';/* Маска улица */
	c_DistrictMask		CONSTANT  VARCHAR(2)[1] :='{DT}';/* Маска городской район*/
	c_PartLocalityMask	CONSTANT  VARCHAR(2)[1] :='{LP}';/* Маска подчиненный населенный пункт*/
	c_MainLocalityMask	CONSTANT  VARCHAR(2)[1] :='{LM}';/* Маска основной населенный пункт*/
	c_PartTerritoryMask	CONSTANT  VARCHAR(2)[1] :='{TP}';/* Маска района субъекта федерации*/
	c_MainTerritoryMask	CONSTANT  VARCHAR(2)[1] :='{TM}';/* Маска субъект федерации (регион)*/
	c_CountryMask		CONSTANT  VARCHAR(2)[1] :='{CY}';/* Маска страна*/
	v_ShortTypeName		    VARCHAR(10);   /* Тип адресообразующего элемента */ 
	v_AddressObjectName     VARCHAR(100);  /* Название адресообразующего элемента */
	v_AOLevel               INTEGER;       /* Уровень адресообразующего элемента*/	
	v_MinCurrStatus         INTEGER;	   /* Минимальное значение текущего статуса адресообразующего элемента*/	
	v_TreeAddressObjectName	VARCHAR(1000); /* Полное в иерархии название элемента*/ 
	v_ObjectGroup           VARCHAR(50);   /* Группа адресообразующего элемента */
	v_TreeLeverCount        INTEGER;	   /* Счетчик цикла*/
	v_Return_Error_i        INTEGER := 0;  /* Код возврата*/
	cursor_AddressObjectTree RefCURSOR;    /* курсор по иерархии адреса*/
	v_Return_Error          INTEGER :=0;	/* Код возврата */
--******************************************************************************  
--******************************************************************************
 BEGIN
	SELECT INTO v_MinCurrStatus MIN(addrobj.currstatus) 
          FROM fias_addrobj addrobj
          WHERE aoguid=a_AOGUID;
	OPEN cursor_AddressObjectTree FOR SELECT rtf_ShortTypeName,
                        REPLACE(rtf_AddressObjectName,'  ',' '),
                        rtf_AOLevel,fias_AddressObjects_ObjectGroup(rtf_AOGUID )
          FROM fias_AddressObjects_AddressObjectTree(a_AOGUID) 
          ORDER BY rtf_AOLevel;
	v_TreeLeverCount:=0;
	v_TreeAddressObjectName:='';
	FETCH FIRST FROM cursor_AddressObjectTree INTO v_ShortTypeName,v_AddressObjectName,
                        v_AOLevel,v_ObjectGroup;
	WHILE FOUND
	LOOP
		v_TreeLeverCount:=v_TreeLeverCount+1;	
		IF v_ObjectGroup=c_CountryGroupValue AND c_CountryMask <@ a_MaskArray 
                                     AND v_AOLevel = 0 THEN
			v_TreeAddressObjectName:=v_TreeAddressObjectName||
                               CASE WHEN v_TreeAddressObjectName = '' THEN ''  
                                  ELSE ', ' END ||
                               v_AddressObjectName||' '||v_ShortTypeName;
		ELSIF v_ObjectGroup=c_RegionGroupValue 
                                     AND c_MainTerritoryMask <@ a_MaskArray
                                     AND v_AOLevel <=2 THEN
			v_TreeAddressObjectName:=v_TreeAddressObjectName||
                                CASE WHEN v_TreeAddressObjectName='' THEN ''
                                         ELSE ', ' END ||
                                CASE WHEN UPPER(v_ShortTypeName) LIKE 
                                               UPPER('%Респ%') THEN 'Республика ' ||
                               v_AddressObjectName ELSE v_AddressObjectName||
                                              ' '||v_ShortTypeName END;
        /* Для случая, когда город является регионом */
        ELSIF v_ObjectGroup=c_CityGroupValue AND c_MainLocalityMask <@ a_MaskArray
                                     AND v_AOLevel = 1 THEN
			v_TreeAddressObjectName:=v_TreeAddressObjectName||
                                CASE WHEN v_TreeAddressObjectName = '' THEN ''
                                          ELSE ', ' END ||
                                     CASE WHEN UPPER(LEFT(v_AddressObjectName,6+
                                         LENGTH(v_ShortTypeName)))='ЗАТО '||
                                         UPPER(TRIM(v_ShortTypeName))||'.'  THEN
                                             v_AddressObjectName
                                       ELSE v_ShortTypeName || ' ' || v_AddressObjectName END;                                              
		ELSIF v_ObjectGroup=c_TerritoryGroupValue 
                                     AND c_PartTerritoryMask <@ a_MaskArray 
                                     AND v_AOLevel =3 THEN
			v_TreeAddressObjectName:=v_TreeAddressObjectName||
                                CASE WHEN v_TreeAddressObjectName='' THEN ''
                                         ELSE ', ' END ||
                                v_AddressObjectName||' '||v_ShortTypeName;
		ELSIF v_ObjectGroup=c_CityGroupValue
                                     AND c_MainLocalityMask <@ a_MaskArray AND v_AOLevel = 4 THEN
			v_TreeAddressObjectName:=v_TreeAddressObjectName||
                                    CASE WHEN v_TreeAddressObjectName='' THEN ''
                                          ELSE ', ' END ||
                                     CASE WHEN UPPER(LEFT(v_AddressObjectName,6+
                                         LENGTH(v_ShortTypeName)))='ЗАТО '||
                                         UPPER(TRIM(v_ShortTypeName))||'.'  THEN
                                             v_AddressObjectName
                                       ELSE v_ShortTypeName ||' '|| v_AddressObjectName END;
		ELSIF v_ObjectGroup=c_LocalityGroupValue 
                                     AND c_DistrictMask <@ a_MaskArray AND v_AOLevel = 5 THEN
			v_TreeAddressObjectName:=v_TreeAddressObjectName||
                                    CASE WHEN v_TreeAddressObjectName='' THEN '' 
                                        ELSE ', ' END ||
                                    v_AddressObjectName||' '||v_ShortTypeName ;
		ELSIF v_ObjectGroup=c_LocalityGroupValue 
                                    AND c_PartLocalityMask <@ a_MaskArray 
                                    AND v_AOLevel = 6 THEN
			v_TreeAddressObjectName:=v_TreeAddressObjectName||
                                   CASE WHEN v_TreeAddressObjectName='' THEN ''
                                        ELSE ', ' END ||
                                   v_ShortTypeName ||' '|| v_AddressObjectName;
		ELSIF v_ObjectGroup=c_StreetGroupValue 
                                   AND c_StreetMask <@ a_MaskArray 
                                   AND v_AOLevel =7  THEN
			v_TreeAddressObjectName:=v_TreeAddressObjectName||
                                   CASE WHEN v_TreeAddressObjectName='' THEN '' 
                                        ELSE ', ' END ||
                                   v_ShortTypeName ||' '|| v_AddressObjectName;
		END IF;
		FETCH NEXT  FROM cursor_AddressObjectTree INTO v_ShortTypeName, v_AddressObjectName, v_AOLevel,v_ObjectGroup;
	END LOOP;
	CLOSE cursor_AddressObjectTree;
 	RETURN 	v_TreeAddressObjectName;
  END $function$;

END $$;
--<DO>--
--core_cachmanagment_updatetimestamp
DO $$
BEGIN
    CREATE OR REPLACE FUNCTION public.core_cachmanagment_updatetimestamp(pcacheobject character varying, pcachekey bigint DEFAULT 0, pextradata character varying DEFAULT 'NULL'::character varying)
 RETURNS void
 LANGUAGE plpgsql
AS $function$
	begin
		update core_cache_updates set cache_timestamp = current_timestamp
		where cacheobject = pcacheobject 
			and cachekey = coalesce(pcachekey,0)
			and extradata = coalesce(pextradata,'NULL');
		if not found then
			INSERT INTO CORE_CACHE_UPDATES(id,cacheobject, CACHEKEY, EXTRADATA, CACHE_TIMESTAMP)
        	VALUES (nextval('reg_object_seq'), pcacheobject, pcachekey, pextradata, current_timestamp);
		end if;
	END $function$;

END $$;
--<DO>--
--core_updstru_checkexistsequence
DO $$
BEGIN
    CREATE OR REPLACE FUNCTION public.core_updstru_checkexistsequence(character varying)
 RETURNS boolean
 LANGUAGE plpgsql
AS $function$
    declare
          sName ALIAS FOR $1;
          nCnt numeric;
    begin
          SELECT COUNT(*)
          INTO nCnt
          FROM pg_catalog.pg_sequences as s
          --TABLE pg_catalog.pg_sequences
          WHERE s.sequencename = lower(sName) AND s.schemaname NOT IN ('pg_catalog', 'information_schema');

          if (nCnt = 0) then
              return false;
          else
              return true;
          end if;

    END $function$;

END $$;
--<DO>--
--core_updstru_isnullablecolumn
DO $$
BEGIN
    CREATE OR REPLACE FUNCTION public.core_updstru_isnullablecolumn(character varying, character varying)
 RETURNS boolean
 LANGUAGE plpgsql
AS $function$
    declare
		sTabName ALIAS FOR $1;
        sColName ALIAS FOR $2;
        sNullable varchar(3);
    begin
        begin
			SELECT c.is_nullable
			INTO sNullable
            FROM information_schema.columns c
            --TABLE information_schema.columns
        	WHERE lower(c.table_name) = lower(sTabName) AND lower(c.column_name) = lower(sColName) AND c.table_schema NOT IN ('pg_catalog', 'information_schema');
        exception
          when NO_DATA_FOUND then
            return false;
        end;

        if sNullable = 'Yes' then
			return true;
        else
          	return false;
        end if;

    END $function$;

END $$;
--<DO>--
--core_updstru_checkexistfunction
DO $$
BEGIN
    CREATE OR REPLACE FUNCTION public.core_updstru_checkexistfunction(character varying)
 RETURNS boolean
 LANGUAGE plpgsql
AS $function$
    declare
		sFuncName ALIAS FOR $1;
        nCnt numeric;
    begin
		SELECT COUNT(*)
        INTO nCnt
        FROM pg_catalog.pg_proc as p
        --TABLE pg_catalog.pg_proc
        WHERE p.proname = lower(sFuncName);

        if nCnt = 0 then
        	return false;
        else
        	return true;
        end if;

    END $function$;

END $$;
--<DO>--
--core_updstru_getcolumndefault
DO $$
BEGIN
    CREATE OR REPLACE FUNCTION public.core_updstru_getcolumndefault(character varying, character varying)
 RETURNS character varying
 LANGUAGE plpgsql
AS $function$
  declare
		tableName ALIAS FOR $1;
        columnName  ALIAS FOR $2;
		defaultValue varchar(200);
  begin
    SELECT c.column_default
    INTO defaultValue
    FROM information_schema.columns as c
    WHERE lower(c.table_name) = lower(tableName) AND lower(c.column_name) = lower(columnName);

    return defaultValue;
  END $function$;

END $$;
--<DO>--
--core_srd_role_pkg_getfullfunctionname
DO $$
BEGIN
    CREATE OR REPLACE FUNCTION public.core_srd_role_pkg_getfullfunctionname(p_function_id bigint)
 RETURNS character varying
 LANGUAGE plpgsql
AS $function$
declare
cur record;
vFullName varchar(4000) := '';
begin
for cur in (
	with recursive csf as (
	select f.id, f.parent_id, f.functionname , 1 lvl
	from core_srd_function f
	where f.id = p_function_id
	union all
	select f.id, f.parent_id, f.functionname , lvl+1
	from core_srd_function f
	join csf on csf.parent_id = f.id
	)
	select *
	from csf
	order by lvl
) loop
	vFullName := cur.functionname || ' - ' || vFullName;
end loop;
	return substr(vFullName,1,length(vFullName)-3);
END $function$;

END $$;
--<DO>--
--pckg_mfc_criteria_set_nach_calcsummismatch
DO $$
BEGIN
    CREATE OR REPLACE FUNCTION public.pckg_mfc_criteria_set_nach_calcsummismatch(p_input_file_ids bigint[], p_delta numeric DEFAULT 0.10)
 RETURNS void
 LANGUAGE plpgsql
AS $function$
BEGIN
  update insur_input_nach
  set    criteria_json =  
	coalesce(criteria_json::jsonb -
      		(select i
            from generate_series(0, jsonb_array_length(criteria_json::jsonb) - 1) AS i
       		where criteria_json::jsonb -> i ->> 'Id' = '12166003'), coalesce(criteria_json::jsonb,'[]'))::jsonb
            || concat('{ "Id":12166003, "Value":', case when 
            (opl is null and sum_nach is not null) or (opl is not null and sum_nach is null) or abs(round(opl * 1.79, 2) - round(sum_nach, 2)) > p_delta 
            then 1 else 0 end, '}')::jsonb
  where link_id_file = any(p_input_file_ids);
END $function$;

END $$;
--<DO>--
--pckg_mfc_criteria_set_nach_flatkolgpmismatch
DO $$
BEGIN
    CREATE OR REPLACE FUNCTION public.pckg_mfc_criteria_set_nach_flatkolgpmismatch(p_input_file_ids bigint[])
 RETURNS void
 LANGUAGE plpgsql
AS $function$
BEGIN
  update insur_input_nach iin
  set    criteria_json =  
	coalesce(criteria_json::jsonb -
      		(select i
            from generate_series(0, jsonb_array_length(criteria_json::jsonb) - 1) AS i
       		where criteria_json::jsonb -> i ->> 'Id' = '12166012'), coalesce(criteria_json::jsonb,'[]'))::jsonb
            || concat('{ "Id":12166012, "Value":', case when 
            	coalesce((select ifq.kol_gp from insur_flat_q ifq join insur_building_q ibq on ifq.link_object_mkd = ibq.emp_id
				where ibq.unom = iin.unom and ifq.kvnom = iin.kvnom limit 1), 0) <> coalesce(iin.kolgp, 0) 
            then 1 else 0 end, '}')::jsonb
  where link_id_file = any(p_input_file_ids);
END $function$;

END $$;
--<DO>--
--pckg_mfc_criteria_set_nach_flatnotfound
DO $$
BEGIN
    CREATE OR REPLACE FUNCTION public.pckg_mfc_criteria_set_nach_flatnotfound(p_input_file_ids bigint[])
 RETURNS void
 LANGUAGE plpgsql
AS $function$
BEGIN
  update insur_input_nach iin
  set    criteria_json =  
      coalesce(criteria_json::jsonb -
      		(select i
            from generate_series(0, jsonb_array_length(criteria_json::jsonb) - 1) AS i
       		where criteria_json::jsonb -> i ->> 'Id' = '12166010'), coalesce(criteria_json::jsonb,'[]'))::jsonb
            || concat('{ "Id":12166010, "Value":', case when 
            	exists(select 1 from insur_flat_q ifq
                join insur_building_q ibq on ifq.link_object_mkd = ibq.emp_id
                where ibq.unom = iin.unom and ifq.kvnom = iin.kvnom limit 1) 
            then 1 else 0 end, '}')::jsonb
  where link_id_file = any(p_input_file_ids);
END $function$;

END $$;
--<DO>--
--pckg_mfc_criteria_set_nach_flatoplmismatch
DO $$
BEGIN
    CREATE OR REPLACE FUNCTION public.pckg_mfc_criteria_set_nach_flatoplmismatch(p_input_file_ids bigint[])
 RETURNS void
 LANGUAGE plpgsql
AS $function$
BEGIN
  update insur_input_nach iin
  set    criteria_json =  
	coalesce(criteria_json::jsonb -
      		(select i
            from generate_series(0, jsonb_array_length(criteria_json::jsonb) - 1) AS i
       		where criteria_json::jsonb -> i ->> 'Id' = '12166011'), coalesce(criteria_json::jsonb,'[]'))::jsonb
            || concat('{ "Id":12166011, "Value":', case when 
            	coalesce((select ifq.fopl from insur_flat_q ifq join insur_building_q ibq on ifq.link_object_mkd = ibq.emp_id
				where ibq.unom = iin.unom and ifq.kvnom = iin.kvnom limit 1), 0) <> coalesce(iin.fopl, 0) 
            then 1 else 0 end, '}')::jsonb
  where link_id_file = any(p_input_file_ids);
END $function$;

END $$;
--<DO>--
--pckg_mfc_criteria_set_nach_foploplmismatch
DO $$
BEGIN
    CREATE OR REPLACE FUNCTION public.pckg_mfc_criteria_set_nach_foploplmismatch(p_input_file_ids bigint[])
 RETURNS void
 LANGUAGE plpgsql
AS $function$
DECLARE
	separateFlatTypeId	bigint;	 /* ИД справочника тип квартиры - отдельная квартира */
BEGIN
  select into separateFlatTypeId ift.id from insur_flat_type ift
    	where code = 1;

  update insur_input_nach iin
  set    criteria_json =  
	coalesce(criteria_json::jsonb -
      		(select i
            from generate_series(0, jsonb_array_length(criteria_json::jsonb) - 1) AS i
       		where criteria_json::jsonb -> i ->> 'Id' = '12166009'), coalesce(criteria_json::jsonb,'[]'))::jsonb
            || concat('{ "Id":12166009, "Value":', case when 
            (separateFlatTypeId is not null and iin.flat_type_id = separateFlatTypeId and iin.fopl <> iin.opl)
            then 1 else 0 end, '}')::jsonb
  where link_id_file = any(p_input_file_ids);
END $function$;

END $$;
--<DO>--
--pckg_mfc_criteria_set_nach_kvnomnommismatch
DO $$
BEGIN
    CREATE OR REPLACE FUNCTION public.pckg_mfc_criteria_set_nach_kvnomnommismatch(p_input_file_ids bigint[])
 RETURNS void
 LANGUAGE plpgsql
AS $function$
BEGIN
  update insur_input_nach iin
  set    criteria_json =  
	coalesce(criteria_json::jsonb -
      		(select i
            from generate_series(0, jsonb_array_length(criteria_json::jsonb) - 1) AS i
       		where criteria_json::jsonb -> i ->> 'Id' = '12166006'), coalesce(criteria_json::jsonb,'[]'))::jsonb
            || concat('{ "Id":12166006, "Value":', case when
            (nullif(iin.kvnom, '') <> nullif(iin.nom, '') || nullif(iin.nomi, ''))
            then 1 else 0 end, '}')::jsonb
  where link_id_file = any(p_input_file_ids);
END $function$;

END $$;
--<DO>--
--core_updstru_checkexistindex
DO $$
BEGIN
    CREATE OR REPLACE FUNCTION public.core_updstru_checkexistindex(character varying)
 RETURNS boolean
 LANGUAGE plpgsql
AS $function$
  declare
		sIndexName ALIAS FOR $1;
        nCnt numeric;
  begin
		SELECT COUNT(*)
        INTO nCnt
        FROM pg_catalog.pg_indexes as i
        --TABLE pg_catalog.pg_indexes
        WHERE lower(i.indexname) = lower(sIndexName) AND i.schemaname = 'public';

        if(nCnt = 0)then
        	return false;
        else
        	return true;
        end if;

  END $function$;

END $$;
--<DO>--
--pckg_mfc_criteria_set_nach_morethanonenach
DO $$
BEGIN
    CREATE OR REPLACE FUNCTION public.pckg_mfc_criteria_set_nach_morethanonenach(p_input_file_ids bigint[])
 RETURNS void
 LANGUAGE plpgsql
AS $function$
BEGIN
  update insur_input_nach iin
  set    criteria_json =  
	coalesce(criteria_json::jsonb -
      		(select i
            from generate_series(0, jsonb_array_length(criteria_json::jsonb) - 1) AS i
       		where criteria_json::jsonb -> i ->> 'Id' = '12166004'), coalesce(criteria_json::jsonb,'[]'))::jsonb
            || concat('{ "Id":12166004, "Value":0}')::jsonb
  where link_id_file = any(p_input_file_ids) and not exists(select 1 from insur_input_nach iin2 where 
            	iin2.emp_id != iin.emp_id and
                --в одном загружаемом файле
                iin2.link_id_file = any(p_input_file_ids) and 
                iin2.kodpl = iin.kodpl and 
                iin2.sum_nach > 0);
                
  update insur_input_nach iin
  set    criteria_json =  
	coalesce(criteria_json::jsonb -
      		(select i
            from generate_series(0, jsonb_array_length(criteria_json::jsonb) - 1) AS i
       		where criteria_json::jsonb -> i ->> 'Id' = '12166004'), coalesce(criteria_json::jsonb,'[]'))::jsonb
            || concat('{ "Id":12166004, "Value":1}')::jsonb
  where link_id_file = any(p_input_file_ids) and exists(select 1 from insur_input_nach iin2 where 
            	iin2.emp_id != iin.emp_id and
                --в одном загружаемом файле
                iin2.link_id_file = any(p_input_file_ids) and 
                iin2.kodpl = iin.kodpl and 
                iin2.sum_nach > 0);
END $function$;

END $$;
--<DO>--
--pckg_mfc_criteria_set_nach_nachwithoutopl
DO $$
BEGIN
    CREATE OR REPLACE FUNCTION public.pckg_mfc_criteria_set_nach_nachwithoutopl(p_input_file_ids bigint[])
 RETURNS void
 LANGUAGE plpgsql
AS $function$
BEGIN
  update insur_input_nach iin
  set    criteria_json =  
	coalesce(criteria_json::jsonb -
      		(select i
            from generate_series(0, jsonb_array_length(criteria_json::jsonb) - 1) AS i
       		where criteria_json::jsonb -> i ->> 'Id' = '12166008'), coalesce(criteria_json::jsonb,'[]'))::jsonb
            || concat('{ "Id":12166008, "Value":', case when
            (iin.sum_nach > 0 and (iin.opl is null or iin.opl = 0))
            then 1 else 0 end, '}')::jsonb
  where link_id_file = any(p_input_file_ids);
END $function$;

END $$;
--<DO>--
--pckg_mfc_criteria_set_nach_suspiciousunom
DO $$
BEGIN
    CREATE OR REPLACE FUNCTION public.pckg_mfc_criteria_set_nach_suspiciousunom(p_input_file_ids bigint[])
 RETURNS void
 LANGUAGE plpgsql
AS $function$
BEGIN
  update insur_input_nach iin
  set    criteria_json =  
	coalesce(criteria_json::jsonb -
      		(select i
            from generate_series(0, jsonb_array_length(criteria_json::jsonb) - 1) AS i
       		where criteria_json::jsonb -> i ->> 'Id' = '12166005'), coalesce(criteria_json::jsonb,'[]'))::jsonb
            || concat('{ "Id":12166005, "Value":', case when
            (iin.unom = -1 or iin.unom = 0 or iin.unom = 1 or iin.unom = 99999) 
            then 1 else 0 end, '}')::jsonb
  where link_id_file = any(p_input_file_ids);
END $function$;

END $$;
--<DO>--
--pckg_mfc_criteria_set_plat_calcsummismatch
DO $$
BEGIN
    CREATE OR REPLACE FUNCTION public.pckg_mfc_criteria_set_plat_calcsummismatch(p_input_file_ids bigint[], p_delta numeric DEFAULT 0.10)
 RETURNS void
 LANGUAGE plpgsql
AS $function$
BEGIN
  update insur_input_plat iip
  set    criteria_json =  
	coalesce(criteria_json::jsonb -
      		(select i
            from generate_series(0, jsonb_array_length(criteria_json::jsonb) - 1) AS i
       		where criteria_json::jsonb -> i ->> 'Id' = '12166003'), coalesce(criteria_json::jsonb,'[]'))::jsonb
            || concat('{ "Id":12166003, "Value":', case when 
            (opl is null and sum_nach is not null) or (opl is not null and sum_nach is null) or abs(round(opl * 1.79, 2) - round(sum_nach, 2)) > p_delta  
            then 1 else 0 end, '}')::jsonb
  where link_id_file = any(p_input_file_ids);
END $function$;

END $$;
--<DO>--
--pckg_mfc_criteria_set_plat_flatnotfound
DO $$
BEGIN
    CREATE OR REPLACE FUNCTION public.pckg_mfc_criteria_set_plat_flatnotfound(p_input_file_ids bigint[])
 RETURNS void
 LANGUAGE plpgsql
AS $function$
BEGIN
  update insur_input_plat iip
  set    criteria_json =  
	coalesce(criteria_json::jsonb -
      		(select i
            from generate_series(0, jsonb_array_length(criteria_json::jsonb) - 1) AS i
       		where criteria_json::jsonb -> i ->> 'Id' = '12166010'), coalesce(criteria_json::jsonb,'[]'))::jsonb
            || concat('{ "Id":12166010, "Value":', case when 
            exists(select 1 from insur_flat_q ifq
                join insur_building_q ibq on ifq.link_object_mkd = ibq.emp_id
                where ibq.unom = iip.unom and ifq.kvnom = iip.nom limit 1) 
            then 1 else 0 end, '}')::jsonb
  where link_id_file = any(p_input_file_ids);
END $function$;

END $$;
--<DO>--
--insur_building_accrued_sum
DO $$
BEGIN
    CREATE OR REPLACE FUNCTION public.insur_building_accrued_sum(building_id bigint, month_count integer)
 RETURNS numeric
 LANGUAGE plpgsql
AS $function$
/*Сумма начислений*/
DECLARE
  v_sum NUMERIC; /*Сумма начислений*/
BEGIN
  SELECT INTO v_sum SUM(n.sum_nach)
  FROM insur_flat_q fl
  JOIN insur_fsp_q f on fl.emp_id = f.obj_id and f.actual = 1 and f.obj_reestr_id = 317
  JOIN insur_input_nach n on n.fsp_id = f.emp_id
  WHERE fl.link_object_mkd = building_id AND fl.actual = 1 AND
        n.period_reg_date = date_trunc('month', current_timestamp - month_count * interval '1 month') AND
        n.type_source_code = 12121001;
  RETURN v_sum;
END $function$;

END $$;
--<DO>--
--pckg_mfc_criteria_set_plat_flatoplmismatch
DO $$
BEGIN
    CREATE OR REPLACE FUNCTION public.pckg_mfc_criteria_set_plat_flatoplmismatch(p_input_file_ids bigint[])
 RETURNS void
 LANGUAGE plpgsql
AS $function$
BEGIN
  update insur_input_plat iip
  set    criteria_json =  
	coalesce(criteria_json::jsonb -
      		(select i
            from generate_series(0, jsonb_array_length(criteria_json::jsonb) - 1) AS i
       		where criteria_json::jsonb -> i ->> 'Id' = '12166011'), coalesce(criteria_json::jsonb,'[]'))::jsonb
            || concat('{ "Id":12166011, "Value":', case when
            coalesce((select ifq.fopl from insur_flat_q ifq join insur_building_q ibq on ifq.link_object_mkd = ibq.emp_id
				where ibq.unom = iip.unom and ifq.kvnom = iip.nom limit 1), 0) <> coalesce(iip.opl, 0) 
            then 1 else 0 end, '}')::jsonb
  where link_id_file = any(p_input_file_ids);
END $function$;

END $$;
--<DO>--
--pckg_mfc_criteria_set_plat_suspiciousunom
DO $$
BEGIN
    CREATE OR REPLACE FUNCTION public.pckg_mfc_criteria_set_plat_suspiciousunom(p_input_file_ids bigint[])
 RETURNS void
 LANGUAGE plpgsql
AS $function$
BEGIN
  update insur_input_plat iip
  set    criteria_json =  
	coalesce(criteria_json::jsonb -
      		(select i
            from generate_series(0, jsonb_array_length(criteria_json::jsonb) - 1) AS i
       		where criteria_json::jsonb -> i ->> 'Id' = '12166005'), coalesce(criteria_json::jsonb,'[]'))::jsonb
            || concat('{ "Id":12166005, "Value":', case when
            (iip.unom = -1 or iip.unom = 0 or iip.unom = 1 or iip.unom = 99999)
            then 1 else 0 end, '}')::jsonb
  where link_id_file = any(p_input_file_ids);
END $function$;

END $$;
--<DO>--
--pckg_mfc_criteria_set_plat_unomaddressmismatch
DO $$
BEGIN
    CREATE OR REPLACE FUNCTION public.pckg_mfc_criteria_set_plat_unomaddressmismatch(p_input_file_ids bigint[])
 RETURNS void
 LANGUAGE plpgsql
AS $function$
BEGIN
  update insur_input_plat iip
  set    criteria_json =  
	coalesce(criteria_json::jsonb -
      		(select i
            from generate_series(0, jsonb_array_length(criteria_json::jsonb) - 1) AS i
       		where criteria_json::jsonb -> i ->> 'Id' = '12166004'), coalesce(criteria_json::jsonb,'[]'))::jsonb
            || concat('{ "Id":12166004, "Value":0}')::jsonb
  where link_id_file = any(p_input_file_ids) and not exists(select 1 from insur_input_plat iip2 where
                iip2.unom = iip.unom and
                iip2.adres_t <> iip.adres_t);
                
  update insur_input_plat iip
  set    criteria_json =  
	coalesce(criteria_json::jsonb -
      		(select i
            from generate_series(0, jsonb_array_length(criteria_json::jsonb) - 1) AS i
       		where criteria_json::jsonb -> i ->> 'Id' = '12166004'), coalesce(criteria_json::jsonb,'[]'))::jsonb
            || concat('{ "Id":12166004, "Value":1}')::jsonb
  where link_id_file = any(p_input_file_ids) and exists(select 1 from insur_input_plat iip2 where
                iip2.unom = iip.unom and
                iip2.adres_t <> iip.adres_t);
END $function$;

END $$;
--<DO>--
--insur_flat_accured_sum
DO $$
BEGIN
    CREATE OR REPLACE FUNCTION public.insur_flat_accured_sum(flat_id bigint, month_count integer)
 RETURNS numeric
 LANGUAGE plpgsql
AS $function$
/*Сумма начислений*/
DECLARE
  v_sum NUMERIC; /*Сумма начислений*/
BEGIN
  SELECT INTO v_sum SUM(n.sum_nach)
  FROM insur_fsp_q f
  JOIN insur_input_nach n on n.fsp_id = f.emp_id
  WHERE f.obj_id = flat_id AND
  		f.obj_reestr_id = 317 AND
        f.actual = 1 AND
        n.period_reg_date = date_trunc('month', current_timestamp - month_count * interval '1 month') AND
        n.type_source_code = 12121001;
  RETURN v_sum;
END $function$;

END $$;
--<DO>--
--insur_flat_accured_opl
DO $$
BEGIN
    CREATE OR REPLACE FUNCTION public.insur_flat_accured_opl(flat_id bigint, month_count integer)
 RETURNS numeric
 LANGUAGE plpgsql
AS $function$
/*Сумма площадей по начислениям*/
DECLARE
  v_opl NUMERIC; /*Сумма площадей*/
BEGIN
  SELECT INTO v_opl SUM(n.opl)
  FROM insur_fsp_q f
  JOIN insur_input_nach n on n.fsp_id = f.emp_id
  WHERE f.obj_id = flat_id AND
  		f.obj_reestr_id = 317 AND
        f.actual = 1 AND
        n.period_reg_date = date_trunc('month', current_timestamp - month_count * interval '1 month') AND
        n.type_source_code = 12121001;
  RETURN v_opl;
END $function$;

END $$;
--<DO>--
--insur_flat_credited_opl
DO $$
BEGIN
    CREATE OR REPLACE FUNCTION public.insur_flat_credited_opl(flat_id bigint, month_count integer)
 RETURNS numeric
 LANGUAGE plpgsql
AS $function$
/*Сумма площадей по выплатам*/
DECLARE
  v_opl NUMERIC; /*Сумма площадей*/
BEGIN
  SELECT INTO v_opl SUM(p.opl)
  FROM insur_fsp_q f
  JOIN insur_input_plat p on p.fsp_id = f.emp_id
  WHERE f.obj_id = flat_id AND
  		f.obj_reestr_id = 317 AND
        f.actual = 1 AND
        p.period_reg_date = date_trunc('month', current_timestamp - month_count * interval '1 month') AND
        p.type_source_code = 12121001;
  RETURN v_opl;
END $function$;

END $$;
--<DO>--
--insur_flat_credited_sum
DO $$
BEGIN
    CREATE OR REPLACE FUNCTION public.insur_flat_credited_sum(flat_id bigint, month_count integer)
 RETURNS numeric
 LANGUAGE plpgsql
AS $function$
/*Сумма выплат*/
DECLARE
  v_sum NUMERIC; /*Сумма выплат*/
BEGIN
  SELECT INTO v_sum SUM(p.sum_opl)
  FROM insur_fsp_q f
  JOIN insur_input_plat p on p.fsp_id = f.emp_id
  WHERE f.obj_id = flat_id AND
  		f.obj_reestr_id = 317 AND
        f.actual = 1 AND
        p.period_reg_date = date_trunc('month', current_timestamp - month_count * interval '1 month') AND
        p.type_source_code = 12121001;
  RETURN v_sum;
END $function$;

END $$;
--<DO>--
--core_register_pkg_getorcreatechangeset
DO $$
BEGIN
    CREATE OR REPLACE FUNCTION public.core_register_pkg_getorcreatechangeset(itdinstanceid bigint, iuserid bigint, istatus bigint, changesetdate timestamp without time zone, OUT ichangesetid bigint)
 RETURNS bigint
 LANGUAGE plpgsql
AS $function$
BEGIN
  begin
      -- проверка есть ли сохраненный ранее набор изменений для данного документа и пользователя со статусом 1
      select chs.id
        into strict ichangesetid
        from core_td_changeset chs
       where chs.status = istatus
         and chs.td_id = itdinstanceid
         and chs.user_id = iuserid;
    exception
      when no_data_found then
        --создание нового набора изменений
        insert into core_td_changeset
          (id, td_id, changeset_date, status, user_id)
        values
          (nextval('seq_core_td'), itdinstanceid, changesetdate, istatus, iuserid)
        returning id into ichangesetid;
    end;
END $function$;

END $$;
--<DO>--
--core_register_pkg_getuserkeystring
DO $$
BEGIN
    CREATE OR REPLACE FUNCTION public.core_register_pkg_getuserkeystring(p_register_id bigint, p_object_id bigint, p_date timestamp without time zone)
 RETURNS character varying
 LANGUAGE plpgsql
AS $function$
BEGIN
  return to_char(p_object_id, '99999999999');
END $function$;

END $$;
--<DO>--
--pckg_ref_podpisant_getitemlist
DO $$
BEGIN
    CREATE OR REPLACE FUNCTION public.pckg_ref_podpisant_getitemlist(_s_filter character varying, OUT o_lst refcursor)
 RETURNS refcursor
 LANGUAGE plpgsql
 STRICT
AS $function$
DECLARE
   _sqlParam varchar;
   ref_cursor refcursor; 
   l_sql varchar;
BEGIN
if _s_filter is not null then 
_sqlParam = ' and ' || _s_filter ;
end if;
  l_sql := 'SELECT l."ID", l."POST", l."CODE", l."NAME", l."IS_DELETED", l."TEXT", l."NAME" ||   '' - '' || l."POST", '''' as short_title, 0 is_archives FROM fm_podpisant l WHERE l."IS_DELETED" = 0' || _sqlParam;  
  OPEN o_lst FOR EXECUTE l_sql || ' ORDER BY l."NAME"';
END $function$;

END $$;
--<DO>--
--core_updstru_checkexistconstraint
DO $$
BEGIN
    CREATE OR REPLACE FUNCTION public.core_updstru_checkexistconstraint(character varying)
 RETURNS boolean
 LANGUAGE plpgsql
AS $function$
    declare
          sName ALIAS FOR $1;
          nCnt numeric;
    begin
          SELECT COUNT(*)
          INTO nCnt
          FROM information_schema.constraint_table_usage c
          --TABLE pg_catalog.pg_constraint --LEFT OUTER JOIN pg_catalog.pg_namespace AS n ON n.oid = c.relnamespace
          WHERE lower(c.constraint_name) = lower(sName);

          if nCnt = 0 then
              return false;
          else
              return true;
          end if;

    END $function$;

END $$;
--<DO>--
--core_updstru_deletefkrefconstraints
DO $$
BEGIN
    CREATE OR REPLACE FUNCTION public.core_updstru_deletefkrefconstraints(character varying)
 RETURNS void
 LANGUAGE plpgsql
AS $function$
  declare
		sTable ALIAS FOR $1;
        qRow RECORD;
  begin
    -- Удаление внешних ссылок
    FOR qRow IN (SELECT tc.table_name as table_name_pk, tc.constraint_name as constraint_name_pk
                      , rc.constraint_name as constraint_name_fk, trc.table_name as table_name_fk
                 FROM information_schema.table_constraints as tc
                      INNER JOIN information_schema.referential_constraints as rc ON rc.unique_constraint_name = tc.constraint_name
                      INNER JOIN information_schema.table_constraints as trc ON trc.constraint_name = rc.constraint_name
                 WHERE tc.constraint_type = 'PRIMARY KEY' AND lower(tc.table_name) = lower(sTable)) LOOP
      execute immediate 'alter table ' || qRow.table_name_fk || ' drop constraint ' || qRow.constraint_name_fk;
    end loop;

  END $function$;

END $$;
--<DO>--
--fill_system_daily_statistics
DO $$
BEGIN
    CREATE OR REPLACE FUNCTION public.fill_system_daily_statistics()
 RETURNS void
 LANGUAGE plpgsql
AS $function$
declare
  ID_VAL                       numeric;
  STAT_DATE_VAL                date;
  DB_SIZE_VAL                  numeric;
  ERRORS_VAL                   numeric;
  WARNINGS_VAL                 numeric;
  ACTIONS_VAL                  numeric;
  SESSIONS_VAL                 numeric;
  CHANGES_VAL                  numeric;
  DIAGNOSTICS_SLOW_VAL         numeric;
  
  LONG_PROC_RUN_VAL                numeric;
  LONG_PROC_RUN_ERROR_VAL          numeric;
  LONG_PROC_INSUR_LOAD_RUN_VAL                numeric;
  LONG_PROC_INSUR_LOAD_RUN_ERROR_VAL          numeric;
  
  BTI_OBJECTS_LOADED_VAL       numeric;
  BTI_OBJECTS_LOADED_ERROR_VAL numeric;
  
  EHD_OBJECTS_LOADED_VAL       numeric;
  EHD_OBJECTS_LOADED_ERROR_VAL numeric;
  
  INSUR_BUILDING_LOADED_VAL       numeric;
  INSUR_BUILDING_LOADED_ERROR_VAL numeric;
  
  INSUR_FLAT_LOADED_VAL       numeric;
  INSUR_FLAT_LOADED_ERROR_VAL numeric;
  
  TOTAL_COUNT_INSUR_BUILDING_VAL NUMERIC;
  TOTAL_COUNT_INSUR_FLAT_VAL     NUMERIC;
  
  
begin
  -- DATE
  --STAT_DATE_VAL := trunc(sysdate) - 1;
  STAT_DATE_VAL := CURRENT_DATE - 1;
  
  INSERT INTO system_daily_stat_db_obj
  (SELECT STAT_DATE_VAL as STAT_DATE
       , 'INDEX' as SEGMENT_TYPE
       , i.indexname as SEGMENT_NAME
       , i.tablename as TABLE_NAME
       , GetSizeOfRelation(i.indexname::varchar)::numeric/(1024*1024) as SIZE_MEG
  FROM pg_indexes as i
  WHERE i.schemaname = 'public'

  UNION ALL

  SELECT STAT_DATE_VAL as STAT_DATE
       , 'TABLE' as SEGMENT_TYPE
       , t.table_name as SEGMENT_NAME
       , t.table_name as TABLE_NAME
       , GetSizeOfRelation(t.table_name::varchar)::numeric/(1024*1024) as SIZE_MEG
  FROM information_schema.tables as t);

  --DB_SIZE
  --select sum(t.bytes) / (1024 * 1024) into DB_SIZE_VAL from user_extents t;
  --SELECT pg_catalog.pg_database_size('cipjs_main') / (1024 * 1024) INTO DB_SIZE_VAL;
  SELECT sum(t.size_meg) INTO DB_SIZE_VAL from system_daily_stat_db_obj t where t.stat_date = STAT_DATE_VAL;
  

  -- ID
  select count(1) + 1 into ID_VAL from SYSTEM_DAILY_STATISTICS;

  --ERRORS
  select count(1)
    into ERRORS_VAL
    from core_error_log t
   where t.errordate >= STAT_DATE_VAL
     and t.errordate < STAT_DATE_VAL + 1
     and t.msgtype = 'ERROR';

  --WARNINGS
  select count(1)
    into WARNINGS_VAL
    from core_error_log t
   where t.errordate >= STAT_DATE_VAL
     and t.errordate < STAT_DATE_VAL + 1
     and t.msgtype = 'WARNING';

  --ACTIONS
  select count(1)
    into ACTIONS_VAL
    from core_srd_audit t
   where t.actiontime >= STAT_DATE_VAL
     and t.actiontime < STAT_DATE_VAL + 1;

  --SESSIONS
  select count(1)
    into SESSIONS_VAL
    from core_srd_session t
   where t.logintime >= STAT_DATE_VAL
     and t.logintime < STAT_DATE_VAL + 1;

  --CHANGES
  select count(1)
    into CHANGES_VAL
    from core_td_change ch
    join core_td_changeset chs
      on chs.id = ch.changeset_id
   where chs.changeset_date >= STAT_DATE_VAL
     and chs.changeset_date < STAT_DATE_VAL + 1;

  --DIAGNOSTICS_SLOW
  select count(1)
    into DIAGNOSTICS_SLOW_VAL
    from core_diagnostics t
   where t.action_date >= STAT_DATE_VAL
     and t.action_date < STAT_DATE_VAL + 1
     and t.execution_duration > 10000000;
  
  -- LONG PROCESS
  select count(1)
    into LONG_PROC_RUN_VAL
    from core_long_process_queue t
  where t.end_date >= STAT_DATE_VAL
    and t.end_date < STAT_DATE_VAL + 1
    and t.status = 3;
  
  -- LONG PROCESS ERROR
  select count(1)
    into LONG_PROC_RUN_ERROR_VAL
    from core_long_process_queue t
  where t.end_date >= STAT_DATE_VAL
    and t.end_date < STAT_DATE_VAL + 1
    and t.status = 4;
     
    
  -- LONG PROCESS INSUR LOAD
  select count(1)
    into LONG_PROC_INSUR_LOAD_RUN_VAL
    from core_long_process_queue t
  where t.end_date >= STAT_DATE_VAL
    and t.end_date < STAT_DATE_VAL + 1
    and t.status = 3
    and t.process_type_id in (3,5,7,8);
  
  -- LONG PROCESS INSUR LOAD ERROR
  select count(1)
    into LONG_PROC_INSUR_LOAD_RUN_ERROR_VAL
    from core_long_process_queue t
  where t.end_date >= STAT_DATE_VAL
    and t.end_date < STAT_DATE_VAL + 1
    and t.status = 4
    and t.process_type_id in (3,5,7,8);
    
  -- BTI_OBJECTS_LOADED_VAL
  -- BTI_OBJECTS_LOADED_ERROR_VAL
  
  -- EHD_OBJECTS_LOADED_VAL
  -- EHD_OBJECTS_LOADED_ERROR_VAL
  
  -- INSUR_BUILDING_LOADED_VAL
  select count(1)
    into INSUR_BUILDING_LOADED_VAL
    from import_log_insur_building t
   where t.date_loaded >= STAT_DATE_VAL
     and t.date_loaded < STAT_DATE_VAL + 1
     and t.is_error = 0;
     
  -- INSUR_BUILDING_LOADED_ERROR_VAL
  select count(1)
    into INSUR_BUILDING_LOADED_ERROR_VAL
    from import_log_insur_building t
   where t.date_loaded >= STAT_DATE_VAL
     and t.date_loaded < STAT_DATE_VAL + 1
     and t.is_error = 1;
  
  -- INSUR_FLAT_LOADED_VAL
  select count(1)
    into INSUR_FLAT_LOADED_VAL
    from import_log_insur_flat_b t
   where t.date_loaded >= STAT_DATE_VAL
     and t.date_loaded < STAT_DATE_VAL + 1
     and t.is_error = 0;
     
  -- INSUR_FLAT_LOADED_ERROR_VAL
  select count(1)
    into INSUR_FLAT_LOADED_ERROR_VAL
    from import_log_insur_flat_b t
   where t.date_loaded >= STAT_DATE_VAL
     and t.date_loaded < STAT_DATE_VAL + 1
     and t.is_error = 0;
  
  -- TOTAL_COUNT_INSUR_BUILDING_VAL
   select count(1)
    into TOTAL_COUNT_INSUR_BUILDING_VAL
    from insur_building_q t
   where t.actual = 1;
  
  -- TOTAL_COUNT_INSUR_FLAT_VAL
   select count(1)
    into TOTAL_COUNT_INSUR_FLAT_VAL
    from insur_flat_q t
   where t.actual = 1;

  insert into SYSTEM_DAILY_STATISTICS
    (ID,
     STAT_DATE,
     DB_SIZE,
     ERRORS,
     WARNINGS,
     ACTIONS,
     SESSIONS,
     CHANGES,
     DIAGNOSTICS_SLOW,
     
     LONG_PROC_RUN,
     LONG_PROC_RUN_ERROR,
     LONG_PROC_INSUR_LOAD_RUN,
     LONG_PROC_INSUR_LOAD_RUN_ERROR,
  
     INSUR_BUILDING_LOADED,
     INSUR_BUILDING_LOADED_ERROR,
  
     INSUR_FLAT_LOADED,
     INSUR_FLAT_LOADED_ERROR,
  
     TOTAL_COUNT_INSUR_BUILDING,
     TOTAL_COUNT_INSUR_FLAT
     
     )
  VALUES
    (ID_VAL,
     STAT_DATE_VAL,
     DB_SIZE_VAL,
     ERRORS_VAL,
     WARNINGS_VAL,
     ACTIONS_VAL,
     SESSIONS_VAL,
     CHANGES_VAL,
     DIAGNOSTICS_SLOW_VAL,
     
     LONG_PROC_RUN_VAL,
     LONG_PROC_RUN_ERROR_VAL,
     LONG_PROC_INSUR_LOAD_RUN_VAL,
     LONG_PROC_INSUR_LOAD_RUN_ERROR_VAL,
  
     INSUR_BUILDING_LOADED_VAL,
     INSUR_BUILDING_LOADED_ERROR_VAL,
  
     INSUR_FLAT_LOADED_VAL,
     INSUR_FLAT_LOADED_ERROR_VAL,
  
     TOTAL_COUNT_INSUR_BUILDING_VAL,
     TOTAL_COUNT_INSUR_FLAT_VAL
     
     );

END $function$;

END $$;
--<DO>--
--mfc_otch_get_opl_stat
DO $$
BEGIN
    CREATE OR REPLACE FUNCTION public.mfc_otch_get_opl_stat(p_okrug_id bigint, p_district_id bigint, p_period timestamp without time zone)
 RETURNS mfc_otch_type
 LANGUAGE plpgsql
AS $function$
    declare
     rval mfc_otch_type;
    begin
    with sz as (
select iip.kodpl, iip.sum_opl,
	row_number() over(partition by iip.kodpl order by iip.pmt_date) rn
from insur_okrug io
join insur_district id on id.okrug_id = io.id
join insur_input_file iif on iif.district_id = id.id
join insur_input_plat iip on iip.link_id_file = iif.emp_id
where iip.sum_opl > 0 and iip.status_identif_code in (10000107, 10000109)
and iif.period_reg_date = p_period
and (io.id = p_okrug_id or p_okrug_id is NULL)
and (id.id = p_district_id or p_district_id is null)
), sn as (
select iin.kodpl, iin.sum_nach,
	row_number() over(partition by iin.kodpl order by iin.emp_id) rn
from insur_okrug io
join insur_district id on id.okrug_id = io.id
join insur_input_file iif on iif.district_id = id.id
join insur_input_nach iin on iin.link_id_file = iif.emp_id
where iin.sum_nach > 0 
and iif.period_reg_date = p_period
and (io.id = p_okrug_id or p_okrug_id is NULL)
and (id.id = p_district_id or p_district_id is null)
)
select  
	count(case when sz.sum_opl < coalesce(sn.sum_nach,0) then 1 END),
	count(case when sz.sum_opl = coalesce(sn.sum_nach,0) then 1 END),
	count(case when sz.sum_opl > coalesce(sn.sum_nach,0) then 1 END),
    0,
    count(*), 
	sum(sz.sum_opl),
    round(sum(sz.sum_opl) * 0.07,2)
into rval
from sz
left join sn on sn.kodpl = sz.kodpl and sn.rn = sz.rn;

return rval;
END $function$;

END $$;
--<DO>--
--get_flat_number_for_sort
DO $$
BEGIN
    CREATE OR REPLACE FUNCTION public.get_flat_number_for_sort(flat_number character varying)
 RETURNS character varying
 LANGUAGE plpgsql
AS $function$
DECLARE
	v_str varchar(100);
	rval  varchar(100);
BEGIN

  SELECT INTO v_str (
    SELECT array_to_string(a, '')
    FROM regexp_matches(flat_number, '[0-9]+[а-яА-Я]{0,1}', 'g') AS f(a)
    LIMIT 1
  );
  
  rval := lpad(upper(v_str),
                 case
                   when not flat_number ~ '[0-9]+[а-яА-Я]{1}' then
                    5
                   else
                    6
                 end,
                 '0') || case
              when not flat_number ~ '[0-9]+[а-яА-Я]{1}' then
               '0'
              else
               ''
            end;
  RETURN rval;
END $function$;

END $$;
--<DO>--
--mv_refrech_fnc
DO $$
BEGIN
    CREATE OR REPLACE FUNCTION public.mv_refrech_fnc()
 RETURNS void
 LANGUAGE plpgsql
AS $function$
declare
mview record;
begin
for mview in (
	select * from mv_refresh_list
	order by id
) loop

	insert into mv_refresh_log(refresh_date, mv_name, msg_event)
	values (current_timestamp, mview.mv_name, 'Start');

	begin
		execute 'REFRESH MATERIALIZED VIEW '||mview.mv_name;

		insert into mv_refresh_log(refresh_date, mv_name, msg_event)
		values (current_timestamp, mview.mv_name, 'Success');
	exception when others then
		insert into mv_refresh_log(refresh_date, mv_name, msg_event, err_message)
		values (current_timestamp, mview.mv_name, 'Error', sqlerrm);
	end;
end loop;
END $function$;

END $$;
--<DO>--
--fias_fill_insur_buildings_address
DO $$
BEGIN
    CREATE OR REPLACE FUNCTION public.fias_fill_insur_buildings_address()
 RETURNS void
 LANGUAGE plpgsql
AS $function$
DECLARE
	v_emp_id    BIGINT;	      /* ИД здания */
    v_addressId BIGINT;       /* идентификатор адреса здания */
    cur_Buildings RefCURSOR;
BEGIN
	OPEN cur_Buildings FOR 
    	SELECT emp_id
          FROM insur_building_q
          WHERE address_id IS NULL AND actual = 1;
          
	FETCH FIRST FROM cur_Buildings INTO v_emp_id;
	WHILE FOUND
	LOOP
    	SELECT INTO v_addressId fias_fill_insur_address(v_emp_id);  
        
        IF v_addressId IS NOT NULL THEN 
        	UPDATE insur_building_q SET address_id = v_addressId
            	WHERE emp_id = v_emp_id;              
        END IF;                                
        
		FETCH NEXT FROM cur_Buildings INTO v_emp_id;
	END LOOP;
	CLOSE cur_Buildings;
END $function$;

END $$;
--<DO>--
--core_updstru_checkexistcolumn
DO $$
BEGIN
    CREATE OR REPLACE FUNCTION public.core_updstru_checkexistcolumn(character varying, character varying)
 RETURNS boolean
 LANGUAGE plpgsql
AS $function$
    declare
		sTabName ALIAS FOR $1;
		sColName ALIAS FOR $2;
		nCnt numeric;
    begin
		SELECT COUNT(*)
        INTO nCnt
        FROM information_schema.columns c
        --TABLE information_schema.columns
        WHERE lower(c.table_name) = lower(sTabName) AND lower(c.column_name) = lower(sColName) AND c.table_schema = 'public';

      	if nCnt = 0 then
        	return false;
	    else
    	    return true;
        end if;

    END $function$;

END $$;
--<DO>--
--get_313_doc_nom_for_sort
DO $$
BEGIN
    CREATE OR REPLACE FUNCTION public.get_313_doc_nom_for_sort(doc_nom character varying)
 RETURNS character varying
 LANGUAGE plpgsql
AS $function$
DECLARE
	iSlash integer;
	vYear varchar;
    vYearNom varchar;
    vDocNom varchar;
BEGIN
	/* Найти слэш */
	SELECT strpos(doc_nom, '/') INTO iSlash;
    if(iSlash = 0)then
    	return doc_nom;
    end if;

	/* Подстрока с годом */
	SELECT substr(doc_nom, iSlash + 1, length(doc_nom) - iSlash) INTO vYear;
	/* Подстрока с номером из подстроки с годом */
   	SELECT substr(vYear, 3, length(vYear) - 2) INTO vYearNom;
  	SELECT replace(vYearNom, '-', '0') INTO vYearNom;
    SELECT right('000' || vYearNom, 3) INTO vYearNom;
   	SELECT left(vYear, 2) INTO vYear;
    /* Подстрока с номером документа */
    SELECT right(substr(doc_nom, 0, iSlash), 5) INTO vDocNom;
	/* Вывод результата */
    return vYear || vDocNom || vYearNom;
END $function$;

END $$;
--<DO>--
--validate_address
DO $$
BEGIN
    CREATE OR REPLACE FUNCTION public.validate_address(a_address character varying)
 RETURNS character varying
 LANGUAGE plpgsql
AS $function$
DECLARE
    v_address VARCHAR(1000);
BEGIN
  v_address:= regexp_replace(a_address, 'г ', 'г. ', 'gi');
  v_address:= regexp_replace(v_address, 'город ', 'г. ', 'gi');
  v_address:= regexp_replace(v_address, 'дом ', 'д. ', 'gi');
  v_address:= regexp_replace(v_address, 'корпус ', 'корп. ', 'gi');
  v_address:= regexp_replace(v_address, 'строение ', 'стр. ', 'gi');
  v_address:= regexp_replace(v_address, 'владение ', 'влд. ', 'gi');
  v_address:= regexp_replace(v_address, 'территория ', 'тер. ', 'gi');
  v_address:= regexp_replace(v_address, 'район ', 'р-н ', 'gi');
  v_address:= regexp_replace(v_address, 'аллея', 'ал. ', 'gi');
  v_address:= regexp_replace(v_address, ' ал,', ' ал.,', 'gi');
  v_address:= regexp_replace(v_address, ' ш ', ' ш. ', 'gi');
  v_address:= regexp_replace(v_address, ' ш, ', ' ш., ', 'gi');
  v_address:= regexp_replace(v_address, 'шоссе', 'ш. ', 'g');
  v_address:= regexp_replace(v_address, 'дер\. ', 'д. ', 'gi');
  v_address:= regexp_replace(v_address, 'деревня', 'д.', 'gi');
  v_address:= regexp_replace(v_address, ' д ', ' д. ', 'gi');
  v_address:= regexp_replace(v_address, ' c ', ' c. ', 'gi');
  v_address:= regexp_replace(v_address, 'улица', 'ул.', 'g');
  v_address:= regexp_replace(v_address, '(ул\.)(.+?)(улица)', '\2\1', 'gi');
  v_address:= regexp_replace(v_address, 'ул ', 'ул. ', 'gi');
  v_address:= regexp_replace(v_address, ' ул, ', ' ул., ', 'gi');
  v_address:= regexp_replace(v_address, 'наб ', 'наб. ', 'gi');
  v_address:= regexp_replace(v_address, 'тер ', 'тер. ', 'gi');
  v_address:= regexp_replace(v_address, 'туп ', 'туп. ', 'gi');
  v_address:= regexp_replace(v_address, 'туп,', 'туп.,', 'gi');
  v_address:= regexp_replace(v_address, ' п ', 'п. ', 'gi');
  v_address:= regexp_replace(v_address, 'поселение', 'п.', 'gi');
  v_address:= regexp_replace(v_address, 'бульв\. ', 'б-р ', 'gi');
  v_address:= regexp_replace(v_address, 'бульвар,', 'б-р,', 'gi');
  v_address:= regexp_replace(v_address, 'пр-кт проспект ', 'пр-кт ', 'gi');
  v_address:= regexp_replace(v_address, 'проспект ', 'пр-кт ', 'gi');
  v_address:= regexp_replace(v_address, 'просп\.', 'пр-кт ', 'gi');
  v_address:= regexp_replace(v_address, 'пр\.', 'пр-д', 'gi');
  v_address:= regexp_replace(v_address, 'проезд', 'пр-д', 'gi');
  v_address:= regexp_replace(v_address, 'платформа ', 'платф. ', 'gi');
  v_address:= regexp_replace(v_address, 'пл ', 'пл. ', 'gi');
  v_address:= regexp_replace(v_address, 'ст ', 'ст. ', 'gi');
  v_address:= regexp_replace(v_address, 'квартал ', 'кв-л ', 'gi'); 
  v_address:= regexp_replace(v_address, 'кварт\.', 'кв-л', 'gi'); 
  v_address:= regexp_replace(v_address, 'мкр', 'мкр.', 'gi'); 
  v_address:= replace(v_address, ' , ', ', '); 
  v_address:= replace(v_address, '  ', ' ');   
  
RETURN v_address;
END $function$;

END $$;
--<DO>--
--fias_get_address
DO $$
BEGIN
    CREATE OR REPLACE FUNCTION public.fias_get_address(a_emp_id bigint)
 RETURNS TABLE(fulladdress character varying, guidregion character varying, typeregion character varying, region character varying, typeavtonomnyyokrug character varying, avtonomnyyokrug character varying, typedistrict character varying, district character varying, typeurbanterritory character varying, urbanterritory character varying, typelocality character varying, locality character varying, cityguid character varying, typecity character varying, city character varying, streetguid character varying, typestreet character varying, street character varying, houseguid character varying, typehouse character varying, house character varying, typecorpus character varying, corpus character varying, typestructure character varying, structure character varying, postalcode character varying)
 LANGUAGE plpgsql
AS $function$
DECLARE
	c_PostalCodeTypeName  CONSTANT VARCHAR(10):='индекс';
    c_HouseShortTypeName  CONSTANT VARCHAR(10):='д.';
    c_BuildShortTypeName  CONSTANT VARCHAR(10):='корп.';
    c_StructShortTypeName CONSTANT VARCHAR(10):='стр.';
    
	v_houseguid 			VARCHAR(255);	/* guid-ссылка на таблицу fias_house */
  	v_codekladr 			VARCHAR(255);   /* код КЛАДР */
    v_tempHouseGuid 		VARCHAR(255);
    v_tempAoGuid    		VARCHAR(255);
    v_address       		VARCHAR(500); 	/* адрес здания */
    
    v_guid 					VARCHAR(55);
  	v_aolevel				INTEGER; 
    v_shorttypename 		VARCHAR(255); 
    v_addressobjectname 	VARCHAR(255);
    
    v_guidRegion			VARCHAR(55);
    v_typeRegion 			VARCHAR(25);
    v_region 				VARCHAR(55);
    
  	v_typeAvtonomnyyOkrug 	VARCHAR(25);	
  	v_avtonomnyyOkrug 	  	VARCHAR(55);
    
  	v_typeDistrict  		VARCHAR(25);
  	v_district 				VARCHAR(55);
    
  	v_typeUrbanTerritory 	VARCHAR(25);
  	v_urbanTerritory 	 	VARCHAR(55);
    
  	v_typeLocality 			VARCHAR(25);
  	v_locality 	   			VARCHAR(55);
    
    v_cityGuid				VARCHAR(55); 	/* guid города */
    v_typeCity				VARCHAR(25); 	/* тип города */
    v_city					VARCHAR(100); 	/* город */
    
    v_streetGuid			VARCHAR(55); 	/* guid улицы */
	v_typeStreet			VARCHAR(25); 	/* тип улицы */
    v_street				VARCHAR(100); 	/* улица */
    
    v_typeHouse				VARCHAR(25); 	/* тип дома */
    v_house					VARCHAR(100); 	/* дом */ 
    
    v_typeCorpus			VARCHAR(25); 	/* тип корпуса */
    v_corpus				VARCHAR(25); 	/* корпус */
    
    v_typeStructure			VARCHAR(25); 	/* тип строения */
    v_structure				VARCHAR(25); 	/* строение */
    
    v_postalCode			VARCHAR(10); 	/* почтовый индекс */
    
    cur_FiasPartsAddress 	RefCURSOR;
BEGIN
/*
	a_emp_id - идентификатор здания в таблице insur_building
*/
	IF a_emp_id IS NOT NULL THEN    
    	SELECT INTO v_houseguid, v_codekladr  
                    lower(guid_fias_mkd) AS guid_fias_mkd, code_kladr
               FROM insur_building_q
        	   WHERE emp_id = a_emp_id AND actual = 1;            

        v_tempHouseGuid := v_houseguid;
        
        IF v_tempHouseGuid IS NULL AND v_codekladr IS NOT NULL THEN    
            SELECT INTO v_tempHouseGuid kladr_to_fias(v_codekladr);     
        END IF;   
                            
        IF v_tempHouseGuid IS NOT NULL THEN     
        	v_address:= '';
                               
            OPEN cur_FiasPartsAddress FOR 
                SELECT rtf_guid, rtf_aolevel, rtf_shorttypename, rtf_addressobjectname
                  FROM fias_houses_addressobjecttree(v_tempHouseGuid)
                ORDER BY rtf_aolevel;
                  
            FETCH FIRST FROM cur_FiasPartsAddress INTO v_guid, v_aolevel, v_shorttypename, v_addressobjectname;
            WHILE FOUND
            LOOP                          
                IF v_shorttypename = 'г' THEN 
                	v_shorttypename = 'г.';
             	END IF;           	
                               
                IF v_aolevel = 1 THEN 
                	v_guidRegion:= v_guid;                  
                    v_typeRegion:= v_shorttypename;
                    v_region:= v_addressobjectname;
                ELSEIF v_aolevel = 2 THEN             
                	v_typeAvtonomnyyOkrug := v_shorttypename; 
                    v_avtonomnyyOkrug:= v_addressobjectname;                    
                ELSEIF v_aolevel = 3 THEN             
                	v_typeDistrict:= v_shorttypename; 
                    v_district:= v_addressobjectname;    
                ELSEIF v_aolevel = 4 THEN   
                	v_cityGuid:= v_guid;                        
                    v_typeCity:= v_shorttypename; 
                    v_city:= v_addressobjectname; 
                ELSEIF v_aolevel = 5 THEN           
                	v_urbanTerritory:= v_shorttypename; 
                    v_typeUrbanTerritory:= v_addressobjectname; 
                ELSEIF v_aolevel = 6 THEN             
                	v_typeLocality:= v_shorttypename; 
                    v_locality:= v_addressobjectname;        
                ELSEIF v_aolevel = 7 THEN   
                	v_streetGuid:= v_guid;  
                    
                    v_shorttypename:= CASE v_shorttypename 
                        WHEN 'ул' THEN 'ул.'
                        WHEN 'пер' THEN 'пер.'
                        WHEN 'тер' THEN 'тер.'
                        WHEN 'наб' THEN 'наб.'
                        WHEN 'туп' THEN 'туп.'
                        WHEN 'ш' THEN 'ш.'
                        WHEN 'д' THEN 'д.' 
                        WHEN 'мкр' THEN 'мкр.'
                        WHEN 'проезд' THEN 'пр-д'                                      
                        WHEN 'аллея' THEN 'ал.'
                        WHEN 'ал' THEN 'ал.'
                        WHEN 'платф' THEN 'платф.'
                        WHEN 'пл' THEN 'пл.'
                    END;  
                                                         
                	v_typeStreet:= v_shorttypename; 
                    v_street:= v_addressobjectname;
                ELSEIF v_aolevel = 8 AND v_shorttypename = c_PostalCodeTypeName THEN
                	v_postalCode:= v_addressobjectname;
                ELSEIF v_aolevel = 8 AND v_shorttypename = c_HouseShortTypeName THEN
                	v_typeHouse:= v_shorttypename; 
                	v_house:= v_addressobjectname;
                ELSEIF v_aolevel = 8 AND v_shorttypename = c_BuildShortTypeName THEN
                	v_typeCorpus:= v_shorttypename; 
                	v_corpus:= v_addressobjectname;
                ELSEIF v_aolevel = 8 AND v_shorttypename = c_StructShortTypeName THEN
                	v_typeStructure:= v_shorttypename; 
                	v_structure:= v_addressobjectname;
                END IF;                                
                
                FETCH NEXT FROM cur_FiasPartsAddress INTO v_guid, v_aolevel, v_shorttypename, v_addressobjectname;
            END LOOP;
            CLOSE cur_FiasPartsAddress;  
            
            IF COALESCE(TRIM(v_postalCode), '') <> '' THEN
        				v_address:= v_address || CASE WHEN v_address = '' THEN '' 
                         				              ELSE ', ' END || v_postalCode;
            END IF;
            IF COALESCE(TRIM(v_region), '') <> '' THEN
        				v_address:= v_address || CASE WHEN v_address = '' THEN '' 
                         				           ELSE ', ' END || v_typeRegion ||' '|| v_region;
            END IF;
            IF COALESCE(TRIM(v_avtonomnyyOkrug), '') <> '' THEN
        				v_address:= v_address || CASE WHEN v_address = '' THEN '' 
                         				           ELSE ', ' END || v_typeAvtonomnyyOkrug ||' '|| v_avtonomnyyOkrug;
            END IF;
            IF COALESCE(TRIM(v_district), '') <> '' THEN
        				v_address:= v_address || CASE WHEN v_address = '' THEN '' 
                         				              ELSE ', ' END || v_typeDistrict ||' '|| v_district;   
            END IF;
            IF COALESCE(TRIM(v_city), '') <> '' AND TRIM(v_region) <> TRIM(v_city) THEN                                      
        				v_address:= v_address || CASE WHEN v_address = '' THEN '' 
                         				              ELSE ', ' END || v_typeCity ||' '|| v_city; 
            END IF;
            IF COALESCE(TRIM(v_urbanTerritory), '') <> '' THEN
        				v_address:= v_address || CASE WHEN v_address = '' THEN '' 
                         				              ELSE ', ' END || v_typeUrbanTerritory ||' '|| v_urbanTerritory;
            END IF;
            IF COALESCE(TRIM(v_locality), '') <> '' THEN
        				v_address:= v_address || CASE WHEN v_address = '' THEN '' 
                         				              ELSE ', ' END || v_typeLocality ||' '|| v_locality;       
            END IF;
            IF COALESCE(TRIM(v_street), '') <> '' THEN
        				v_address:= v_address || CASE WHEN v_address = '' THEN '' 
                         				              ELSE ', ' END || v_street ||' '|| v_typeStreet;            
            END IF;
            IF COALESCE(TRIM(v_house), '') <> '' THEN
        				v_address:= v_address || CASE WHEN v_address = '' THEN '' 
                         				              ELSE ', ' END || v_typeHouse ||' '|| v_house;
            END IF; 
            IF COALESCE(TRIM(v_corpus), '') <> '' THEN
        				v_address:= v_address || CASE WHEN v_address = '' THEN '' 
                         				              ELSE ', ' END || v_typeCorpus ||' '|| v_corpus;
            END IF; 
            IF COALESCE(TRIM(v_structure), '') <> '' THEN
        				v_address:= v_address || CASE WHEN v_address = '' THEN '' 
                         				              ELSE ', ' END || v_typeStructure ||' '|| v_structure;
            END IF;           
                                                   
        END IF;                
    
    END IF;
    
    RETURN QUERY SELECT v_address, v_guidRegion, v_typeRegion, v_region,  
                        v_typeAvtonomnyyOkrug, v_avtonomnyyOkrug, v_typeDistrict, v_district,  
                        v_typeUrbanTerritory, v_urbanTerritory, v_typeLocality, v_locality, 
                        v_cityGuid, v_typeCity, v_city, v_streetGuid, v_typeStreet,
                        v_street, v_houseguid, v_typeHouse, v_house, v_typeCorpus, v_corpus,    
                        v_typeStructure, v_structure, v_postalCode;                       
END $function$;

END $$;
