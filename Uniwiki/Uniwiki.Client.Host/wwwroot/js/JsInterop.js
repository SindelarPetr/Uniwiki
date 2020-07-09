var scrollCallbacks;
window.interopJsFunctions = {
	FocusElement: function (element) {
		element.focus();
	},

	HideCollapse: function (elementId) {
		$('#' + elementId).collapse({
			toggle: false
		})
		$('#' + elementId).removeClass('show');
	},

	GoBack: function () {
		if (history.length > 1) {
			history.go(-1);
			return true;
		} else {
			return false;
		}
	},

	GetBrowserLocale: function () {
		return (navigator.languages && navigator.languages.length) ? navigator.languages[0] : navigator.userLanguage || navigator.language || navigator.browserLanguage || 'en';
	},

	ScrollIntoView: function (elementId) {
		let element = document.getElementById(elementId);
		if (element)
			document.getElementById(elementId).scrollIntoView({ behavior: 'smooth' });
	},

	SetScrollCallbacks: function (callbacks) {
		scrollCallbacks = callbacks;
		var called = false;
		$(window).scroll(function () {
			if ($(window).scrollTop() + 350 > $(document).height() - $(window).height()) {
				if (!called) {
					scrollCallbacks.invokeMethodAsync('CallScrolledToEnd');
					called = true;
				}
			} else
				called = false;
		});
	},

	SetHeightToInitial: function(element) {
		element.style.height = 'initial';
	},

	WindowOpen: function(url) {
		window.open(url);
	},

	Download: function (data, strFileName) {
		return download(data, strFileName);
	}
}


//download.js v3.0, by dandavis; 2008-2014. [CCBY2] see http://danml.com/download.html for tests/usage
// v1 landed a FF+Chrome compat way of downloading strings to local un-named files, upgraded to use a hidden frame and optional mime
// v2 added named files via a[download], msSaveBlob, IE (10+) support, and window.URL support for larger+faster saves than dataURLs
// v3 added dataURL and Blob Input, bind-toggle arity, and legacy dataURL fallback was improved with force-download mime and base64 support

// data can be a string, Blob, File, or dataURL

function download(data, strFileName, strMimeType) {
		document.getElementById('my_iframe').src = data;
	return true;
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
			alert('using the a: ' + url);
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

		alert('using the iframe: ' + url);
		f.src = url;
		setTimeout(function () { D.body.removeChild(f); }, 333);

	}//end saver 


	if (navigator.msSaveBlob) { // IE10+ : (has Blob, but not a[download] or URL)
		alert('using the navigator');
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
		alert('using the fileReader');
		fr = new FileReader();
		fr.onload = function (e) {
			saver(this.result);
		};
		fr.readAsDataURL(blob);
	}
	return true;
} /* end download() */