DO $$
   DECLARE
	object_cursor CURSOR FOR select f.obj_id from insur_fsp_q f where f.actual = 1 and f.obj_id is not null
	--and f.obj_id = 28626737 
    group by f.obj_id having count(1) > 1;
  	current_object RECORD;
    current_fsp RECORD;
    fsp_num integer;
    objects_count integer;
  BEGIN
  	objects_count := 0;
  
    OPEN object_cursor;
    LOOP
    FETCH object_cursor INTO current_object;
    EXIT WHEN NOT FOUND;
  	fsp_num := 1;
    
    FOR current_fsp IN select f.emp_id from insur_fsp_q f where f.actual = 1 and f.obj_id = current_object.obj_id order by f.s_
     LOOP
        update insur_fsp_q f
        set fsp_number = left(f.fsp_number, length(f.fsp_number) - 1) || fsp_num :: varchar
        where emp_id = current_fsp.emp_id;
          
        fsp_num := fsp_num + 1;
          
     END LOOP;
      
    objects_count := objects_count + 1;
    
    RAISE NOTICE 'Объектов обработано: %', objects_count;
        
    END LOOP;
  END;
$$;