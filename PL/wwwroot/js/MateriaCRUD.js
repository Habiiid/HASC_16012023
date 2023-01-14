$(document).ready(function () { //click
    GetAll();
});

function GetAll() {
    $.ajax({
        type: 'GET',
        url: 'http://localhost:5156/api/Materia/MostrarTodasLasMaterias',
        success: function (result) { //200 OK 
            $('#myModal').modal('hide');
            $('#ModalUpdate').modal('hide');

            $('#tblContent tbody').empty();
            $.each(result.objects, function (i, materia) {
                var filas =
                    '<tr>'
                    + '<td class="text-center"> '
                    + '<a href="#" class="btn btn-warning bi bi-pencil-square" onclick="GetById(' + materia.idMateria + ')">'
                    + '</a> '
                    + '</td>'
                    + "<td class='text-center'>" + materia.nombre + "</td>"
                    + "<td class='text-center'>" + materia.costo + "</ td>"
                    + "<td class='text-center'>" + materia.descripcion + "</td>"        
                    + '<td class="text-center"> '
                    + '<a href="#" class="btn btn-danger bi bi-trash" onclick="return Eliminar(' + materia.idMateria + ')">'
                    + '</a> '
                    + '</td>'


                    + "</tr>";
                $("#tblContent tbody").append(filas);
            });
        },
        error: function (result) {
            alert('Error en la consulta.' + result.responseJSON.ErrorMessage);
        }
    });
};

//function GetById(IdMateria) {
//    $.ajax({
//        type: 'GET',
//        url: 'http://localhost:5051/api/Materia/MostrarUnaMateria/' + IdMateria,
//        success: function (result) {
//            $('#txtIdMateria').val(result.object.idMateria);
//            $('#txtNombre').val(result.object.nombre);
//            $('#txtCosto').val(result.object.costo);
//            $('#txtDescripcion').val(result.object.descripcion);
//            $('#ModalUpdate').modal('show');
//        },
//        error: function (result) {
//            alert('Error en la consulta.' + result.responseJSON.ErrorMessage);
//        }
//    });
//};


//function Actualizar() {
//    $('#ModalUpdate').modal('hide');
//    var materia = {
//        IdMateria: $('#txtIdMateria').val(),
//        Nombre: $('#txtNombre').val(),
//        Costo: $('#txtCosto').val(),
//        Descripcion: $('#txtDescripcion').val()       
//    };

//    if (materia.IdMateria == '') {
//        materia.IdMateria = 0;
//        Add(materia);
//    }
//    else {
//        Update(materia);
//    }
//};

function Modal() {
    $('#txtIdMateria').val('');
    $('#txtNombre').val('');
    $('#txtCosto').val('');
    $('#txtDescripcion').val('');
};

function ModalCerrar() {
    $('#ModalUpdate').modal('hide');
}



