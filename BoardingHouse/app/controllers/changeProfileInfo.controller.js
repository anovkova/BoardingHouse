/// <reference path="../templetes/admin/addContact.html" />
'use strict';

angular.module('userModule').controller('changeInfoController', ['$scope', 'userService', 'user', '$uibModalInstance',
      function ($scope, userService, user, $uibModalInstance) {
          $scope.user = user;
          $scope.flag = true;

          $scope.Save = function (userForm) {
              if (userForm.$valid) {
                  userService.addNewUser($scope.user, function (data) {
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