"use strict";
// Class definition

var KTDatatableJsonRemoteDemo = function () {
	// Private functions

	// basic demo
	var demo = function () {

		var datatable = $('.kt-datatable').KTDatatable({
			// datasource definition
			data: {
				type: 'remote',
				source: '/Admin/Tag/GetAll',
				
				pageSize: 10,
			},

			// layout definition
			layout: {
				scroll: false, // enable/disable datatable scroll both horizontal and vertical when needed.
				footer: false // display/hide footer
			},

			// column sorting
			sortable: true,

			pagination: true,

			search: {
				input: $('#generalSearch')
			},

			// columns definition
			columns: [
				{
					field: 'tagName',
					title: 'Name',
					
				},  {
					field: 'Actions',
					title: 'Actions',
					sortable: false,
					width: 110,
					autoHide: false,
					overflow: 'visible',
					template: function (row) {
						console.log(row.id);
						return `\
						<a href="/Admin/Tag/Upsert/${row.id}" class="btn btn-primary" title="Edit details">\
							<i class="flaticon2-edit icon-2x"></i>\
						</a>\
                         <button type="button" class="btn btn-danger" data-toggle="modal" data-target="#exampleModalCenter">
							 <i class="flaticon-delete-1 icon-2x"></i>
						</button>
                         <div class="modal hide" id="exampleModalCenter" data-backdrop="static" tabindex="1" role="dialog" aria-labelledby="staticBackdrop" aria-hidden="true" >\
								<div class="modal-dialog modal-dialog-centered" role="document">\
										<div class="modal-content">\
												 <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#exampleModalCenter">\
														<h2>Alert</h2><h5 class="text-white">Are You Sure Want To Delete ??</h5>\
												 </button>\
												<button type="button" class="btn btn-danger" data-toggle="modal" data-target="#exampleModalCenter">\
														<a href="/Admin/Tag/DeleteAPI/${row.id}"<i class="flaticon-delete-1 text-black-50 icon-4x"></i> <h5 class="text-white">Delete!!</h5></a>\
												</button>\
												<button type="button" class="btn btn-danger" data-toggle="modal" data-target="#exampleModalCenter">\
														<i class="flaticon2-delete text-black-50 icon-4x"></i><h5 class="text-white">Cancel</h5>\
												</button>\
							 </div>\
							</div>\
						</div>\
					`;
					},
				}],

		});

    $('#kt_form_status').on('change', function() {
      datatable.search($(this).val().toLowerCase(), 'Status');
    });

    $('#kt_form_type').on('change', function() {
      datatable.search($(this).val().toLowerCase(), 'Type');
    });

    $('#kt_form_status,#kt_form_type').selectpicker();

	};

	return {
		// public functions
		init: function () {
			demo();
		}
	};
}();

jQuery(document).ready(function () {
	KTDatatableJsonRemoteDemo.init();
});