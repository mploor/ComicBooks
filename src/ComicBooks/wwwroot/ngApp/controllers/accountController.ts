namespace ComicBooks.Controllers {

    export class AccountController {
        public externalLogins;

        public getUserName() {
            return this.accountService.getUserName();
        }

        public getClaim(type) {
            return this.accountService.getClaim(type);
        }

        public isLoggedIn() {
            return this.accountService.isLoggedIn();
        }

        public logout() {
            this.accountService.logout();
            this.$location.path('/');
        }

        public getExternalLogins() {
            return this.accountService.getExternalLogins();
        }

        constructor(private accountService: ComicBooks.Services.AccountService, private $location: ng.ILocationService) {
            this.getExternalLogins().then((results) => {
                this.externalLogins = results;
            });
        }
    }

    angular.module('ComicBooks').controller('AccountController', AccountController);


    export class LoginController {
        public loginUser;
        public validationMessages;

        public login() {
            this.accountService.login(this.loginUser).then(() => {
                this.$location.path('/');
                var cssFile = this.accountService.getClaim("theme");
                if (cssFile == null) { cssFile = "/css/spidey.css"; }
                this.accountService.setTheme(cssFile);
            }).catch((results) => {
                this.validationMessages = results;
            });
        }

        constructor(private accountService: ComicBooks.Services.AccountService, private $location: ng.ILocationService) { }
    }


    export class RegisterController {
        public registerUser;
        public validationMessages;
        public theme;

        public register() {
            this.accountService.register(this.registerUser).then(() => {
                this.accountService.setTheme(this.theme.themeFile);
                this.$http.post("/api/account/theme", this.theme).then((response) => {
                    this.$location.path('/');
                });
            }).catch((results) => {
                this.validationMessages = results;
            });
        }

        constructor(private $http: ng.IHttpService, private accountService: ComicBooks.Services.AccountService, private $location: ng.ILocationService) { }
    }





    export class ExternalRegisterController {
        public registerUser;
        public validationMessages;

        public register() {
            this.accountService.registerExternal(this.registerUser.email)
                .then((result) => {
                    this.$location.path('/');
                }).catch((result) => {
                    this.validationMessages = result;
                });
        }

        constructor(private accountService: ComicBooks.Services.AccountService, private $location: ng.ILocationService) {}

    }

    export class ConfirmEmailController {
        public validationMessages;

        constructor(
            private accountService: ComicBooks.Services.AccountService,
            private $http: ng.IHttpService,
            private $stateParams: ng.ui.IStateParamsService,
            private $location: ng.ILocationService
        ) {
            let userId = $stateParams['userId'];
            let code = $stateParams['code'];
            accountService.confirmEmail(userId, code)
                .then((result) => {
                    this.$location.path('/');
                }).catch((result) => {
                    this.validationMessages = result;
                });
        }
    }

    export class EditUserController {
        public theme;

        constructor(private $http: ng.IHttpService, private $state: ng.ui.IStateService, private accountService: ComicBooks.Services.AccountService,) {
        }

        public setTheme() {
            this.accountService.setTheme(this.theme.themeFile);
            this.$http.post("/api/account/theme", this.theme).then((response) => {
                this.$state.reload();
            });
        }
    }

    export class MakeAdminController {
        public newUser;

        constructor(private $http: ng.IHttpService, private $state: ng.ui.IStateService) {
        }

        public makeAdmin() {
            this.$http.post("/api/account/admin", this.newUser).then((response) => {
                this.$state.reload();
            });
        }
    }
}
