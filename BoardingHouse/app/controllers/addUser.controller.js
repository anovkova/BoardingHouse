/// <reference path="../templetes/admin/addContact.html" />
'use strict';

angular.module('adminModule').controller('addUserController',
 [
     '$scope',
    'adminService',
     'user',
      '$uibModalInstance',
      function ($scope, adminService, user, $uibModalInstance) {
          $scope.user = user;
          $scope.flag = false;
          $scope.Save = function (userForm) {
            
              if (userForm.$valid) {
                  adminService.addNewUser($scope.user, function (data) {
                      if (!!data) {
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
                  if (userForm.PhoneNumber.$invalid) {
                      userForm.PhoneNumber.$pristine = false;
                  }
                  if (userForm.Email.$invalid) {
                      userForm.Email.$pristine = false;
                  }
                  if (userForm.Password.$invalid) {
                      userForm.Password.$pristine = false;
                  }
              }
          }

          $scope.cancel = function () {
              $uibModalInstance.close();
          };
      }
 ]);