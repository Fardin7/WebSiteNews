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

                var url = "/admin" + $(data).attr("action");
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
                        url: "/admin" + url,
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

function paging(newscount, pagenumber, newstype) {

    $.ajax({
        type: "POST",
        url: "/News/NewsPaging?newscount=" + newscount + "&pagenumber=" + pagenumber + "&newstype=" + newstype,
        datatype: "json",
        success: function (data) {
            var model = JSON.parse(data);

            $("#lastnewscontainer").empty();
            var result = '<div class="col-12">'
                +
                '<div class="recent-active dot-style d-flex dot-style" >';
            for (var i = 0; i < model.length; i++) {

                result = ' <div class="single-recent mb-100"> <div class="what-img"><img src="' + model[i].ImageAddress + '" />'

                    +
                    ' </div>'
                    +
                    '<div class="what-cap">'
                    +
                    '<span class="color1">' + model[i].Title + ' </span><h4>'
                    +
                    '<a href=' + model[i].Url + '>' + '</a>'
                    +
                    '  </h4>'
                    +
                    ' </div>'
                    +
                    ' </div>'




            }
            result += ' </div>'
                +
                ' </div>';
            $("#lastnewscontainer").appendTo(result);




        },
        error: function (ex) {

        }
    });

}
function arrowpaging(currentpage, pages, newstype, newscategory) {
    pagingnews(4, currentpage, newstype, newscategory);
    renderpaging(currentpage, pages, newstype, newscategory);
}
function viewpaging(newstype, newscategory) {

    $.ajax({
        type: "POST",
        url: "/NewsCategory/ViewPaging?newstype=" + newstype + "&newscategory=" + newscategory,
        datatype: "json",
        success: function (data) {
            var currentpage = data.pages > 3 ? 3 : data.pages;
            renderpaging(currentpage, data.pages, data.newstype, data.newscategory)
        },
        error: function (ex) {

        }
    });


}
function renderpaging(currentpages, pages, newstype, newscategory) {

    var leftarrow = "";
    var rightarrow = "";

    var rightcounter = currentpages + 1;
    var leftcounter = currentpages - 1;
    if (pages > 3 && currentpages - 2 > 1) {

        leftarrow = '<li  onclick="arrowpaging(' + leftcounter + ',' + pages + ',' + newstype + ',' + newscategory + ')"  class="page-item"><a class="page-link" ><span class="flaticon-arrow roted"></span></a></li>';
    }

    if (pages > 3 && currentpages < pages) {
        rightarrow = '<li  onclick="arrowpaging(' + rightcounter + ',' + pages + ',' + newstype + ',' + newscategory + ')" class="page-item"><a class="page-link" ><span class="flaticon-arrow right-arrow"></span></a></li>';
    }

    $("#pagingcontainer").empty();
    var result =
        '<div class="col-xl-12">'
        +
        '<div class="single-wrap d-flex justify-content-center"  >'
        +

        '<nav aria-label="Page navigation example"   >'
        +
        '<ul class="pagination justify-content-start">'
        +
        leftarrow
    for (var i = (currentpages) < 3 ? 1 : currentpages - 2; i <= currentpages; i++) {

        result += "<li class='page-item' id='" + i + "'><a class='page-link'   onclick='pagingnews(4" + "," + i + "," + newstype + "," + newscategory + ")'>" + i + "</a></li>";

    }

    result += rightarrow
        +
        '</ul>'
        +
        '</nav>'
        +
        '</div>'
        +
        '</div >';

    $("#pagingcontainer").append(result);
    $("#" + currentpages).css("color", "red");

}
function pagingnews(newscount, pagenumber, newstype, newscategory) {
    var previuseitemid = pagenumber - 1;
    var nextitemid = pagenumber + 1;
    $("#" + previuseitemid).removeAttr("style");
    $("#" + nextitemid).removeAttr("style");
    $("#" + pagenumber).css("color", "red");
    $.ajax({
        type: "POST",
        url: "/NewsCategory/PagingNewsOfNewsCategory?newscount=" + newscount + "&pagenumber=" + pagenumber + "&type=" + newstype + "&newscategory=" + newscategory,
        datatype: "json",
        success: function (data) {
            var model = JSON.parse(data);

            $("#" + newscategory).empty();

            var result = '<div class="whats-news-caption"><div class="row" >';

            for (var i = 0; i < model.length; i++) {

                result +=
                    '<div class="col-lg-6 col-md-6">'
                    +
                    ' <div class="single-what-news mb-100">'
                    +
                    '<div class="what-img">'
                    +
                    '<img style="height:370px;width:344.5px; "' + 'src="' + model[i].ImageAddress + '"' + 'alt="' + model[i].Title + '">'
                    +
                    '</div>'
                    +
                    '<div class="what-cap">'
                    +
                    '<span class="color1">'
                    + model[i].Title +
                    '</span>'
                    +
                    '<h4>'
                    +
                    '<a  href="' + model[i].Url + '">'
                    +
                    model[i].Description
                    +
                    '</a>'
                    +
                    '</h4>'
                    +
                    '</div>'
                    +
                    '</div>'
                    +
                    '</div>'
                    ;

            }
            result += '</div></div>';
            $("#" + newscategory).append(result);

        },
        error: function (ex) {

        }
    });

}

function pagingrelatednews(type, categoryname, newscategoryname, count, pagenumber, pagecount) {

    var rightcounter = pagenumber + 1;
    var leftcounter = rightcounter - 2;

    $.ajax({
        type: "POST",
        url: "/News/NewsOfNewsCategoryAndCategoryPaging?type=" + type + "&categoryname=" + categoryname + "&newscategoryname=" + newscategoryname + "&count=" + count + "&pagenumber=" + pagenumber,
        datatype: "json",
        success: function (data) {
            var model = JSON.parse(data);
            if (model.length > 0) {
                $("#relatednews").empty();
                var leftarrow = "";
                var rightarrow = "";
                if (leftcounter >= 1 && leftcounter < pagecount) {
                    leftarrow = '<div  style="cursor:pointer;"     onclick="pagingrelatednews(' + type + ',' + categoryname + ',' + newscategoryname + ',' + count + ',' + leftcounter + ',' + pagecount + ')"' + '  class="col-lg-6 col-md-6 col-12 nav-left flex-row d-flex justify-content-start align-items-center" >'



                }
                else {
                    leftarrow = '<div class="col-lg-6 col-md-6 col-12 nav-left flex-row d-flex justify-content-start align-items-center">'


                }
                if (rightcounter <= pagecount && rightcounter > 1) {
                    rightarrow = '<div style="cursor:pointer;" class="thumb" onclick="pagingrelatednews(' + type + ',' + categoryname + ',' + newscategoryname + ',' + count + ',' + rightcounter + ',' + pagecount + ')"  >'

                }
                else {
                    rightarrow = '<div class="thumb">'
                }
                var result = "";

                result += '<div class="row">'
                    +

                    leftarrow
                    +
                    '<div class="thumb" >'
                    +
                    '<a>'
                    +

                    '<img class="img-fluid" style="width:60px;height:60px;" src="' + model[0].ImageAddress + '" alt="">'
                    +

                    '</a>'
                    +
                    '</div>'
                    +
                    '<div class="arrow">'
                    +
                    '<a>'
                    +
                    '<span class="lnr text-white ti-arrow-left"></span>'
                    +
                    '</a>'
                    +
                    '</div>'
                    +
                    '<div class="detials" >'
                    +
                    '<p>قبل</p>'
                    +
                    '<a href="' + model[0].Url + '">'
                    +
                    '<h4>' + model[0].Title + '</h4>'
                    +
                    '</a>'
                    +
                    '</div>'
                    +
                    '</div >'
                    +
                    '<div class="col-lg-6 col-md-6 col-12 nav-right flex-row d-flex justify-content-end align-items-center">'
                    +
                    '<div class="detials">'
                    +
                    '<p>بعد</p>'
                    +
                    '<a href="' + model[1].Url + '">'
                    +
                    '<h4 >' + model[1].Title + '</h4>'
                    +
                    '</a>'
                    +
                    '</div>'
                    +

                    '<div class="arrow">'
                    +
                    '<a >'
                    +
                    '<span class="lnr text-white ti-arrow-right"></span>'
                    +
                    '</a>'
                    +
                    '</div>'

                    +
                    rightarrow
                    +
                    '<a>'
                    +
                    '<img class="img-fluid" style="width:60px;height:60px;"  src="' + model[1].ImageAddress + '"  alt="">'
                    +
                    '</a>'
                    +
                    '</div >'
                    +
                    '</div >'
                    +
                    '</div >'
                    ;

                $("#relatednews").append(result);

            }

        },
        error: function (ex) {

        }
    });


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



function newsofnewsandnewscategorypaging(type, categoryname, newscategoryname, count, currentpages, pagecount) {


    newsofcategorandnewscategory(count, currentpages, type, newscategoryname, categoryname);
    var rightcounter = currentpages + 1;
    var leftcounter = currentpages - 1;


    $("#newspagingcontiner").empty();
    var leftarrow = "";
    var rightarrow = "";
    if (leftcounter >= 1 && leftcounter < pagecount) {
        leftarrow = '<li   style="cursor:pointer;" class="page-item"  onclick="newsofnewsandnewscategorypaging(' + type + ',' + categoryname + ',' + newscategoryname + ',' + count + ',' + leftcounter + ',' + pagecount + ')">'
            +
            '<a  class="page-link" aria-label="Previous">'
            +
            '<i class="ti-angle-left"></i>'

        '</a>'
            +
            '</li>'
            ;


    }
    else {


        leftarrow = '';
    }
    if (rightcounter <= pagecount && rightcounter > 1) {

        rightarrow = '<li   style="cursor:pointer;" class="page-item"  onclick="newsofnewsandnewscategorypaging(' + type + ',' + categoryname + ',' + newscategoryname + ',' + count + ',' + rightcounter + ',' + pagecount + ')">'
            +
            '<a  class="page-link" aria-label="Next">'
            +
            '<i class="ti-angle-right"></i>'

        '</a>'
            +
            '</li>'
            ;

    }
    else {
        rightarrow = '';
    }
    var result = '<ul class="pagination">';
    result += leftarrow;
    for (var i = (currentpages) < 3 ? 1 : currentpages - 1; i <= currentpages; i++) {

        result += '<li  id="'+i+'" class="page-item" onclick="newsofcategorandnewscategory(' + count + ', ' + i + ', ' + type + ',' + newscategoryname + ', ' + categoryname + ')">'
            +
            '<a  class="page-link">' + i + '</a>'
            +
            '</li>';

    }
    result += rightarrow;
    result += '</ul >';

    $("#newspagingcontiner").append(result);
  
    $("#newspagingcontiner" +"  #" + currentpages).css("color", "red")


}
function newsofcategorandnewscategory(newscount, pagenumber, newstype, newscategory, category) {
    var previuseitemid = pagenumber - 1;
    var nextitemid = pagenumber + 1;
    $("#" + previuseitemid).removeAttr("style");
    $("#" + nextitemid).removeAttr("style");
    $("#newspagingcontiner" + "  #" + pagenumber).css("color", "red")
    $.ajax({
        type: "POST",
        url: "/News/NewsOfNewsCategoryAndCategoryPaging?count=" + newscount + "&pagenumber=" + pagenumber + "&type=" + newstype + "&categoryname=" + category + "&newscategoryname=" + newscategory,
        datatype: "json",
        success: function (data) {
            var model = JSON.parse(data);

            $("#newsofnewscategoryandcateory").empty();

            var result = '';

            for (var i = 0; i < model.length; i++) {

                result +=
                    '<article class="blog_item">'
                    +
                    '<div class="blog_item_img">'
                    +
                    '<img style="width:770px;height:385px;" class="card-img rounded-0" src="' + model[i].ImageAddress + '" alt="">'

                    +

                    '<a href="' + model[i].Url + '" class="blog_item_date">'

                    +
                    '<h3>تاریخ به فارسی</h3>'
                    +
                    '<p> تاریخ به فارسی</p>'
                    +
                    '</a>'
                    +
                    ' </div>'
                    +

                    '<div class="blog_details">'
                    +
                    '<a class="d-inline-block" href="' + model[i].Url + '">'
                    +
                    '<h2>' + model[i].Title + '</h2 >'
                    +
                    '</a>'
                    +
                    '<p>'
                    +
                    model[i].Description
                    +
                    '</p>'
                    +
                    '<ul class="blog-info-link">'
                    +
                    '<li><a ><i class="fa fa-user"></i>' + model[i].SubCategoryTitle + ',' + model[i].NewsCategoryTitle + '</a ></li >'
                    +
                    '<li><a ><i class="fa fa-comments"></i> 03  بازدید</a></li>'
                    +
                    '</ul>'
                    +
                    '</div>'
                    +
                    '</article>'
                    ;

            }

            $("#newsofnewscategoryandcateory").append(result);

        },
        error: function (ex) {

        }
    });

}