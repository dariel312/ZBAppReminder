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