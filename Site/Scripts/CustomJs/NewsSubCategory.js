$("#newssubcategorysubmitbtn").click(function () {

    $.ajax({
        type: "POST",
        url: "/NewsSubCategories/Create",
        data: $("#NewsSubcategoriesfrm").serialize(),
        success: function (data) {
            $("#NewsSubCategorytable").empty();
            var lankanListArray = JSON.parse(data);
            $.each(lankanListArray, function () {
                
                $("#NewsSubCategorytable").append("<tr id='" + this.Id+"' ><td>" + this.Title + "</td>" + "<td>" +
                    "<td><a href='/admin/NewsSubCategories/Edit/" + this.Id + "'>ویرایش</a></td> <td><button    onclick=deletenewsSubcategories('" + this.Id +"') >حذف</button></td></tr>").val(this.Id);
            });
          
        },
        error: function (ex) {
            alert(ex);
        }
    });


})
function deletenewsSubcategories(id) {

    $.ajax({
        type: "post",
        url: "/admin/NewsSubCategories/Delete/"+id,
        success: function (data) {
            if (data == "ok") {
                $("#NewsSubCategorytable #" + id).remove();
            }
            else {
                alert(data)
            }
            //$("#Subcategoriestable").empty();
            //    var lankanListArray = JSON.parse(data);
            //    $.each(lankanListArray, function () {
            //        $("#Subcategoriestable").append("<tr><td>" + this.Title + "</td>" + "<td>" + "<img  style='width:210px;height:160px;' src='" + this.ImageAddress + "'>" + "</td>" +
            //            "<td><a href='/admin/Subcategories/Edit/" + this.Id + "'>ویرایش</a></td> <td><button   onclick=deleteSubcategories('" + this.Id + "') >حذف</button></td></tr>").val(this.Id);
            //    });           

        },
        error: function (ex) {
            alert(ex);
        }
    });
}


