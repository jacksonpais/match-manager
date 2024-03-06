var HomeHelper = function () {
    this.apiUrl = localStorage.getItem("api_url");
}

HomeHelper.prototype = {
    _registerEventListener: function () {
    }
}
var homeHelper = new HomeHelper();