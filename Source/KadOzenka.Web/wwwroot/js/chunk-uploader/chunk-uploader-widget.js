import { uuid } from './uuid-generator.js';

class Containing {
	self = null;
	_kendoUploader = null;
	_uuidPackage = "";
	_resolveUpload = null;
	_rejectUpload = null;
	constructor() {
		self = this;
	}
	options = {
		chunkSize: 2000000,
		allowedExtensions: null
	}

	upload = async () => {
		if (self._kendoUploader) {
			self._uuidPackage = uuid();
			self._filesUploaderComparer._filesCount = self.getFiles() && self.getFiles().length;
			self._filesUploaderComparer._filesUpload = 0;
			self._kendoUploader.upload();

			return new window.Promise((resolve, reject) => {
				self._resolveUpload = resolve;
				self._rejectUpload = reject;
			});
		} else {
			console.error('kendo upload не установлен');
			return window.Promise.reject('kendo upload не установлен');
		}
	}

	_create = function () {
		self._initKendoUpload.call(this);
		self._setCustomStyle();
	}

	_initKendoUpload() {
		self._kendoUploader = $(this.element).find('input').kendoUpload({
			multiple: true,
			localization: {
				select: 'Выбрать файлы'
			},
			validation: {
				allowedExtensions: this.options.allowedExtensions
			},
			async: {
				autoUpload: false,
				chunkSize: this.options.chunkSize,
				saveUrl: '/UploadFile/ChunkSave',
				saveField: 'files'
			},
			error: function (e) {
				if (e.XMLHttpRequest.responseText) {
					Common.ShowError(e.XMLHttpRequest.responseText);
				} else {
					Common.ShowError("При загрузке выбранного файла возникла ошибка (подробно в журнале ошибок)");
				}
				self.deleteFilesByUid(self._uuidPackage);
				self._rejectUpload();
			},
			upload: function (e) {
				e['data'] = { 'uuid': self._uuidPackage }
			},
			success: function ({ operation, files, response }) {
				if (response.uploaded) {
					const count = files.length;
					self._filesUploaderComparer._filesUpload += count;
				}

				if (!!self._filesUploaderComparer.uploaded()) {
					self._resolveUpload(self._uuidPackage);
				}
			}
		}).data('kendoUpload');
		this._kendoUploader = self._kendoUploader;
	}

	deleteFilesByUid = (uuid = null) => {
		if (uuid !== null) {
			$.post('/UploadFile/DeleteFiles', { uuid }, (isDelete) => {
				if (!isDelete) {
					console.error("Файлы не удалены");
				}
			})};
		}
	_setCustomStyle() {
		var styleTag = $('<style>.k-upload-async .k-action-buttons { display: none; }</style>');
		$('html > head').append(styleTag);
	}

	getFiles = () => {
		if (self._kendoUploader) {
			var files = self._kendoUploader.getFiles();
			var tagFiles = self._kendoUploader.wrapper.find('li.k-file.k-toupload');
			var tagUids = tagFiles.toArray().map(item => $(item).data('uid'));
			return files.filter(item => tagUids.indexOf(item.uid) !== -1);
		}
		return 0;
		
	}

	_filesUploaderComparer = {
		_filesCount: 0,
		_filesUpload: 0,
		uploaded() {
			if (this._filesCount === this._filesUpload) {
				return true;
			} else {
				return false;
			}
		}
	}
}
$.widget('ko.chunkUpload', new Containing());