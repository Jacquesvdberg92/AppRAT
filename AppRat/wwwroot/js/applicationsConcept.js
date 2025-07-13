//$(document).ready(function () {
//    // Initialize the date range picker with default values for the current month
//    var defaultStartDate = moment().startOf('month');
//    var defaultEndDate = moment().endOf('month');

//    $('#advance-daterange').daterangepicker({
//        format: 'MM/DD/YYYY',
//        startDate: defaultStartDate,
//        endDate: defaultEndDate,
//        minDate: '01/01/2012',
//        maxDate: '12/31/2015',
//        dateLimit: { days: 60 },
//        showDropdowns: true,
//        showWeekNumbers: true,
//        timePicker: false,
//        timePickerIncrement: 1,
//        timePicker12Hour: true,
//        ranges: {
//            'Today': [moment(), moment()],
//            'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
//            'Last 7 Days': [moment().subtract(6, 'days'), moment()],
//            'Last 30 Days': [moment().subtract(29, 'days'), moment()],
//            'This Month': [moment().startOf('month'), moment().endOf('month')],
//            'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
//        },
//        opens: 'right',
//        drops: 'down',
//        buttonClasses: ['btn', 'btn-sm'],
//        applyClass: 'btn-primary',
//        cancelClass: 'btn-default',
//        separator: ' to ',
//        locale: {
//            applyLabel: 'Submit',
//            cancelLabel: 'Cancel',
//            fromLabel: 'From',
//            toLabel: 'To',
//            customRangeLabel: 'Custom',
//            daysOfWeek: ['Su', 'Mo', 'Tu', 'We', 'Th', 'Fr', 'Sa'],
//            monthNames: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'],
//            firstDay: 1
//        }
//    }, function (start, end, label) {
//        // Update the displayed date range
//        $('#advance-daterange span').html(start.format('YYYY-MM-DD') + ' - ' + end.format('YYYY-MM-DD'));

//        // Make an AJAX request to fetch the updated partial view content
//        $.ajax({
//            url: '@Url.Action("GetApplicationsPartialView", "AR_Development")',
//            type: 'GET',
//            data: {
//                startDate: start.format('YYYY-MM-DD'),
//                endDate: end.format('YYYY-MM-DD')
//            },
//            success: function (result) {
//                // Update the partial view container with the new content
//                $('#partialViewContainer').html(result);

//                // Initialize DataTables on the table inside the loaded partial view
//                // ... (existing DataTables initialization code)

//                var height = $(window).height() - $('#header').height() - 165;
//                var options = {
//                    dom: "<'row'<'col-7 col-md-6 d-flex justify-content-start'f><'col-5 col-md-6 text-end'B>><'row'<'col-sm-12'tr>><'row'<'col-sm-12 col-md-5 fs-12px'i><'col-sm-12 col-md-7 fs-12px'p>>",
//                    scrollY: height,
//                    scrollX: true,
//                    paging: false,
//                    fixedColumns: {
//                        left: 3
//                    },
//                    order: [[1, 'asc']],
//                    columnDefs: [
//                        { targets: 'no-sort', orderable: false }
//                    ],
//                    buttons: [
//                        {
//                            extend: 'excel',
//                            text: '<i class="fa fa-download fa-fw me-1"></i> Export Excel',
//                            className: 'btn btn-outline-default btn-sm text-nowrap rounded-2',
//                            footer: true
//                        }]
//                };

//                if ($(window).width() < 767) {
//                    options.fixedColumns = { left: 2 };
//                }
//                $('#datatable').DataTable(options);

//                $('[data-id="table"]').removeClass('d-none');
//                handelTooltipPopoverActivation();
//                $(window).trigger('resize');
//            },
//            error: function (error) {
//                console.error(error);
//            }
//        });
//    });
//});


//$(document).ready(function () {
//    // Initialize the date range picker with default values for the current month
//    var defaultStartDate = moment().startOf('month');
//    var defaultEndDate = moment().endOf('month');

//    $('#advance-daterange').daterangepicker({
//        format: 'MM/DD/YYYY',
//        startDate: moment().subtract(29, 'days'),
//        endDate: moment(),
//        minDate: '01/01/2012',
//        maxDate: '12/31/2015',
//        dateLimit: { days: 60 },
//        showDropdowns: true,
//        showWeekNumbers: true,
//        timePicker: false,
//        timePickerIncrement: 1,
//        timePicker12Hour: true,
//        ranges: {
//            'Today': [moment(), moment()],
//            'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
//            'Last 7 Days': [moment().subtract(6, 'days'), moment()],
//            'Last 30 Days': [moment().subtract(29, 'days'), moment()],
//            'This Month': [moment().startOf('month'), moment().endOf('month')],
//            'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
//        },
//        opens: 'right',
//        drops: 'down',
//        buttonClasses: ['btn', 'btn-sm'],
//        applyClass: 'btn-primary',
//        cancelClass: 'btn-default',
//        separator: ' to ',
//        locale: {
//            applyLabel: 'Submit',
//            cancelLabel: 'Cancel',
//            fromLabel: 'From',
//            toLabel: 'To',
//            customRangeLabel: 'Custom',
//            daysOfWeek: ['Su', 'Mo', 'Tu', 'We', 'Th', 'Fr', 'Sa'],
//            monthNames: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'],
//            firstDay: 1
//        }
//    }, function (start, end, label) {
//        $('#advance-daterange span').html(start.format('MMMM D, YYYY') + ' - ' + end.format('MMMM D, YYYY'));
//    }), function (start, end, label) {
//        // Update the displayed date range
//        $('#advance-daterange span').html(start.format('MMMM D, YYYY') + ' - ' + end.format('MMMM D, YYYY'));

//        // Make an AJAX request to fetch the updated partial view content
//        $.ajax({
//            url: '@Url.Action("GetApplicationsPartialView", "AR_Development")',
//            type: 'GET',
//            data: {
//                startDate: start.format('YYYY-MM-DD'),
//                endDate: end.format('YYYY-MM-DD')
//            },
//            success: function (result) {
//                // Update the partial view container with the new content
//                $('#partialViewContainer').html(result);

//                // Initialize DataTables on the table inside the loaded partial view
//                // ... (existing DataTables initialization code)

//                // Additional code for any other JavaScript functionality you may have
//            },
//            error: function (error) {
//                console.error(error);
//            }
//        });
//    });
//});



//var handleRenderTableData = function () {
//    var height = $(window).height() - $('#header').height() - 165;
//    var options = {
//        dom: "<'row'<'col-7 col-md-6 d-flex justify-content-start'f><'col-5 col-md-6 text-end'B>><'row'<'col-sm-12'tr>><'row'<'col-sm-12 col-md-5 fs-12px'i><'col-sm-12 col-md-7 fs-12px'p>>",
//        scrollY: height,
//        scrollX: true,
//        paging: false,
//        fixedColumns: {
//            left: 3
//        },
//        order: [[1, 'asc']],
//        columnDefs: [
//            { targets: 'no-sort', orderable: false }
//        ],
//        buttons: [
//            {
//                extend: 'excel',
//                text: '<i class="fa fa-download fa-fw me-1"></i> Export Excel',
//                className: 'btn btn-outline-default btn-sm text-nowrap rounded-2',
//                footer: true
//            }]
//    };

//    if ($(window).width() < 767) {
//        options.fixedColumns = { left: 2 };
//    }

//    // Destroy any existing DataTable
//    var table = $('#datatable').DataTable();
//    table.destroy();

//    // Reinitialize DataTable
//    $('#datatable').DataTable(options);

//    $('[data-id="table"]').removeClass('d-none');
//    handelTooltipPopoverActivation();
//    $(window).trigger('resize');
//};

//var handleRenderTableData = function () {
//    var height = $(window).height() - $('#header').height() - 165;
//    var options = {
//        dom: "<'row'<'col-7 col-md-6 d-flex justify-content-start'f><'col-5 col-md-6 text-end'B>><'row'<'col-sm-12'tr>><'row'<'col-sm-12 col-md-5 fs-12px'i><'col-sm-12 col-md-7 fs-12px'p>>",
//        scrollY: height,
//        scrollX: true,
//        paging: false,
//        fixedColumns: {
//            left: 3
//        },
//        order: [[1, 'asc']],
//        columnDefs: [
//            { targets: 'no-sort', orderable: false }
//        ],
//        buttons: [
//            {
//                extend: 'excel',
//                text: '<i class="fa fa-download fa-fw me-1"></i> Export Excel',
//                className: 'btn btn-outline-default btn-sm text-nowrap rounded-2',
//                footer: true
//            }]
//    };

//    if ($(window).width() < 767) {
//        options.fixedColumns = { left: 2 };
//    }
//    $('#datatable').DataTable(options);

//    $('[data-id="table"]').removeClass('d-none');
//    handelTooltipPopoverActivation();
//    $(window).trigger('resize');
//};

/* Controller
------------------------------------------------ *///handleRenderTableData();
//$(document).ready(function () {
//    // Initialize the date range picker
//    $('#advance-daterange').daterangepicker({
//        format: 'MM/DD/YYYY',
//        startDate: moment().startOf('month'), // Set default start date to the beginning of the current month
//        endDate: moment().endOf('month'),     // Set default end date to the end of the current month
//        minDate: '01/01/2012',
//        maxDate: '12/31/2030',
//        dateLimit: { days: 60 },
//        showDropdowns: true,
//        showWeekNumbers: true,
//        timePicker: false,
//        timePickerIncrement: 1,
//        timePicker12Hour: true,
//        ranges: {
//            'Today': [moment(), moment()],
//            'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
//            'Last 7 Days': [moment().subtract(6, 'days'), moment()],
//            'Last 30 Days': [moment().subtract(29, 'days'), moment()],
//            'This Month': [moment().startOf('month'), moment().endOf('month')],
//            'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
//        },
//        opens: 'right',
//        drops: 'down',
//        buttonClasses: ['btn', 'btn-sm'],
//        applyClass: 'btn-primary',
//        cancelClass: 'btn-default',
//        separator: ' to ',
//        locale: {
//            applyLabel: 'Submit',
//            cancelLabel: 'Cancel',
//            fromLabel: 'From',
//            toLabel: 'To',
//            customRangeLabel: 'Custom',
//            daysOfWeek: ['Su', 'Mo', 'Tu', 'We', 'Th', 'Fr', 'Sa'],
//            monthNames: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'],
//            firstDay: 1
//        }
//    }, function (start, end, label) {
//        // Update the displayed date range
//        $('#advance-daterange span').html(start.format('MMMM D, YYYY') + ' - ' + end.format('MMMM D, YYYY'));

//        // Make an AJAX request to fetch the updated partial view content
//        $.ajax({
//            url: '@Url.Action("GetApplicationsPartialView", "AR_Development")',
//            type: 'GET',
//            data: {
//                startDate: start.format('YYYY-MM-DD'),
//                endDate: end.format('YYYY-MM-DD')
//            },
//            success: function (result) {
//                // Update the partial view container with the new content
//                $('#partialViewContainer').html(result);

//                // Initialize DataTables on the table inside the loaded partial view
//                // ... (existing DataTables initialization code)

//                // Additional code for any other JavaScript functionality you may have
//            },
//            error: function (error) {
//                console.error(error);
//            }
//        });
//    });

//    // Set the default date range to the current month
//    var defaultStartDate = moment().startOf('month');
//    var defaultEndDate = moment().endOf('month');
//    $('#advance-daterange').data('daterangepicker').setStartDate(defaultStartDate);
//    $('#advance-daterange').data('daterangepicker').setEndDate(defaultEndDate);


//    $('#advance-daterange span').html(moment().subtract(29, 'days').format('MMMM D, YYYY') + ' - ' + moment().format('MMMM D, YYYY'));

//    $('#advance-daterange').daterangepicker({
//        format: 'MM/DD/YYYY',
//        startDate: moment().subtract(29, 'days'),
//        endDate: moment(),
//        minDate: '01/01/2012',
//        maxDate: '12/31/2015',
//        dateLimit: { days: 60 },
//        showDropdowns: true,
//        showWeekNumbers: true,
//        timePicker: false,
//        timePickerIncrement: 1,
//        timePicker12Hour: true,
//        ranges: {
//            'Today': [moment(), moment()],
//            'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
//            'Last 7 Days': [moment().subtract(6, 'days'), moment()],
//            'Last 30 Days': [moment().subtract(29, 'days'), moment()],
//            'This Month': [moment().startOf('month'), moment().endOf('month')],
//            'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
//        },
//        opens: 'right',
//        drops: 'down',
//        buttonClasses: ['btn', 'btn-sm'],
//        applyClass: 'btn-primary',
//        cancelClass: 'btn-default',
//        separator: ' to ',
//        locale: {
//            applyLabel: 'Submit',
//            cancelLabel: 'Cancel',
//            fromLabel: 'From',
//            toLabel: 'To',
//            customRangeLabel: 'Custom',
//            daysOfWeek: ['Su', 'Mo', 'Tu', 'We', 'Th', 'Fr', 'Sa'],
//            monthNames: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'],
//            firstDay: 1
//        }
//    }, function (start, end, label) {
//        $('#advance-daterange span').html(start.format('MMMM D, YYYY') + ' - ' + end.format('MMMM D, YYYY'));
//    });
//});