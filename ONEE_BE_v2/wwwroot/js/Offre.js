$(document).ready(function () {
    GetOffres();
});

function GetOffres() {
    $.ajax({
        url: '/offres/GetOffres',
        type: 'GET',
        dataType: 'json',
        contentType: 'application/json;charset=utf-8',
        success: function (response) {
            if (!response || response.length === 0) {
                var object = '<tr><td colspan="7">' + 'Aucune offre disponible' + '</td></tr>';
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
        error: function (xhr, status, error) {
            alert('Impossible d\'afficher les offres : ' + error);
            try {
                // Actions spécifiques en cas d'erreur côté client
            } catch (ex) {
                console.error('Erreur côté client : ' + ex.message);
            }
        }
    });
}

$('#btnAdd').on('click', function () {
    $('#OffreModal').modal('show');
    $('#modalTitle').text('Ajouter une offre');
    $('#Save').show();
    $('#Update').hide();
    ClearData();
});

function Insert() {
    var result = Validate();
    if (!result) {
        return false;
    }

    var formData = {
        id: $('#id').val(),
        titre: $('#Titre').val(),
        DateDebut: $('#dateDebut').val(),
        DateFin: $('#dateFin').val(),
        Nbr_places: $('#nbr_places').val(),
        description: $('#Description').val()
    };

    $.ajax({
        url: '/offres/Insert',
        type: 'POST',
        data: JSON.stringify(formData),
        contentType: 'application/json',
        success: function (response) {
            if (!response) {
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
    $('#id, #Titre, #dateDebut, #dateFin, #nbr_places, #Description').val('');
    $('#Titre, #dateDebut, #dateFin, #nbr_places, #Description').css('border-color', 'lightgrey');
}

function Validate() {
    var isValid = true;
    $('#Titre, #dateDebut, #dateFin, #nbr_places, #Description').each(function () {
        if (!$(this).val().trim()) {
            $(this).css('border-color', 'Red');
            isValid = false;
        } else {
            $(this).css('border-color', 'lightgrey');
        }
    });
    return isValid;
}

$('#Titre, #dateDebut, #dateFin, #nbr_places, #Description').on('change', function () {
    Validate();
});

function Edit(id) {
    $.ajax({
        url: '/offres/Edit/?id=' + id,
        type: 'GET',
        dataType: 'json',
        contentType: 'application/json;charset=utf-8',
        success: function (response) {
            if (!response) {
                alert('Impossible de modifier cette offre');
                console.log("impossible")
            } else if (response.length === 0) {
                alert('Les données ne sont pas accessibles avec l\'ID ' + id);
            } else {
                $('#OffreModal').modal('show');
                $('#modalTitle').text('Modifier une offre');
                $('#Save').hide();
                $('#Update').show();
                $('#id').val(response.id);
                $('#titre').val(response.titre);
                $('#dateDebut').val(response.dateDebut);
                $('#dateFin').val(response.dateFin);
                $('#nbr_places').val(response.nbr_places);
                $('#description').val(response.description);
            }
        },
        error: function () {
            alert('Impossible de modifier les données');
        }
    });
}

function Update() {
    var result = Validate();
    if (!result) {
        return false;
    }

    var formData = {
        id: $('#id').val(),
        titre: $('#titre').val(),
        dateDebut: $('#dateDebut').val(),
        dateFin: $('#dateFin').val(),
        nbr_places: $('#nbr_places').val(),
        description: $('#description').val()
    };

    $.ajax({
        url: '/offres/Update',
        type: 'POST',
        data: JSON.stringify(formData),
        contentType: 'application/json',
        success: function (response) {
            if (!response) {
                alert('Impossible de modifier les données');
                console.log("impossible")
            } else {
                HideModal();
                GetOffres();
                alert(response);
                console.log("done" + response)
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
            url: '/offres/Delete?id=' + id,
            type: 'POST',
            success: function (response) {
                if (!response) {
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
