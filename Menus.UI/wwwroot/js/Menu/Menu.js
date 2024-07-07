$(document).ready(function () {
	$('#tblmenu').DataTable({
		"ajax": {
			"url": "/api/ApiMenus",
			"dataSrc": ""
		},
		"pageLength": 15,
		"language": {
			"lengthMenu": "Mostrar _MENU_ registros por pagina",
			"zeroRecords": "No se encontraron registros",
			"info": "Mostrar pagina _PAGE_ de _PAGES_",
			"infoEmpty": "No se encontraron registros",
			"infoFiltered": "(filtrado de _MAX_ registros totales)",
			"search": "Buscar:",
			"paginate": {
				"next": "siguiente",
				"previous": "Anterior"
			}
		},
		"scrollX": true,
		"responsive": true,
		"columns": [
			{ data: 'id_menu', "visible": false },
			{ data: 'menu_coach' },
			{ data: 'menu_alumno' },
			{ data: 'menu_ejer' },
			{ data: 'menu_fecha' },
			{
				data: "id", render: function (data, type, row, meta) {
					return "<button class='btn btn-success btn-sm' onclick='Edit(" + JSON.stringify(row) + ")'>Editar</button>" +
						"<button class='btn btn-danger btn-sm'  onclick='Delete(" + JSON.stringify(row) + ")'>Eliminar</button>";
				}
			}
		]
	});

	$("#btnsave").click(function () {

		var oMenu = {
			id_menu: $.trim($("#id_menu").val()) == "" ? 0 : $("#id_menu").val(),
			menu_titulo: $("#menu_coach").val(),
			menu_desc: $("#menu_alumno").val(),
			menu_observ: $("#menu_ejer").val(),
			menu_fecha: $("#menu_fecha").val(),
		};

		$.post("/api/ApiMenus", { oMenu: oMenu })
			.done(function (data) {
				Reset();
				ReloadGrid();
				swal('Mensaje de confirmacion', 'Registro Grabado', 'success');
			});
	});

	$("#btnAddnew").click(function () {
		Reset();
	});
});

function Edit(oMenu) {
	$("#id_menu").val(oMenu.id_menu);
	$("#menu_coach").val(oMenu.menu_titulo);
	$("#menu_alumno").val(oMenu.menu_desc);
	$("#menu_ejer").val(oMenu.menu_observ);
	$("#menu_fecha").val(oMenu.menu_fecha);
}

function Delete(oMenu) {
	var id = oMenu.id_menu;
	$.ajax({
		url: "/api/ApiMenus/" + id,
		type: "DELETE",
		success: function (result) {
			swal('Mensaje de confirmacion', 'Menu Eliminado', 'success');
			ReloadGrid();
		}
	});
}

function Reset() {
	$("#id_menu").val(0);
	$("#menu_coach").val("");
	$("#menu_alumno").val("");
	$("#menu_ejer").val("");
	$("#menu_fecha").val("");
}
function ReloadGrid() {
	$("#tblmenu").DataTable().clear();
	$("#tblmenu").DataTable().ajax.reload();
}