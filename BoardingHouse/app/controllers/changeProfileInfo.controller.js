/// <reference path="../templetes/admin/addContact.html" />
'use strict';

angular.module('userModule').controller('changeInfoController',
 [
     '$scope',
    'userService',
     'user',
      '$uibModalInstance',
      function ($scope, userService, user, $uibModalInstance) {

          //$scope.FirstName = user.FirstName;
          //$scope.LastName = user.LastName;
          //$scope.PhoneNumber = user.PhoneNumber;
          //$scope.Embg = user.Embg;
          //$scope.Email = user.Email;
          //$scope.Password = user.Password;
          //$scope.Role = user.Role;
          //$scope.flag = user.flag;
          //$scope.Id = user.Id;
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

          //$scope.Update = function (userFrom) {
          //    if (userFrom.$valid) {
          //        //var dataForSend = {
          //        //    Id: $scope.Id,
          //        //    firstName: $scope.FirstName,
          //        //    lastName: $scope.LastName,
          //        //    phoneNumber: $scope.PhoneNumber,
          //        //    embg: $scope.Embg,
          //        //    email: $scope.Email,
          //        //    password: $scope.Password,
          //        //    role: $scope.Role
          //        //}
          //        adminService.UpdateUser($scope.user, function (data) {
          //            if (!!data) {
          //                $uibModalInstance.close();
          //            }
          //        }, function (error) { });
          //    }
          //}
      }
 ]);