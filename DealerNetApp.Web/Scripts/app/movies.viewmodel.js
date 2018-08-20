function MoviesViewModel(app, dataModel) {
    var self = this;

    self.movieList = ko.observableArray();
    self.categoryList = ko.observableArray();
    self.movieFilter = ko.observable('');

    self.addToOrder = function (m) {
        var query = '?movieId=' + m.id;
        if (app.dataModel.orderId != '')
            query += '&orderId=' + app.dataModel.orderId;
        $.ajax({
            method: 'get',
            url: app.dataModel.addMovieUrl + query,
            contentType: "application/json; charset=utf-8",
            headers: {
                'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
            },
            success: function (data) {
                $.notify("Movie added to order");
                app.dataModel.orderId = data.Id;
            }
        });
    }

    self.showAll = function (c) {
        self.movieFilter('');
        $.ajax({
            method: 'get',
            url: app.dataModel.moviesUrl,
            contentType: "application/json; charset=utf-8",
            headers: {
                'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
            },
            success: function (data) {
                self.movieList(data);
            }
        });
    }

    self.showCat = function (c) {
        $.ajax({
            method: 'get',
            url: app.dataModel.moviesUrl + '?catId=' + c.id,
            contentType: "application/json; charset=utf-8",
            headers: {
                'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
            },
            success: function (data) {
                self.movieList(data);
            }
        });
    }

    self.SearchMovie = function () {

        $.ajax({
            method: 'get',
            url: app.dataModel.moviesUrl + '?prodName=' + self.movieFilter(),
            contentType: "application/json; charset=utf-8",
            headers: {
                'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
            },
            success: function (data) {
                self.movieList(data);
            }
        });
    }


    Sammy(function () {
        this.get('#movies', function () {
            $(".view").hide();
            $("#movies").show();

            // Make a call to the protected Web API by passing in a Bearer Authorization Header
            $.ajax({
                method: 'get',
                url: app.dataModel.moviesUrl,
                contentType: "application/json; charset=utf-8",
                headers: {
                    'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
                },
                success: function (data) {
                    self.movieList(data);
                }
            });

            $.ajax({
                method: 'get',
                url: app.dataModel.categoriesUrl,
                contentType: "application/json; charset=utf-8",
                headers: {
                    'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
                },
                success: function (data) {
                    self.categoryList(data);
                }
            });
        });
        this.get('/', function () { this.app.runRoute('get', '#movies') });
    });

    return self;
}

app.addViewModel({
    name: "Movies",
    bindingMemberName: "movies",
    factory: MoviesViewModel
});
