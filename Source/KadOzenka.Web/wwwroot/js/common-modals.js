function ShowModal(modal, width, height) {
	if (!modal.data("kendoWindow")) {
		modal.kendoWindow({
			visible: false,
			resizable: true,
			modal: true,
			width: width,
			height: height
		}).data("kendoWindow").center();
	}
	modal.data("kendoWindow").open();
}