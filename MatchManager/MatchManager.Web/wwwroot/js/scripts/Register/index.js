var RegisterHelper = function () {
    this.form = document.getElementById("register_form");
    this.registerObj = {};
    this.apiUrl = localStorage.getItem("api_url");
}

RegisterHelper.prototype = {
    _registerEventListener: function () {
        if (this.form.attachEvent) {
            this.form.attachEvent("submit", registerHelper._processForm);
        } else {
            this.form.addEventListener("submit", registerHelper._processForm);
        }
    },
    _processForm: function (e) {
        if (e.preventDefault) e.preventDefault();
        registerHelper.registerObj = {
            firstName: document.getElementById("user_first_name").value,
            lastName: document.getElementById("user_last_name").value,
            email: document.getElementById("user_email").value,
            password: document.getElementById("user_password").value,
            confirmpassword: document.getElementById("user_confirm_password").value,
        }
        if (registerHelper._validateForm(registerHelper.registerObj)) {
            registerHelper._submitForm();
        }
        return false;
    },
    _validateForm: function () {
        let obj = registerHelper.registerObj;
        let rtn = true;

        let errFirstName = document.getElementById("err_user_first_name");
        let errLastName = document.getElementById("err_user_last_name");
        let errEmail = document.getElementById("err_user_email");
        let errPassword = document.getElementById("err_user_password");
        let errConfirmPassword = document.getElementById("err_user_confirm_password");

        if (utils.IsNullOrEmpty(obj.firstName)) {
            _displayError(errFirstName, true, "Enter First Name");
        } else if (!/^[a-zA-Z ]*$/.test(obj.firstName)) {
            _displayError(errFirstName, true, "First Name must contain only alphabets");
        } else {
            _displayError(errFirstName, false);
        }

        if (utils.IsNullOrEmpty(obj.lastName)) {
            _displayError(errLastName, true, "Enter Last Name");
        } else if (!/^[a-zA-Z ]*$/.test(obj.lastName)) {
            _displayError(errLastName, true, "Last Name must contain only alphabets");
        } else {
            _displayError(errLastName, false);
        }

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

        if (utils.IsNullOrEmpty(obj.confirmpassword)) {
            _displayError(errConfirmPassword, true, "Enter Confirm Password");
        } else if (String(obj.password) !== String(obj.confirmpassword)) {
            _displayError(errConfirmPassword, true, "Confirm Password is not equal to entered password");
        } else {
            _displayError(errConfirmPassword, false);
        }

        function _displayError (element, show, message = null) {
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
        const response = fetch(this.apiUrl + "account/register", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json",
            },
            body: JSON.stringify(registerHelper.registerObj),
        });
        const result = await response;
        let data = await result.json();
        if (data.isSuccess) {
            document.getElementById("successMessage").classList.remove("hidden");
            document.getElementById("successMessage").firstElementChild.nextElementSibling.innerHTML = data.result;
            registerHelper._clearFields();
        } else {
            document.getElementById("errorMessage").classList.remove("hidden");
            document.getElementById("errorMessage").firstElementChild.nextElementSibling.innerHTML = data.errorMessages[0];
        }
        return false;
    },
    _clearFields: function () {
        document.getElementById("user_first_name").value = "";
        document.getElementById("user_last_name").value = "";
        document.getElementById("user_email").value = "";
        document.getElementById("user_password").value = "";
        document.getElementById("user_confirm_password").value = "";
    }
}
var registerHelper = new RegisterHelper();