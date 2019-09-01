DO $$
    DECLARE
        rows_updated integer;
        package_num integer;
    BEGIN
      
      package_num := 0;
      
       LOOP
          
         update insur_fsp_q f
             set obj_reestr_id = 317,
                 obj_id       =
                 (select flat.emp_id
                    from insur_input_nach n
                    join insur_flat_q flat
                      on flat.actual = 1
                     and flat.unom = n.unom
                     and flat.kvnom = n.kvnom
                   where n.fsp_id = f.emp_id
                     and n.type_source_code = 12121001
                   limit 1)
           where id in (select f.id from insur_fsp_q f where f.obj_reestr_id is null limit 1000);
           
           RAISE NOTICE 'Package: %', package_num;
           
           package_num := package_num + 1;
           
           GET DIAGNOSTICS rows_updated = ROW_COUNT;
          
          IF rows_updated = 0 THEN
              EXIT;
          END IF;
      END LOOP;
    
    END
$$;