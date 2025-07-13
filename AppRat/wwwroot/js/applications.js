var handleRenderTableData = function () {
    var height = $(window).height() - $('#header').height() - 165;
    var options = {
        dom: "<'row'<'col-7 col-md-6 d-flex justify-content-start'f><'col-5 col-md-6 text-end'B>><'row'<'col-sm-12'tr>><'row'<'col-sm-12 col-md-5 fs-12px'i><'col-sm-12 col-md-7 fs-12px'p>>",
        scrollY: height,
        scrollX: true,
        paging: false,
        fixedColumns: {
            left: 3
        },
        order: [[1, 'asc']],
        columnDefs: [
            { targets: 'no-sort', orderable: false }
        ],
        buttons: [
            {
                extend: 'excel',
                text: '<i class="fa fa-download fa-fw me-1"></i> Export Excel',
                className: 'btn btn-outline-default btn-sm text-nowrap rounded-2',
                footer: true
            }]
    };

    if ($(window).width() < 767) {
        options.fixedColumns = { left: 2 };
    }
    $('#datatable').DataTable(options);

    $('[data-id="table"]').removeClass('d-none');
    handelTooltipPopoverActivation();
    $(window).trigger('resize');
};


/* Controller
------------------------------------------------ */
$(document).ready(function () {
    var defaultStartDate = moment().startOf('month');
    var defaultEndDate = moment().endOf('month');

    $('#advance-daterange').daterangepicker({
        format: 'MM/DD/YYYY',
        startDate: defaultStartDate,
        endDate: defaultEndDate,
        minDate: '01/01/2021',
        maxDate: '12/31/2024',
        dateLimit: { days: 60 },
        showDropdowns: true,
        showWeekNumbers: true,
        timePicker: false,
        timePickerIncrement: 1,
        timePicker12Hour: true,
        ranges: {
            'Today': [moment(), moment()],
            'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
            'Last 7 Days': [moment().subtract(6, 'days'), moment()],
            'Last 30 Days': [moment().subtract(29, 'days'), moment()],
            'This Month': [moment().startOf('month'), moment().endOf('month')],
            'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
        },
        opens: 'right',
        drops: 'down',
        buttonClasses: ['btn', 'btn-sm'],
        applyClass: 'btn-primary',
        cancelClass: 'btn-default',
        separator: ' to ',
        locale: {
            applyLabel: 'Submit',
            cancelLabel: 'Cancel',
            fromLabel: 'From',
            toLabel: 'To',
            customRangeLabel: 'Custom',
            daysOfWeek: ['Su', 'Mo', 'Tu', 'We', 'Th', 'Fr', 'Sa'],
            monthNames: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'],
            firstDay: 1
        }
    }, function (start, end, label) {
        // Update the displayed date range
        $('#advance-daterange span').html(start.format('YYYY-MM-DD') + ' - ' + end.format('YYYY-MM-DD'));

        // Make an AJAX request to fetch the updated partial view content
        $.ajax({
            url: '@Url.Action("GetApplicationsPartialView", "AR_Development")',
            type: 'GET',
            data: {
                startDate: start.format('YYYY-MM-DD'),
                endDate: end.format('YYYY-MM-DD')
            },
            success: function (result) {
                // Update the partial view container with the new content
                $('#partialViewContainer').html(result);

                // Initialize DataTables on the table inside the loaded partial view
                // ... (existing DataTables initialization code)

                var height = $(window).height() - $('#header').height() - 165;
                var options = {
                    dom: "<'row'<'col-7 col-md-6 d-flex justify-content-start'f><'col-5 col-md-6 text-end'B>><'row'<'col-sm-12'tr>><'row'<'col-sm-12 col-md-5 fs-12px'i><'col-sm-12 col-md-7 fs-12px'p>>",
                    scrollY: height,
                    scrollX: true,
                    paging: false,
                    fixedColumns: {
                        left: 3
                    },
                    order: [[1, 'asc']],
                    columnDefs: [
                        { targets: 'no-sort', orderable: false }
                    ],
                    buttons: [
                        {
                            extend: 'excel',
                            text: '<i class="fa fa-download fa-fw me-1"></i> Export Excel',
                            className: 'btn btn-outline-default btn-sm text-nowrap rounded-2',
                            footer: true
                        }]
                };

                if ($(window).width() < 767) {
                    options.fixedColumns = { left: 2 };
                }
                $('#datatable').DataTable(options);

                $('[data-id="table"]').removeClass('d-none');
                handelTooltipPopoverActivation();
                $(window).trigger('resize');
            },
            error: function (error) {
                console.error(error);
            }
        });
    });
    //$.ajax({
    //    url: 'Url.Action("GetApplicationsPartialView", "AR_Development")',
    //    type: 'GET',
    //    success: function (result) {
    //        console.log("Partial view loaded successfully.");
    //        $('#partialViewContainer').html(result);

    //        // Initialize DataTables on the table inside the loaded partial view
    //        //$('#datatable').DataTable();
    //        var height = $(window).height() - $('#header').height() - 165;
    //        var options = {
    //            dom: "<'row'<'col-7 col-md-6 d-flex justify-content-start'f><'col-5 col-md-6 text-end'B>><'row'<'col-sm-12'tr>><'row'<'col-sm-12 col-md-5 fs-12px'i><'col-sm-12 col-md-7 fs-12px'p>>",
    //            scrollY: height,
    //            scrollX: true,
    //            paging: false,
    //            fixedColumns: {
    //                left: 3
    //            },
    //            order: [[1, 'asc']],
    //            columnDefs: [
    //                { targets: 'no-sort', orderable: false }
    //            ],
    //            buttons: [
    //                {
    //                    extend: 'excel',
    //                    text: '<i class="fa fa-download fa-fw me-1"></i> Export Excel',
    //                    className: 'btn btn-outline-default btn-sm text-nowrap rounded-2',
    //                    footer: true
    //                }]
    //        };

    //        if ($(window).width() < 767) {
    //            options.fixedColumns = { left: 2 };
    //        }
    //        $('#datatable').DataTable(options);

    //        $('[data-id="table"]').removeClass('d-none');
    //        handelTooltipPopoverActivation();
    //        $(window).trigger('resize');
    //    },
    //    error: function (error) {
    //        console.error(error);
    //    }
    //});
});

//$(document).ready(function () {
//    handleRenderTableData();

//    var minDate = '01/01/2023';
//    var maxDate = '12/31/2030';
//    var dateRangePicker;

//    function initializeDateRangePicker(startDate, endDate) {
//        if (dateRangePicker) {
//            dateRangePicker.data('daterangepicker').remove();
//        }

//        dateRangePicker = $('#daterange').daterangepicker({
//            format: 'MM/DD/YYYY',
//            startDate: startDate,
//            endDate: endDate,
//            minDate: minDate,
//            maxDate: maxDate,
//            alwaysShowCalendars: true,
//            dateLimit: { days: 60 },
//            showDropdowns: true,
//            /*showWeekNumbers: true,*/
//            timePicker: false,
//            timePickerIncrement: 1,
//            timePicker12Hour: true,
//            ranges: {
//                'Today': [moment(), moment()],
//                'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
//                'Last 7 Days': [moment().subtract(6, 'days'), moment()],
//                'Last 30 Days': [moment().subtract(29, 'days'), moment()],
//                'This Month': [moment().startOf('month'), moment().endOf('month')],
//                'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
//            },
//            opens: 'right',
//            drops: 'down',
//            buttonClasses: ['btn', 'btn-sm'],
//            applyClass: 'btn-primary',
//            cancelClass: 'btn-default',
//            separator: ' to ',
//            locale: {
//                applyLabel: 'Submit',
//                cancelLabel: 'Cancel',
//                fromLabel: 'From',
//                toLabel: 'To',
//                customRangeLabel: 'Custom',
//                daysOfWeek: ['Su', 'Mo', 'Tu', 'We', 'Th', 'Fr', 'Sa'],
//                monthNames: [
//                    'January', 'February', 'March', 'April', 'May', 'June', 'July', 
//                    'August', 'September', 'October', 'November', 'December'
//                ],
//                firstDay: 1
//            }
//        }, function (start, end, label) {
//            $('#advance-daterange span').html(
//                start.format('MMMM D, YYYY') + ' - ' + end.format('MMMM D, YYYY')
//            );
//        });
//    }

//    var startDate = moment().startOf('month');
//    var endDate = moment().endOf('month');

//    initializeDateRangePicker(startDate, endDate);

//    $('#date-range-selector').on('change', function () {
//        var selectedRange = $(this).val();
//        var newStartDate, newEndDate;

//        if (selectedRange === 'Last 7 Days') {
//            newStartDate = moment().subtract(6, 'days');
//            newEndDate = moment();
//        } else if (selectedRange === 'Last 30 Days') {
//            newStartDate = moment().subtract(29, 'days');
//            newEndDate = moment();
//        }

//        initializeDateRangePicker(newStartDate, newEndDate);
//    });
//});


//$(document).ready(function () {
//    handleRenderTableData();

//    // Set the minDate and maxDate to the range of 2023
//    var minDate = '01/01/2023';
//    var maxDate = '12/31/2023';
//    var dateRangePicker; // Variable to store the Date Range Picker instance

//    function initializeDateRangePicker(startDate, endDate) {
//        if (dateRangePicker) {
//            // Destroy the existing picker
//            dateRangePicker.data('daterangepicker').remove();
//        }

//        dateRangePicker = $('#advance-daterange').daterangepicker({
//            format: 'MM/DD/YYYY',
//            startDate: startDate,
//            endDate: endDate,
//            minDate: minDate,
//            maxDate: maxDate,
//            // ... (other options)
//        }, function (start, end, label) {
//            $('#advance-daterange span').html(start.format('MMMM D, YYYY') + ' - ' + end.format('MMMM D, YYYY'));
//        });
//    }

//    // Calculate the start and end dates for the current month
//    var startDate = moment().startOf('month');
//    var endDate = moment().endOf('month');

//    // Initialize with the current month
//    initializeDateRangePicker(startDate, endDate);

//    // Change the date range dynamically
//    $('#date-range-selector').on('change', function () {
//        var selectedRange = $(this).val();
//        var newStartDate, newEndDate;

//        if (selectedRange === 'Last 7 Days') {
//            newStartDate = moment().subtract(6, 'days');
//            newEndDate = moment();
//        } else if (selectedRange === 'Last 30 Days') {
//            newStartDate = moment().subtract(29, 'days');
//            newEndDate = moment();
//        } // Add more conditions for other ranges if needed

//        initializeDateRangePicker(newStartDate, newEndDate);
//    });
//});

