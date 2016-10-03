'use strict';

angular.module('mainModule').controller('mainController',
 ['$scope', 'appService', function ($scope, appService) {
     $scope.msg = "";

     $scope.user = {
         Email: "",
         Password: ""
     }

     $scope.login = function (loginForm) {
         if (loginForm.$valid) {
             appService.login($scope.user, function (data) {
                 if (!!data)
                     window.location.href = data;
                 else {
                     $scope.msg = "Внесовте погршна е-маил адреса или погрешна лозинка!";
                     loginForm.Password.$pristine = false;
                 }
             }, function () {
                 $scope.msg = "Внесовте погршна е-маил адреса или погрешна лозинка!";
                 loginForm.Password.$pristine = false;
             });
         }
         else {
             if (loginForm.Email.$invalid) {
                 loginForm.Email.$pristine = false;
             }
             if (loginForm.Password.$invalid) {
                 loginForm.Password.$pristine = false;
             }
         }
     }
 }
 ]);