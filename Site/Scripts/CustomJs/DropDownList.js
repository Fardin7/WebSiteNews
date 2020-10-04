function selectsub(ddcontext, tagid, btnid, dataname) {
    var id = ddcontext.value;
    var text = ddcontext.text;
    if (id != "") {
        $("#" + tagid).removeAttr("hidden")
        $("#" + btnid).removeAttr("hidden")
        $("#" + btnid).attr("data-id", id);
        $("#" + btnid).attr("data-name", dataname);
        $.ajax({
            type: "GET",
            url: "/admin/News/FillNewsCategory/",
            contentType: "application/json; charset=utf-8",
            data: { "id": id, "categorytype": tagid },
            datatype: "json",
            success: function (data) {
                $("#" + tagid).empty();

                var lankanListArray = JSON.parse(data);
                $.each(lankanListArray, function () {
                    $("#" + tagid).append($("<option></option>").val(this.Id).html(this.Title));
                });

            },
            error: function (ex) {
                alert(ex);
            }
        });
    }

}

$(function () {
    $(".btn-flat").click(function () {
        var dropdownid = $(this).attr('dropdown-id');
        var controllername = $(this).attr('controller-name');
        var dataid = $(this).attr("data-id");
        var dataname = $(this).attr("data-name");
        var TeamDetailPostBackURL = '/' + controllername + '/Create';
        var options = { "backdrop": "static", keyboard: true };
        $.ajax({
            type: "GET",
            url: "/admin" + TeamDetailPostBackURL,
            contentType: "application/json; charset=utf-8",
            datatype: "json",

            success: function (data) {

                var url = $(data).attr("action");
                var formid = $(data).attr("id");
                $('.modal-body').html(data);
                $('#myModal').modal(options);
                $('#myModal').modal('show');
                $("#submitbtn").unbind();
                if (dataid != undefined && dataname != undefined) {
                    $("#" + formid).append("<input name=" + dataname + "  value=" + dataid + "  id=" + dataname + " style='visibility:hidden'/>");

                }

                $("#submitbtn").click(function () {

                    $.ajax({
                        type: "POST",
                        url:  url,
                        data: $("#" + formid).serialize(),
                        success: function (data) {
                            $("#" + dropdownid).empty();
                            var lankanListArray = JSON.parse(data);
                            $.each(lankanListArray, function () {
                                $("#" + dropdownid).append($("<option></option>").val(this.Id).html(this.Title));
                            });
                            $('#myModal').modal('hide');

                        },
                        error: function (ex) {
                            alert(ex);
                        }
                    });


                })
            },
            error: function () {
                alert("Dynamic content load failed.");
            }
        });
    });
    $("#closbtn").click(function () {
        $('#myModal').modal('hide');
    });
});

//$("#selectfile ").change(function () {
function uploadfile(uploadedfile, gifid, url) {
    $("#" + gifid).css("visibility", "visible");
    if (window.FormData !== undefined) {

        var fileUpload = uploadedfile;
        //$("#selectfile").get(0);
        var files = fileUpload.files;

        // Create FormData object  
        var fileData = new FormData();

        // Looping over all files and add it to FormData object  
        for (var i = 0; i < files.length; i++) {
            fileData.append(files[i].name, files[i]);
        }


        $.ajax({
            url: url,
            type: "POST",
            contentType: false, // Not to set any content header  
            processData: false, // Not to process data  
            data: fileData,
            success: function (result) {

                $("#" + gifid).css("visibility", "hidden")
            },
            error: function (err) {
                alert(err.statusText);
            }
        });
    } else {
        alert("FormData is not supported.");
    }
}


//})

var deleteclick = function () {
    $.ajax({
        type: "GET",
        url: "/admin/News/DeleteFile",

        success: function (data) {
            if (data) {
                $("#filedownloadlink").remove();
                $("#deletefile").remove();
            }
        },
        error: function (ex) {

        }
    });

}
function DeleteImage(addressimage) {
    $.ajax({
        type: "GET",
        url: "/admin/News/DeleteImage",
        data: addressimage,
        success: function (data) {
            if (data) {
                $("#newsimage").remove();
                $("#deleteimage").remove();
            }
        },
        error: function (ex) {

        }
    });

}
$("#deletefile").click(deleteclick)




$("#submitnewsform").click(function (e) {
    if ($("#newsform").valid()) {

        var subcategory = $("#SubcategoryId").val();
        var newssubcategory = $("#NewsSubcategoryId").val();
        if (subcategory == null || newssubcategory == null) {
            $("#Subcategoryvld").html(" please select subcategory!");
            $("#NewsSubcategoryvld").html("please select newssubcategory!");

            e.preventDefault();
        }

    }
})
$("#CategoryId ").change(function () {
    $("#Subcategoryvld").empty();


})
$("#NewsCategoryId").change(function () {

    $("#NewsSubcategoryvld").empty();

})
$("#submiteditform").click(function () {
    var formdata = {};

    var token = $('input[name=__RequestVerificationToken]').val();

    formdata.Id = $("#Id").val();
    formdata.Title = $("#Title").val();
    formdata.Description = $("#Description").val();
    formdata.Body = $('.textarea').val();
    formdata.KeyWord = $("#tags").val();
    formdata.PublishDate = null;
    //$('#filter-date').datetimepicker('getValue');
    formdata.IsActive = $("#IsActive").is(':checked');
    formdata.ImageAddress = $("#newsimage").attr("src");
    formdata.SubcategoryId = $("#SubcategoryId").val();
    formdata.NewsType = $('input[name=NewsType]:checked', '#editform').val();
    formdata.NewsSubcategoryId = $("#NewsSubcategoryId").val();

    $.ajax({
        type: "POST",
        url: "/admin/News/Edit",
        dataType: 'json',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: {
            __RequestVerificationToken: token,
            News: formdata
        },

        success: function (data) {

            if (data.data == "1") {

                if (data.existfile) {
                    $("#uploadfileholder").empty();
                    $("#uploadfileholder").append("<a href=/admin/News/GenerateFile/?filename=" + data.existfilename + " id='filedownloadlink'>" + data.existfilename + "</a>"
                        +

                        "<img src='/PanelFile/Icons/deletefileicon.png' style='width:25px;' id='deletefile' />")
                    $("#deletefile").click(deleteclick);
                }
                alert("تغییرات با موفقیت انجام شد")
            }
            else {
                alert("fail1")
            }

        },
        error: function (ex) {
            alert(ex);
        }
    });


})


// Delete news
var DeleteNews = function (newsid) {

    var deleteconfirm = confirm("از حذف خبر مطمئن هستید؟");
    if (deleteconfirm) {
        $.ajax(
            {
                type: "GET",
                url: "/admin/News/Delete",
                data: { id: newsid },
                success: function (data) {
                    if (data) {
                        alert("حذف شد!");
                        window.location.href = "/admin/News/Index";

                    }
                }
                ,
                error: function (ex) {
                    alert(ex);
                }

            }

        )
    }
}



$("#submitcommentform").click(function () {
    if ($("#commentForm").valid()) {
        $.ajax({
            type: "POST",
            url: "/Comment/Insert",
            data: {
                Description: $("#Description").val(),
                Name: $("#Name").val(),
                Email: $("#Email").val(),
                NewsId: $("#NewsId").val()

            },
            datatype: "json",
            success: function (data) {


                var result = JSON.parse(data);
                $("#resultofsendcomment").text(result.message)
            },
            error: function (ex) {
                alert(ex);
            }
        });
    }



})

$("#registernewsletter").click(function () {
    if ($("#newsletterForm").valid()) {
        $.ajax({
            type: "POST",
            url: "/NewsLetter/Create",
            data: {
                Email: $("#newsletterForm #Email").val(),


            },
            datatype: "json",
            success: function (data) {


                var result = JSON.parse(data);
                $("#newsletterregisterresult").text(result.message)
            },
            error: function (ex) {
                alert(ex);
            }
        });
    }



})
$("#newsletter-submit").click(function () {
    if ($("#footernewsletterForm").valid()) {
        $.ajax({
            type: "POST",
            url: "/NewsLetter/Create",
            data: {
                Email: $("#footernewsletterForm #Email").val(),


            },
            datatype: "json",
            success: function (data) {


                var result = JSON.parse(data);
                $("#resultoffooternewsletter").text(result.message)
            },
            error: function (ex) {
                alert(ex);
            }
        });
    }



})

$("#submitcontactus").click(function () {
    if ($("#contactForm").valid()) {
        $.ajax({
            type: "POST",
            url: "/Contact/Create",
            data: {
                Description: $("#Description").val(),
                Name: $("#Name").val(),
                Email: $("#Email").val()


            },
            datatype: "json",
            success: function (data) {


                var result = JSON.parse(data);
                $("#resultofconactus").text(result.message)
            },
            error: function (ex) {
                alert(ex);
            }
        });
    }



})
