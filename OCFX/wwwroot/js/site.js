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

/* Comment Bidness */
function showCommentBox() {
    if ($('#CommentBox').prop("display", "none")){
        $('#CommentBox').show();
    } else {
        $('#CommentBox').hide();
    }
}


function showReplyBox() {
    if ($('#ReplyBox').prop("display", "none")) {
        $('#ReplyBox').show();
    } else {
        $('#ReplyBox').hide();
    }
}

function showSendBox() {
    if ($('#SendBox').prop("display", "none")) {
        $('#SendBox').show();
    } else {
        $('#SendBox').hide();
    }
}