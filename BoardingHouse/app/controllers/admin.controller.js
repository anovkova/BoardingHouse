'use strict';

angular.module('adminModule').controller('adminController',
 [
     '$scope',
    'adminService',
      '$uibModal',
      function ($scope, adminService, $uibModal) {
          $scope.users = null;
         

          $scope.loadUsers = function () {
              adminService.getUsers(function (data) {
                  $scope.users = data;
              }, function () { });
          }
          $scope.loadUsers();

          $scope.LogOff = function () {
              adminService.LogOff(function (data) {
                  window.location.href = data;
              }, function () { });
          }
          
          $scope.AddUser = function (selectedUser) {
              var modalInstance = $uibModal.open({
                  templateUrl: '/app/templetes/admin/addContact.html',
                  backdrop: 'static',
                  windowClass: 'modal',
                  scope: $scope,
                  controller: 'addUserController',
                  resolve: {
                      user: function () {
                          return !!!selectedUser ? {
                              FirstName: "",
                              LastName: "",
                              PhoneNumber: "",
                              Embg: "",
                              Email: "",
                              Password: "",
                              Role: {
                                  Id: 2,
                                  Title: ""
                              },
                              flag: true,
                              Id: ""
                          } : selectedUser;
                      }
                  }

              });
              modalInstance.result
                .then(function () {
                    $scope.loadUsers();
                }, function (reason) {
                    alert(reason);
                });
          };
      }
 ]);