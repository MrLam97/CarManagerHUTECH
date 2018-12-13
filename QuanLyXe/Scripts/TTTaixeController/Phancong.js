var Taixe = {
    init: function () {
        Taixe.Events();
    },

    Events: function () {
        $('#btnLuu').off('click').on('click', function (e) {
            e.preventDefault();
            var ngaybd = $('#Ngaybd').val().toString();
            var ngaykt = $('#Ngaykt').val().toString();
            var taixe = $('#taixe').val().toString();
            var xe = $('#xe').val().toString();
            var ghichu = $('#ghichu').val().toString();
            // alert("ket qua: "+ ngaybd + " + "+ ngaykt +" + "+taixe+" + "+xe+" + "+ghichu);
            $('aaaa').empty();
            $('aaaa').append("<p>" + "doanxem" + "</p>");
            $.ajax({
                url: "/TTTaixe/PhancongTX",
                type: "POST",
                data:{
                    ngaybd: ngaybd,
                    ngaykt: ngaykt,
                    xe: xe,
                    taixe: taixe,
                    ghichu:ghichu
                },
                datatype: "json",
                success: function (res) {
                    if (res.status == true) {
                        var input = "<tr><td>" + taixe + "</td><td>" + xe + "</td><td>" + ghichu + "</td><td>" + ngaybd + "</td></tr>";
                        $('#datatable>tbody').append(input);
                        alert("Đã lưu");
                    }
                    }
            });
        });
    }

}

Taixe.init();



