var Session = function () {
}

Session.prototype = {
    SetToken: function (value) {
        if (!utils.IsUndefinedOrNullOrEmpty(value)) {
            localStorage.setItem("token", value);
        }
    },
    GetToken: function () {
        return localStorage.getItem("token");
    },
    IsTokenValid: function () {
        return !utils.IsUndefinedOrNullOrEmpty(session.GetToken());
    }
}
var session = new Session();