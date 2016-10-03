'use strict';

angular.module('userModule').factory('userService',
    [
    '$http',
        function ($http) {
            return {
                getLoginUser: function (successcb, errorcb) {
                    $http({
                        method: 'POST',
                        url: 'User/GetLoginUser',
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
                getCurrentRentByUser: function (successcb, errorcb) {
                    $http({
                        method: 'POST',
                        url: 'User/GetCurrentRentByUser',
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
                getAllRentByUser: function (successcb, errorcb) {
                    $http({
                        method: 'POST',
                        url: 'User/GetAllRentByUser',
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