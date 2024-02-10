var LoginHelper = function () {
    this.form = document.getElementById("login_form");
    this.loginObj = {};
}

LoginHelper.prototype = {
    _registerEventListener: function () {
        if (this.form.attachEvent) {
            form.attachEvent("submit", loginHelper._processForm);
        } else {
            this.form.addEventListener("submit", loginHelper._processForm);
        }
    },
    _processForm: function (e) {
        if (e.preventDefault) e.preventDefault();
        loginHelper.loginObj = {
            email: document.getElementById("user_email"),
            password: document.getElementById("user_password")
        }
        if (loginHelper._validateForm(loginHelper.loginObj)) {
            loginHelper._submitForm();
        }
        return false;
    },
    _validateForm: function () {
        let obj = loginHelper.loginObj;
        let rtn = true;

        let errEmail = document.getElementById("err_user_email");
        let errPassword = document.getElementById("err_user_password");

        if (utils.IsNullOrEmpty(obj.email.value)) {
            _displayError(errEmail, true, "Enter Email Id");
        } else if (!utils.IsEmailValid(obj.email.value)) {
            _displayError(errEmail, true, "Enter a valid email");
        } else {
            _displayError(errEmail, false);
        }

        if (utils.IsNullOrEmpty(obj.password.value)) {
            _displayError(errPassword, true, "Enter Password");
        } else if (!/^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$/.test(obj.password.value)) {
            _displayError(errPassword, true, "Password must be at least 8 characters long and contain at least 1 letter and 1 number");
        } else {
            _displayError(errPassword, false);
        }

        function _displayError(element, show, message = null) {
            if (show) {
                element.classList.remove("hidden")
                element.innerHTML = message;
                rtn = false;
            } else {
                element.classList.add("hidden")
                element.innerHTML = "";
                rtn = true;
            }
        }
        return rtn;
    },
    _submitForm: async function () {
        const response = fetch(api_url + "account/login", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(loginHelper.loginObj),
        });
        const result = await response.json();
        console.log("Success:", result);
    }
}
var loginHelper = new LoginHelper();