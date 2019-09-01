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
END;