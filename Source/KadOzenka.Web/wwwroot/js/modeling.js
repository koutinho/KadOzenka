MathJax = {
	loader: { load: ['input/asciimath', 'output/chtml'] },
	asciimath: {
		decimalsign: "."
	}
}


function getInfoDependedOnAlgorithmType(modelId, algType) {
	$.ajax({
		url: `/Modeling/GetFormulaInfo?modelId=${modelId}&algType=${algType}`,
		type: 'GET',
		success: function (e) {
			$('#Formula').val(e.formula);
			$('#A0').data("kendoNumericTextBox").value(e.a0);
			$("#mathFormula").empty().append("`" + e.formula + "`");
			redrawFormula();
		},
		error: function (e) {
			Common.ShowError(e.responseText);
		}
	});
}


function redrawFormula() {
	//TODO KOMO-98 разобраться, почему иногда падает с ошибкой
	try {
		MathJax.typesetPromise().then(() => {
			MathJax.typesetPromise();
		}).catch((err) => console.log(err.message));
	}
	catch (exception) {
		console.error('Ошибка во время работы MathJax (будет поправлена позднее)', exception);
	}
}


function initCopyButton() {
	$("#copyFormulaBtn").data("kendoButton").bind("click", copyFormula);

	$("#copyFormulaBtn").kendoTooltip({
		filter: "span",
		position: "top",
		content: "Скопировано"
	}).data("kendoTooltip");
}


function copyFormula(btn) {
	btn.preventDefault();
	var formula = $("#Formula").val();
	//работает только через https
	//navigator.clipboard.writeText(formula);
	var proxy = document.createElement('textarea');
	$(proxy).val(formula)
		.prop('readonly', true)
		.css({ "position": "absolute", "left": "9999px" })
		.appendTo($('body'));
	proxy.select();
	document.execCommand('copy');
	$(proxy).remove();
	var tooltip = $("#copyFormulaBtn").data("kendoTooltip");
	tooltip.show();
}


function initToolBarForFactorsGrid(addFactorFunc, editFactorFunc, deleteFactorFunc) {
	$('#factorsToolbar').kendoToolBar({
		items: [
			{
				type: 'button',
				id: 'addFactorBtn',
				text: '',
				attributes: { title: "Добавить" },
				icon: 'add',
				click: addFactorFunc
			},
			{
				type: 'button',
				id: 'editFactorBtn',
				text: '',
				attributes: { title: "Изменить" },
				icon: 'edit',
				click: editFactorFunc
			},
			{
				type: 'button',
				id: 'deleteFactorBtn',
				className: "k-state-disabled",
				text: '',
				attributes: { title: "Удалить" },
				icon: 'delete',
				click: deleteFactorFunc
			}
		]
	});
	$(".k-toolbar [title]").kendoTooltip({
		position: "top"
	});
}


function initFactorsGrid(getDataForModelAttributesDownloaderFunc, onGridChangeFunc) {
    $('#grid').kendoGrid({
        dataSource: {
            transport: {
                read: {
                    url: '/Modeling/GetModelAttributes',
                    data: getDataForModelAttributesDownloaderFunc,
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json'
                }
            },
            schema: {
                model: {
                    id: "AttributeId",
                    fields: {
                        AttributeId: { type: "number" },
                        DictionaryId: { type: "number" },
                        AttributeName: { type: "string" },
                        DictionaryName: { type: "string" },
                        Correction: { type: "number" },
                        IsActive: { type: "boolean" },
                        MarkType: { type: "string" }
                    }
                }
            }
        },
        columns: [
            {
                field: 'AttributeId',
                hidden: true
            },
            {
                field: 'DictionaryId',
                hidden: true
            },
            {
                field: 'AttributeName',
                title: 'Фактор',
                width: "40%"
            },
            {
                field: 'MarkType',
                title: 'Тип метки',
                attributes: { style: "text-align: center;" }
            },
            {
                field: 'DictionaryName',
                title: 'Справочник',
                attributes: { style: "text-align: center;" }
            },
            {
                field: 'Coefficient',
                title: 'Коэффициент',
                attributes: { style: "text-align: center;" }
            },
            {
                field: 'CorrectingTerm',
                title: 'Корректирующее слагаемое',
                headerAttributes: { style: "word-wrap: break-word;" }
            },
            {
                field: 'K',
                title: 'К'
            },
            {
                field: 'Correction',
                title: 'Поправка',
                attributes: { style: "text-align: center;" }
            },
            {
                field: 'IsActive',
                title: 'Использовать в модели',
                template: '<input type="checkbox" #= IsActive ? \'checked="checked"\' : "" # class="chkbx" disabled="disabled" />',
                attributes: { style: "text-align: center;" }
            }
        ],
        change: onGridChangeFunc,
        dataBound: onGridChangeFunc,
        width: '100%',
        scrollable: true,
        selectable: true,
        pageable: {
            pageSize: 10
        }
    });
}