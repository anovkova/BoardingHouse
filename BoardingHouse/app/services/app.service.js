'use strict';

angular.module('mainModule').factory('appService',
    [
    '$http',
        function ($http) {
            return {
                login: function (user, successcb, errorcb) {
                    $http({
                        method: 'POST',
                        url: 'Home/Login',
                        data: JSON.stringify(user),
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