'use strict';

angular.module('userModule').controller('userProfileController',
 [
     '$scope',
     'userService',
           '$uibModal',
           'Upload',
      function ($scope, userService, $uibModal, upload) {
          $scope.userFullName = null;
          $scope.file = '';

          $scope.getLoginUser = function () {
              userService.getLoginUser(function(data) {
                  $scope.userFullName = data.FirstName + " " + data.LastName;
                  $scope.user = data;
                  $scope.getRentByUser();
                  $scope.getAllRentByUser();
              }, function() {});
          };

          $scope.getLoginUser();

          $scope.LogOff = function () {
              userService.LogOff(function (data) {
                  window.location.href = data;
              }, function () { });
          };

          $scope.getRentByUser = function () {
              userService.getCurrentRentByUser($scope.user, function (data) {
                  $scope.currentRent = data;
              }, function () { });
          };
          $scope.getAllRentByUser = function () {
              userService.getAllRentByUser($scope.user, function (data) {
                  $scope.allRents = data;
              }, function () { });
          };

          $scope.updateInfo = function () {
              var modalInstance = $uibModal.open({
                  templateUrl: '/app/templetes/admin/addContact.html',
                  backdrop: 'static',
                  windowClass: 'modal',
                  scope: $scope,
                  controller: 'changeInfoController',
                  resolve: {
                      user: function () {
                          return $scope.user;
                      }
                  }

              });
              modalInstance.result
                .then(function () {
                    $scope.getLoginUser();
                }, function (reason) {
                    alert(reason);
                });
          };

          $scope.$watch('file', function (newVal) {
              if (!!newVal) {
                  upload.upload({
                      url: 'User/UploadPicture',
                      file: newVal,
                      progress: function(e) {}
                  }).then(function(data, status, headers, config) {
                      // file is uploaded successfully
                      console.log(data);
                  });
              }
          });
      }
 ]);