function OrderDetailViewModel(app, dataModel) {
    var self = this;

    self._order = ko.observableArray();
    self.orderNum = ko.observableArray();
    self.orderDate = ko.observableArray();
    self.statusDesc = ko.observableArray();
    self.totalCost = ko.observableArray();
    self.Items = ko.observableArray();

    self.addMoreItems = function () {
            app.dataModel.orderId = app.dataModel.currentOrderId;
            app.navigateToMovies();
    }

    self.addItem = function (m) {
        var query = '?movieId=' + m.movie.id;
        query += '&orderId=' + app.dataModel.currentOrderId;
        $.ajax({
            method: 'get',
            url: app.dataModel.addMovieUrl + query,
            contentType: "application/json; charset=utf-8",
            headers: {
                'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
            },
            success: function (data) {
                var num = (+(m.qty()))
                num++;
                m.qty(num);
                self.totalCost(data.TotalCost);
                $.notify("Movie added to order");
            }
        });
    }

    self.deleteItem = function (m) {
        app.dataModel.currentOrderId;
        var query = '?movieId=' + m.movie.id;
        query += '&orderId=' + m.movieOrderId;
        $.ajax({
            method: 'get',
            url: app.dataModel.removeMovieUrl + query,
            contentType: "application/json; charset=utf-8",
            headers: {
                'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
            },
            success: function (data) {
                self.totalCost(data.TotalCost);
                var num = (+(m.qty()))
                num--;
                m.qty(num);
                if(num < 1)
                self.Items.remove(m);
                $.notify("Movie removed from order");
            }
        });
    }

    Sammy(function () {
        this.get('#orderdetail', function () {
            if (app.dataModel.currentOrderId == '')
            app.navigateToHome();
            $(".view").hide();
            $("#orderdetail").show();
            app.dataModel.orderId = '';
            // Make a call to the protected Web API by passing in a Bearer Authorization Header
            $.ajax({
                method: 'get',
                url: app.dataModel.ordersUrl + '/' + app.dataModel.currentOrderId,
                contentType: "application/json; charset=utf-8",
                headers: {
                    'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
                },
                success: function (data) {
                    self.orderNum(data.orderNum);
                    self.orderDate(new Date(data.orderDate).toDateString());
                    
                    self.totalCost(data.totalCost);
                    var mapped = data.orderDetails.map(function (d) {
                        d.qty = ko.observable(d.qty);
                        return d;
                    });
                    self.Items(mapped);
                    self.statusDesc(data.orderStatu.statusDesc);
                    self._order(data);
                }
            });
        });
        this.get('/', function () { this.app.runRoute('get', '#orderdetail') });
    });

    return self;
}

app.addViewModel({
    name: "OrderDetail",
    bindingMemberName: "orderdetail",
    factory: OrderDetailViewModel
});
