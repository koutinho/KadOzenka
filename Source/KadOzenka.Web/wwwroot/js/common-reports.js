function processReport() {
	kendo.ui.progress($('#getReportForm'), true);
	var form = $('#getReportForm');
	var formObject = Common.Functions.FormToObject(form);
	$.ajax({
		type: form.attr('method'),
		url: form.attr('action'),
		data: formObject,
		traditional: true,
		success: function (response) {
			if (response.Errors) {
				var errors = getErrors(response.Errors);
				Common.ShowError(errors);
				return;
			} else {
				Common.ShowMessage("Процесс добавлен в очередь. Результат будет отправлен на почту.");
			}
		},
		error: function (response) {
			Common.ShowError(response.responseText);
		},
		complete: function () {
			kendo.ui.progress($('#getReportForm'), false);
		}
	});
}