var LoginHelper = function () {
    this.form = document.getElementById("login_form");
    this.loginObj = {};
    this.apiUrl = localStorage.getItem("api_url");
}

LoginHelper.prototype = {
    _registerEventListener: function () {
        if (session.IsTokenValid()) {
            window.location = "/";
        }
        if (this.form.attachEvent) {
            form.attachEvent("submit", loginHelper._processForm);
        } else {
            this.form.addEventListener("submit", loginHelper._processForm);
        }
    },
    _processForm: function (e) {
        if (e.preventDefault) e.preventDefault();
        loginHelper.loginObj = {
            email: document.getElementById("user_email").value,
            password: document.getElementById("user_password").value
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

        if (utils.IsNullOrEmpty(obj.email)) {
            _displayError(errEmail, true, "Enter Email Id");
        } else if (!utils.IsEmailValid(obj.email)) {
            _displayError(errEmail, true, "Enter a valid email");
        } else {
            _displayError(errEmail, false);
        }

        if (utils.IsNullOrEmpty(obj.password)) {
            _displayError(errPassword, true, "Enter Password");
        } else if (!/^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$/.test(obj.password)) {
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
        document.getElementById("successMessage").classList.add("hidden");
        document.getElementById("errorMessage").classList.add("hidden");
        const response = fetch(this.apiUrl + "account/login", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json",
            },
            body: JSON.stringify(loginHelper.loginObj),
        });
        const result = await response;
        let data = await result.json();
        if (data.isSuccess) {
            sessionStorage.setItem("token", data.result.accessToken);
            window.location = "/";
        } else {
            document.getElementById("errorMessage").classList.remove("hidden");
            document.getElementById("errorMessage").innerHTML = data.errorMessages[0];
        }
    },
    _clearFields: function () {
        document.getElementById("user_email").value = "";
        document.getElementById("user_password").value = "";
    }
}
var loginHelper = new LoginHelper();