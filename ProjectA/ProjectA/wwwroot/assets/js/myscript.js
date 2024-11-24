document.addEventListener('DOMContentLoaded', function ()
{
    var addProductInput = document.getElementById('addProduct');
    if (addProductInput)
    {
        addProductInput.addEventListener('change', function (event) { var input = event.target; var reader = new FileReader(); reader.onload = function () { var dataURL = reader.result; var imagePreview = document.getElementById('img-preview'); imagePreview.src = dataURL; imagePreview.style.display = 'block'; }; if (input.files && input.files[0]) { reader.readAsDataURL(input.files[0]); } });
    }
});
function deleteCategory(id) {
    if (confirm('Are you sure you want to delete this category?')) {
        var form = document.createElement('form');
        form.method = 'post';
        form.action = '/Admin/DeleteCategory/' + id;
        document.body.appendChild(form);
        form.submit();
    }
}
function editCategory(id) {

    {
        var form = document.createElement('form');
        form.method = 'get';
        form.action = '/Admin/EditCategory/' + id;
        document.body.appendChild(form);
        form.submit();
    }
}
function deleteItem(id) {
    if (confirm('Are you sure you want to delete this product?')) {
        var form = document.createElement('form');
        form.method = 'post';
        form.action = '/Admin/Delete/' + id;
        document.body.appendChild(form);
        form.submit();
    }
}
function editItem(id) {

    {
        var form = document.createElement('form');
        form.method = 'get';
        form.action = '/Admin/Edit/' + id;
        document.body.appendChild(form);
        form.submit();
    }
}


function checkBeforeSubmit() {
    
    var usernameList = [];
    var newUsername = $('#usernameRegister').val();
    $.ajax({
        url: '../Client/GetAllUsernames',
        method: 'get',
        success: function (data) {
            
            usernameList = JSON.stringify(data);
 
            if (usernameList.includes(newUsername)) {
                alert('Username already exists. Please choose a different username.');
                
            } else {
                $('#successRegisterModal').modal('show');
                
                
            }
        },
        error: function (error) {
            console.log(error);
        }

    });  
}
$('#successRegisterModal').on('hidden.bs.modal', function () {
    document.getElementById('register-form').submit();
})

function closeModal() {
    $('#successRegisterModal').modal('hide');
}
function addToCart(productId) {
    var quantity = document.getElementById('quantity-' + productId).value;
    var url = '/Home/AddToCart?id=' + productId + '&quantity=' + quantity;
    $.ajax(
        {
            url: url,
            type: 'POST',
            data: { id: productId, quantity: quantity },
            success: function ()
            {
                refreshCart();
            }
        });
    
}
function refreshCart()
{
    $('#cart-partial').load('/Home/GetCartPartial');
}
function closeCart() {
    $('#ltn__utilize-cart-menu').hide();
}
function openCart() {
    $('#ltn__utilize-cart-menu').show();
}

function updateCart()
{
    var cartItems = [];
    $('.cart-plus-minus-box').each(function ()
    {
        var itemId = $(this).data('id');
        var quantity = $(this).val();
        cartItems.push(
            { id: itemId, quantity: quantity }
        );
    });
    $.ajax(
        {
            url: '/Home/UpdateCart',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(cartItems),
            success: function (response) { // X? lý ph?n h?i t? server 
                alert('Cart updated successfully!');
                location.reload();
            },
            error: function (error) { // X? lý l?i 
                alert('Error updating cart.'); 
            }
        });
}