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
    refreshSmallCart();
}
function closeCart() {
    $('#ltn__utilize-cart-menu').hide();
}
function openCart() {
    $('#ltn__utilize-cart-menu').show();
}
function refreshSmallCart() {
    $('.mini-cart-icon-partital').load('/Home/GetSmallCartPartial');
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

function submitOrder() {
    var ward = document.getElementById('checkout-ward').value;
    var district = document.getElementById('checkout-district').value;
    var province = document.getElementById('checkout-province').value;
    var street = document.getElementById('checkout-street').value;
    var note = document.getElementById('checkout-note').value;
    var paymentMethod = document.querySelector('input[name="PaymentMethod"]:checked').value;

    var formData = {
        Ward: ward,
        District: district,
        Province: province,
        Street: street,
        OrderNotes: note,
        paymentMethod: paymentMethod
    };
    $.ajax({
        url: '/Account/PlaceOrder',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(formData),
        success: function (res) {
            alert(res.Message);
        },
        error: function (err) {
            alert("error");
        }
    })
}
window.addEventListener('load', function (event) {
    
    refreshSmallCart();
    
});
$(document).ready(function ()
{
    $.ajax({
        url: 'Home/LoadCategoryMenu',
        type: 'GET',
        success: function (data)
        {
            $('#category-menu').html(data);
        },
        
    });
    $('#product-list-table').DataTable();
    $('#category-list-table').DataTable();
});

     function validateAddProductForm() {
            var isValid = true;

            var nameInput = document.getElementById('product-name').value;
            var nameError = document.getElementById('name-error');
            if (nameInput.trim() === '') {
                nameError.style.display = 'block';
            isValid = false;
                } else {
                nameError.style.display = 'none';
                }

            var unitInput = document.getElementById('unit').value;
            var unitError = document.getElementById('unit-error');
            if (unitInput.trim() === '') {
                unitError.style.display = 'block';
            isValid = false;
                } else {
                unitError.style.display = 'none';
                }

            var quantityInput = document.getElementById('quantity').value;
            var quantityError = document.getElementById('quantity-error');
            if (quantityInput.trim() === '') {
                quantityError.style.display = 'block';
            isValid = false;
                } else {
                quantityError.style.display = 'none';
                }

            var priceInput = document.getElementById('price').value;
            var priceError = document.getElementById('price-error');
            if (priceInput.trim() === '') {
                priceError.style.display = 'block';
                    isValid = false;
                } else {
                    priceError.style.display = 'none';
                }
         var productDetail = CKEDITOR.instances['product-detail'].getData();
         var shortDes = document.getElementById('short-description').value;
         var formData = new FormData();
         var category = document.getElementById('CategoryId').value;
         var imgSrc = $('#addProduct')[0].files[0];
         formData.append('Name', nameInput);
         formData.append('ShortDescription', shortDes);
         formData.append('Description', productDetail);
         formData.append('Instock', quantityInput);
         formData.append('Unit', unitInput);
         formData.append('Price', priceInput);
         formData.append('CategoryId', category);
         formData.append('Image', imgSrc);
         var xhr = new XMLHttpRequest();
         xhr.open('POST', '/Admin/AddProduct', true);
         xhr.onload = function ()
         {
             if (xhr.status === 200)
             {
                 alert('Form submitted successfully!');
                 window.location.href = '/Admin/ProductList';
             } else
             {
                 alert('An error occurred while submitting the form.');
             }
         };
         xhr.send(formData);
    }

function validateAddCategoryForm() {
    var isValid = true;

    var nameInput = document.getElementById('category-name').value;
    var nameError = document.getElementById('name-category-error');
    if (nameInput.trim() === '') {
        nameError.style.display = 'block';
        isValid = false;
    } else {
        nameError.style.display = 'none';
    }

    var desInput = document.getElementById('category-description').value;
    var desError = document.getElementById('description-category-error');
    if (desInput.trim() === '') {
        desError.style.display = 'block';
        isValid = false;
    } else {
        desError.style.display = 'none';
    }

    var parentIDInput = document.getElementById('category-parentID').value;
    var parentIDError = document.getElementById('parentId-category-error');
    if (parentIDInput.trim() === '') {
        parentIDError.style.display = 'block';
        isValid = false;
    } else {
        parentIDError.style.display = 'none';
    }
    
    if (isValid) {
        document.getElementById('add-category-form').submit();
    }
}


document.addEventListener("DOMContentLoaded", function () {
    var links = document.querySelectorAll(".tab-link");
    links.forEach(function (link) {
        link.addEventListener("click", function () {
            links.forEach(function (link) {
                link.classList.remove("active");
            });
            this.classList.add("active");

            var tabPanes = document.querySelectorAll(".tab-pane");
            tabPanes.forEach(function (pane) {
                pane.classList.remove("active", "show");
            });
            var targetPane = document.querySelector(this.getAttribute("href"));
            console.log(targetPane);
            targetPane.classList.add("active");
        });
    });
});

