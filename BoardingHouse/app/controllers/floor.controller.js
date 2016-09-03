'use strict';

angular.module('adminModule').controller('floorController',
 [
     '$scope',
     'adminService',
     '$uibModal',
      function ($scope, adminService, $uibModal) {
          $scope.floors = null;
          $scope.freeRooms = [];
          $scope.selectedFloor = null;
          $scope.RoomsOfSelectedFloor = null;
          $scope.showRoom = true;
          $scope.from = new Date();
          $scope.to = new Date();
          $scope.selectedUser = null;
          $scope.showAvailableRoom = false;
          $scope.disabled = true;

          $scope.loadUsers = function () {
              adminService.getUsers(function (data) {
                  $scope.users = data;
              }, function () { });
          }

          $scope.loadUsers();

          $scope.loadFloors = function () {
              adminService.getFloors(function (data) {
                  $scope.floors = data;  
                  var free=0;
                  for (var i = 0; i < $scope.floors.length; i++) {
                      {
                          free = 0;
                          var m = $scope.floors[i].Rooms.length;
                          for (var j = 0; j <m; j++) {
                              if ($scope.floors[i].Rooms[j].NumOfbeds > $scope.floors[i].Rooms[j].Rents.length)
                                  free += 1;
                          }
                          $scope.freeRooms[i] = free;
                      }   
                  }
              }, function () { });
          };

          $scope.loadFloors();

          $scope.update = function (floor) {
              $scope.showRoom = true;
              var dataForSend = {
                  NumOfFloor: floor.NumOfFloor
              }
              adminService.getFloorsByNumOfFloor(dataForSend, function (data) {             
                  $scope.RoomsOfSelectedFloor = data.Rooms;
                 
              }, function () { });
            
          }
        
          $scope.reserveRoom = function () {
              $scope.showRoom = false;
          }

          $scope.rowClass = function (room) {
              if (room.Rents.length == room.NumOfbeds)
                  return "noFreeBeds";
              else return "hasFreeBeds";
          }

          $scope.roomInfo = function (room) {
              var modalInstance = $uibModal.open({
                  templateUrl: '/app/templetes/admin/roomInfo.html',
                  backdrop: 'static',
                  windowClass: 'modal',
                  scope: $scope,
                  controller: 'roomInfoController',
                  resolve: {
                      room: function () {
                          return room;
                      },
                      reservation: function() {
                          return {
                              userId: $scope.selectedUser,
                              dateStart: $scope.from,
                              dateEnd: $scope.to,
                              floorId: $scope.FoorS,
                              roomId: room.Id
                          }
                      }

                  }

              });
              modalInstance.result
                .then(function () {
                
                }, function (reason) {
                    alert(reason);
                });
          };

          $scope.today = function () {
              $scope.dt = new Date();
          };
          $scope.open1 = function () {
              $scope.popup1.opened = true;
          };

          $scope.popup1 = {
              opened: false
          };

          $scope.open2 = function () {
              $scope.popup2.opened = true;
          };

          $scope.popup2 = {
              opened: false
          };

          $scope.formats = ['dd-MMMM-yyyy', 'yyyy/MM/dd', 'dd.MM.yyyy', 'shortDate'];
          $scope.format = $scope.formats[0];

          $scope.$watch('to', function (newValue, oldValue) {
              $scope.readyForRes();
          }, true);

          $scope.$watch('from', function (newValue, oldValue) {
              $scope.readyForRes();
          }, true);

          $scope.checkRooms = function (floorS) {
              $scope.floorS = floorS;
              $scope.readyForRes();
          }

          $scope.readyForRes = function () {
              if ($scope.from != null && $scope.to != null && $scope.selectedUser != null && $scope.floorS !=null) {
                  $scope.showAvailableRoom = true;

                  var dataForSend = {
                      //User:$scope.selectedUser,
                      Floor: $scope.floorS,
                      dateStart: $scope.from,
                      dateEnd: $scope.to,
                      floorId: $scope.floorS
                  }
                  adminService.getFreeRoom(dataForSend, function (data) {
                      $scope.aRooms = data;
                  }, function () { });

              }
          }

          $scope.user = function (user) {
              $scope.selectedUser = user;
              $scope.readyForRes();
          }

          $scope.selectRoom = function (selectedRoom) {
              $scope.disabled = false;
              $scope.selectedRoom = selectedRoom;
          }

          $scope.makeAreservation = function () {
              var dataForSend = {
                  userId: $scope.selectedUser,
                  dateStart: $scope.from,
                  dateEnd: $scope.to,
                  floorId: $scope.FoorS,
                  roomId: $scope.selectedRoom
              }
              adminService.makeAReservation(dataForSend, function (data) {
                  // $scope.aRooms = data;
              }, function () { });

          }
        
          $scope.dateFrom = function(from){
              $scope.from = from;
          }

          $scope.dateTo = function (to) {
              $scope.to = to;
          }
      }

      
      
]);