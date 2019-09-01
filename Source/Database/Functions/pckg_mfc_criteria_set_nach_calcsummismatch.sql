CREATE OR REPLACE FUNCTION public.pckg_mfc_criteria_set_nach_calcsummismatch(p_input_file_id bigint)
 RETURNS void
 LANGUAGE plpgsql
AS $function$
BEGIN
  update insur_input_nach
  set    criteria_json =  
	coalesce(criteria_json::jsonb -
      		(select i
            from generate_series(0, jsonb_array_length(criteria_json::jsonb) - 1) AS i
       		where criteria_json::jsonb -> i ->> 'Id' = '12166001'), coalesce(criteria_json::jsonb,'[]'))::jsonb
            || concat('{ "Id":12166001, "Value":', CASE WHEN (opl is null and sum_nach is not null) or (opl is not null and sum_nach is null) or ROUND(opl * 1.79, 2) <> ROUND(sum_nach, 2) THEN 1 ELSE 0 END, '}')::jsonb
  where link_id_file = p_input_file_id;
END;
$function$
