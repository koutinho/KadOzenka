DO
$do$
begin
	  if not core_updstru_checkexistcolumn('insur_invoice','num_invoice') then
		begin
		  ALTER TABLE public.insur_invoice  ADD num_invoice varchar(255);
		  comment on column public.insur_invoice.num_invoice is 'Номер счета' ;

		  insert into CORE_REGISTER_ATTRIBUTE (ID, NAME, REGISTERID, TYPE, PARENTID, REFERENCEID, VALUE_FIELD, CODE_FIELD, VALUE_TEMPLATE, PRIMARY_KEY, USER_KEY, QSCOLUMN, INTERNAL_NAME, IS_NULLABLE, DESCRIPTION, LAYOUT)
		  values (355002600, 'Номер счета', 355, 4, Null, Null, 'NUM_INVOICE', '', '', 0, Null, Null, 'numInvoice', 1, Null, Null);

		end;
	  end if;
	  if not core_updstru_checkexistcolumn('insur_invoice','date_invoice') then
		begin
		  ALTER TABLE public.insur_invoice  ADD date_invoice timestamp;
		  comment on column public.insur_invoice.date_invoice is 'Дата счета' ;

		 insert into CORE_REGISTER_ATTRIBUTE (ID, NAME, REGISTERID, TYPE, PARENTID, REFERENCEID, VALUE_FIELD, CODE_FIELD, VALUE_TEMPLATE, PRIMARY_KEY, USER_KEY, QSCOLUMN, INTERNAL_NAME, IS_NULLABLE, DESCRIPTION, LAYOUT)
		  values (355002700, 'Дата счета', 355, 5, Null, Null, 'DATE_INVOICE', '', '', 0, Null, Null, 'dateInvoice', 1, Null, Null);

		end;
	  end if;
		if not core_updstru_checkexistcolumn('insur_invoice','num_zacluch') then
		begin
		  ALTER TABLE public.insur_invoice  ADD num_zacluch varchar(255);
		  comment on column public.insur_invoice.num_zacluch is 'Номер заключения' ;

		  insert into CORE_REGISTER_ATTRIBUTE (ID, NAME, REGISTERID, TYPE, PARENTID, REFERENCEID, VALUE_FIELD, CODE_FIELD, VALUE_TEMPLATE, PRIMARY_KEY, USER_KEY, QSCOLUMN, INTERNAL_NAME, IS_NULLABLE, DESCRIPTION, LAYOUT)
		  values (355003000, 'Номер заключения', 355, 4, Null, Null, 'NUM_ZACLUCH', '', '', 0, Null, Null, 'Numzacluch', 1, Null, Null);

		end;
	  end if;
	  if not core_updstru_checkexistcolumn('insur_invoice','date_zacluch') then
		begin
		  ALTER TABLE public.insur_invoice  ADD date_zacluch timestamp;
		  comment on column public.insur_invoice.date_zacluch is 'Дата заключения' ;

		 insert into CORE_REGISTER_ATTRIBUTE (ID, NAME, REGISTERID, TYPE, PARENTID, REFERENCEID, VALUE_FIELD, CODE_FIELD, VALUE_TEMPLATE, PRIMARY_KEY, USER_KEY, QSCOLUMN, INTERNAL_NAME, IS_NULLABLE, DESCRIPTION, LAYOUT)
		  values (355003100, 'Дата заключения', 355, 5, Null, Null, 'DATE_ZACLUCH', '', '', 0, Null, Null, 'DateZacluch', 1, Null, Null);

		end;
	  end if;

 end
$do$;
