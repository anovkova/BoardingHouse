/// <reference path="../templetes/admin/addContact.html" />
'use strict';

angular.module('adminModule').controller('roomInfoController',
 [
     '$scope',
    'adminService',
     'room',
      '$uibModalInstance',
      'reservation',
 
        function ($scope, adminService, room, $uibModalInstance, reservation) {
          debugger;
          $scope.room = room;
          $scope.reservation = reservation;
          $scope.msg = "";

          $scope.cancel = function () {
              $uibModalInstance.close();
          };
          $scope.makeAreservation = function () {
              adminService.makeAReservation($scope.reservation, function (data) {
                  if (data == "False")
                      $scope.msg = "Веќе постои резервација за тој корисник во тој период";
                  else {
                      $uibModalInstance.close();
                     
                  }
                
              }, function () { });

          }
      }
 ]);