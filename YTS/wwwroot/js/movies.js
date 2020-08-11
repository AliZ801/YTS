jQuery.noConflict()(function ($) {
    $(document).ready(function () {
        loadDataTable();
    })
})

function loadDataTable() {

    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "Movies/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "id", "width": "10%" },
            { "data": "title", "width": "25%" },
            { "data": "genres.genre", "width": "21%" },
            { "data": "quality.qual", "width": "12%" },
            { "data": "ratings.rating", "width": "12%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                                <a href="Movies/AddMovie/${data}" class='btn btn-success text-white' style='cursor:pointer; width:40px;'>
                                    <i class='far fa-edit'></i>
                                </a>
                                &nbsp;
                                <a onclick=Delete("Movies/Delete/${data}") class='btn btn-danger text-white' style='cursor:pointer; width:40px;'>
                                    <i class='far fa-trash-alt'></i>
                                </a>
                            </div>
                            `;
                }, "width": "20%"
            }
        ],
        "language": {
            "emptyTable": "No records found."
        },
        "width": "100%"
    });
}

function Delete(url) {
    swal({
        title: "Are you sure you want to delete?",
        text: "You will not be able to restore the content!",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Yes, delete it!",
        closeOnconfirm: true
    }, function () {
        $.ajax({
            type: 'DELETE',
            url: url,
            success: function (data) {
                if (data.success) {
                    toastr.success(data.message);
                    dataTable.ajax.reload();
                }
                else {
                    toastr.error(data.message);
                }
            }
        });
    });
}