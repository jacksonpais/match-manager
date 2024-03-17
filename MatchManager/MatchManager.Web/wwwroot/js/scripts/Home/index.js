var HomeHelper = function () {
    this.apiUrl = localStorage.getItem("api_url");
}

HomeHelper.prototype = {
    _registerEventListener: function () {
        if (!session.IsTokenValid()) {
            window.location = "/login";
        }
    }
}
var homeHelper = new HomeHelper();