CREATE OR REPLACE FUNCTION public.fias_get_address (
  a_emp_id bigint
)
RETURNS TABLE (
  fulladdress varchar,
  guidregion varchar,
  typeregion varchar,
  region varchar,
  typeavtonomnyyokrug varchar,
  avtonomnyyokrug varchar,
  typedistrict varchar,
  district varchar,
  typeurbanterritory varchar,
  urbanterritory varchar,
  typelocality varchar,
  locality varchar,
  cityguid varchar,
  typecity varchar,
  city varchar,
  streetguid varchar,
  typestreet varchar,
  street varchar,
  houseguid varchar,
  typehouse varchar,
  house varchar,
  typecorpus varchar,
  corpus varchar,
  typestructure varchar,
  structure varchar,
  postalcode varchar
) AS
$body$
DECLARE
	c_PostalCodeTypeName  CONSTANT VARCHAR(10):='индекс';
    c_HouseShortTypeName  CONSTANT VARCHAR(10):='д.';
    c_BuildShortTypeName  CONSTANT VARCHAR(10):='корп.';
    c_StructShortTypeName CONSTANT VARCHAR(10):='стр.';
    
    v_linkBtiFsks    		BIGINT;
    v_linkEgrnBild    		BIGINT;
    
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
    	SELECT INTO v_houseguid, v_codekladr, v_linkBtiFsks, v_linkEgrnBild  
                    lower(guid_fias_mkd) AS guid_fias_mkd, code_kladr, 
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
                            
        IF v_tempHouseGuid IS NOT NULL THEN     
        	v_address:= '';
                               
            OPEN cur_FiasPartsAddress FOR 
                SELECT rtf_guid, rtf_aolevel, rtf_shorttypename, rtf_addressobjectname
                  FROM fias_houses_addressobjecttree(v_tempHouseGuid)
                ORDER BY rtf_aolevel;
                  
            FETCH FIRST FROM cur_FiasPartsAddress INTO v_guid, v_aolevel, v_shorttypename, v_addressobjectname;
            WHILE FOUND
            LOOP                                         
                v_shorttypename:= CASE v_shorttypename 
                	WHEN 'г' THEN 'г.'
                    WHEN 'ул' THEN 'ул.'
                    WHEN 'пер' THEN 'пер.'
                    WHEN 'тер' THEN 'тер.'
                    WHEN 'наб' THEN 'наб.'
                    WHEN 'туп' THEN 'туп.'
                    WHEN 'ш' THEN 'ш.'
                    WHEN 'д' THEN 'д.' 
                    WHEN 'п' THEN 'п.' 
                    WHEN 'мкр' THEN 'мкр.'
                    WHEN 'проезд' THEN 'пр-д'                                      
                    WHEN 'аллея' THEN 'ал.'
                    WHEN 'ал' THEN 'ал.'
                    WHEN 'платф' THEN 'платф.'
                    WHEN 'пл' THEN 'пл.'
                    ELSE v_shorttypename
                END;          	
                               
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
END;
$body$
LANGUAGE 'plpgsql'
VOLATILE
CALLED ON NULL INPUT
SECURITY INVOKER
COST 100 ROWS 1000;

ALTER FUNCTION public.fias_get_address (a_emp_id bigint)
  OWNER TO cipjs_main;