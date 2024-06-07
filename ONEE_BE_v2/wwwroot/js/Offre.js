$(document).ready(function () {
    GetOffres();
});
function formatDate(dateString) {
    // Create a new Date object from the date string
    let date = new Date(dateString);

    // Get the year, month, and day parts
    let year = date.getFullYear();
    let month = (date.getMonth() + 1).toString().padStart(2, '0'); // Months are 0-based
    let day = date.getDate().toString().padStart(2, '0');

    // Return the formatted date string
    return `${year}-${month}-${day}`;
};
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
                console.log(response);
                var object = '';
                var searchTerm = $('#searchInput').val().toLowerCase();
                var offresTrouvees = false;
                $.each(response, function (index, item) {
                    if (item.titre.toLowerCase().includes(searchTerm)) {
                        offresTrouvees = true;
                        object += '<tr>';
                        object += '<td>' + item.id + '</td>';
                        object += '<td>' + item.titre + '</td>';
                        object += '<td>' + formatDate(item.dateDebut) + '</td>';
                        object += '<td>' + formatDate(item.dateFin) + '</td>';
                        object += '<td>' + item.nbr_places + '</td>';
                        object += '<td>' + item.description + '</td>';
                        object += '<td><a href="#" class="btn btn-primary btn-sm" onclick="Edit(' + item.id + ')">Modifier</a> <a href="#" class="btn btn-danger btn-sm" onclick="Delete(' + item.id + ')">Supprimer</a></td>';
                        object += '</tr>';
                    }
                });
                if (!offresTrouvees) {
                    object = '<tr><td colspan="7">' + 'Aucune offre correspondant à la recherche' + '</td></tr>';
                }
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

$('#searchButton').click(function () {
    GetOffres();
});

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
        Titre: $('#titre').val(),
        dateDebut: formatDate($('#dateDebut').val()),
        dateFin: formatDate($('#dateFin').val()),
        nbr_places: $('#nbr_places').val(),
        Description: $('#description').val()
    };

    console.log(formData);
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
    $('#titre, #dateDebut, #dateFin, #nbr_places, #description').val('');
    $('#titre, #dateDebut, #dateFin, #nbr_places, #description').css('border-color', 'lightgrey');
}

function Validate() {
    var isValid = true;
    $('#titre, #dateDebut, #dateFin, #nbr_places, #description').each(function () {
        if (!$(this).val().trim()) {
            $(this).css('border-color', 'Red');
            isValid = false;
        } else {
            $(this).css('border-color', 'lightgrey');
        }
    });
    return isValid;
}

$('#titre, #dateDebut, #dateFin, #nbr_places, #description').on('change', function () {
    Validate();
});

function Edit(id) {
    $.ajax({
        url: '/offres/Edit/?id=' + id,
        type: 'GET',
        dataType: 'json',
        contentType: 'application/json;charset=utf-8',
        success: function (response) {
            selectedItemID = id;
            console.log(selectedItemID)
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
                $('#dateDebut').val(formatDate(response.dateDebut));
                $('#dateFin').val(formatDate(response.dateFin));
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
        Id: $('#id').val(),
        Titre: $('#titre').val(),
        dateDebut: $('#dateDebut').val(),
        dateFin: $('#dateFin').val(),
        nbr_places: $('#nbr_places').val(),
        Description: $('#description').val()
    };

    $.ajax({
        url: '/offres/Update',
        type: 'POST',
        data: JSON.stringify(formData),
        contentType: 'application/json;charset=utf-8',
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
