
'use strict';

angular.module('adminModule').controller('reservationController', ['$scope', 'adminService',
        function ($scope, adminService) {
            $scope.Rents = [];

            $scope.getAllRents = function () {
                adminService.getAllRents(function (data) {
                    debugger;
                    $scope.Rents = data;
                }, function () { });
            }

            $scope.getAllRents();

            $scope.updateRent = function (data) {
                debugger;
            }
        }
]);