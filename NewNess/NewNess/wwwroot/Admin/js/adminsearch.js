$(document).on("keyup", "#mysearch", function () {

    if ($("#mysearch").val().length > 0) {
        $.ajax({
            url: "/Admin/Dashboard/AdminSearch",
            type: "get",
            data: {
                "key": $("#mysearch").val()
            },
            success: function (response) {
                $("#list").empty()
                $("#list").append(response)
            }
        });
    }
    else {
        $("#list li ").remove()
        } 
});

//$(document).ready(function () {
//    $("#search").keyup(function () {
//        $("#result").hide("fast");

//        var newStrSearch = $("#search").val();

//        //var newStrSearch = $("#search").val();


//        $.ajax({
//            type: "POST",
//            url: "/Product/LiveTagSearch",
//            data: { search: newStrSearch },
//            success: function (categories) {
//                //code to show result data in View page
//            }
//        });
//    });
//});

