// JavaScript source code
$(function () {
    var id_noteids = [];
    //divs with datanoteId attribute, push its data-noteId's to an array
    $("div[data-noteid]").each(function (i, e) {
        id_noteids.push($(e).data("noteid"))
        console.log(id_noteids.length)
    })

    $.ajax({
        method: "POST",
        url: "/Note/GetLikes",
        data: { id_arr: id_noteids }

    }).done(function (data) {
        if (data.result != null && data.result.length > 0) {
            for (var i = 0; i < data.result.length; i++) {
                var id = data.result[i];
                var div = $("div[data-noteid =" + id + "]");
                var btn = div.find("button[data-like]");
                btn.data("like", true);

                var span = btn.children().first();
                span.removeClass("glyphicon-heart-empty");
                span.addClass("glyphicon-heart");
            }
        }
    }).fail(function () {
        console.log("fail")
    })
    //QUERY FOR BUTTON PROBLEM
    $("button[data-like]").click(function () {
        
        var btn = $(this);
        var likeArg = btn.data("like");
        var noteId = btn.data("noteid")
        //Span has not setted put class in it
        var spanHeart = btn.find("span.like-heart");
        var spanLikenumb = btn.find("span.likenumber")
        $.ajax({
            method: "Post",
            url: "/Note/SetLike",
            data: { noteId: noteId, likeArg: !likeArg }
        }).done(function (data) {
            if (data.error) {
                alert("Failed To Like")
            } else {
                likeArgs = !likeArg;
                btn.data("like", likeArg)

                spanLikenumb.text(data.resultLike);

                spanHeart.removeClass("glyphicon-heart-empty")
                spanHeart.removeClass("glyphicon-heart")

                if (likeArg) {
                    spanHeart.addClass("glyphicon-heart")
                } else {
                    spanHeart.addClass("glyphicon-heart-empty")
                }
            }
        }).fail(function () {
            alert("Failed to Connect Server")
        })
    })
})
