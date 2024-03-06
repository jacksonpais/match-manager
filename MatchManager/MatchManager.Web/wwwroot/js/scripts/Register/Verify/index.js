var RegisterVerificationHelper = function () {
    debugger
    this.apiUrl = localStorage.getItem("api_url");
}

RegisterVerificationHelper.prototype = {
    _registerEventListener: function () {
        debugger
        setTimeout(registerVerificationHelper._verifyAccount, 5000);
    },
    _verifyAccount: function () {
        debugger
        const urlParams = new URLSearchParams(window.location.search);
        const response = fetch(this.apiUrl + "account/registration/verify" + urlParams, {
            method: "GET",
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json",
            },
        });
        const result = await response;
        let data = await result.json();
        if (data.isSuccess) {
            window.location = "/account-verification/completed";
        } else {
            document.getElementById("errorMessage").classList.remove("hidden");
            document.getElementById("errorMessage").firstElementChild.nextElementSibling.innerHTML = data.errorMessages[0];
        }
    }
}
var registerVerificationHelper = new RegisterVerificationHelper();