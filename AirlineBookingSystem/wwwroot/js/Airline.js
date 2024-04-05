var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/Airline/GetAll"
        },
        "columns": [
            { "data": "name", "width": "15%" },
            { "data": "code", "width": "20%" },
          
            {
                "data": "airlineId",
                "render": function (data) {
                    return `
                    <div class="text-center">
                        <a href="/Admin/Airline/SaveOrEdit/${data}" class="btn btn-info">
                            <i class="fas fa-edit"></i>
                        </a>
                        <a class="btn btn-danger" onclick="Delete('/Admin/Airline/Delete/${data}')">
                            <i class="fas fa-trash"></i>
                        </a>
                    </div>`;
                }
            }
        ]
    });
}

function Delete(url) {
    swal({
        title: "Want to delete data?",
        text: "Delete information!!!",
        icon: "warning",
        buttons: true,
        dangerModel: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                url: url,
                type: "DELETE",
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload();
                        toastr.success(data.message);
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            })
        }
    })
}
