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
END;
$function$
