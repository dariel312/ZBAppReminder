(function () {
    /*
     * Deprecated Directive - Were now using a component instead of a directive
     */
    const PaginatorDirective = function (paginator) {
        return {
            restrict: 'AE',
            template: `
                <ul class="pagination">
                    <li class="page-item" ng-class="{disabled: ngModel.Number == 0 || ngModel.Count < 1}" ng-click="clickPrev()">
                        <a class="page-link" aria-label="Previous">
                            <i class="material-icons">keyboard_arrow_left</i>
                        </a>
                    </li>
                    <li ng-repeat="n in list" ng-class="{active: (ngModel.Number == n)}" ng-click="onClick(n)">
                        <a>{{n + 1}}</a>
                    </li>
                    <li class="page-item" ng-class="{disabled: ngModel.Number == ngModel.Count - 1 || ngModel.Count < 1}" ng-click="clickNext()">
                        <a class="page-link" aria-label="Next">
                            <i class="material-icons">keyboard_arrow_right</i>
                        </a>
                    </li>
                </ul>
            `,
            transclude: false,
            require: 'ngModel',
            scope: {
                ngModel: '=',
                onPage: '&',
                showPages: '<',
                array: '='
            },
            link: function ($scope, $elem, $attr, ngModel) {

                if ($scope.ngModel === null || $scope.ngModel === undefined) {
                    $scope.ngModel = new paginator();
                }

                ngModel.$validators['page'] = function renderList(modelValue, viewValue) {
                    if ($scope.showPages === false)
                        return;

                    if ($scope.array !== null && $scope.array !== undefined) {
                        $scope.ngModel.GetCount($scope.array);
                        console.log("WE HIT IT");
                    }

                    var pages = $scope.ngModel;
                    $scope.list = [];
                    for (var i = 0; i < pages.Count; i++) {
                        if (i < 3)
                            $scope.list.push(i);
                        else if (i < pages.Number + 2 && i > pages.Number - 2)
                            $scope.list.push(i);
                        else if (i > pages.Count - 4)
                            $scope.list.push(i);

                    }

                    return true;
                };
                $scope.onClick = function (n) {
                    $scope.ngModel.Number = n;
                    $scope.onPage();

                };
                $scope.clickPrev = function () {
                    if ($scope.ngModel.Number !== 0) {
                        $scope.ngModel.Number--;
                        $scope.onPage();
                    }
                };
                $scope.clickNext = function () {
                    if ($scope.ngModel.Number !== $scope.ngModel.Count - 1) {
                        $scope.ngModel.Number++;
                        $scope.onPage();
                    }
                };
            }
        };
    };
    const PaginatorComponent = {
        template: `
                <div class="paginator-inner">
                   <div class="paginator-item" ng-class="{disabled: $ctrl.paginator.Number == 0 || $ctrl.paginator.Count < 1}" ng-click="$ctrl.clickPrev()">
                        <i class="material-icons">keyboard_arrow_left</i>
                    </div>
                    <div class="paginator-item" ng-repeat="n in $ctrl.list track by $index" 
                                                ng-class="{active: $ctrl.paginator.Number == n, disabled: n == '-'}" 
                                                ng-click="$ctrl.onClick(n)">
                        <span ng-if="n !== '-'">{{n + 1}}</span>
                        <span ng-if="n === '-'">{{n}}</span>
                    </div>
                    <div class="paginator-item" ng-class="{disabled: $ctrl.paginator.Number == $ctrl.paginator.Count - 1 || $ctrl.paginator.Count < 1}" ng-click="$ctrl.clickNext()">
                        <i class="material-icons">keyboard_arrow_right</i>
                    </div>
                </div>
            `,
        bindings: {
            paginator: '=', //paginator
            array: '<', //array being paged - used to get the page Count
            onPage: '&', //event callback
            showPages: '<', //setting
        },
        controller: function (paginator) {
            let $ctrl = this;
            $ctrl.list = [];
            $ctrl.array = [];
            $ctrl.paginator = null;
            $ctrl.showPages = true;


            $ctrl.$onChanges = function () {
                renderList();
            };


            $ctrl.onClick = function (n) {
                if (!isNaN(n)) {
                    $ctrl.paginator.Number = n;
                    $ctrl.onPage(); //event call back
                    renderList();
                }

            };
            $ctrl.clickPrev = function () {
                if ($ctrl.paginator.Number !== 0) {
                    $ctrl.paginator.Number--;
                    $ctrl.onPage();
                    renderList();
                }
            };
            $ctrl.clickNext = function () {
                if ($ctrl.paginator.Number !== $ctrl.paginator.Count - 1) {
                    $ctrl.paginator.Number++;
                    $ctrl.onPage();
                    renderList();
                }
            };

            function renderList() {
                $ctrl.list = [];

                //dont continue if show pages is false
                if ($ctrl.showPages === false)
                    return;

                //model needs to be set
                if ($ctrl.paginator === null && $ctrl.paginator !== undefined)
                    return;

                //update count
                $ctrl.paginator.GetCount($ctrl.array);

                var pages = $ctrl.paginator;
                for (var p = 0; p < pages.Count; p++) {
                    if (p < 1 && pages.Number > 1) {
                        $ctrl.list.push(p);

                        //dont show - if list is small
                        if (pages.Count > 3)
                            $ctrl.list.push('-');
                    }
                    else if (p < pages.Number + 2 && p > pages.Number - 2) {
                        $ctrl.list.push(p);
                    }
                    else if (p > pages.Count - 2) {

                        if (pages.Number < pages.Count - 1);
                            $ctrl.list.push('-');
                        $ctrl.list.push(p);
                    }

                }

                return true;
            }

        }
    };
    const PaginatorFactory = function () {
        function Paginator() {
            let self = this;
            this.Count = 0;
            this.Size = 12; //Default 12
            this.Number = 0;

            this.GetCount = function (array) {
                self.Count = Math.ceil(array.length / self.Size);
                return self.Count;
            };
            this.CreateUpdatedPaginator = function (newLength) {
                var pager = new Paginator();
                pager.Size = self.Size;
                pager.Count = Math.ceil(newLength / self.Size);
                pager.Number = self.Number;

                if (self.Count < 1) {
                    //Reset if last pager had no data
                    pager.Number = 0;
                }
                return pager;
            };
        }

        return Paginator;
    };

    /*
        This filter allows you to filter a data set in ngRepeat. 
        The only parameter (use the factory to create the object) required is the paginator object associated with the paginator directive.
    */
    function PaginatorFilter() {
        return function (input, pager) {
            var a = pager.Size * pager.Number;
            var b = pager.Size * (pager.Number + 1);
            return input.slice(a, b);
        };
    }

    var app = angular.module('tlmd.paginator', ['ngAnimate']);
    app.factory('paginator', PaginatorFactory);
    //app.directive('paginator', PaginatorDirective);
    app.component('paginator', PaginatorComponent);
    app.filter('paginator', PaginatorFilter);

})();