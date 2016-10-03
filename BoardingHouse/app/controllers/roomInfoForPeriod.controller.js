'use strict';

angular.module('adminModule').controller('roomInfoForPeriodController', ['$scope', 'room', 'dateStart', 'dateEnd', '$uibModalInstance',
        function ($scope, room, dateStart, dateEnd, $uibModalInstance) {
            $scope.room = room;

            $scope.cancel = function () {
                $uibModalInstance.dismiss();
            };

            $scope.forPeriod = function (rent) {
                var rentDateStart = $scope.parseDate(rent.DateStart);
                var rentDateEnd = $scope.parseDate(rent.DateEnd);

                if (!rentDateEnd) {
                    if (!dateEnd)
                        return true;

                    return rentDateStart <= dateEnd;
                }

                if (!dateEnd) {
                    return rentDateEnd>= dateStart;
                }

                return (dateStart >= rentDateStart && dateStart <= rentDateEnd) ||
                        (dateEnd >= rentDateStart && dateEnd <= rentDateEnd) ||
                        (dateStart <= rentDateStart && dateEnd >= rentDateEnd);
            }

            $scope.parseDate = function(jsonDateString) {
                return new Date(parseInt(jsonDateString.replace('/Date(', '')));
            }

            $scope.makeReservation = function () {
                $uibModalInstance.close(true);
            }
        }
]);