var cart = {
    init: function () {
        cart.regEvents();
    },
    regEvents: function () {
        $('#btnviewCart').off('click').on('click', function () {
            window.location.href = '/gio-hang'
        });

        $('.btnContinue').off('click').on('click',function(){
            window.location.href = "/trang-chu"
        });

        $('#btnPayment').off('click').on('click', function () {
            window.location.href = "/thanh-toan"
        });

        $('.btn-delete').off('click').on('click', function (e) {
            e.preventDefault();
            $.ajax({
                url: 'Cart/Delete',
                data: {id: $(this).data('id')},
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.Status == true) {
                        window.location.href = "/gio-hang"
                    }
                }
            });
        });

        $('#btnDeleteAll').off('click').on('click', function () {
            $.ajax({
                url: 'Cart/DeleteAll',
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.Status == true) {
                        window.location.href = "/gio-hang"
                    }
                }
            })
        });

        $('#btnUpdate').off('click').on('click', function () {
            var listQuanlity = $('.txtQuanlity');
            var cartList = [];
            $.each(listQuanlity, function (i, item) {
                cartList.push({
                    Quanlity: $(item).val(),
                    Product: {
                        ProductID: $(item).data('id')
                    }
                });
            });

            $.ajax({
                url: 'Cart/Update',
                data: { cartModel: JSON.stringify(cartList) },
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.Status == true) {
                        window.location.href="/gio-hang"
                    }
                }
            })
        });
    }
}

cart.init();

