namespace ComicBooks.Controllers {

    export class HomeController {
    }

    export class ViewCollectionController {
        public comics;
        public titles;
        public viewChoice = "";
        public chosenComic;

        constructor(private $http: ng.IHttpService, private $stateParams: ng.ui.IStateParamsService, private $state: ng.ui.IStateService, private myServices: ComicBooks.Services.MyServices) {
            this.$http.get("/api/comics").then((response) => {
                this.comics = response.data;
            });
            this.$http.get("/api/titles/user").then((response) => {
                this.titles = response.data;
                this.viewChoice = myServices.getTitleChoice(this.$stateParams["id"], this.titles);
            });
        }

        public bookChoice(comic) {
            this.chosenComic = comic;
        }

        public editComic() {
            this.$http.post("/api/comics", this.chosenComic).then((response) => {
                this.$state.reload();
            });
        }

        public deleteComic() {
            this.$http.delete("/api/comics/" + this.chosenComic.id).then((response) => {
                this.$state.reload();
            })
        }
    }

    export class CollectionSummaryController {
        public titles;
        public totPrice = 0;
        public totValue = 0;
        public totCount = 0;

        constructor(private $http: ng.IHttpService, private myServices: ComicBooks.Services.MyServices) {
            this.$http.get("/api/titles/user").then((response) => {
                this.titles = response.data;
                this.totPrice = myServices.totalPrice(this.titles);
                this.totValue = myServices.totalValue(this.titles);
                this.totCount = myServices.totalCount(this.titles);
            });
        }
    }

    export class TitleSummaryController {
        public titles;

        constructor(private $http: ng.IHttpService) {
            this.$http.get("/api/titles/all").then((response) => {
                this.titles = response.data;
            });
        }
    }

    export class TitleListController {
        public titles;
        public chosenTitle;

        constructor(private $http: ng.IHttpService, private $state: ng.ui.IStateService) {
            this.$http.get("/api/titles/full").then((response) => {
                this.titles = response.data;
            });
        }

        public titleChoice(title) {
            this.chosenTitle = title;
        }

        public editTitle() {
            this.$http.post("/api/titles", this.chosenTitle).then((response) => {
                this.$state.reload();
            });
        }

        public deleteTitle() {
            this.$http.delete("/api/titles/" + this.chosenTitle.id).then((response) => {
                this.$state.reload();
            })
        }
    }

    export class AddComicController {
        public comic;
        public titles;
        public grades = ["0.5", "1.0", "1.5", "1.8", "2.0", "2.5", "3.0", "3.5", "4.0", "4.5", "5.0", "5.5", "6.0", "6.5", "7.0", "7.5", "8.0", "8.5", "9.0", "9.2", "9.4", "9.6", "9.8", "10.0"];
        public numError = [];
        public priceError = [];
        public valueError = [];
        public titleError = [];
        public comics = [];
        public index = [1, 2, 3, 4, 5, 6, 7, 8];
        public currTitle;
        public message = "";

        constructor(private $http: ng.IHttpService, private $state: ng.ui.IStateService, private myServices: ComicBooks.Services.MyServices) {
            this.$http.get("/api/titles").then((response) => {
                this.titles = response.data;
            });
        }

        public addComic() {
            this.message = this.myServices.addComics(this.comics, this.numError, this.priceError, this.valueError, this.titleError);
        }
    }

    export class AddTitleController {
        public title

        constructor(private $http: ng.IHttpService, private $state: ng.ui.IStateService) {
        }

        public addTitle() {
            this.$http.post("/api/titles", this.title).then((response) => {
                this.$state.reload();
            });
        }
    }

    export class WishListController {
        public comics;
        public titles;
        public newComic;
        public chosenComic;
        public viewChoice = "";

        constructor(private $http: ng.IHttpService, private $state: ng.ui.IStateService) {
            this.$http.get("/api/comics/wish").then((response) => {
                this.comics = response.data;
            });
            this.$http.get("/api/titles").then((response) => {
                this.titles = response.data;
            });
        }

        public addComic() {
            this.newComic.wishList = true;
            this.$http.post("/api/comics", this.newComic).then((response) => {
                this.$state.reload();
            })
        }

        public bookChoice(comic) {
            this.chosenComic = comic;
        }

        public deleteComic() {
            console.log(this.chosenComic.id);
            this.$http.delete("/api/comics/" + this.chosenComic.id).then((response) => {
                this.$state.reload();
            })
        }
    }
}
