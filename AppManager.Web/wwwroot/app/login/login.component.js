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