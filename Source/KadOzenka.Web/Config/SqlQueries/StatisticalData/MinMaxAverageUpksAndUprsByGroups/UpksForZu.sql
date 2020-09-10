SELECT 
	(SELECT L2_R205.GROUP_NAME AS "GROUP_NAME" FROM KO_GROUP L2_R205 WHERE L2_R205.ID = L1_R205.PARENT_ID) ParentGroup,
	{0}
    count(*) as "ObjectsCount",
	min(L1_R201.UPKS) AS "Min",
    avg(L1_R201.UPKS) AS "Avg",
    sum(L1_R201.CADASTRAL_COST) / NULLIF(sum(L1_R201.SQUARE), 0)  AS "AvgWeight",
    max(L1_R201.UPKS) AS "Max"
FROM KO_UNIT L1_R201 LEFT JOIN KO_GROUP L1_R205 ON (L1_R201.GROUP_ID = L1_R205.ID)
WHERE 
(L1_R201.TASK_ID IN ({1}) AND L1_R201.PROPERTY_TYPE_CODE = 4 )
group by ParentGroup {2}
order by ParentGroup {2}
