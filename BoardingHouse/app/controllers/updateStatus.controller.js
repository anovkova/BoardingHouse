'use strict';

angular.module('adminModule').controller('updateStatusController', ['$scope', 'adminService', 'bill', '$uibModalInstance', '$window',
      function ($scope, adminService, bill, $uibModalInstance, $window) {
          $scope.bill = bill;
       

          $scope.cancel = function () {
              $uibModalInstance.close();
          };

          $scope.Save = function () {
              adminService.updateStatus($scope.bill, function (data) {
                  if (!!data) {
                      $window.open("data:text/json," + encodeURIComponent(data),
                   "_blank");
                      $uibModalInstance.close();
                  }
              }, function (error) { });
          };
      }
]);