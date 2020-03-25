
$(document).ready(function () {
    $("#formProduct").submit(updateProduct);
    $("#formDeleteProduct").submit(deleteProduct);
});

// Not implemented
function getOrders() {
    console.log('getOrders() not implemented yet.');
}

// Gets products list
function getProducts() {
    console.log('Get()');
    ajaxCall('GET',
        location.origin + '/api/Product',
        productsShow)
}

// Shows products list
function productsShow(data) {
    console.log('show product: ' + data);
    var shtml = '';
    var template = $("#productsTable").html();
    $.each(data, function (i, item) {
        console.log(item.id + " | " + item.name + " | " + item.price);
        shtml += template.replace(/\[Id\]/g, item.id).replace('[Name]', item.name).replace('[Price]', item.price);
    });
    $("#productsTable").html(shtml);
}

// Gets Single product
function getProduct(id) {
    console.log('Get(' + id + ')');
    ajaxCall('GET',
        location.origin + '/api/Product/' + id,
        productShow)
}

// Shows Single product
function productShow(data) {
    console.log('Show: ' + data);
    $('#Id').val(data.id);
    $('#Name').val(data.name);
    $('#Price').val(data.price);
}

// Prepares the post ajax call
function updateProduct() {
   console.log('Update');
   if ($("#formProduct").valid()) {
        ajaxCall('POST',
            location.origin + '/api/Product',
            redirectToProductList,
            'application/x-www-form-urlencoded; charset=UTF-8',
            $('#formProduct').serialize());
    }
    return false;
}

// Prepares the delete ajax call
function deleteProduct() {
    console.log('Delete');
    ajaxCall('DELETE',
        location.origin + '/api/Product/' + $('#Id').val(),
        redirectToProductList);
    return false;
}

// Redirects to the list
function redirectToProductList() {
    document.location = location.origin + '/Product';
}

// Generic ajax ajax call
function ajaxCall(method, url, callback, contentType, data) {
    console.log('ajaxCall(' + method + ', ' + url + ', ' + callback + ', ' + contentType + ', ' + data +')');
    $.ajax({
        method: method,
        url: url,
        data: data ? data : "",
        contentType: contentType ? contentType : '',
        success: function (data) {
            //$("#result").html('success');
            console.log('success');
            if (callback) {
                callback(data);
            }
        },
        failure: function (data) {
            //$("#result").html(data.responseText);
            console.log(data.responseText);
        },
        error: function (data) {
            //$("#result").html(data.responseText);
            console.log(data.responseText);
        }
    });
}