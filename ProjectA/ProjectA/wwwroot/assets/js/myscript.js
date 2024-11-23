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
   