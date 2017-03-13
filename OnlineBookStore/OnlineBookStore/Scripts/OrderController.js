var order = {
    init: function () {
        order.changS();
        order.deleteO();
    },

    deleteO: function() {
        $('.delete-Order').off('click').on('click', function (e) {
            if (confirm('Bạn có muốn xóa bản ghi này?')) {
                e.preventDefault();
                var btn = $(this);
                var id = btn.data('cateid');
                $.ajax({
                    url: '/Admin/Order/Delete',
                    data: { id: id },
                    dateType: 'json',
                    type: 'POST',
                    success: function (res) {
                        if (res.Status == true) {
                            OnSuccess = $('#row_' + id + '').remove();
                        }
                    }
                });
            }
        });
    },

    changS: function () {
        $('.btn-Sta').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: '/Admin/Order/ChangeStatus',
                data: { id: id },
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.Status == true) {
                        btn.text('Đã thanh toán');
                    } else {
                        btn.text('Chưa thanh toán');
                    }
                }
            });
        });
    }
}

order.init();