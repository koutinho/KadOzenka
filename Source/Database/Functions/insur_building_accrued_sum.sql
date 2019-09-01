/*Сумма начислений*/
DECLARE
  v_sum NUMERIC; /*Сумма начислений*/
BEGIN
  SELECT INTO v_sum SUM(n.sum_nach)
  FROM insur_flat_q fl
  JOIN insur_fsp_q f on fl.emp_id = f.obj_id and f.actual = 1 and f.obj_reestr_id = 317
  JOIN insur_input_nach n on n.fsp_id = f.emp_id
  WHERE fl.link_object_mkd = building_id AND
        n.period_reg_date = date_trunc('month', current_timestamp - month_count * interval '1 month') AND
        n.type_source_code = 12121001;
  RETURN v_sum;
END;