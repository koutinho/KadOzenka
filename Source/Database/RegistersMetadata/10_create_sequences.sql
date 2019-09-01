
-- X. Загрузка последовательностей(sequences)

--<DO>--
DO $$
BEGIN
  if (not core_updstru_checkexistsequence('core_diagnostics_seq')) then
    CREATE SEQUENCE core_diagnostics_seq AS bigint INCREMENT BY 1 MINVALUE 1 MAXVALUE 9223372036854775807 START WITH 2;
    end if;
END $$;

--<DO>--
DO $$
BEGIN
  if (not core_updstru_checkexistsequence('core_error_log_seq')) then
    CREATE SEQUENCE core_error_log_seq AS bigint INCREMENT BY 1 MINVALUE 1 MAXVALUE 9223372036854775807 START WITH 2;
    end if;
END $$;

--<DO>--
DO $$
BEGIN
  if (not core_updstru_checkexistsequence('core_layout_det_id_seq')) then
    CREATE SEQUENCE core_layout_det_id_seq AS bigint INCREMENT BY 1 MINVALUE 1 MAXVALUE 9999999999999 START WITH 2;
    end if;
END $$;

--<DO>--
DO $$
BEGIN
  if (not core_updstru_checkexistsequence('core_layout_id_seq')) then
    CREATE SEQUENCE core_layout_id_seq AS bigint INCREMENT BY 1 MINVALUE 1 MAXVALUE 9999999999999 START WITH 2;
    end if;
END $$;

--<DO>--
DO $$
BEGIN
  if (not core_updstru_checkexistsequence('core_srd_seq')) then
    CREATE SEQUENCE core_srd_seq AS bigint INCREMENT BY 1 MINVALUE 1 MAXVALUE 9223372036854775807 START WITH 2;
    end if;
END $$;

--<DO>--
DO $$
BEGIN
  if (not core_updstru_checkexistsequence('qry_qryid_seq')) then
    CREATE SEQUENCE qry_qryid_seq AS bigint INCREMENT BY 1 MINVALUE 1 MAXVALUE 9223372036854775807 START WITH 2;
    end if;
END $$;

--<DO>--
DO $$
BEGIN
  if (not core_updstru_checkexistsequence('qryfilter_qryfilterid_seq')) then
    CREATE SEQUENCE qryfilter_qryfilterid_seq AS bigint INCREMENT BY 1 MINVALUE 1 MAXVALUE 9223372036854775807 START WITH 2;
    end if;
END $$;

--<DO>--
DO $$
BEGIN
  if (not core_updstru_checkexistsequence('reference_id_seq')) then
    CREATE SEQUENCE reference_id_seq AS bigint INCREMENT BY 1 MINVALUE 1 MAXVALUE 9223372036854775807 START WITH 2;
    end if;
END $$;

--<DO>--
DO $$
BEGIN
  if (not core_updstru_checkexistsequence('refitem_itemid_seq')) then
    CREATE SEQUENCE refitem_itemid_seq AS bigint INCREMENT BY 1 MINVALUE 1 MAXVALUE 9223372036854775807 START WITH 2;
    end if;
END $$;

--<DO>--
DO $$
BEGIN
  if (not core_updstru_checkexistsequence('reg_object_seq')) then
    CREATE SEQUENCE reg_object_seq AS bigint INCREMENT BY 1 MINVALUE 1 MAXVALUE 9223372036854775807 START WITH 2;
    end if;
END $$;

--<DO>--
DO $$
BEGIN
  if (not core_updstru_checkexistsequence('seq_core_td')) then
    CREATE SEQUENCE seq_core_td AS bigint INCREMENT BY 1 MINVALUE 1 MAXVALUE 999999999999999999 START WITH 2;
    end if;
END $$;

--<DO>--
DO $$
BEGIN
  if (not core_updstru_checkexistsequence('treehelper_id_seq')) then
    CREATE SEQUENCE treehelper_id_seq AS bigint INCREMENT BY 1 MINVALUE 1 MAXVALUE 9223372036854775807 START WITH 2;
    end if;
END $$;
