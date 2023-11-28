var dataTable;

$(document).ready(function () {
    const urlParams = new URLSearchParams(window.location.search);
    const status = urlParams.get('status');
    const bookingApiUrl = status ? `/booking/getall?status=${status}` : '/booking/getall';
    loadDataTable(bookingApiUrl);
});

function loadDataTable(bookingApiUrl) {
    dataTable = $('#tblBookings').DataTable({
        "ajax": {
            url: bookingApiUrl
        },
        "columns": [
            { data: 'id', "width": "5%" },
            { data: 'name', "width": "15%" },
            { data: 'phone', "width": "10%" },
            { data: 'email', "width": "10%" },
            { data: 'status', "width": "10%" },
            { data: 'checkInDate', "width": "10%" },
            { data: 'nights', "width": "10%" },
            /*render: $.fn.dataTable.render.number(',', '.', 2) is a column rendering option in the DataTables library for jQuery.
            This means that the data in this column will be formatted as a number with a comma as the thousands separator 
            and a dot as the decimal separator, with two decimal places.*/
            { data: 'totalCost', render: $.fn.dataTable.render.number(',', '.', 2), "width": "10%" }, 
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-75 btn-group">
                        <a href="/booking/bookingDetails?bookingId=${data}" class="btn btn-outline-warning mx-2">
                            <i class="bi bi-pencil-square">Details</i>
                        </a>
                    </div>`
                }
            }
        ]
    })
}