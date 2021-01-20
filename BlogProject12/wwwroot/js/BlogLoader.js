var dataTable;

$(document).ready(function () {

    loadDataTable();

});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({

        "ajax": {
            "url": "/Admin/Blog/GetAll"
        },
        "columns": [

            { "data": "blogTitle", "width": "60%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                          <div class="text-center">
                          <a href="/Admin/User/Delete/${data}">Delete</a>
                           </div>
                          <div class="text-center">
                          <a href="/Admin/Blog/Upsert/${data}">Edit</a>
                           </div>
                          
                           `;
                },
                "width": "40%"
            }
        ]
    });
}

