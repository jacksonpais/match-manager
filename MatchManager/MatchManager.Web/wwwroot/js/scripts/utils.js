var Utils = function () {
}

Utils.prototype = {
    IsNullOrEmpty: function (value) {
        let rtn = true;
        if (value !== null) {
            if (value.trim().length !== 0) {
                rtn = false;
            }
        }
        return rtn;
    },
    IsUndefinedOrNullOrEmpty: function (value) {
        let rtn = true;
        if (value !== undefined) {
            if (value !== null) {
                if (value.trim().length !== 0) {
                    rtn = false;
                }
            }
        }
        return rtn;
    },
    IsEmailValid: function(email) {
        return String(email)
            .toLowerCase()
            .match(
                /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|.(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
            );
    }
}
var utils = new Utils();