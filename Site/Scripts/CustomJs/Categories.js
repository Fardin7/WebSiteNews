$("#categorysubmitbtn").click(function () {

    $.ajax({
        type: "POST",
        url: "/Categories/Create",
        data: $("#categoryfrm").serialize(),
        success: function (data) {
            $("#categorytable").empty();
            var lankanListArray = JSON.parse(data);
            $.each(lankanListArray, function () {
                
                $("#categorytable").append("<tr><td>" + this.Title + "</td>" + "<td>" + "<img  style='width:210px;height:160px;' src='" + this.ImageAddress + "'>" + "</td>" +
                    "<td><a href='/admin/Categories/Edit/" + this.Id + "'>ویرایش</a></td> <td><button    onclick=deletecategory('" + this.Id +"') >حذف</button></td></tr>").val(this.Id);
            });
          
        },
        error: function (ex) {
            alert(ex);
        }
    });


})
function deletecategory(id) {

    $.ajax({
        type: "post",
        url: "/admin/Categories/Delete/"+id,
        success: function (data) {          
                $("#categorytable").empty();
                var lankanListArray = JSON.parse(data);
                $.each(lankanListArray, function () {
                    $("#categorytable").append("<tr><td>" + this.Title + "</td>" + "<td>" + "<img  style='width:210px;height:160px;' src='" + this.ImageAddress + "'>" + "</td>" +
                        "<td><a href='/admin/Categories/Edit/" + this.Id + "'>ویرایش</a></td> <td><button   onclick=deletecategory('" + this.Id + "') >حذف</button></td></tr>").val(this.Id);
                });           

        },
        error: function (ex) {
            alert(ex);
        }
    });
}

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

