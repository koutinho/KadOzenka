WITH RESULT_DATA AS
(
    SELECT
        KO_UNIT.TOUR_ID AS TourId,
        KO_UNIT.CADASTRAL_NUMBER AS CadastralNumber,
        KO_UNIT.OBJECT_ID AS ObjectId,
        MARKET_REGION_DICTIONATY.ZONE AS Zone,
        MARKET_REGION_DICTIONATY.ZONE_NAME_BY_CIRCLES AS ZoneNameByCircles,
        MARKET_REGION_DICTIONATY.DISTRICT AS DistrictName,
        MARKET_REGION_DICTIONATY.REGION AS RegionName,
        KO_UNIT.PROPERTY_TYPE AS PropertyType,
        KO_UNIT.UPKS AS ObjectUpks,
        KO_UNIT.CADASTRAL_COST AS ObjectCost,
        KO_UNIT.SQUARE AS ObjectSquare,
        'Зона ' || MARKET_REGION_DICTIONATY.ZONE || '_' || MARKET_REGION_DICTIONATY.DISTRICT AS ZoneDistrict,
        'Зона ' || MARKET_REGION_DICTIONATY.ZONE || '_' || MARKET_REGION_DICTIONATY.DISTRICT || '_' || MARKET_REGION_DICTIONATY.REGION AS ZoneDistrictRegion,
        (
        ({0}) AND KO_UNIT.TOUR_ID={4}
        )
        OR 
        (
        ({1}) AND KO_UNIT.TOUR_ID={5}
        ) AS IsGroupChanged
    FROM KO_UNIT 
    JOIN
        MARKET_REGION_DICTIONATY ON (KO_UNIT.CADASTRAL_BLOCK = MARKET_REGION_DICTIONATY.CADASTRAL_QUARTAL)
    WHERE
        (KO_UNIT.PROPERTY_TYPE_CODE <> 4 OR KO_UNIT.PROPERTY_TYPE_CODE IS NULL) AND
        (KO_UNIT.PROPERTY_TYPE_CODE <> 0 OR KO_UNIT.PROPERTY_TYPE_CODE IS NULL) AND 
        KO_UNIT.TASK_ID IN ({2},{3})
)
SELECT
    Zone,
    ZoneNameByCircles,
    DistrictName,
    RegionName,
    ZoneDistrict,
    ZoneDistrictRegion,
    PropertyType,
    COUNT(*) FILTER (WHERE TourId={4}) AS FirstTourObjectCount,
    COUNT(*) FILTER (WHERE TourId={5}) AS SecondTourObjectCount,
    COUNT(*) FILTER (WHERE TourId={4} AND NOT(IsGroupChanged)) AS FirstTourObjectCountWithoutGroupChanging,
    COUNT(*) FILTER (WHERE TourId={5} AND NOT(IsGroupChanged)) AS SecondTourObjectCountWithoutGroupChanging,
    COUNT(*) FILTER (WHERE TourId={4} AND IsGroupChanged) AS FirstTourObjectCountWithGroupChanging,
    COUNT(*) FILTER (WHERE TourId={5} AND IsGroupChanged) AS SecondTourObjectCountWithGroupChanging,
    MIN(ObjectUpks) FILTER (WHERE TourId={4}) AS FirstTourMinUpks,
    AVG(ObjectUpks) FILTER (WHERE TourId={4}) AS FirstTourAverageUpks,
    MAX(ObjectUpks) FILTER (WHERE TourId={4}) AS FirstTourMaxUpks,
    MIN(ObjectUpks) FILTER (WHERE TourId={5}) AS SecondTourMinUpks,
    AVG(ObjectUpks) FILTER (WHERE TourId={5}) AS SecondTourAverageUpks,
    MAX(ObjectUpks) FILTER (WHERE TourId={5}) AS SecondTourMaxUpks,
    ((MIN(ObjectUpks) FILTER (WHERE TourId={4}))/NULLIF(MIN(ObjectUpks) FILTER (WHERE TourId={5}), 0)) AS MinUpksVarianceBetweenTours,
    ((AVG(ObjectUpks) FILTER (WHERE TourId={4}))/NULLIF(AVG(ObjectUpks) FILTER (WHERE TourId={5}), 0)) AS AverageUpksVarianceBetweenTours,
    ((MAX(ObjectUpks) FILTER (WHERE TourId={4}))/NULLIF(MAX(ObjectUpks) FILTER (WHERE TourId={5}), 0)) AS MaxUpksVarianceBetweenTours
FROM RESULT_DATA
GROUP BY 
    Zone,
    ZoneNameByCircles,
    DistrictName,
    RegionName,
    ZoneDistrict,
    ZoneDistrictRegion,
    PropertyType
ORDER BY
    Zone,
    DistrictName,
    RegionName,
    PropertyType;