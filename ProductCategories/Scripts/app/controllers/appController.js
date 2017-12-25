var app = angular.module('app');

app.controller('mainController', ['$scope', 'appService', '$modal', function ($scope, appService, $modal) {

    $scope.products = [];
    $scope.categories = [];
    $scope.pageNumber = 1

    $scope.init = function () {

        appService.getAppData({ pageNumber: $scope.pageNumber })

            .then(function (result) {

                var data = result.data;
                $scope.products = data.Products;
                $scope.categories = data.Categories;
            });
    }



    $scope.init();

    $scope.updateRecord = function (product) {
        $scope.crudOperation(product);
    };

    $scope.pagination = function (e) {
        var val = e.currentTarget.getAttribute("pagenum");
       
        if ($scope.pageNumber >= 1) {
                {   
                    $scope.pageNumber = $scope.pageNumber + parseInt(val);
                }

        }
        $scope.init()
    }
    $scope.crudOperation = function (product) {

        $modal.open({
            templateUrl: '/HtmlTemplets/HtmlPage1.html',
            windowClass: 'modal', // windowClass - additional CSS class(es) to be added to a modal window template
            controller: function ($scope, $modalInstance, appService, appData) {

                $scope.data = appData;

                if ($scope.data.product) {
                    $scope.data.categories.selectCategory = $scope.data.categories[$scope.data.product.CategoryID];
                }

                $scope.cancel = function () {
                    $modalInstance.close();
                    appData.cb();
                };

                $scope.save = function () {

                    var product = {
                        ProductName: $scope.data.product.ProductName,
                        CategoryID: $scope.data.categories.selectCategory.CategoryID
                    }


                    appService.addProduct(product).then(function (result) {


                    });

                }

            },
            resolve: {
                appData: function () {
                    return {
                        categories: $scope.categories,
                        product: product,
                        cb: $scope.init
                    };
                }
            }
        });

    }

}]);





app.factory('appService', ['httpHelper', function (httpHelper) {

    var getAppData = function (data) {
        return httpHelper('GetAppData', 'GET', data);
    }

    var addProduct = function (product) {
        return httpHelper('AddProduct', 'POST', product);
    }
    return {
        getAppData: getAppData,
        addProduct: addProduct
    }

}]);


app.factory('httpHelper', ['$http', function ($http) {

    var httpHelper = function (action, method, data) {

        return $http({
            method: method || 'GET',
            url: '/api/ApiApp/' + action,
            params: method == "GET" ? data : null,
            data: data
        });
    }

    return httpHelper;

}]);
