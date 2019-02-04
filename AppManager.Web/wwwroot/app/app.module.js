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
