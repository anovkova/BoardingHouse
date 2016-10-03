var module = angular.module('adminModule', ['ui.router', 'ui.bootstrap', 'ui.bootstrap.modal'])
    .config(
    [
        '$stateProvider',
        '$urlRouterProvider',
        function($stateProvider, $urlRouterProvider) {
            $urlRouterProvider.otherwise("/Users");

            $stateProvider
                .state('users', {
                    url: "/Users",
                    templateUrl: "/app/templetes/admin/users.html",
                    controller: 'adminController'
                });
            $stateProvider
                .state('floors', {
                    url: "/Floors",
                    templateUrl: "/app/templetes/admin/floors.html",
                    controller: 'floorController'
                });

            $stateProvider
                .state('reservation', {
                    url: "/Reservation",
                    templateUrl: "/app/templetes/admin/reservations.html",
                    controller: 'reservationController'
                });
            $stateProvider
             .state('bills', {
                 url: "/Bills",
                 templateUrl: "/app/templetes/admin/bills.html",
                 controller: 'billsController'
             });
        }
    ]);


