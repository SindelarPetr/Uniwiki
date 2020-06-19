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
	}
}
