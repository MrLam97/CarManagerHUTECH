var Datxe = {
    init: function () {
        Datxe.Events();
        Datxe.EventXoa();
    },

    Events: function () {
        $('#huydexuat').off('click').on('click', function (e) {
            e.preventDefault();
            var madx = $(this).data('huy');
            $.ajax({
                url: "/Datxe/Huydexuat",
                type: "POST",
                data: { id: madx },
                datatype: "json",
                success: function (res) {
                    if (res.status == true) {
                        alert('Hủy thành công.');
                        window.location = res.url;
                    }
                }
            });
        });
    },

    EventXoa: function(){
        $('.xoadexuat').off('click').on('click', function (e) {
            e.preventDefault();
            var madx = $(this).data('xoa');
            $.ajax({
                url: "/Datxe/Xoadatxe",
                type: "POST",
                data: { id: madx },
                datatype: "json",
                success: function (res) {
                    if (res.status == true) {
                        alert('Xóa thành công.');
                        window.location = res.url;
                    }
                    else
                        if (res.status == false) {
                            alert('Đơn được thực hiện, không thể xóa.');
                            window.location = res.url;
                        }
                }
            });
        });
    }

}

Datxe.init();