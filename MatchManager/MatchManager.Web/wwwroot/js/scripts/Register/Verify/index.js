var RegisterVerificationHelper = function () {
    this.btnVerify = document.getElementById("btnVerify");
    this.errorMessage = document.getElementById("errorMessage");  
    this.successMessage = document.getElementById("successMessage");  
    this.resendVericationContainer = document.getElementById("resendVericationContainer");  
    this.apiUrl = localStorage.getItem("api_url");
}

RegisterVerificationHelper.prototype = {
    _registerEventListener: function () {
        this.errorMessage.classList.add("hidden");
        this.successMessage.classList.add("hidden");
        this.btnVerify.addEventListener("click", registerVerificationHelper._verifyAccount)
    },
    _verifyAccount: async function () {
        let data = {};
        try {
            const urlParams = new URLSearchParams(window.location.search);
            const response = fetch(registerVerificationHelper.apiUrl + "account/registration/verify?" + urlParams, {
                method: "GET",
                headers: {
                    "Content-Type": "application/json",
                    "Accept": "application/json",
                },
            });
            const result = await response;
            data = await result.json();
        } catch (e) {
            data.isSuccess = false;
            data.errorMessages = ["Error occurred on the server"];
        }
        if (data.isSuccess) {
            registerVerificationHelper.successMessage.classList.remove("hidden");
            registerVerificationHelper.successMessage.firstElementChild.nextElementSibling.innerHTML = data.errorMessages[0];
        } else {
            registerVerificationHelper.errorMessage.classList.remove("hidden");
            registerVerificationHelper.errorMessage.firstElementChild.nextElementSibling.innerHTML = data.errorMessages[0];
            registerVerificationHelper.btnVerify.innerText = "Retry"
            registerVerificationHelper.resendVericationContainer.classList.remove("hidden");
        }
    }
}
var registerVerificationHelper = new RegisterVerificationHelper();