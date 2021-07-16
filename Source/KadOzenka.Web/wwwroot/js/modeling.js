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





//Методы для факторов модели

function setFactorParametersVisibility() {
	//debugger;
	var selectedMarkType = Number($('#MarkType').data('kendoDropDownList').value());
	var correctItemBlock = $("#correctItemBlock");
	var kBlock = $("#kBlock");
	var dictionaryBlock = $("#dictionaryBlock");
	var straightMarkType = 2;
	var reverseMarkType = 3;
	var defaultMarkType = 1;
	if (selectedMarkType === straightMarkType || selectedMarkType === reverseMarkType) {
		//todo удалить после добавление этих блоков в авто.модель
		if (correctItemBlock && kBlock) {
			correctItemBlock.show();
			kBlock.show();
		}
	} else {
		//todo удалить после добавление этих блоков в авто.модель
		if (correctItemBlock && kBlock) {
			correctItemBlock.hide();
			kBlock.hide();
		}
	}
	if (selectedMarkType === defaultMarkType) {
		dictionaryBlock.show();
	} else {
		dictionaryBlock.hide();
	}
}