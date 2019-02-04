var ApiInterceptor = function ($q, $window, $injector) {
    return {
        /*Must use $injector otherwise you'll get cirucular dependency*/
        'request': function (config) {
            var api = $injector.get('api');
            var url = config.url.split('/');

            if (api.isLoggedIn() && (url[1] == 'api' || url[0] == 'api')) {
                config.headers.Authorization = "Bearer " + api.getLoginToken();
            }

            return config;
        },

        //Returns to login if unathorized
        'responseError': function (rejection) {
            if (rejection.status === 401) {
                var api = $injector.get('api');
                api.logOff();
            }
            else return $q.reject(rejection);
        }
    };
}
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
const DateFormatFilter = function ($filter) {
    return function (date, format, timezone) {

        if (timezone === null || timezone === undefined)
            timezone = 'UTC';

        return $filter('date')(date, format, timezone);
    };
}
const HomeComponent = {
    templateUrl: "/app/home/home.component.html",
    controller: function () {

    }
};
const AdminRegisterComponent = {
    templateUrl: "/app/admin/admin-register.component.html",
    controller: function ($state, api) {
        let $ctrl = this;
        $ctrl.login = null;


        $ctrl.submit = function () {
            if ($ctrl.form.$valid) {
                $ctrl.form.$submitted = true;

                api.postRegister($ctrl.login).then(
                    function (result) {
                        alert("User has been added");

                        $ctrl.login = null;
                    },
                    function (result) {
                        alert("Error adding user. Make sure user doesnt exist and passwords match");
                    }
                );
            }
        };
    }
};
const AdminComponent = {
    templateUrl: "/app/admin/admin.component.html",
    controller: function ($rootScope, $state) {
        let $ctrl = this;

    }
};
const AppointmentComponent = {
    templateUrl: "/app/appointment/appointment.component.html",
    controller: function (api) {
        let $ctrl = this;
        $ctrl.date = moment().startOf('day').toDate();
        $ctrl.employee = null;
        $ctrl.appointments = [];
        $ctrl.employees = [];

        $ctrl.$onInit = function () {
            getAppointments();

            api.getEmployees().then(function (response) {
                $ctrl.employees = response.data;
                $ctrl.employee = $ctrl.employees[0].empId;

                getAppointments();
            });
        };

        $ctrl.submit = function () {
            getAppointments();
        };

        function getAppointments() {
            var start = moment($ctrl.date).startOf('day').toDate();
            var end = moment($ctrl.date).endOf('day').toDate();

            api.getAppointments(start, end, $ctrl.employee).then(function (response) {
                $ctrl.appointments = response.data;
            });
        }
    }
};
const LoginComponent = {
    templateUrl: "/app/login/login.component.html",
    controller: function ($state, api) {
        let $ctrl = this;
        $ctrl.year = moment().year();
        $ctrl.model = {};


        $ctrl.onSubmit = function () {
            api.logIn($ctrl.model.email, $ctrl.model.password).then(function (response) {
                if (response.data.success == false) {
                    $ctrl.error = true;
                }
                else {
                    $state.go("appointment");
                }
            }, function (response) {
                //error 
                debugger;
            });
        }
    }
};
const NavbarComponent = {
    templateUrl: "/app/navbar/navbar.component.html",
    controller: function ($rootScope, $state, $transitions, api) {
        var $ctrl = this;
        $ctrl.logged_in = api.isLoggedIn();
        $ctrl.user = null;

        $ctrl.$onInit = function () {
            $rootScope.$on('auth-changed', function (event, args) {
                $ctrl.logged_in = api.isLoggedIn();
            });

            $transitions.onSuccess({}, function (transition) {
                var to = transition.to();
                var params = transition.params();

                //Do something on UI Transition
            });

         

            //api.getMe().then(function (user) {
            //    $ctrl.user = user;
            //});

        }

        $ctrl.onLogin = function () {
            $state.go('login');
        };

        $ctrl.onLogout = function () {
            api.logOff();
        };

        $ctrl.submit = function () {
            $state.go("subreddit", { name: $ctrl.subredditName })
            $ctrl.subredditName = null;
        }

       
    }
};
(function () {
    'use strict';
    var app = angular.module('app', ['ngAnimate', 'ngMessages', 'ngSanitize', 'ui.router', 'angularMoment', 'tlmd.paginator']);

    //Declare all angular components/services/factories/filters here
    app.service('api', ApiService);
    app.filter('dateformat', DateFormatFilter);
    app.component('appNavbar', NavbarComponent);
    app.component('appAdmin', AdminComponent);
    app.component('appAdminRegister', AdminRegisterComponent);
    app.component('appAppointment', AppointmentComponent);
    app.component('appHome', HomeComponent);
    app.component('appLogin', LoginComponent);

    //Configure angular here
    app.config(function ($locationProvider, $urlRouterProvider, $stateProvider, $httpProvider, $rootScopeProvider) {
        $locationProvider.html5Mode(true);
        $urlRouterProvider.otherwise('/appointments');

        $stateProvider
            //.state("home", {
            //    url: "/",
            //    component: 'appHome'
            //})
            .state('login', {
                url: "/login",
                component: 'appLogin'
            })
            .state('appointment', {
                url: "/appointments",
                component: 'appAppointment'
            })
            .state("admin-register", {
                url: "/admin/register",
                component: 'appAdminRegister'
            });

        //For api auth
        $httpProvider.interceptors.push(ApiInterceptor);

        //Makes $http parse string dates to JS Dates from JSON
        $httpProvider.defaults.transformResponse.push(function (responseData) {
            convertDateStringsToDates(responseData);
            return responseData;
        });

        $httpProvider.defaults.transformResponse.push(function (responseData) {
            convertDateStringsToDates(responseData);
            return responseData;
        });

    });


    //fixes data bindings coming from ASPNET Dates
    function convertDateStringsToDates(input) {
        // Ignore things that aren't objects.
        if (typeof input !== "object")
            return input;

        for (var key in input) {
            if (!input.hasOwnProperty(key)) continue;

            var value = input[key];
            // Check for string properties which look like dates.
            var date = moment.utc(value, moment.ISO_8601);
            if (typeof value === "string" && date.isValid()) {
                input[key] = date.toDate();
            } else if (typeof value === "object") {
                // Recurse into object
                convertDateStringsToDates(value);
            }
        }
    }


})();