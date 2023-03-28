/*
	Minimaxing by HTML5 UP
	html5up.net | @ajlkn
	Free for personal and commercial use under the CCA 3.0 license (html5up.net/license)
*/

(function($) {

	var $window = $(window),
		$body = $('body');

	// Breakpoints.
		breakpoints({
			xlarge:  [ '1281px',  '1680px' ],
			large:   [ '981px',   '1280px' ],
			medium:  [ '737px',   '980px'  ],
			small:   [ null,      '736px'  ]
		});

	// Nav.

		// Title Bar.
			$(
				'<div id="titleBar">' +
					'<a href="#navPanel" class="toggle"></a>' +
					'<span class="title">' + $('#logo').html() + '</span>' +
				'</div>'
			)
				.appendTo($body);

		// Navigation Panel.
			$(
				'<div id="navPanel">' +
					'<nav>' +
						$('#nav').navList() +
					'</nav>' +
				'</div>'
			)
				.appendTo($body)
				.panel({
					delay: 500,
					hideOnClick: true,
					hideOnSwipe: true,
					resetScroll: true,
					resetForms: true,
					side: 'left',
					target: $body,
					visibleClass: 'navPanel-visible'
				});

})(jQuery);

$(function() {
  //name part From To
  $("#name").on("keyup", function() {
    var value = $(this).val().toLowerCase();
    $("#my-table").find('tr').filter(function() {
      $(this).toggle($(this).find('td').text().toLowerCase().indexOf(value) > -1)
    });
  });


  $("#from,#to").bind('keyup change', function() {

    var val1 = moment($('#from').val().toLowerCase(), 'YYYY/MM/DD');
    var val2 = moment($('#to').val().toLowerCase(), 'YYYY/MM/DD');

    $("#my-table").find('tr').filter(function() {
      $(this).toggle((moment($(this).find('td').text().toLowerCase(),
        'YYYY/MM/DD') >= (val1) || !val1["_isValid"]) && (moment($(this).find('td').text()
        .toLowerCase(),
        'YYYY/MM/DD') <= (val2) || !val2["_isValid"]))
    });
    //console.log(val1["_isValid"], val2["_isValid"]);
  })


});
