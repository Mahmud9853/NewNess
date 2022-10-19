$(document).on("keyup", "#globalsearch", function () {

    if ($("#globalsearch").val().length > 0) {
        $.ajax({
            url:  "/Home/GlobalSearch",
            type: "get",
            data: {
                "key": $("#globalsearch").val()
            },
            success: function (response) {
                $("#mylist").empty()
                $("#mylist").append(response)
            }
        });
    } else {
        $("#mylist").empty()
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

