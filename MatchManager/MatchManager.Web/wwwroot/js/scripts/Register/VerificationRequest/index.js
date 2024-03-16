var VerificationRequestHelper = function () {
    this.form = document.getElementById("verification_form");
    this.verificationObj = {};
    this.apiUrl = localStorage.getItem("api_url");
}

VerificationRequestHelper.prototype = {
    _registerEventListener: function () {
        if (this.form.attachEvent) {
            this.form.attachEvent("submit", verificationRequestHelper._processForm);
        } else {
            this.form.addEventListener("submit", verificationRequestHelper._processForm);
        }
    },
    _processForm: function (e) {
        if (e.preventDefault) e.preventDefault();
        verificationRequestHelper.verificationObj = {
            username: document.getElementById("user_email").value,
            verificationtype: "email",
            "communicationType": "email"
        }
        if (verificationRequestHelper._validateForm(verificationRequestHelper.verificationObj)) {
            verificationRequestHelper._submitForm();
        }
        return false;
    },
    _validateForm: function () {
        let obj = verificationRequestHelper.verificationObj;
        let rtn = true;

        let errEmail = document.getElementById("err_user_email");

        if (utils.IsNullOrEmpty(obj.username)) {
            _displayError(errEmail, true, "Enter Email Id");
        } else if (!utils.IsEmailValid(obj.username)) {
            _displayError(errEmail, true, "Enter a valid email");
        } else {
            _displayError(errEmail, false);
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
        const response = fetch(this.apiUrl + "account/register/request-verification", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json",
            },
            body: JSON.stringify(verificationRequestHelper.verificationObj),
        });
        const result = await response;
        let data = await result.json();
        if (data.isSuccess) {
            document.getElementById("successMessage").classList.remove("hidden");
            document.getElementById("successMessage").firstElementChild.nextElementSibling.innerHTML = data.result;
            verificationRequestHelper._clearFields();
        } else {
            document.getElementById("errorMessage").classList.remove("hidden");
            document.getElementById("errorMessage").firstElementChild.nextElementSibling.innerHTML = data.errorMessages[0];
        }
        return false;
    },
    _clearFields: function () {
        document.getElementById("user_email").value = "";
    }
}
var verificationRequestHelper = new VerificationRequestHelper();