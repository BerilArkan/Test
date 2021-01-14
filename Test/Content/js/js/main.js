 
//Kategori Ekle
function fnSaveCategory() {

    var category = $('#productCategory').val();
     
    $.ajax({
        type: "POST",
        url: "/Home/SetProductCategoryList?category=" + category,
        dataType: "json",
        data: {},
        success: function (result) {
            if (result.success == true) {
                fnSweetAlert('success', result.message, 2500);
                var delayInMilliseconds = 1500;
                setTimeout(function () {
                    window.location.reload();
                }, delayInMilliseconds);
            }
            else {
                fnDirectLoginPage();
            }
        },
        error: function (jqXHR, textStatus, error) {
            if (jqXHR && jqXHR.status == 200) {
                fnSessionCheck(jqXHR);
            } else if (jqXHR && jqXHR.status == 408) {
                fnSessionCheck(jqXHR);
            } else if (jqXHR && jqXHR.status == 302) {
                fnSessionCheck(jqXHR);
            }
            else {
                fnSessionCheck(jqXHR);
            }
        },
    });
}
function fnSweetAlert(type1, title1, timer1) {

    Swal.fire({
        position: 'top-end',
        type: type1,
        title: title1,
        showConfirmButton: false,
        timer: timer1
    })

}
//Kategori Listesi Drop Down -> Ürün Ekle
function fnCategoryListDropDown(dropDownCategoryId, choice) {
    $.ajax({
        type: "GET",
        url: "/Home/GetDBCategoryList",
        dataType: "json",
        data: {},
        success: function (result) {
            if (result.success == true) {
                var s = '<option value = "">Kategori Seçiniz</option>';
                if (choice == "Tümü") {
                    s = '<option value = "">Tümü</option>';
                }
                var data = result.data;

                for (var i = 0; i < data.length; i++) {
                    s += '<option value="' + data[i].CategoryName + '">' + data[i].CategoryName + '</option>';
                }
                $("#" + dropDownCategoryId).html(s); 
            }
            else {
                fnDirectLoginPage();
            }
        },
        error: function (jqXHR, textStatus, error) {
            if (jqXHR && jqXHR.status == 200) {
                fnSessionCheck(jqXHR);
            } else if (jqXHR && jqXHR.status == 408) {
                fnSessionCheck(jqXHR);
            } else if (jqXHR && jqXHR.status == 302) {
                fnSessionCheck(jqXHR);
            }
            else {
                fnSessionCheck(jqXHR);
            }
        },
    });
}

//Ürün Ekle
function fnSaveProduct() {

    var category = $('#chooseCategory').val();
    var productName = $('#addProductName').val();
 
    $.ajax({
        type: "POST",
        url: "/Home/SetProductList?category=" + category + "&product=" + productName,
        dataType: "json",
        data: {},
        success: function (result) {
            if (result.success == true) {
                fnSweetAlert('success', result.message, 2500);
                var delayInMilliseconds = 1500;
                setTimeout(function () {
                    window.location.reload();
                }, delayInMilliseconds);
                fnGetSavedKategoryList();
            }
            else {
                fnDirectLoginPage();
            }
        },
        error: function (jqXHR, textStatus, error) {
            if (jqXHR && jqXHR.status == 200) {
                fnSessionCheck(jqXHR);
            } else if (jqXHR && jqXHR.status == 408) {
                fnSessionCheck(jqXHR);
            } else if (jqXHR && jqXHR.status == 302) {
                fnSessionCheck(jqXHR);
            }
            else {
                fnSessionCheck(jqXHR);
            }
        },
    });
}
//Kategori Ürün Listesi
function fnGetSavedKategoryList() {
    $.ajax({
        type: "GET",
        url: "/Home/GetProductCategoryList",
        dataType: "json",
        success: function (result) {
            if (result.success == true) {
                var table = $('#authorizedList').DataTable({
                    "bDestroy": true,
                    "language": {
                        "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Turkish.json"
                    },
                    "pageLength": 50,
                    "destroy": true,
                    "processing": true,
                    "order": [[0, ""]],
                    "columnDefs": [
                        { "orderable": false, "targets": [0, 1, 2] }

                    ],
                    data: result.data,
                    columns: [
                        { data: "CategoryName", title: "Kategori" },
                        { data: "ProductName", title: "Ürün Adı" },
                        {

                            data: null,
                            title: "İşlemler",
                            defaultContent: '<div class="row"> <div class= "col-md-12"><button type="submit" class="btn btn-sm btn-primary mr-2"><i class="fa fa-pencil" aria-hidden="true"></i></button></div ></div >',
                            render: function (cellValue, type, row, meta) {
                                if (row["CategoryName"] != null && row["ProductName"] != null) {
                                    $('#authorizedList tbody').off('click');
                                    $('#authorizedList tbody').on('click', 'button', function () {
                                        var data = table.row($(this).parents('tr')).data();
                                        fnGetShowUpdateModal(data);
                                    });
                                    return this.data;
                                }
                                return "";
                            },
                        }
                    ],
                    dom: 'rtip',
                    buttons: [
                        {
                            extend: 'excelHtml5',
                            exportOptions: {
                                columns: [0, 1, 2, 3, 4, 5]
                            }
                        },

                    ],

                });

            }
            else {
                fnDirectLoginPage();
            }
        },
        error: function (jqXHR, textStatus, error) {
            if (jqXHR && jqXHR.status == 200) {
                fnSessionCheck(jqXHR);
            } else if (jqXHR && jqXHR.status == 408) {
                fnSessionCheck(jqXHR);
            } else if (jqXHR && jqXHR.status == 302) {
                fnSessionCheck(jqXHR);
            }
            else {
                fnSessionCheck(jqXHR);
            }
        },
    })
}
var beforeProductName;
function fnGetShowUpdateModal(data) {
    beforeProductName = data.ProductName;
    $("#categoryField").val(data.CategoryName);
    $("#productField").val(data.ProductName);
    $('#productCategoryInfo').modal('show');
}
//Ürün adı Güncelleme
function fnUpdateProductInfo() {
     var product = $("#productField").val();
    if (product == "") {
        fnSweetAlert("error", "Ürün Adı Boş Geçilemez.", 1550)
    }
    else {
        $.ajax({
            type: "GET",
            url: "/Home/UpdateProductInfo?productName=" + product + "&beforeProductName=" + beforeProductName,
            dataType: "json",
            data: {},
            success: function (result) {
                if (result.success == true) {
                    fnSweetAlert('success', result.message, 1500);
                    var delayInMilliseconds = 1500;
                    setTimeout(function () {
                        window.location.reload();
                    }, delayInMilliseconds);
                }
                else {
                    fnDirectLoginPage();
                }
            },
            error: function (jqXHR, textStatus, error) {
                if (jqXHR && jqXHR.status == 200) {
                    fnSessionCheck(jqXHR);
                } else if (jqXHR && jqXHR.status == 408) {
                    fnSessionCheck(jqXHR);
                } else if (jqXHR && jqXHR.status == 302) {
                    fnSessionCheck(jqXHR);
                }
                else {
                    fnSessionCheck(jqXHR);
                }
            },
        });
    }
}
//Ürünü silme
function fnDeleteProduct() {
    var product = $('#productField').val();
    $.ajax({
        type: "GET",
        url: "/Home/DeleteProduct?&product=" + product,
        dataType: "json",
        data: {},
        success: function (result) {
            if (result.success == true) {
                fnSweetAlert('success', result.message, 1500);
                var delayInMilliseconds = 1500;
                setTimeout(function () {
                    window.location.reload();
                }, delayInMilliseconds);
            }
            else {
                fnDirectLoginPage();
            }
        },
        error: function (jqXHR, textStatus, error) {
            if (jqXHR && jqXHR.status == 200) {
                fnSessionCheck(jqXHR);
            } else if (jqXHR && jqXHR.status == 408) {
                fnSessionCheck(jqXHR);
            } else if (jqXHR && jqXHR.status == 302) {
                fnSessionCheck(jqXHR);
            }
            else {
                fnSessionCheck(jqXHR);
            }
        },
    });


}
