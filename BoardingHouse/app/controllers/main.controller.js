'use strict';

angular.module('mainModule').controller('mainController',
 [
     '$scope',
    'appService',
      function ($scope, appService) {
         $scope.email = "";
         $scope.password = "";
         $scope.msg = "";
         $scope.login = function (loginFrom) {
             if (loginFrom.$valid) {
                 var user = {
                     email: $scope.email,
                     password: $scope.password
                 }
                 appService.login(user, function (data) {
                     if(data!="")
                         window.location.href = data;
                     else {
                         $scope.msg = "Внесовте погршна е-маил адреса или погрешна лозинка!";
                         loginFrom.password.$pristine = false;
                     }
                 }, function () {
                     $scope.msg = "Внесовте погршна е-маил адреса или погрешна лозинка!";
                     loginFrom.password.$pristine = false;
                 });
             }
             else {
                 if (loginFrom.email.$invalid) {
                     loginFrom.email.$pristine = false;
                 }

                 if (loginFrom.password.$invalid) {
                     loginFrom.password.$pristine = false;
                 }
             }
             
         }
      
     }
 ]);