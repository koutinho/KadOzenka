function ShowModal(modal, width, height, title, onClose) {
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

	if (onClose) {
        modal.data("kendoWindow").one("close", onClose);
    }

	modal.data("kendoWindow").open();
}

function CloseModal(modal) {
    if (modal.data("kendoWindow")) {
		modal.close();
		modal.trigger("close");
    }
}