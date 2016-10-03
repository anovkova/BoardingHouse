
'use strict';

angular.module('adminModule').controller('reservationController', ['$scope', 'adminService', '$uibModal',
        function ($scope, adminService, $uibModal) {
            $scope.Rents = [];

            $scope.getAllRents = function () {
                adminService.getAllRents(function (data) {
                    $scope.Rents = data;
                }, function () { });
            };

            $scope.getAllRents();

            $scope.deleteRent = function (rent) {
                adminService.deleteRent(rent.Id, function (data) {
                    if (!!data) {
                        $scope.getAllRents();
                    }
                }, function (error) { });
            };

            $scope.addNewBill = function (rent) {
                var modalInstance = $uibModal.open({
                    templateUrl: '/app/templetes/admin/addBill.html',
                    backdrop: 'static',
                    windowClass: 'modal',
                    controller: 'addBillController',
                    resolve: {
                        rent: function () {
                            return rent;
                        }
                    }
                });
                modalInstance.result
                  .then(function () {
                      $scope.getAllRents();
                  }, function (reason) {
                      alert(reason);
                  });
            };
        }
]);