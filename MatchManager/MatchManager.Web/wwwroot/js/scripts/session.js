var Session = function () {
}

Session.prototype = {
    SetToken: function (value) {
        if (!utils.IsUndefinedOrNullOrEmpty(value)) {
            sessionStorage.setItem("token", value);
        }
    },
    GetToken: function () {
        return sessionStorage.getItem("token");
    },
    IsTokenValid: function () {
        return !utils.IsUndefinedOrNullOrEmpty(session.GetToken());
    }
}
var session = new Session();