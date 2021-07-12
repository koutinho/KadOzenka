update core_reference
set simple_values = '[
    {
        "Id": 1,
        "Name": "Normalisation",
        "Value": "Нормализация"
    },
    {
        "Id": 2,
        "Name": "Harmonization",
        "Value": "Гармонизация"
    },
    {
        "Id": 4,
        "Name": "UnloadingFromDict",
        "Value": "Выборка из справочника ЦОД"
    },
    {
        "Id": 5,
        "Name": "EstimatedGroup",
        "Value": "Проставление оценочной группы"
    },
    {
        "Id": 6,
        "Name": "TransferAttributesWithoutCreate",
        "Value": "Перенос атрибутов (без создания)"
    },
    {
        "Id": 7,
        "Name": "TransferAttributesWithCreate",
        "Value": "Перенос атрибутов (с созданием)"
    },
    {
        "Id": 8,
        "Name": "Inheritance",
        "Value": "Наследование"
    },
    {
        "Id": 9,
        "Name": "ExportFactorsByTask",
        "Value": "Выгрузка факторов единиц оценки по заданию на оценку"
    },
    {
        "Id": 10,
        "Name": "NormalisationFinal",
        "Value": "Финализация нормализации"
    }
]'
where referenceid = 801;