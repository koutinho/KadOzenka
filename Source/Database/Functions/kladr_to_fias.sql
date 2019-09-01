CREATE OR REPLACE FUNCTION public.kladr_to_fias(a_codekladr character varying)
 RETURNS character varying
 LANGUAGE plpgsql
AS $function$
DECLARE
	v_aoguid    VARCHAR(36);	 /* ИД адресообразующего элемента */
    v_houseGuid VARCHAR(36);	 /* ИД здания */
    v_buildingNum NUMERIC;  /* Счетчик записей домов для КЛАДР 4 */
 	v_codeKladr VARCHAR(36); /* код КЛАДР адресообразующего элемента */
BEGIN
  IF char_length(a_codekladr) > 15 THEN
	SELECT INTO v_aoguid f.aoguid from fias_addrobj f
    	WHERE currstatus = 0 AND
        	  f.plaincode = SUBSTRING(a_codekladr from 1 for 15);
        
    v_buildingNum:= CAST(SUBSTRING(a_codekladr from 16) AS INTEGER);
    
    SELECT INTO v_houseGuid h.houseguid from fias_house h
    	WHERE h.counter = v_buildingNum and h.aoguid = v_aoguid;
    
    RETURN v_houseGuid;
  END IF;
END;
$function$
