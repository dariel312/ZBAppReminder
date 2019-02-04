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