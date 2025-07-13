//$(document).ready(function () {
//	$('#ApplicationCreate-Modal').on('show.bs.modal', function (event) {
//		console.log('Modal is about to show');
//		// Rest of your code to fetch and display content
//		$.get('/AR_Application/ApplicationCreatePartial', function (data) {
//			console.log(data); // Log the retrieved data
//			$('#application-create-partial-content').html(data);
//		});
//	});
//});

//$('#applicationCreateForm').on('submit', function (e) {
//    e.preventDefault();

//    // Get the anti-forgery token value
//    var token = $('input[name="__RequestVerificationToken"]').val();

//    // Serialize the form data
//    var formData = $(this).serialize();

//    // Include the anti-forgery token in the serialized data
//    formData += '&__RequestVerificationToken=' + encodeURIComponent(token);

//    console.log('Form submitted');
//    console.log('Action URL:', this.action);

//    $.ajax({
//        url: this.action,
//        type: 'POST',
//        data: formData, // Include the serialized form data with the token
//        success: function (data) {
//            $('#resultContainer').html(data);
//        },
//        error: function () {
//            alert('An error occurred during form submission.');
//        }
//    });
//});



//$('#applicationCreateForm').submit(function (e) {
//    e.preventDefault();

//    $.ajax({
//        url: $(this).attr('action'),
//        type: 'POST',
//        data: $(this).serialize(),
//        success: function (response) {
//            if (response.success) {
//                // Close the modal when the form is successfully submitted
//                $('#ApplicationCreate-Modal').modal('hide');
//                // Optionally, you can refresh the content of the parent page if needed
//            }
//        }
//    });
//});

//$('#applicationCreateForm').on('submit', function (e) {
//    e.preventDefault();

//    $.ajax({
//        url: '@Url.Action("Create", "AR_Application")', // Adjust the route based on your setup
//        type: 'POST',
//        data: $(this).serialize(),
//        success: function (data) {
//            $('#resultContainer').html(data);
//        },
//        error: function () {
//            alert('An error occurred during form submission.');
//        }
//    });
//});