

function AddPosition() {
    var positions = document.getElementsByClassName("position-selector");
    var maxId = 0;
    Array.from(positions).forEach((item) => {
        var id = Number(item.id);
        if (id > maxId) maxId = id;
    });

    console.log(positions);

    $.ajax({
        type: "GET",
        url: "/Payments/GetPartialPosition/" + (maxId + 1),
        success: function (res) {
            $('#positions').append(res);
        }
    });

    var form = document.getElementById("addForm");
    $(form).removeData("validator")  
        .removeData("unobtrusiveValidation");  
    $.validator.unobtrusive.parse(form);
};

function DeletePosition(index) {
    var positions = document.getElementById("positions");
    var position = document.getElementById(index);
    positions.removeChild(position);
    var form = document.getElementById("addForm");
    $(form).removeData("validator")
        .removeData("unobtrusiveValidation");
    $.validator.unobtrusive.parse(form);

}
