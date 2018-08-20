function AppDataModel() {
    var self = this;
    // Routes
    self.userInfoUrl = "/api/Me";
    self.moviesUrl = "/api/Movies";
    self.ordersUrl = "/api/Orders";
    self.addMovieUrl = "/OrderDetails/AddMovie";
    self.removeMovieUrl = "/OrderDetails/RemoveMovie";
    self.categoriesUrl = "/api/Categories";
    self.orderStatusesUrl = "/api/Statuses";
    self.siteUrl = "/";

    // Route operations

    // Other private operations

    // Operations

    // Data
    self.returnUrl = self.siteUrl;
    self.orderId = '';
    self.currentOrderId = '';

    // Data access operations
    self.setAccessToken = function (accessToken) {
        sessionStorage.setItem("accessToken", accessToken);
    };

    self.getAccessToken = function () {
        return sessionStorage.getItem("accessToken");
    };
}
