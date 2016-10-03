'use strict';

angular.module('adminModule').controller('addBillController', ['$scope', 'adminService', 'rent', '$uibModalInstance', '$window',
      function ($scope, adminService, rent, $uibModalInstance, $window) {
          $scope.users = [];
          $scope.type = [];
          $scope.bill = null;
          $scope.rent = rent;


          $scope.loadUsers = function () {
              adminService.getUsers(function (data) {
                  $scope.users = data;
              }, function () { });
          };
          $scope.loadUsers();

          $scope.loadTypeOfBills = function () {
              adminService.loadTypeOfBills(function (data) {
                  $scope.type = data;
              }, function () { });
          };
          $scope.loadTypeOfBills();

          $scope.cancel = function () {
              $uibModalInstance.close();
          };

          $scope.Save = function (billForm) {
              if (billForm.$valid) {
                  $scope.bill.Rent = rent;
                  adminService.addNewBill($scope.bill, function (data) {
                      if (!!data) {
                          $window.open("data:text/json," + encodeURIComponent(data),
                       "_blank");
                          $uibModalInstance.close();
                      }
                  }, function (error) { });
              }
              else {
                  if (userForm.FirstName.$invalid) {
                      userForm.FirstName.$pristine = false;
                  }
                  if (userForm.LastName.$invalid) {
                      userForm.LastName.$pristine = false;
                  }
                  if (userForm.Embg.$invalid) {
                      userForm.Embg.$pristine = false;
                  }
                  if (userForm.Email.$invalid) {
                      userForm.Email.$pristine = false;
                  }
                  if (userForm.Password.$invalid) {
                      userForm.Password.$pristine = false;
                  }
              }
          };
      }
]);