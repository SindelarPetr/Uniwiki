var nextFileId = 0;
var inputMemory = {};
var xhrMemory = {};

window.MyInput = {
	init: function (element, callbacks) {
		// Add event handler
		element.addEventListener('change', function handleInputFileChange(event) {
			// Get selected files
			var inputFiles = element.files;

			for (var inputFile of inputFiles) {

				var file = {
					id: ++nextFileId,
					lastModified: new Date(inputFile.lastModified).toISOString(),
					name: inputFile.name,
					size: inputFile.size,
					type: inputFile.type
				};

				inputMemory[file.id] = { file: inputFile, callbacks: callbacks };

				// Attach the blob data itself as a non-enumerable property so it doesn't appear in the JSON
				Object.defineProperty(inputFile, 'blob', { value: inputFile });

				// Call onSelected
				callbacks.invokeMethodAsync('HandleFileSelected', file);
			}
			// Clear the value of the element
			element.value = "";
		});
	},

	startUpload: function (fileId, dataForServer) {

		console.log("startUpload");

		var input = inputMemory[fileId];

		if (!input)
			throw Error("No callbacks and file for defined! ID: " + fileId);

		var fileCallbacks = input.callbacks;
		var fileFromInput = input.file;

		// Upload the file
		var xhr = xhrMemory[fileId];

		// Check if the file has some upload in progress
		if (xhr) {
			throw Error("File with ID: " + fileId + " is already being uploaded.");
		}

		xhr = new XMLHttpRequest();
		xhr.fileId = fileId;
		xhrMemory[fileId] = xhr;

		var formData = new FormData();
		formData.append("File", fileFromInput);
		formData.append("Data", dataForServer);

		xhr.open("POST", "/upload");

		console.log(xhr);

		// Callback start
		xhr.onloadstart = function (e) {
			console.log("start");
			console.log(e);
			fileCallbacks.invokeMethodAsync('HandleStart', fileId);
		}

		// Callback progress
		xhr.upload.addEventListener("progress", function (e) {
			if (e.lengthComputable) {
				console.log("progress");
				console.log(e);
				var percentComplete = e.loaded / e.total;
				fileCallbacks.invokeMethodAsync('HandleProgress', { FileId: fileId, Progress: percentComplete });
			}
		}, false);

		// Callback end
		xhr.onloadend = function (e) {
			console.log("end");
			console.log(e);
			xhrMemory[e.target.fileId] = undefined;
			// fileCallbacks.invokeMethodAsync('HandleFinish', e.target.fileId);
			if (xhr.status === 200) { // Handle success
				console.log("success--");
				fileCallbacks.invokeMethodAsync('HandleSuccess', { fileId: e.target.fileId, dataForClient: e.target.responseText });
			} else { // Handle error
				console.log('failed--');
				fileCallbacks.invokeMethodAsync('HandleError', fileId);
			}
		}

		// Callback error
		//xhr.onerror = function (e) {
		//	console.log("-------- error in onerror ......../////////............");
		//	console.log(e);
		//	//fileCallbacks.invokeMethodAsync('HandleError', fileId);
		//}

		// Callback abort
		xhr.onabort = function (e) {
			console.log("abort");
			console.log(e);
			fileCallbacks.invokeMethodAsync('HandleAbort', fileId);
		}

		// Send the file
		xhr.send(formData);
		console.log("after send");
	},

	abortUpload: function (fileId) {
		console.log("before abort");
		if (xhrMemory[fileId])
			xhrMemory[fileId].abort();
		console.log("after abort");
	}
}

