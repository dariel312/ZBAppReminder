/*
    Service that will be a wrapper for all api calls
*/
const ApiService = function ($http, $window, $rootScope, $httpParamSerializer, $state, $q, $timeout) {
    const self = this;
    const token_key = "auth_token";
    var auth_info = null;
    var identity = null;

    //load stored data
    auth_info = ($window.localStorage.getItem(token_key));

    //Helper function for get api requests
    function _get(url, parameters) {
        if (parameters === null || parameters === undefined)
            parameters = {};

        return $http.get(url, { params: parameters });
    };


    function _put(url, data) {
        if (data === null || data === undefined)
            data = {};

        return $http.put(url, data, {});
    };
    function _delete(url, parameters) {
        if (parameters === null || parameters === undefined)
            parameters = {};

        return $http.delete(url, { params: parameters });
    }
    function _post(url, data) {
        if (data === null || data === undefined)
            data = {};

        return $http.post(url, data, {});
    };
    function _postFile(url, formData) {
        return $http({
            method: 'post',
            url: url,
            data: formData,
            headers: { 'Content-Type': undefined }
        });
    }



    function onAuthChanged() {
        $timeout(function () {
            $rootScope.$broadcast('auth-changed');
            console.log("AUTH CHANGE BOOM");
        });
    }
    function getToken() {
        return $window.localStorage.getItem(token_key);
    };
    function saveToken(token) {
        $window.localStorage.setItem(token_key, token);
    };
    function deleteToken() {
        $window.localStorage.removeItem(token_key);
    }



    this.isLoggedIn = function () {
        return getToken() != undefined;
    }

    this.getLoginToken = function () {
        return getToken();
    }

    this.logOff = function () {
        if (self.isLoggedIn) {
            $window.localStorage.removeItem(token_key);
            auth_info = null;
            onAuthChanged();
            $state.go("login");
        }
    };

    //Login
    this.logIn = function (email, password) {
        return $http.post("api/login", {
            email: email,
            password: password
        }).then(function (response) {
            if (response.data != null) {
                saveToken(response.data);
                onAuthChanged();
            }
            return response;
        });
    }
    this.postRegister = function (login) {
        return _post("/api/login/register", login);
    }


    //apointment
    this.getAppointments = function (start, end, employeeId) {
        return _get("/api/appointment", {
            start: start,
            end: end,
            employeeId: employeeId
        });
    }

    this.getEmployees = function () {
        return _get("/api/appointment/employee");
    };
};