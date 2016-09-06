'use strict';

angular.module('adminModule').controller('makeReservationController', ['$scope', 'adminService', 'room', '$uibModalInstance',
      function ($scope, adminService, room, $uibModalInstance) {
          $scope.errorMsg = '';
          $scope.users = [];
          $scope.reservation = {
              RoomId: room.Id,
              User: '',
              DateStart: undefined,
              DateEnd: undefined
          }

          $scope.loadUsers = function () {
              adminService.getUsers(function (data) {
                  $scope.users = data;
              }, function () { });
          }

          $scope.loadUsers();

          $scope.dateStart = { opened: false };
          $scope.dateEnd = { opened: false };

          $scope.dateStartOpen = function() {
              $scope.dateStart.opened = true;
              $scope.dateEnd.opened = false;
          }

          $scope.dateEndOpen = function () {
              $scope.dateStart.opened = false;
              $scope.dateEnd.opened = true;
          }

          $scope.format = "dd.MM.yyyy";

          $scope.cancel = function () {
              $uibModalInstance.dismiss();
          };

          $scope.makeReservation = function () {
              if ($scope.validate()) {
                  var model = {
                      RoomId: $scope.reservation.RoomId,
                      UserId: $scope.reservation.User.Id,
                      DateStart: $scope.reservation.DateStart,
                      DateEnd: $scope.reservation.DateEnd
                  }
                  adminService.makeAReservation(model, function(data) {
                      if (!!data) {
                          $uibModalInstance.close();
                      }
                  }, function (error) {
                      $scope.errorMsg = error.Message;
                  });
              }
          }

          $scope.validate = function () {
              if (!!!$scope.reservation.User) {
                  $scope.errorMsg = 'Корисник е задолжително поле';
                  return false;
              }
              if (!!!$scope.reservation.DateStart) {
                  $scope.errorMsg = 'Датум од е задолжително поле';
                  return false;
              }

              return true;
          }
      }
]);