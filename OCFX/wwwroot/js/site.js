$(document).ready(
	// Event listener for the boxes
	$('input.pointBox').change(function () {
		pointCount();
	})
);

// GOOGLE MAPS CODE //
function myMap() {
	var mapProp = {
		center: new google.maps.LatLng(38.200946, -85.601353),
		zoom: 10
	};
	var map = new google.maps.Map(document.getElementById("googleMap"), mapProp);
}

// SLIDE IN CODE //
var $window = $(window),
	win_height_padded = $window.height() * 1.15;

$(window).on('scroll', revealOnScroll);

function revealOnScroll() {
	var scrolled = $window.scrollTop();
	$(".revealOnScroll:not(.animated)").each(function () {
		var $this = $(this),
			offsetTop = $this.offset().top;

		if (scrolled + win_height_padded > offsetTop) {
			if ($this.data('timeout')) {
				window.setTimeout(function () {
					$this.addClass('animated' + $this.data('animation'));
				}, parseInt($this.data('timeout'), 10));
			} else {
				$this.addClass('animated ' + $this.data('animation'));
			}
		}
	});
}

var o = {
	"point": {
		"X": 0,
		"Y": 0
	}
};


/**
 * A password validation function
 * 
 * */
function passwordValidation() {
    var myInput = document.getElementById("psw");
    var letter = document.getElementById("letter");
    var capital = document.getElementById("capital");
    var number = document.getElementById("number");
    var length = document.getElementById("length");

    // When the user clicks on the password field, show the message box
    myInput.onfocus = function () {
        document.getElementById("message").style.display = "block";
    }

    // When the user clicks outside of the password field, hide the message box
    myInput.onblur = function () {
        document.getElementById("message").style.display = "none";
    }

    // When the user starts to type something inside the password field
    myInput.onkeyup = function () {
        // Validate lowercase letters
        var lowerCaseLetters = /[a-z]/g;
        if (myInput.value.match(lowerCaseLetters)) {
            letter.classList.remove("invalid");
            letter.classList.add("valid");
        } else {
            letter.classList.remove("valid");
            letter.classList.add("invalid");
        }

        // Validate capital letters
        var upperCaseLetters = /[A-Z]/g;
        if (myInput.value.match(upperCaseLetters)) {
            capital.classList.remove("invalid");
            capital.classList.add("valid");
        } else {
            capital.classList.remove("valid");
            capital.classList.add("invalid");
        }

        // Validate numbers
        var numbers = /[0-9]/g;
        if (myInput.value.match(numbers)) {
            number.classList.remove("invalid");
            number.classList.add("valid");
        } else {
            number.classList.remove("valid");
            number.classList.add("invalid");
        }

        // Validate length
        if (myInput.value.length >= 8) {
            length.classList.remove("invalid");
            length.classList.add("valid");
        } else {
            length.classList.remove("valid");
            length.classList.add("invalid");
        }
    };
}

/**
 * Counts all the points for the register page
 */
function pointCount() {
	var pointsAvailable = document.getElementById("point-stock");
	var pointTotal =
		parseInt($("#strengthRegister").val())
		+ parseInt($('#concentrationRegister').val())
		+ parseInt($('#constitutionRegister').val())
		+ parseInt($('#dexterityRegister').val())
		+ parseInt($('#speedRegister').val())
		+ parseInt($('#motivationRegister').val());

	pointsAvailable.innerText = pointTotal;

	if (pointTotal > 30) {
		$('#point-stock').addClass('text-danger');
		$('#submitButton').prop("disabled", true);
	} else if(pointTotal === 30) {
		$('#point-stock').addClass('text-success');
		$('#submitButton').removeAttr("disabled");
	} else {
		$('#point-stock').removeClass('text-danger').removeClass('text-success');
		$('#submitButton').prop("disabled", true);
	}
}

/* Comment/Messaging Bidness */
function showCommentBox() {
    $('#CommentBox').slideToggle();
}

/**
 * Shows the reply box for the page
 * */
function showReplyBox() {
        $('#ReplyBox').slideToggle();
}

/**
 * Shows the send message box for the page
 * */
function showSendBox() {
        $('#SendBox').slideToggle();
}

/*
 * Shows the delete confirmation
 * */
function showDeleteBox() {
        $('#DeleteBox').slideToggle();
}