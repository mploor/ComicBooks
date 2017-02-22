namespace ComicBooks.Services {

    export class MyServices {

        // Check that comic properties are valid, return false if not
        public checkComicOk(i: number, comics, numError, priceError, valueError, titleError) {
            numError[i] = "";
            priceError[i] = "";
            valueError[i] = "";
            titleError[i] = "";

            let status = true;
            if (comics[i].title == null) {
                titleError[i] = "Must select a title";
                status = false;
            }
            if (comics[i].issueNum == undefined) {
                status = false;
            } else if (isNaN(comics[i].issueNum)) {
                numError[i] = "Must be number";
                status = false;
            }
            if (!(comics[i].purchasePrice == null)) {
                if (isNaN(comics[i].purchasePrice)) {
                    priceError[i] = "Must be number";
                    status = false;
                }
            }
            if (!(comics[i].value == null)) {
                if (isNaN(comics[i].value)) {
                    valueError[i] = "Must be number";
                    status = false;
                }
            }
            return status;
        }

        public totalCount(titles) {
            let totCount = 0;
            for (let i = 0; i < titles.length; i++) {
                totCount += titles[i].count;
            }
            return totCount;
        }

        public totalPrice(titles) {
            let totPrice = 0;
            for (let i = 0; i < titles.length; i++) {
                totPrice += titles[i].totalPrice;
            }
            return totPrice;
        }

        public totalValue(titles) {
            let totValue = 0;
            for (let i = 0; i < titles.length; i++) {
                totValue += titles[i].totalValue;
            }
            return totValue;
        }

        // Return title based on selected id, return empty string for id=null
        public getTitleChoice(id, titles) {
            let choice = "";
            if (id == null) {
                return "";
            } else {
                let i = 0;
                while (choice == "") {
                    if (titles[i].titleId == id) {
                        choice = titles[i].title;
                    }
                    i++;
                }
                return choice;
            }
        }

        public addComics(comics, numError, priceError, valueError, titleError) {
            let message = "";
            let count = 0;
            let currTitle = comics[0].title;
            for (let i = 0; i < 10; i++) {
                if (!(comics[i] == null)) {
                    comics[i].title = currTitle;
                    comics[i].wishList = false;
                    if (this.checkComicOk(i, comics, numError, priceError, valueError, titleError)) {
                        this.$http.post("/api/comics", comics[i]).then((response) => {
                            comics[i] = null; 
                        });
                        count++;
                        message = count + " books added to database";
                    }
                }
            }
            return message;
        }

        constructor(private $http: ng.IHttpService) {

        }
    }
    angular.module('ComicBooks').service('myServices', MyServices);
}
