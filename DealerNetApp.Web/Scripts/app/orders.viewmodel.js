function OrdersViewModel(app, dataModel) {
    var self = this;

    self.myHometown = ko.observable("");
    self.orderList = ko.observableArray();

    self.showOrder = function (m) {
        app.dataModel.currentOrderId = m.id;
        app.navigateToOrderDetail();
    }

    Sammy(function () {
        this.get('#orders', function () {
            $(".view").hide();
            $("#orders").show();
            app.dataModel.orderId = '';
            // Make a call to the protected Web API by passing in a Bearer Authorization Header
            $.ajax({
                method: 'get',
                url: app.dataModel.ordersUrl,
                contentType: "application/json; charset=utf-8",
                headers: {
                    'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
                },
                success: function (data) {
                  var _data = data.map(function (d) {
                        d.orderDate = new Date(d.orderDate).toDateString();
                        return d;
                  });
                    self.orderList(_data);
                }
            });
        });
        this.get('/', function () { this.app.runRoute('get', '#orders') });
    });

    return self;
}

app.addViewModel({
    name: "Orders",
    bindingMemberName: "orders",
    factory: OrdersViewModel
});
