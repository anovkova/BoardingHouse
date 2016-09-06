'use strict';

angular.module('adminModule').factory('adminService',
    [
    '$http',
        function ($http) {
            return {
                getUsers: function (successcb, errorcb) {
                    $http({
                        method: 'POST',
                        url: 'Admin/GetAllUsers',
                        data: JSON.stringify(),
                        headers: { 'content-type': 'application/json' }
                    }).
                    success(function (data) {
                        successcb(data);
                    }).
                    error(function (error) {
                        errorcb(error);
                    });
                },
                addNewUser: function (user, successcb, errorcb) {
                    $http({
                        method: 'POST',
                        url: 'Admin/AddUser',
                        data: JSON.stringify(user),
                        headers: { 'content-type': 'application/json' }
                    }).
                    success(function (data) {
                        successcb(data);
                    }).
                    error(function (error) {
                        errorcb(error);
                    });
                },
                getFloors: function (successcb, errorcb) {
                    $http({
                        method: 'POST',
                        url: 'Admin/GetFloors',
                        data: JSON.stringify(),
                        headers: { 'content-type': 'application/json' }
                    }).
                        success(function (data) {
                            successcb(data);
                        }).
                        error(function (error) {
                            errorcb(error);
                        });
                },
                LogOff: function (successcb, errorcb) {
                    $http({
                        method: 'POST',
                        url: 'Admin/LogOff',
                        data: JSON.stringify(),
                        headers: { 'content-type': 'application/json' }
                    }).
                    success(function (data) {
                        successcb(data);
                    }).
                    error(function (error) {
                        errorcb(error);
                    });
                },
                makeAReservation: function (reservation, successcb, errorcb) {
                    $http({
                        method: 'POST',
                        url: 'Admin/MakeAReservation',
                        data: JSON.stringify(reservation),
                        headers: { 'content-type': 'application/json' }
                    }).
                    success(function (data) {
                        successcb(data);
                    }).
                    error(function (error) {
                        errorcb(error);
                    });
                },
                searchFreeFloors: function (model, successcb, errorcb) {
                    $http({
                        method: 'POST',
                        url: 'Admin/SearchFreeFloors',
                        data: JSON.stringify(model),
                        headers: { 'content-type': 'application/json' }
                    }).
                    success(function (data) {
                        successcb(data);
                    }).
                    error(function (error) {
                        errorcb(error);
                    });
                },
                getAllRents: function (successcb, errorcb) {
                    $http({
                        method: 'POST',
                        url: 'Admin/GetAllRents',
                        data: JSON.stringify(),
                        headers: { 'content-type': 'application/json' }
                    }).
                   success(function (data) {
                       successcb(data);
                   }).
                   error(function (error) {
                       errorcb(error);
                   });
                }
            };
        }
    ]);