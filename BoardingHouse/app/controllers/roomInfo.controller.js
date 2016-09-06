/// <reference path="../templetes/admin/addContact.html" />
'use strict';

angular.module('adminModule').controller('roomInfoController', [ '$scope', 'adminService', 'room', '$uibModalInstance',
        function ($scope, adminService, room, $uibModalInstance) {
          $scope.room = room;

          $scope.cancel = function () {
              $uibModalInstance.dismiss();
          };

          $scope.activeOnly = function (rent) {
              return rent.Active;
          }

          $scope.makeReservation = function () {
              $uibModalInstance.close(true);
          }
      }
 ]);