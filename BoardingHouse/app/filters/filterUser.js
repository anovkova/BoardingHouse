'use strict';

angular.module('userModule').filter("jsDate", function () {
    var re = /\/Date\(([0-9]*)\)\//;
    return function (x) {
        var m = x.match(re);
        if (m) {
            var date = new Date(parseInt(m[1]));
            return (date.getDate() + '.' + (date.getMonth() + 1) + '.' + date.getFullYear());
        }
        else return "";
    };
});