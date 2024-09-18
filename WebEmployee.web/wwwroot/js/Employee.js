// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var dataTable;


$(document).ready(function () {
    loadDataTable();
});



function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        responsive: true,
        "ajax": { url: '/employees/getall' },
        "columns": [

            //this is case sensitive
            { data: 'name', "width": "25%" },
            { data: 'lastname', "width": "25%" },

            {
                data: { birthPlaceCity: 'birthPlaceCity', birthPlaceCountry: 'birthPlaceCountry' },
                "render": function (data) {
                    return `${data.birthPlaceCity} ${data.birthPlaceCountry}`
                }
                , "width": "25%"
            },

            { data: 'dateofBirth', "width": "25%" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                    <a href="/employees/upsert?id=${data}" class="mx-2" adesign="blue"> <i class"bi bi-pencil-square"></i> Edit</a>
                    <a onClick=Delete("/employees/delete/${data}") class="mx-2" adesign="gray"> <i class"bi bi-trash-fill"></i> Delete</a>
                    `
                },
                "width": "25%"
            }

        ],
        "language": {
            "url": "https://cdn.datatables.net/plug-ins/1.11.3/i18n/es_es.json"
        }

    });
}


function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                }
            })
        }
    })
}
