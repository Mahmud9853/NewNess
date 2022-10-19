$(document).on("click", ".del-images", function () {
    let item = $(this)
    $.ajax({
        url: "/Admin/News/DeleteNewsImage",
        type: "get",
        data: {
            "proImgId": item.next().val()
        },
        success: function (response) {
            item.parent().remove()
            if (response == "stop") {
                $(".del-images").remove()
            }
            console.log("test")
        }
    });

});
