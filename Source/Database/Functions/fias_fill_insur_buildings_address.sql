CREATE OR REPLACE FUNCTION public.fias_fill_insur_buildings_address()
 RETURNS void
 LANGUAGE plpgsql
AS $function$
DECLARE
	v_emp_id    BIGINT;	      /* ИД здания */
    v_houseGuid VARCHAR(255); /* houseGuid здания */
    v_codeKladr VARCHAR(255); /* код КЛАДР здания */
    v_addressId BIGINT;       /* идентификатор адреса здания */
    cur_Buildings RefCURSOR;
BEGIN
	OPEN cur_Buildings FOR 
    	SELECT emp_id, lower(guid_fias_mkd) AS guid_fias_mkd, code_kladr
          FROM insur_building_q
          WHERE address_id IS NULL AND actual = 1 
           AND (guid_fias_mkd IS NOT NULL OR code_kladr IS NOT NULL);
          
	FETCH FIRST FROM cur_Buildings INTO v_emp_id, v_houseGuid, v_codeKladr;
	WHILE FOUND
	LOOP
    	SELECT INTO v_addressId fias_fill_insur_address(v_emp_id, v_houseGuid, v_codeKladr);  
        
        IF v_addressId IS NOT NULL THEN 
        	UPDATE insur_building_q SET address_id = v_addressId
            	WHERE emp_id = v_emp_id;              
        END IF;                                
        
		FETCH NEXT FROM cur_Buildings INTO v_emp_id, v_houseGuid;
	END LOOP;
	CLOSE cur_Buildings;
END;
$function$
