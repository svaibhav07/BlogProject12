var dataTable;

$(document).ready(function () {

    loadDataTable();

});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({

        "ajax": {
            "url": "/Admin/Tag/GetAll"
        },
        "columns": [

            { "data": "tagName", "width": "60%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                          <div class="text-center">
                          <a href="/Admin/User/Delete/${data}">Delete</a>
                           </div>
                          <div class="text-center">
                          <a href="/Admin/Tag/Upsert/${data}">Edit</a>
                           </div>
                          
                           `;
                },
                "width": "40%"
            }
        ]
    });
}

