CREATE OR REPLACE FUNCTION public.fias_fill_insur_address (
  a_emp_id bigint
)
RETURNS bigint AS
$body$
DECLARE
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
    v_houseguid			VARCHAR(255);	/* guid-ссылка на таблицу fias_house */
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
        END IF;   
        
        SELECT INTO v_addressId emp_id FROM insur_address
            WHERE guid_fias_house = v_tempHouseGuid;   
                    
        IF v_addressId IS NULL THEN                        
            SELECT INTO v_address, v_guidRegion, v_typeRegion, v_region,  
                        v_typeAvtonomnyyOkrug, v_avtonomnyyOkrug, v_typeDistrict, v_district,  
                        v_typeUrbanTerritory, v_urbanTerritory, v_typeLocality, v_locality,
            			v_cityguid, v_typecity, v_city, 
            			v_streetguid, v_typestreet, v_street,
  						v_houseguid, v_typehouse, v_houseNumber, 
                        v_typecorpus, v_corpusNumber, 
                        v_structureTypeName, v_structureNumber, v_postalcode
                        fulladdress, guidregion, typeregion, region,  
  						typeavtonomnyyokrug, avtonomnyyokrug, typedistrict, district,  
  						typeurbanterritory, urbanterritory, typeLocality, locality,
                        cityguid, typecity, city, 
                        streetguid, typestreet, street, 
                        houseguid, typehouse, house, 
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
                		v_address, v_shortAddress, v_houseguid, v_streetguid,                 
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
END;
$body$
LANGUAGE 'plpgsql'
VOLATILE
CALLED ON NULL INPUT
SECURITY INVOKER
COST 100;

ALTER FUNCTION public.fias_fill_insur_address (a_emp_id bigint)
  OWNER TO cipjs_main;