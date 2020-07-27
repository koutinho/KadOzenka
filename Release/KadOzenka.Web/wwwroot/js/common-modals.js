function ShowModal(modal, width, height, title) {
	if (!modal.data("kendoWindow")) {
		modal.kendoWindow({
			visible: false,
			resizable: true,
			modal: true,
			width: width,
			height: height, 
			title: title
		}).data("kendoWindow").center();
	}
	modal.data("kendoWindow").open();
}