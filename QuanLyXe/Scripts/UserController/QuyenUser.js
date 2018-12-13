var home = {
    init: function () {
        home.Events();
    },

    Events: function () {

        $('.hien').off('click').on('click', function (e) {
            e.preventDefault();
            var tendn = $(this).data('id');
            $.ajax({
                url: "/User/Quyenhan",
                type: "POST",
                data: { tendn: tendn },
                datatype: "json",
                success: function (res) {
                    $('#quyen').empty();
                    $.each(res.lstQuyen, function (i, item) {
                        var input = '<li><input type="checkbox" class="chonquyen" value="' + this['_tendn'] + '" data-idq="' + this['_maquyen'] + '" ' + this['_check'] + ' /> ' + this['_tenquyen'] + '</li>';
                        $('#quyen').append(input);
                        $('#Tendn').text('Tên đăng nhập: ' + this['_tendn']);
                    });
                    home.Events1();
                }
            });
        });
    },

    Events1: function () {

        $('.chonquyen').off('click').on('click', function (e) {
            e.preventDefault();
            var tendn = $(this).val();
            var maquyen = $(this).data('idq');
            $.ajax({
                url: "/User/Capnhatquyen",
                type: "POST",
                data: {
                    tendn: tendn,
                    maquyen: maquyen
                },
                datatype: "json",
                success: function (res) {
                    $('#quyen').empty();
                    $.each(res.lstQuyen, function (i, item) {
                        var input = '<li><input type="checkbox" class="chonquyen" value="' + this['_tendn'] + '" data-idq="' + this['_maquyen'] + '" ' + this['_check'] + ' /> ' + this['_tenquyen'] + '</li>';
                        $('#quyen').append(input);
                        $('#Tendn').text('Tên đăng nhập: ' + this['_tendn']);
                    });
                    home.Events1();
                    $('#thongbao').text('Thay đổi thành công');
                    setTimeout(function () { $('#thongbao').empty(); }, 1500);
                }
            });
        });
    },

}


home.init();