
//tao preview img khi add product
document.addEventListener('DOMContentLoaded', function ()
{
    var addProductInput = document.getElementById('addProduct');
    if (addProductInput)
    {
        addProductInput.addEventListener('change', function (event) {
            var input = event.target;
            var reader = new FileReader();
            reader.onload = function ()
            {
                var dataURL = reader.result;
                var imagePreview = document.getElementById('img-preview');
                imagePreview.src = dataURL;
                imagePreview.style.display = 'block';
            };
            if (input.files && input.files[0])
            {
                reader.readAsDataURL(input.files[0]);
            }
        });
    }
});

//delete category
function deleteCategory(id) {
    if (confirm('Are you sure you want to delete this category?')) {
        var form = document.createElement('form');
        form.method = 'post';
        form.action = '/Admin/DeleteCategory/' + id;
        document.body.appendChild(form);
        form.submit();
    }
}

//chuyen tab sang edit category khi bam vao edit
function editCategory(id) {

    {
        var form = document.createElement('form');
        form.method = 'get';
        form.action = '/Admin/EditCategory/' + id;
        document.body.appendChild(form);
        form.submit();
    }
}

//delete product bang id 
function deleteItem(id) {
    if (confirm('Are you sure you want to delete this product?')) {
        var form = document.createElement('form');
        form.method = 'post';
        form.action = '/Admin/Delete/' + id;
        document.body.appendChild(form);
        form.submit();
    }
}

//edit product bang id
function editItem(id) {

    {
        var form = document.createElement('form');
        form.method = 'get';
        form.action = '/Admin/Edit/' + id;
        document.body.appendChild(form);
        form.submit();
    }
}

// validate truoc khi dang ky tk moi
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
//tat modal
function closeModal() {
    $('#successRegisterModal').modal('hide');
}

// xu ly UI addToCart
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

//refresh lai cart UI moi lan chuyen tab
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


//update cart o trang cart
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

//submit order
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

// xu ly UI menu cua category
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

//validate form add product
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
         var category = document.getElementById('CategoryId').value;
         var imgSrc = $('#addProduct')[0].files[0];
         var productDetail = CKEDITOR.instances['product-detail'].getData();
         var shortDes = document.getElementById('short-description').value;
         var formData1 = {
             Name: nameInput,
             ShortDescription: shortDes,
             Description: productDetail,
             InStock: quantityInput,
             Unit: unitInput,
             Price: priceInput,
             CategoryId: category,
             Image: imgSrc
         };
         var formData = new FormData();
         formData.append('Name', nameInput);
         formData.append('ShortDescription', shortDes);
         formData.append('Description', productDetail);
         formData.append('Instock', quantityInput);
         formData.append('Unit', unitInput);
         formData.append('Price', priceInput);
         formData.append('CategoryId', category);
         formData.append('Image', imgSrc);

         $.ajax({
             url: '/Admin/AddProduct',
             type: 'POST',
             data: formData,
             contentType: false,
             processData: false,  
             success: function (res) {
                 alert("Form submitted successfully!");
             },
             error: function (err) {
                 alert("error");
             }
         })
         
    }

    //validate form add category
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



// sua lai UI cua web
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

