
'use strict';

angular.module('adminModule').controller('reservationController',
 [
     '$scope',
     'adminService',
        function ($scope, adminService) {
            $scope.rents = null;
            $scope.current = new Date().toJSON();

            $scope.getAllRents = function () {
                adminService.getAllRents(function (data) {
                    $scope.rents = data;
                }, function () { });
            }

            $scope.getAllRents();

            $scope.UpdateRent = function (rent) {
                var modalInstance = $uibModal.open({
                    templateUrl: '/app/templetes/admin/updateRent.html',
                    backdrop: 'static',
                    windowClass: 'modal',
                    scope: $scope,
                    controller: 'reservationController',
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