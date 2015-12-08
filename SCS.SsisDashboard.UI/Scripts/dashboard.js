var executionsTable;
var executablesTable;
var executablesWrapper;
var interval;

function UpdateTables() {
    $('.blockUI').block({
        message: '<h1>loading...</h1>', 
        fadein: 200,
        fadeout: 400, 
        overlayCSS: {
            backgroundColor: '#000',
            opacity: 0.45,
            cursor: 'wait'
        },
    });
    try {
        UpdateKPI();
        UpdateProjectList();
    }
    catch (error) {
        App.MsgDialog('type-warning', error);
    }
    window.setTimeout(function () { $('.blockUI').unblock();}, 100); //without a delay we dont get the block ui mask....no idea why
}

$(document).ready(function () {

    // KPI section
    $("#updateKPI").off("click");
    $("#updateKPI").click(function () {
        UpdateTables();
    });

    $("#updateFequency").change(function () {
        if (interval) {
            clearInterval(interval);
        }
        var frequency = $(this).val();
        if (frequency > 0) {
            frequency = frequency * 1000;
            interval = setInterval(UpdateTables, frequency);
        }
    });

    $(".statusPanel").click(function () {
        //unselect any selected panels
        $(".statusPanel").removeClass("selected");
        //set selected style to this panel
        $(this).addClass("selected");
        var status = $(this).attr('data-status');
        FilterProjectList(status);
    });

    // Executions section
    // keep ref here so its not lost
    executablesWrapper = $("#executablesWrapper");

    executionsTable = $('#executions').DataTable({
        "pageLength": 5,
        "bSort": true,
        "lengthMenu": [[2, 5, 10, 25, 50, -1], [2, 5, 10, 25, 50, "All"]],
        "order": [[0, "desc"]],
        rowId: 'ExecutionId',
        "columns": [
            { "data": "ExecutionId" },
            { "data": "ProjectName" },
            { "data": "PackageName" },
            { "data": "Status" },
            { "data": "RunDateString" },
            { "data": "StartTimeString" },
            { "data": "EndTimeString" },
            { "data": "ElapsedTimeInMinutes" },
            { "data": "NumberOfWarnings" },
            { "data": "NumberOfErrors" }
        ],
        "columnDefs": [
            {
                "render": function (data, type, row) {
                    return '<a data-id="' + data + '" class="details-control">' + data + '</a>'
                    ;
                },
                "targets": 0
            }
        ]
    });

    $('.dataTables_filter input').attr('placeholder', 'Search')

    // Add event listener for opening and closing details
    $('#executions tbody').on('click', 'a.details-control', function () {
        var table = $('#executions').DataTable();
        var tr = $(this).closest('tr');
        var row = table.row(tr);
        if (row.child.isShown()) {
            // This row is already open - close it
            row.child.hide();
        }
        else {
            // Open this row
            //close all open rows and set data to null as we cant share/reuse datatable
            $("#executions tbody tr").each(function (index) {
                var row = table.row(this);
                if (row.child()) {
                    row.child().hide();
                }
            });
            var html = GeExecutables(this);
            if (html) {
                row.child(html).show();
            }
            else {

                alert("No datatable HTML!");
            }
        }
    });

    UpdateTables();

});

function IntialiseExecutablesTable() {
    executablesTable = $('#executables').DataTable({
        "pageLength": 5,
        "lengthMenu": [[2, 5, 10, 25, 50, -1], [2, 5, 10, 25, 50, "All"]],
        "order": [[0, "desc"]],
        rowId: 'ExecutableId',
        "columns": [
            { "data": "Name" },
            { "data": "StartTimeString" },
            { "data": "EndTimeString" },
            { "data": "Duration" },
            { "data": "ExecutionResult" },
            { "data": "ExecutionValue" }
        ]
    });

    executablesInitialised = true;
}

function FilterProjectList(status) {
    var statusText = status;
    status = status.toLowerCase();
    if (status == 'all') {
        status = '';
    }
    //filter the table
    executionsTable.column(3).search(status, false, true).draw(true);

    // add in status text to table header
    var statusLabel = $("#statusFilter");
    if (statusLabel) {
        statusLabel.remove();
    }

    var html = '<label id="statusFilter">&nbsp &nbsp' + statusText + ' Executions</label>';
    $(html).insertAfter('#executions_length label');
}

function GeExecutables(control) {
    var id = ($(control).data("id"));
    var data = App.GetApiData("/Home/GetExecutables", { executionId: id });
    if (!executablesTable) {
        IntialiseExecutablesTable();
    }
    executablesTable.clear();
    executablesTable.rows.add(data).draw();;
    return executablesWrapper;
}

function UpdateProjectList(control) {
    var data = App.GetApiData("/Home/GetExecutions", null);
    executionsTable.clear();
    executionsTable.rows.add(data).draw();;
    return executablesWrapper;
}

function UpdateKPI() {
    var data = App.GetApiData("/Home/GetKPI", null)
    if (data) {
        //reset all to 0
        $('.statusCount').html(0);
        $.each(data, function (i, item) {
            $('#' + item.ExecutionStatus).html(item.RowCount);
        });
    }
    $("#lastUpdated").html("Updated at:" + App.FormatDate(new Date()));
}

