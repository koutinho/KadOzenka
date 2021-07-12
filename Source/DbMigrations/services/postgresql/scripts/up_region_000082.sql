do $$
    begin
        if (select 1 from common_registers_with_soft_deletion where main_table_name = 'GBU_SOURCE2_A') = 1
then delete from common_registers_with_soft_deletion where main_table_name = 'GBU_SOURCE2_A';
end if;
end;
$$;

INSERT INTO common_registers_with_soft_deletion VALUES(nextval('REG_OBJECT_SEQ'), 2, 'GBU_SOURCE2_A');