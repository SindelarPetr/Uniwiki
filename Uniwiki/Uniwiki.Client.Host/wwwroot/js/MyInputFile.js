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

//download.js v3.0, by dandavis; 2008-2014. [CCBY2] see http://danml.com/download.html for tests/usage
// v1 landed a FF+Chrome compat way of downloading strings to local un-named files, upgraded to use a hidden frame and optional mime
// v2 added named files via a[download], msSaveBlob, IE (10+) support, and window.URL support for larger+faster saves than dataURLs
// v3 added dataURL and Blob Input, bind-toggle arity, and legacy dataURL fallback was improved with force-download mime and base64 support

// data can be a string, Blob, File, or dataURL




function download(data, strFileName, strMimeType) {

	var self = window, // this script is only for browsers anyway...
		u = "application/octet-stream", // this default mime also triggers iframe downloads
		m = strMimeType || u,
		x = data,
		D = document,
		a = D.createElement("a"),
		z = function (a) { return String(a); },


		B = self.Blob || self.MozBlob || self.WebKitBlob || z,
		BB = self.MSBlobBuilder || self.WebKitBlobBuilder || self.BlobBuilder,
		fn = strFileName || "download",
		blob,
		b,
		ua,
		fr;

	//if(typeof B.bind === 'function' ){ B=B.bind(self); }

	if (String(this) === "true") { //reverse arguments, allowing download.bind(true, "text/xml", "export.xml") to act as a callback
		x = [x, m];
		m = x[0];
		x = x[1];
	}



	//go ahead and download dataURLs right away
	if (String(x).match(/^data\:[\w+\-]+\/[\w+\-]+[,;]/)) {
		return navigator.msSaveBlob ?  // IE10 can't do a[download], only Blobs:
			navigator.msSaveBlob(d2b(x), fn) :
			saver(x); // everyone else can save dataURLs un-processed
	}//end if dataURL passed?

	try {

		blob = x instanceof B ?
			x :
			new B([x], { type: m });
	} catch (y) {
		if (BB) {
			b = new BB();
			b.append([x]);
			blob = b.getBlob(m); // the blob
		}

	}



	function d2b(u) {
		var p = u.split(/[:;,]/),
			t = p[1],
			dec = p[2] == "base64" ? atob : decodeURIComponent,
			bin = dec(p.pop()),
			mx = bin.length,
			i = 0,
			uia = new Uint8Array(mx);

		for (i; i < mx; ++i) uia[i] = bin.charCodeAt(i);

		return new B([uia], { type: t });
	}

	function saver(url, winMode) {


		if ('download' in a) { //html5 A[download] 			
			a.href = url;
			a.setAttribute("download", fn);
			a.innerHTML = "downloading...";
			D.body.appendChild(a);
			setTimeout(function () {
				a.click();
				D.body.removeChild(a);
				if (winMode === true) { setTimeout(function () { self.URL.revokeObjectURL(a.href); }, 250); }
			}, 66);
			return true;
		}

		//do iframe dataURL download (old ch+FF):
		var f = D.createElement("iframe");
		D.body.appendChild(f);
		if (!winMode) { // force a mime that will download:
			url = "data:" + url.replace(/^data:([\w\/\-\+]+)/, u);
		}


		f.src = url;
		setTimeout(function () { D.body.removeChild(f); }, 333);

	}//end saver 


	if (navigator.msSaveBlob) { // IE10+ : (has Blob, but not a[download] or URL)
		return navigator.msSaveBlob(blob, fn);
	}

	if (self.URL) { // simple fast and modern way using Blob and URL:
		saver(self.URL.createObjectURL(blob), true);
	} else {
		// handle non-Blob()+non-URL browsers:
		if (typeof blob === "string" || blob.constructor === z) {
			try {
				return saver("data:" + m + ";base64," + self.btoa(blob));
			} catch (y) {
				return saver("data:" + m + "," + encodeURIComponent(blob));
			}
		}

		// Blob but not URL:
		fr = new FileReader();
		fr.onload = function (e) {
			saver(this.result);
		};
		fr.readAsDataURL(blob);
	}
	return true;
} /* end download() */
