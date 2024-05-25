$(document).ready(function () {
    GetOffres();
});

function GetOffres() {
    $.ajax({
        url: '/offre/GetOffres',
        type: 'get',
        dataType: 'json',
        contentType: 'application/json;charset=utf-8',
        success: function (response) {
            if (response == null || response == undefined || response.length == 0) {
                var object = '';
                object += '<tr>';
                object += '<td colspan="5">' + 'Les offres ne sont pas disponnibles' + '</td>';
                object += '</tr>';
                $('#tblBody').html(object);
            }
            else {
                var object = '';
                $.each(response, function (index, item) {
                    object += '<tr>';
                    object += '<td>' + item.Id + '</td>';
                    object += '<td>' + item.Titre + '</td>';
                    object += '<td>' + item.dateDebut + '</td>';
                    object += '<td>' + item.dateFin + '</td>';
                    object += '<td>' + item.nbr_places + '</td>';
                    object += '<td>' + item.Description + '</td>';
                    object += '<td> <a href="#" class="btn btn-primary btn-sm" onclick="Edit(' + item.Id + ')">Edit</a> <a href="#" class="btn btn-danger btn-sm" onclick="Delete(' + item.Id + ')">Delete</a></td>';
                    object += '</tr>';
                });
                $('#tblBody').html(object);
            }
        },
        error: function () {
            alert('impossible d afficher les offres');
        }
    });
}

$('#btnAdd').click(function () {
    $('#OffreModal').modal('show');
    $('#modalTitle').text('Ajouter Offre');
});
function Insert() {
    var result = Validate();
    if (result == false) {
        return false;
    }

    var formData = new Object();
    formData.Id = $('#Id').val();
    formData.Titre = $('#Titre').val();
    formData.dateDebut = $('#dateDebut').val();
    formData.dateFin = $('#dateFin').val();
    formData.nbr_places = $('#nbr_places').val();
    formData.Description = $('#Description').val();

    $.ajax({
        url: '/offre/Insert',
        data: formData,
        type: 'post',
        success: function (response) {
            if (response == null || response == undefined || response.length == 0) {
                alert('impossible de enregistrer les données');
            }
            else {
                HideModal();
                GetOffres();
                alert(response);
            }
        },
        error: function () {
            alert('impossible d afficher les offres');
        }
    });

}


function HideModal() {
    ClearData();
    $('#OffreModal').modal('hide');
}

function ClearData() {
    //$('#Id').val();
    $('#Titre').val();
    $('#dateDebut').val();
    $('#dateFin').val();
    $('#nbr_places').val();
    $('#Description').val();

    $('#Titre').css('border-color', 'lightgrey');
    $('#dateDebut').css('border-color', 'lightgrey');
    $('#dateFin').css('border-color', 'lightgrey');
    $('#nbr_places').css('border-color', 'lightgrey');
    $('#Description').css('border-color', 'lightgrey');

}

function Validate() {
    var isValid = true;
    if ($('#Titre').val().trim() == "") {
        $('#Titre').css('border-color', 'Red');
        isValid = false;
    } else {
        $('#Titre').css('border-color', 'lightgrey');
    }
    if ($('#dateDebut').val().trim() == "") {
        $('#dateDebut').css('border-color', 'Red');
        isValid = false;
    } else {
        $('#dateDebut').css('border-color', 'lightgrey');
    }
    if ($('#dateFin').val().trim() == "") {
        $('#dateFin').css('border-color', 'Red');
        isValid = false;
    } else {
        $('#dateFin').css('border-color', 'lightgrey');
    }
    if ($('#nbr_places').val().trim() == "") {
        $('#nbr_places').css('border-color', 'Red');
        isValid = false;
    } else {
        $('#nbr_places').css('border-color', 'lightgrey');
    }
    if ($('#Description').val().trim() == "") {
        $('#Description').css('border-color', 'Red');
        isValid = false;
    } else {
        $('#Description').css('border-color', 'lightgrey');
    }
    return isValid;
}
$('#Titre').change(function () {
    Validate();
})
$('#dateDebut').change(function () {
    Validate();
})
$('#dateFin').change(function () {
    Validate();
})
$('#nbr_places').change(function () {
    Validate();
})
$('#Description').change(function () {
    Validate();
})


function Edit(id) {
    $.ajax({
        url: '/offre/Edit?id' + id,
        type: 'get',
        dataType: 'json',
        contentType: 'application/json;charset=utf-8',
        success: function (response) {
            if (response == null || response == undefined) {
                alert('impossible de modifier cette offre');
            }
            else if (response.length == 0) {
                alert('les données ne sont pas accessibles avec id ' + id);

            } else {
                $('#OffreModal').modal('show');
                $('#modalTitle').text('Modifier Offre');
                $('#Save').css('display', 'none');
                $('#Update').css('display', 'block');
                $('#Id').val(response.Id);
                $('#Titre').val(response.Titre);
                $('#dateDebut').val(response.dateDebut);
                $('#dateFin').val(response.dateFin);
                $('#nbr_places').val(response.nbr_places);
                $('#Description').val(response.Description);
            }
        },
        error: function () {
            alert('impossible de modifier les données');
        }
    });
}

function Update() {
    var result = Validate();
    if (result == false) {
        return false;
    }
    var formData = new Object();
    formData.Id = $('#Id').val();
    formData.Titre = $('#Titre').val();
    formData.dateDebut = $('#dateDebut').val();
    formData.dateFin = $('#dateFin').val();
    formData.nbr_places = $('#nbr_places').val();
    formData.Description = $('#Description').val();

    $.ajax({
        url: '/offre/Update',
        data: formData, 
        type: 'post',
        success: function (response) {
            if (response == null || response == undefined || response.length == 0) {
                alert('impossible de modifier les données');
            }
            else {
                HideModal();
                GetOffres();
                alert(response);
            }
        },
        error: function () {
            alert('impossible de modifier les données');
        }
    });
}

function Delete(id) {
    if (confirm('Vous etes d accord de supprimer cette offre ?') {
    $.ajax({
        url: '/offre/Delete?id' + id,
        type: 'post',
        success: function (response) {
            if (response == null || response == undefined) {
                alert('impossible de supprimer cette offre');
            }else {
                GetOffres();
                alert('impossible d afficher les offres');
            }
        },
        error: function () {
            alert('impossible de supprimer cette offre');
        }
    });

    }
}