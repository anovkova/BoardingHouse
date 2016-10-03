'use strict';

angular.module('adminModule').controller('billsController', ['$scope', 'adminService', '$uibModal',
      function ($scope, adminService, $uibModal) {

          $scope.Bills = null;

          $scope.getAllBills = function () {
              adminService.getAllBills(function (dataFromService) {
                  $scope.Bills = dataFromService;
              }, function (error) {
              })
          };

          $scope.getAllBills();

          $scope.updateStatus = function (bill) {
              var modalInstance = $uibModal.open({
                  templateUrl: '/app/templetes/admin/updateStatus.html',
                  backdrop: 'static',
                  windowClass: 'modal',
                  controller: 'updateStatusController',
                  resolve: {
                      bill : function() {
                          return bill;
                      }
                  }
              });
              modalInstance.result
                .then(function () {
                    $scope.getAllBills();
                }, function (reason) {
                    alert(reason);
                });
          };
      }
]);