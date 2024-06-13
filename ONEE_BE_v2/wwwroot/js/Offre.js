$(document).ready(function () {
    InitTabulator();
});


function InitTabulator() {
    GetOffres();
}

function GetOffres() {
    $.ajax({
        url: '/offres/GetOffres',
        type: 'GET',
        dataType: 'json',
        contentType: 'application/json;charset=utf-8',
        success: function (response) {
            var tabulatorData = [];
            if (response && response.length > 0) {
                // Filtrer les offres avec un statut "actif"
                var offresActives = response.filter(function (item) {
                    return item.status === "active";
                });

                $.each(offresActives, function (index, item) {
                    tabulatorData.push({
                        id: item.id,
                        titre: item.titre,
                        dateDebut: formatDate(item.dateDebut),
                        dateFin: formatDate(item.dateFin),
                        nbr_places: item.nbr_places,
                        description: item.description
                    });
                });
            }

            var table = new Tabulator("#tabulatorContainer", {
                data: tabulatorData,
                layout: "fitColumns",
                columns: [
                    { title: "Id", field: "id" },
                    { title: "Titre", field: "titre" },
                    { title: "Date Début", field: "dateDebut" },
                    { title: "Date Fin", field: "dateFin" },
                    { title: "Nombre de places", field: "nbr_places" },
                    { title: "Description", field: "description" },
                    {
                        title: "Actions",
                        formatter: function (cell, formatterParams) {
                            var id = cell.getData().id;
                            return "<button class='btn btn-primary btn-sm' onclick='Edit(" + id + ")'>Modifier</button> " +
                                "<button class='btn btn-danger btn-sm' onclick='Archive(" + id + ")'>Archiver</button>";
                        }
                    }
                ]
            });

            // Fonction de recherche dynamique
            $("#searchInput").on("input", function () {
                var searchText = $(this).val().toLowerCase();
                if (searchText.trim() === "") {
                    table.setData(tabulatorData); // Afficher toutes les offres si la recherche est vide
                } else {
                    var filteredData = tabulatorData.filter(function (item) {
                        return item.titre.toLowerCase().includes(searchText);
                    });
                    table.setData(filteredData); // Afficher les offres filtrées selon le titre
                }
            });

            $("#tabulatorContainer").show(); // Afficher la table une fois que les données sont chargées

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

/*function GetOffres1() {
    $.ajax({
        url: '/OffresArchivee/GetOffres1',
        type: 'GET',
        dataType: 'json',
        contentType: 'application/json;charset=utf-8',
        success: function (response) {
            var tabulatorData = [];
            if (response && response.length > 0) {
                // Filtrer les offres avec un statut "actif"
                var offresActives = response.filter(function (item) {
                    return item.status === "archive" || item.status === "archivee"; // Assurez-vous que le champ status correspond à votre structure de données
                });
                $.each(offresActives, function (index, item) {
                    tabulatorData.push({
                        id: item.id,
                        titre: item.titre,
                        dateDebut: formatDate(item.dateDebut),
                        dateFin: formatDate(item.dateFin),
                        nbr_places: item.nbr_places,
                        description: item.description
                    });
                });
            }
            var table = new Tabulator("#tabulatorContainer", {
                data: tabulatorData,
                layout: "fitColumns",
                columns: [
                    { title: "Id", field: "id" },
                    { title: "Titre", field: "titre" },
                    { title: "Date Début", field: "dateDebut" },
                    { title: "Date Fin", field: "dateFin" },
                    { title: "Nombre de places", field: "nbr_places" },
                    { title: "Description", field: "description" }
                    /*{
                        title: "Actions",
                        formatter: function (cell, formatterParams) {
                            var id = cell.getData().id;
                            return "<button class='btn btn-primary btn-sm' onclick='Edit(" + id + ")'>Modifier</button> " +
                                "<button class='btn btn-danger btn-sm' onclick='Delete(" + id + ")'>Supprimer</button>";
                        }
                    }
                ]
            });
            $("#searchInput").on("input", function () {
                var searchText = $(this).val().toLowerCase();
                if (searchText.trim() === "") {
                    table.setData(tabulatorData); // Afficher toutes les offres si la recherche est vide
                } else {
                    var filteredData = tabulatorData.filter(function (item) {
                        return item.titre.toLowerCase().includes(searchText);
                    });
                    table.setData(filteredData); // Afficher les offres filtrées selon le titre
                }
            });
            $("#tabulatorContainer").show(); // Afficher la table une fois que les données sont chargées
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
*/
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
                console.log(response);
            }
        },
        error: function () {
            alert('Impossible d\'enregistrer les données');
        }
    });
}

function formatDate(dateString) {
    let date = new Date(dateString);
    let year = date.getFullYear();
    let month = (date.getMonth() + 1).toString().padStart(2, '0');
    let day = date.getDate().toString().padStart(2, '0');
    return `${day}-${month}${year}-${year}`;
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
            if (!response) {
                alert('Impossible de modifier cette offre');
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
function HideEditModal() {
    $('#OffreModal').hide();
}

function Delete(id) {
    if (confirm('Vous êtes d\'accord pour supprimer cette offre ?')) {
        $.ajax({
            url: '/offres/Delete/?id=' + id,
            type: 'POST',
            success: function (response) {
                if (!response) {
                    alert('Impossible de supprimer cette offre');
                } else {
                    GetOffres();
                    HideEditModal();
                }
            },
            error: function () {
                alert('Impossible de supprimer cette offre');
            }
        });
    }
}
function Archive(id) {
    if (confirm('Vous êtes d\'accord pour archiver cette offre ?')) {
        $.ajax({
            url: '/offres/Archive/?id=' + id,
            type: 'POST',
            success: function (response) {
                if (!response) {
                    alert('Impossible d\'archiver cette offre');
                } else {
                    GetOffres();
                    HideEditModal();
                }
            },
            error: function () {
                alert('Impossible d\'archiver cette offre');
            }
        });
    }
}

