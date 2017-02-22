namespace ComicBooks {

    angular.module('ComicBooks', ['ui.router', 'ngResource', 'ui.bootstrap']).config((
        $stateProvider: ng.ui.IStateProvider,
        $urlRouterProvider: ng.ui.IUrlRouterProvider,
        $locationProvider: ng.ILocationProvider
    ) => {
        // Define routes
        $stateProvider
            .state('home', {
                url: '/',
                templateUrl: '/ngApp/views/home.html',
                controller: ComicBooks.Controllers.HomeController,
                controllerAs: 'controller'
            })
            .state('viewCollection', {
                url: '/viewCollection',
                templateUrl: '/ngApp/views/viewCollection.html',
                controller: ComicBooks.Controllers.ViewCollectionController,
                controllerAs: 'controller'
            })
            .state('viewCollectionId', {
                url: '/viewCollection/:id',
                templateUrl: '/ngApp/views/viewCollection.html',
                controller: ComicBooks.Controllers.ViewCollectionController,
                controllerAs: 'controller'
            })
            .state('collectionSummary', {
                url: '/viewCollectionSummary',
                templateUrl: '/ngApp/views/collectionSummary.html',
                controller: ComicBooks.Controllers.CollectionSummaryController,
                controllerAs: 'controller'
            })
            .state('addComic', {
                url: '/addComic',
                templateUrl: '/ngApp/views/addComic.html',
                controller: ComicBooks.Controllers.AddComicController,
                controllerAs: 'controller'
            })
            .state('addTitle', {
                url: '/addTitle',
                templateUrl: '/ngApp/views/addTitle.html',
                controller: ComicBooks.Controllers.AddTitleController,
                controllerAs: 'controller'
            })
            .state('titleSummary', {
                url: '/titleSummary',
                templateUrl: '/ngApp/views/titleSummary.html',
                controller: ComicBooks.Controllers.TitleSummaryController,
                controllerAs: 'controller'
            })
            .state('titleList', {
                url: '/titleList',
                templateUrl: '/ngApp/views/titleList.html',
                controller: ComicBooks.Controllers.TitleListController,
                controllerAs: 'controller'
            })
            .state('wishList', {
                url: '/wishList',
                templateUrl: '/ngApp/views/wishList.html',
                controller: ComicBooks.Controllers.WishListController,
                controllerAs: 'controller'
            })
            .state('login', {
                url: '/login',
                templateUrl: '/ngApp/views/login.html',
                controller: ComicBooks.Controllers.LoginController,
                controllerAs: 'controller'
            })
            .state('editUser', {
                url: '/editUser',
                templateUrl: '/ngApp/views/editUser.html',
                controller: ComicBooks.Controllers.EditUserController,
                controllerAs: 'controller'
            })
            .state('makeAdmin', {
                url: '/makeAdmin',
                templateUrl: '/ngApp/views/makeAdmin.html',
                controller: ComicBooks.Controllers.MakeAdminController,
                controllerAs: 'controller'
            })
            .state('register', {
                url: '/register',
                templateUrl: '/ngApp/views/register.html',
                controller: ComicBooks.Controllers.RegisterController,
                controllerAs: 'controller'
            })
            .state('externalRegister', {
                url: '/externalRegister',
                templateUrl: '/ngApp/views/externalRegister.html',
                controller: ComicBooks.Controllers.ExternalRegisterController,
                controllerAs: 'controller'
            }) 
            .state('notFound', {
                url: '/notFound',
                templateUrl: '/ngApp/views/notFound.html'
            });

        // Handle request for non-existent route
        $urlRouterProvider.otherwise('/notFound');

        // Enable HTML5 navigation
        $locationProvider.html5Mode(true);
    });
    
    angular.module('ComicBooks').factory('authInterceptor', (
        $q: ng.IQService,
        $window: ng.IWindowService,
        $location: ng.ILocationService
    ) =>
        ({
            request: function (config) {
                config.headers = config.headers || {};
                config.headers['X-Requested-With'] = 'XMLHttpRequest';
                return config;
            },
            responseError: function (rejection) {
                if (rejection.status === 401 || rejection.status === 403) {
                    $location.path('/login');
                }
                return $q.reject(rejection);
            }
        })
    );

    angular.module('ComicBooks').config(function ($httpProvider) {
        $httpProvider.interceptors.push('authInterceptor');
    });

    $(window).scroll(function () {
        var scroll = $(window).scrollTop();

        if (scroll > 90) {
            $(".mynavbar").addClass("navbar-fixed-top");
        }
        if (scroll < 90) {
            $(".navbar-fixed-top").removeClass("navbar-fixed-top");
        }
    });

}
