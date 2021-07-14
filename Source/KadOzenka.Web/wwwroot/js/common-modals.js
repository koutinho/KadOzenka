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

function ShowIframe(content, modal, width, height, title, onClose, forceRecreate) {
	if (!modal.data("kendoWindow") || forceRecreate) {
		modal.kendoWindow({
			content:{
				url: content,
				iframe: true
			},
			visible: false,
			resizable: true,
			modal: true,
			width: width,
			height: height,
			title: title,
			iframe: true
		});
	}

	if (onClose) {
		modal.data("kendoWindow").one("close", onClose);
	}

	modal.data("kendoWindow").open();
}