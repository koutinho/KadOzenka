CREATE OR REPLACE FUNCTION public.fias_fill_insur_flats_address()
 RETURNS void
 LANGUAGE plpgsql
AS $function$
DECLARE
	v_f_emp_id    BIGINT;	    /* ИД помещения */
    v_b_emp_id    BIGINT;	    /* ИД здания */
    v_houseGuid   VARCHAR(255); /* houseGuid здания */
    v_codeKladr   VARCHAR(255); /* код КЛАДР здания */
    v_addressId   BIGINT;       /* идентификатор адреса здания */
    cur_Flats     RefCURSOR;
BEGIN
	OPEN cur_Flats FOR
    	SELECT b.emp_id AS b_emp_id, f.emp_id AS f_emp_id, b.guid_fias_mkd, b.code_kladr FROM insur_flat_q f
        INNER JOIN insur_building_q b on f.link_object_mkd = b.emp_id
          WHERE f.address_id is null AND f.actual = 1 AND b.actual = 1;
          
	FETCH FIRST FROM cur_Flats INTO v_b_emp_id, v_f_emp_id, v_houseGuid, v_codeKladr;
	WHILE FOUND
	LOOP
    	SELECT INTO v_addressId fias_fill_insur_address(v_b_emp_id, v_houseGuid, v_codeKladr);  
        
        IF v_addressId IS NOT NULL THEN 
        	UPDATE insur_flat_q SET address_id = v_addressId
            	WHERE emp_id = v_f_emp_id;              
        END IF;                                  
        
		FETCH NEXT FROM cur_Flats INTO v_b_emp_id, v_f_emp_id, v_houseGuid, v_codeKladr;
	END LOOP;
	CLOSE cur_Flats;
END;
$function$
