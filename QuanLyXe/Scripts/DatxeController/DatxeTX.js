var DatxeTX = {
    init: function () {
        DatxeTX.Events();
        DatxeTX.Events1();
    },

    Events: function () {
        $('#LuuTT').off('click').on('click', function (e) {
            e.preventDefault();
            var madx = $(this).data('iddxtx');
            $.ajax({
                url: "/Taixe/DatxeTX/Capnhat",
                type: "POST",
                data: { id: madx },
                datatype: "json",
                success: function (res) {
                    location.reload();
                }
            });
        });
    },
    Events1: function () {
        $('#LuuTTKT').off('click').on('click', function (e) {
            e.preventDefault();
            var madx = $(this).data('iddxtxkt');
            $.ajax({
                url: "/Taixe/DatxeTX/CapnhatKT",
                type: "POST",
                data: { id: madx },
                datatype: "json",
                success: function (res) {
                    location.reload();
                }
            });
        });
    }

}

DatxeTX.init();