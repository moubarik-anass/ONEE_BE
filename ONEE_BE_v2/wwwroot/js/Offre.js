$(document).ready(function () {
    GetOffres();
});

function GetOffres() {
    $.ajax({
        url: '/offre/GetOffres',
        type: 'GET',
        dataType: 'json',
        contentType: 'application/json;charset=utf-8',
        success: function (response) {
            if (response == null || response == undefined || response.length == 0) {
                var object = '';
                object += '<tr>';
                object += '<td colspan="7">Aucune offre disponible</td>';
                object += '</tr>';
                $('#tblBody').html(object);
            } else {
                var object = '';
                $.each(response, function (index, item) {
                    object += '<tr>';
                    object += '<td>' + item.id + '</td>';
                    object += '<td>' + item.titre + '</td>';
                    object += '<td>' + item.dateDebut + '</td>';
                    object += '<td>' + item.dateFin + '</td>';
                    object += '<td>' + item.nbr_places + '</td>';
                    object += '<td>' + item.description + '</td>';
                    object += '<td><a href="#" class="btn btn-primary btn-sm" onclick="Edit(' + item.id + ')">Modifier</a> <a href="#" class="btn btn-danger btn-sm" onclick="Delete(' + item.id + ')">Supprimer</a></td>';
                    object += '</tr>';
                });
                $('#tblBody').html(object);
            }
        },
        error: function () {
            alert('Impossible d\'afficher les offres');
        }
    });
}

$('#btnAdd').click(function () {
    $('#OffreModal').modal('show');
    $('#modalTitle').text('Ajouter une offre');
    $('#Save').show();
    $('#Update').hide();
    ClearData();
});

function Insert() {
    var result = Validate();
    if (result == false) {
        return false;
    }

    var formData = {
        id: $('#Id').val(),
        titre: $('#Titre').val(),
        dateDebut: $('#dateDebut').val(),
        dateFin: $('#dateFin').val(),
        nbr_places: $('#nbr_places').val(),
        description: $('#Description').val()
    };

    $.ajax({
        url: '/offre/Insert',
        type: 'POST',
        data: formData,
        success: function (response) {
            if (response == null || response == undefined || response.length == 0) {
                alert('Impossible d\'enregistrer les données');
            } else {
                HideModal();
                GetOffres();
                alert(response);
            }
        },
        error: function () {
            alert('Impossible d\'enregistrer les données');
        }
    });
}

function HideModal() {
    ClearData();
    $('#OffreModal').modal('hide');
}

function ClearData() {
    $('#Id').val('');
    $('#Titre').val('');
    $('#dateDebut').val('');
    $('#dateFin').val('');
    $('#nbr_places').val('');
    $('#Description').val('');

    $('#Titre').css('border-color', 'lightgrey');
    $('#dateDebut').css('border-color', 'lightgrey');
    $('#dateFin').css('border-color', 'lightgrey');
    $('#nbr_places').css('border-color', 'lightgrey');
    $('#Description').css('border-color', 'lightgrey');
}

function Validate() {
    var isValid = true;
    if ($('#Titre').val().trim() === "") {
        $('#Titre').css('border-color', 'Red');
        isValid = false;
    } else {
        $('#Titre').css('border-color', 'lightgrey');
    }
    if ($('#dateDebut').val().trim() === "") {
        $('#dateDebut').css('border-color', 'Red');
        isValid = false;
    } else {
        $('#dateDebut').css('border-color', 'lightgrey');
    }
    if ($('#dateFin').val().trim() === "") {
        $('#dateFin').css('border-color', 'Red');
        isValid = false;
    } else {
        $('#dateFin').css('border-color', 'lightgrey');
    }
    if ($('#nbr_places').val().trim() === "") {
        $('#nbr_places').css('border-color', 'Red');
        isValid = false;
    } else {
        $('#nbr_places').css('border-color', 'lightgrey');
    }
    if ($('#Description').val().trim() === "") {
        $('#Description').css('border-color', 'Red');
        isValid = false;
    } else {
        $('#Description').css('border-color', 'lightgrey');
    }
    return isValid;
}

$('#Titre').change(function () {
    Validate();
});
$('#dateDebut').change(function () {
    Validate();
});
$('#dateFin').change(function () {
    Validate();
});
$('#nbr_places').change(function () {
    Validate();
});
$('#Description').change(function () {
    Validate();
});

function Edit(id) {
    $.ajax({
        url: '/offre/Edit?id=' + id,
        type: 'GET',
        dataType: 'json',
        contentType: 'application/json;charset=utf-8',
        success: function (response) {
            if (response == null || response == undefined) {
                alert('Impossible de modifier cette offre');
            } else if (response.length === 0) {
                alert('Les données ne sont pas accessibles avec l\'ID ' + id);
            } else {
                $('#OffreModal').modal('show');
                $('#modalTitle').text('Modifier une offre');
                $('#Save').hide();
                $('#Update').show();
                $('#Id').val(response.id);
                $('#Titre').val(response.titre);
                $('#dateDebut').val(response.dateDebut);
                $('#dateFin').val(response.dateFin);
                $('#nbr_places').val(response.nbr_places);
                $('#Description').val(response.description);
            }
        },
        error: function () {
            alert('Impossible de modifier les données');
        }
    });
}

function Update() {
    var result = Validate();
    if (result === false) {
        return false;
    }

    var formData = {
        id: $('#Id').val(),
        titre: $('#Titre').val(),
        dateDebut: $('#dateDebut').val(),
        dateFin: $('#dateFin').val(),
        nbr_places: $('#nbr_places').val(),
        description: $('#Description').val()
    };

    $.ajax({
        url: '/offre/Update',
        type: 'POST',
        data: formData,
        success: function (response) {
            if (response == null || response == undefined || response.length === 0) {
                alert('Impossible de modifier les données');
            } else {
                HideModal();
                GetOffres();
                alert(response);
            }
        },
        error: function () {
            alert('Impossible de modifier les données');
        }
    });
}

function Delete(id) {
    if (confirm('Vous êtes d\'accord pour supprimer cette offre ?')) {
        $.ajax({
            url: '/offre/Delete?id=' + id,
            type: 'POST',
            success: function (response) {
                if (response == null || response == undefined) {
                    alert('Impossible de supprimer cette offre');
                } else {
                    GetOffres();
                }
            },
            error: function () {
                alert('Impossible de supprimer cette offre');
            }
        });
    }
}