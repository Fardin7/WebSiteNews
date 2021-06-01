$("#NewsCategorysubmitbtn").click(function () {

    $.ajax({
        type: "POST",
        url: "/NewsCategories/Create",
        data: $("#NewsCategoryfrm").serialize(),
        success: function (data) {
            $("#NewsCategorytable").empty();
            var lankanListArray = JSON.parse(data);
            $.each(lankanListArray, function () {
                
                $("#NewsCategorytable").append("<tr id='" + this.Id + "' ><td>" + this.Title + "</td>"  +
                    "<td><a href='/admin/NewsCategories/Edit/" + this.Id + "'>ویرایش</a></td> <td><button    onclick=deleteNewsCategory('" + this.Id +"') >حذف</button></td></tr>").val(this.Id);
            });
          
        },
        error: function (ex) {
            alert(ex);
        }
    });


})
function deleteNewsCategory(id) {

    $.ajax({
        type: "post",
        url: "/admin/NewsCategories/Delete/"+id,
        success: function (data) {   

            if (data == "ok") {
                $("#NewsCategorytable #" + id).remove();
            }
            else {
                alert(data)
            }
            //$("NewsCategorytable").empty();
            //    var lankanListArray = JSON.parse(data);
            //    $.each(lankanListArray, function () {
            //        $("#NewsCategorytable").append("<tr><td>" + this.Title + "</td>" + "<td>" + "<img  style='width:210px;height:160px;' src='" + this.ImageAddress + "'>" + "</td>" +
            //            "<td><a href='/admin/NewsCategory/Edit/" + this.Id + "'>ویرایش</a></td> <td><button   onclick=deleteNewsCategory('" + this.Id + "') >حذف</button></td></tr>").val(this.Id);
            //    });           

        },
        error: function (ex) {
            alert(ex);
        }
    });
}


