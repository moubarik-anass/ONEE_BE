$(function () {
    GetOffres();
});

function GetOffres() {
    $.ajax({
        url: '/offre/GetOffres',
        type: 'GET',
        dataType: 'json',
        contentType: 'application/json;charset=utf-8',
        success: function (response) {
            if (!response || response.length === 0) {
                var object = '<tr><td colspan="7">' +'Aucune offre disponible' + '</td></tr>';
                $('#tblBody').html(object);
            } else {
                var object = '';
                $.each(response, function (index, item) {
                    object += '<tr>';
                    object += '<td>' + item.id + '</td>';
                    object += '<td>' + item.titre + '</td>';
                    object += '<td>' + item.DateDebut + '</td>';
                    object += '<td>' + item.DateFin + '</td>';
                    object += '<td>' + item.Nbr_places + '</td>';
                    object += '<td>' + item.description + '</td>';
                    object += '<td><a href="#" class="btn btn-primary btn-sm" onclick="EditData(' + item.Id + ')">Modifier</a> <a href="#" class="btn btn-danger btn-sm" onclick="DeleteData(' + item.Id + ')">Supprimer</a></td>';
                    object += '</tr>';
                });
                $('#tblBody').html(object);
            }
        },
        error: function (xhr, status, error) {
            // Affichage de l'erreur sp�cifique
            alert('Impossible d\'afficher les offres : ' + error);

            // Utilisation d'un bloc try-catch pour g�rer les erreurs c�t� client
            try {
                // Vous pouvez ajouter ici des actions sp�cifiques � effectuer en cas d'erreur c�t� client
            } catch (ex) {
                console.error('Erreur c�t� client : ' + ex.message);
            }
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
    if (!result) {
        return false;
    }

    var formData = {
        Id: $('#Id').val(),
        Titre: $('#Titre').val(),
        dateDebut: $('#dateDebut').val(),
        dateFin: $('#dateFin').val(),
        nbr_places: $('#nbr_places').val(),
        Description: $('#Description').val()
    };

    $.ajax({
        url: '/offre/Insert',
        type: 'POST',
        data: JSON.stringify(formData),
        contentType: 'application/json',
        success: function (response) {
            if (!response) {
                alert('Impossible d\'enregistrer les donn�es');
            } else {
                HideModal();
                GetOffres();
                alert(response);
            }
        },
        error: function () {
            alert('Impossible d\'enregistrer les donn�es');
        }
    });
}

function HideModal() {
    ClearData();
    $('#OffreModal').modal('hide');
}

function ClearData() {
    $('#Id, #Titre, #dateDebut, #dateFin, #nbr_places, #Description').val('');
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

$('#Titre, #dateDebut, #dateFin, #nbr_places, #Description').change(function () {
    Validate();
});

function Edit(id) {
    $.ajax({
        url: '/offre/Edit?id=' + id,
        type: 'GET',
        dataType: 'json',
        contentType: 'application/json;charset=utf-8',
        success: function (response) {
            if (!response) {
                alert('Impossible de modifier cette offre');
            } else if (response.length === 0) {
                alert('Les donn�es ne sont pas accessibles avec l\'ID ' + id);
            } else {
                $('#OffreModal').modal('show');
                $('#modalTitle').text('Modifier une offre');
                $('#Save').hide();
                $('#Update').show();
                $('#Id').val(response.Id);
                $('#Titre').val(response.Titre);
                $('#dateDebut').val(response.dateDebut);
                $('#dateFin').val(response.dateFin);
                $('#nbr_places').val(response.nbr_places);
                $('#Description').val(response.Description);
            }
        },
        error: function () {
            alert('Impossible de modifier les donn�es');
        }
    });
}

function Update() {
    var result = Validate();
    if (!result) {
        return false;
    }

    var formData = {
        Id: $('#Id').val(),
        Titre: $('#Titre').val(),
        dateDebut: $('#dateDebut').val(),
        dateFin: $('#dateFin').val(),
        nbr_places: $('#nbr_places').val(),
        Description: $('#Description').val()
    };

    $.ajax({
        url: '/offre/Update',
        type: 'POST',
        data: JSON.stringify(formData),
        contentType: 'application/json',
        success: function (response) {
            if (!response) {
                alert('Impossible de modifier les donn�es');
            } else {
                HideModal();
                GetOffres();
                alert(response);
            }
        },
        error: function () {
            alert('Impossible de modifier les donn�es');
        }
    });
}

function Delete(id) {
    if (confirm('Vous �tes d\'accord pour supprimer cette offre ?')) {
        $.ajax({
            url: '/offre/Delete?id=' + id,
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
