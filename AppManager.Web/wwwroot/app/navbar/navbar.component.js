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