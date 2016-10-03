'use strict';

angular.module('adminModule').controller('floorController', ['$scope', 'adminService', '$uibModal',
      function ($scope, adminService, $uibModal) {
          $scope.users = [];
          $scope.floors = [];
          $scope.searchFloors = [];
          $scope.format = "dd.MM.yyyy";
          $scope.dateStart = { opened: false };
          $scope.dateEnd = { opened: false };
          $scope.search = {
              User: '',
              DateStart: undefined,
              DateEnd: undefined
          }

          $scope.loadFloors = function () {
              adminService.getFloors(function (data) {
                  $scope.floors = data;
              }, function () { });
          };

          $scope.loadFloors();

          $scope.roomInfo = function (room) {
              $uibModal.open({
                  templateUrl: '/app/templetes/admin/roomInfo.html',
                  backdrop: 'static',
                  windowClass: 'modal',
                  scope: $scope,
                  controller: 'roomInfoController',
                  resolve: {
                      room: function () {
                          return room;
                      }
                  }
              }).result
                .then(function (makeReservation) {
                    if (makeReservation) {
                        //make new reservation
                        $uibModal.open(
                            {
                                templateUrl: '/app/templetes/admin/makeReservation.html',
                                backdrop: 'static',
                                windowClass: 'modal',
                                scope: $scope,
                                controller: 'makeReservationController',
                                resolve: {
                                    room: function () {
                                        return room;
                                    },
                                    dateStart: function () {
                                        return undefined;
                                    },
                                    dateEnd: function () {
                                        return undefined;
                                    },
                                    user: function() {
                                        return '';
                                    }
                                }
                            }).result
                            .then(function () {
                                $scope.loadFloors();
                            }, function () {
                                //dismiss
                            });
                    }
                }, function () {
                    //dismiss
                });
          };

          $scope.loadUsers = function () {
              adminService.getUsers(function (data) {
                  $scope.users = data;
              }, function () { });
          }

          $scope.loadUsers();

          $scope.dateStartOpen = function () {
              $scope.dateStart.opened = true;
              $scope.dateEnd.opened = false;
          }

          $scope.dateEndOpen = function () {
              $scope.dateStart.opened = false;
              $scope.dateEnd.opened = true;
          }

          $scope.searchFreeFloors = function () {
              var model = {
                  UserId: $scope.search.User.Id,
                  DateStart: $scope.search.DateStart,
                  DateEnd: $scope.search.DateEnd
              }

              adminService.searchFreeFloors(model, function (data) {
                  if (!!data) {
                      $scope.searchFloors = data;
                  }
              }, function () { });
          }

          $scope.$watch('search', function () {
              $scope.searchFloors = [];
              if (!!$scope.search.User && $scope.search.DateStart && (!$scope.search.DateEnd || $scope.search.DateEnd >= $scope.search.DateStart)) {
                  $scope.searchFreeFloors();
              }
          }, true);

          $scope.roomInfoForPeriod = function (room) {
              $uibModal.open({
                  templateUrl: '/app/templetes/admin/roomInfoForPeriod.html',
                  backdrop: 'static',
                  windowClass: 'modal',
                  scope: $scope,
                  controller: 'roomInfoForPeriodController',
                  resolve: {
                      room: function () {
                          return room;
                      },
                      dateStart: function () {
                          return $scope.search.DateStart;
                      },
                      dateEnd: function () {
                          return $scope.search.DateEnd;
                      }
                  }
              }).result
                .then(function (makeReservation) {
                    //cancel
                    $uibModal.open(
                            {
                                templateUrl: '/app/templetes/admin/makeReservation.html',
                                backdrop: 'static',
                                windowClass: 'modal',
                                scope: $scope,
                                controller: 'makeReservationController',
                                resolve: {
                                    room: function () {
                                        return room;
                                    },
                                    dateStart: function () {
                                        return $scope.search.DateStart;
                                    },
                                    dateEnd: function () {
                                        return $scope.search.DateEnd;
                                    },
                                    user: function () {
                                        return $scope.search.User;
                                    }
                                }
                            }).result
                            .then(function () {
                                $scope.searchFreeFloors();
                            }, function () {
                                //dismiss
                            });
                }, function () {
                    //dismiss
                });
          }
      }
]);