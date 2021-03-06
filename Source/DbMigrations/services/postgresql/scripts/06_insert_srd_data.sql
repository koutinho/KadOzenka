INSERT INTO public.core_srd_role (id, rolename, roletag, isadmin, all_registers_read, all_registers_write, subsystem) VALUES (3, 'Администраторы', null, 1, 1, 1, null);;
INSERT INTO public.core_srd_department (id, code, name, manager, name_genitive_case, is_deleted) VALUES (1, null, 'Сервисные пользователи', null, null, 0);

/*users*/
INSERT INTO public.core_srd_user (id, department_id, username, fullname, name, surname, patronymic, fullname_for_doc, position, is_deleted, change_date, password_md5, email, phone, external_id, isdomainuser, external_system, blocked_untill, password_change_date, prev_passwords, blocked_from, max_entrance_count, current_entrance_count, position_for_doc, short_name, short_name_for_doc) 
VALUES (1, 1, 'admin', 'Администратор', 'Админ', 'Админ', 'Админ', 'Админ А.А.', 'admin', 0, '2021-01-01 00:00:01', '86ce15a7f39fc14323b76c9b95c66165', null, null, null, 0, null, null, null, null, null, null, null, null, null, null);

/*fake user local*/
INSERT INTO public.core_srd_user (id, department_id, username, fullname, name, surname, patronymic, fullname_for_doc, position, is_deleted, change_date, password_md5, email, phone, external_id, isdomainuser, external_system, blocked_untill, password_change_date, prev_passwords, blocked_from, max_entrance_count, current_entrance_count, position_for_doc, short_name, short_name_for_doc) 
VALUES (2, 1, 'IIS GENIX1\diachkov', 'GENIX1\diachkov', null, null, null, null, null, 0, '2021-01-01 00:00:02', 'ae88c2c6358e900db720690a0e11aaa5', null, null, null, 0, null, null, null, null, null, null, null, null, null, null);

/*fake user qa*/
INSERT INTO public.core_srd_user (id, department_id, username, fullname, name, surname, patronymic, fullname_for_doc, position, is_deleted, change_date, password_md5, email, phone, external_id, isdomainuser, external_system, blocked_untill, password_change_date, prev_passwords, blocked_from, max_entrance_count, current_entrance_count, position_for_doc, short_name, short_name_for_doc) 
VALUES (3, 1, 'PG-UBUNTU\ROOT', 'PG-UBUNTU\ROOT', null, null, null, null, null, 0, '2021-01-01 00:00:02', 'ae88c2c6358e900db720690a0e11aaa5', null, null, null, 0, null, null, null, null, null, null, null, null, null, null);



/*developers*/

/*KOROTKOVA*/
INSERT INTO public.core_srd_user (id, department_id, username, fullname, name, surname, patronymic, fullname_for_doc, position, is_deleted, change_date, password_md5, email, phone, external_id, isdomainuser, external_system, blocked_untill, password_change_date, prev_passwords, blocked_from, max_entrance_count, current_entrance_count, position_for_doc, short_name, short_name_for_doc) 
VALUES (11, 1, 'COREPARTNERS\KOROTKOVA', 'COREPARTNERS\KOROTKOVA', null, null, null, null, null, 0, '2020-01-16 12:36:06.332350', '204abb9c08a4ec478005f9002ea8f066', null, null, null, 0, null, null, null, null, null, null, null, null, null, null);

/*MITRYUSHINA*/
INSERT INTO public.core_srd_user (id, department_id, username, fullname, name, surname, patronymic, fullname_for_doc, position, is_deleted, change_date, password_md5, email, phone, external_id, isdomainuser, external_system, blocked_untill, password_change_date, prev_passwords, blocked_from, max_entrance_count, current_entrance_count, position_for_doc, short_name, short_name_for_doc) 
VALUES (12, 1, 'DESKTOP-FENT7QB\YASIL', 'DESKTOP-FENT7QB\YASIL', null, null, null, null, null, 0, '2019-10-17 12:04:45.865006', 'f0a52f20c0edebf204f112e81e5e3ed6', null, null, null, 0, null, null, null, null, null, null, null, null, null, null);

/*ALEX KOROTKOV*/
INSERT INTO public.core_srd_user (id, department_id, username, fullname, name, surname, patronymic, fullname_for_doc, position, is_deleted, change_date, password_md5, email, phone, external_id, isdomainuser, external_system, blocked_untill, password_change_date, prev_passwords, blocked_from, max_entrance_count, current_entrance_count, position_for_doc, short_name, short_name_for_doc) 
VALUES (13, 1, 'DESKTOP-JU2T4MH\ALEX', 'DESKTOP-JU2T4MH\ALEX', null, null, null, null, null, 0, '2020-06-26 20:49:31.416883', '08985faab9f27113eef8adfc2200ac27', null, null, null, 0, null, null, null, '<CLOB>', null, null, null, null, null, null);

/*DMITRII*/
INSERT INTO public.core_srd_user (id, department_id, username, fullname, name, surname, patronymic, fullname_for_doc, position, is_deleted, change_date, password_md5, email, phone, external_id, isdomainuser, external_system, blocked_untill, password_change_date, prev_passwords, blocked_from, max_entrance_count, current_entrance_count, position_for_doc, short_name, short_name_for_doc) 
VALUES (14, 1, 'DESKTOP-6QQPIEF\DMITRII', 'DESKTOP-6QQPIEF\DMITRII', null, null, null, null, null, 0, '2021-03-29 22:05:46.960000', null, null, null, null, 0, null, null, null, null, null, null, null, null, null, null);

/*ALEX BOZHKO*/
INSERT INTO public.core_srd_user (id, department_id, username, fullname, name, surname, patronymic, fullname_for_doc, position, is_deleted, change_date, password_md5, email, phone, external_id, isdomainuser, external_system, blocked_untill, password_change_date, prev_passwords, blocked_from, max_entrance_count, current_entrance_count, position_for_doc, short_name, short_name_for_doc) 
VALUES (15, 1, 'DESKTOP-ALEX\ALEX', 'DESKTOP-ALEX\ALEX', null, null, null, null, null, 0, '2019-11-06 11:05:10.265269', 'ae88c2c6358e900db720690a0e11aaa5', null, null, null, 0, null, null, null, null, null, null, null, null, null, null);

/* YAROSLAV SILANOV */
INSERT INTO public.core_srd_user (id, department_id, username, fullname, name, surname, patronymic, fullname_for_doc, position, is_deleted, change_date, password_md5, email, phone, external_id, isdomainuser, external_system, blocked_untill, password_change_date, prev_passwords, blocked_from, max_entrance_count, current_entrance_count, position_for_doc, short_name, short_name_for_doc) 
VALUES (16, 1, 'DESKTOP-0VCVNBT\YASIL', 'DESKTOP-0VCVNBT\YASIL', null, null, null, null, null, 0, '2020-10-27 16:53:49.600000', null, null, null, null, 0, null, null, null, null, null, null, null, null, null, null);

/* USER ??? */
INSERT INTO public.core_srd_user (id, department_id, username, fullname, name, surname, patronymic, fullname_for_doc, position, is_deleted, change_date, password_md5, email, phone, external_id, isdomainuser, external_system, blocked_untill, password_change_date, prev_passwords, blocked_from, max_entrance_count, current_entrance_count, position_for_doc, short_name, short_name_for_doc) 
VALUES (17, 1, 'DESKTOP-PH30T11\USER', 'DESKTOP-PH30T11\USER', null, null, null, null, null, 0, '2019-10-28 13:00:17.750893', '627e44f6a6ce65390dab7386429b3867', null, null, null, 0, null, null, null, null, null, null, null, null, null, null);

/*fake user Producton*/
INSERT INTO public.core_srd_user (id, department_id, username, fullname, name, surname, patronymic, fullname_for_doc, position, is_deleted, change_date, password_md5, email, phone, external_id, isdomainuser, external_system, blocked_untill, password_change_date, prev_passwords, blocked_from, max_entrance_count, current_entrance_count, position_for_doc, short_name, short_name_for_doc) 
VALUES (18, 1, 'NGINX\ROOT', 'NGINX\ROOT', null, null, null, null, null, 0, '2021-01-01 00:00:02', 'ae88c2c6358e900db720690a0e11aaa5', null, null, null, 0, null, null, null, null, null, null, null, null, null, null);
INSERT INTO public.core_srd_user (id, department_id, username, fullname, name, surname, patronymic, fullname_for_doc, position, is_deleted, change_date, password_md5, email, phone, external_id, isdomainuser, external_system, blocked_untill, password_change_date, prev_passwords, blocked_from, max_entrance_count, current_entrance_count, position_for_doc, short_name, short_name_for_doc) 
VALUES (19, 1, 'NGINX\ROOT.LONGPROCESS',  'NGINX\ROOT.LONGPROCESS', null, null, null, null, null, 0, '2021-01-01 00:00:02', 'ae88c2c6358e900db720690a0e11aaa5', null, null, null, 0, null, null, null, null, null, null, null, null, null, null);


/*roles*/
INSERT INTO public.core_srd_user_role (id, user_id, role_id) VALUES (1, 1, 3);
INSERT INTO public.core_srd_user_role (id, user_id, role_id) VALUES (2, 2, 3);
INSERT INTO public.core_srd_user_role (id, user_id, role_id) VALUES (3, 3, 3);
/*developers*/
INSERT INTO public.core_srd_user_role (id, user_id, role_id) VALUES (11, 11, 3);
INSERT INTO public.core_srd_user_role (id, user_id, role_id) VALUES (12, 12, 3);
INSERT INTO public.core_srd_user_role (id, user_id, role_id) VALUES (13, 13, 3);
INSERT INTO public.core_srd_user_role (id, user_id, role_id) VALUES (14, 14, 3);
INSERT INTO public.core_srd_user_role (id, user_id, role_id) VALUES (15, 15, 3);
INSERT INTO public.core_srd_user_role (id, user_id, role_id) VALUES (16, 16, 3);
INSERT INTO public.core_srd_user_role (id, user_id, role_id) VALUES (17, 17, 3);

INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (38976, 503, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (38977, 555, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (38978, 554, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (38979, 557, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (38980, 556, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (38981, 549, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (38982, 550, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (38983, 521, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (38984, 508, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (38985, 547, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (38986, 509, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (38987, 519, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (38988, 512, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (38989, 544, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (38990, 559, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (33488, 31, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (33489, 350, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (33490, 351, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (33491, 352, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (33492, 353, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (33493, 354, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (33494, 39, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (33495, 28, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (33496, 357, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (33497, 358, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (33498, 32, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (33499, 37, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (33500, 355, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (33501, 36, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (33502, 359, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (33503, 360, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (33504, 361, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (33505, 362, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (38991, 513, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (38992, 558, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (38993, 514, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (38994, 516, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (38995, 515, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (38996, 517, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (38997, 506, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (38998, 546, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (38999, 507, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (39000, 518, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (39001, 545, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (39002, 551, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (39003, 552, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (39004, 553, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (39005, 510, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (39006, 548, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (39007, 511, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (33523, 1, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (33524, 29, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (33525, 12344, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (33526, 1300126, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (33527, 33, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (33528, 1300138, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (33529, 1300140, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (33530, 1300139, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (33531, 50, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (33532, 1300131, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (33533, 51, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (33534, 52, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (33535, 55, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (33536, 35, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (33537, 30, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (39008, 520, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (39009, 560, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (39010, 561, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (39011, 562, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (39012, 563, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (39013, 564, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (39014, 602, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (39015, 603, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (39016, 605, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (39017, 607, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (39018, 606, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (33571, 612, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (33572, 614, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (33573, 615, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (33574, 617, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (33575, 616, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (33576, 502, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (33577, 622, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (33578, 619, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (33579, 631, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (33580, 632, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (33581, 633, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (33582, 624, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (33583, 618, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (33584, 625, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (33585, 629, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (33586, 630, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (33587, 628, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (33588, 626, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (33589, 621, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (33590, 620, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (33591, 623, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (33592, 635, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (33593, 642, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (33594, 643, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (33595, 634, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (33596, 638, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (33597, 639, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (33598, 640, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (33599, 641, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (33600, 637, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (33601, 636, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (33602, 644, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (33603, 645, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (42493, 653, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (42494, 651, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (42495, 654, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (42496, 652, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (45770, 526, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (45773, 529, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (45775, 527, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (46278, 531, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (46279, 533, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (46280, 532, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (35518, 504, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (35519, 610, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (35520, 609, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (35521, 608, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (35522, 611, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (35523, 500, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (35524, 571, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (35525, 575, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (35526, 573, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (35527, 574, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (35528, 572, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (35529, 570, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (35530, 580, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (35531, 582, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (35532, 581, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (35533, 578, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (35534, 577, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (35535, 576, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (35536, 501, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (35537, 583, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (35538, 601, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (35539, 587, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (35540, 592, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (35541, 591, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (35542, 593, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (35543, 594, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (35544, 590, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (35545, 588, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (35546, 586, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (35547, 589, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (35548, 584, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (35549, 595, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (35550, 646, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (35551, 597, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (35552, 596, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (35553, 585, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (35554, 598, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (35555, 599, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (35556, 600, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (35559, 505, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (35560, 523, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (35561, 525, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (35565, 530, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (35566, 613, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (35567, 528, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (34059, 40, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (34985, 647, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (34986, 648, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (34987, 649, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (35568, 534, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (35572, 524, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (35573, 535, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (35574, 537, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (35575, 536, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (35576, 541, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (35577, 543, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (35578, 542, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (35579, 538, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (35580, 540, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (35581, 539, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (53975, 1300141, 3);
INSERT INTO public.core_srd_role_function (id, function_id, role_id) VALUES (53976, 1300142, 3);

