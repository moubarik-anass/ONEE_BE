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
                var offresActives = response.filter(function (item) {
                    return item.status === "active";
                });
                $.each(offresActives, function (index, item) {
                    if (new Date(item.dateFin) < new Date()) {
                        ArchiveOffer(item.id);
                    } else {
                        tabulatorData.push({
                            id: item.id,
                            titre: item.titre,
                            dateDebut: formatDate0(item.dateDebut),
                            dateFin: formatDate0(item.dateFin),
                            nbr_places: item.nbr_places,
                            diplome: item.diplome,
                            specialite: item.specialite,
                            centreConcours: item.centreConcours,
                            age: item.age,
                            path: item.path,
                            description: item.description
                        });
                    }
                });
            }
            var table = new Tabulator("#tabulatorContainer", {
                data: tabulatorData,
                layout: "fitColumns",
                columns: [
                    { title: "Id", field: "id", width: 300 },
                    { title: "Titre", field: "titre" },
                    { title: "Date Début", field: "dateDebut" },
                    { title: "Date Fin", field: "dateFin" },
                    { title: "Nombre de places", field: "nbr_places", width: 113 },
                    { title: "Diplome", field: "diplome" },
                    { title: "Specialite", field: "specialite" },
                    { title: "Centre de Concours", field: "centreConcours" },
                    { title: "Age", field: "age", width: 113 },
                    {
                        title: "Fichier Decision",
                        field: "id",
                        formatter: function (cell, formatterParams, onRendered) {
                            var id = cell.getValue();
                            return "<a href='/offres/DownloadFile?id=" + id + "' target='_blank'>Cliquer ici pour télécharger</a>";
                        }
                    },
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
            $("#searchInput").on("input", function () {
                var searchText = $(this).val().toLowerCase();
                if (searchText.trim() === "") {
                    table.setData(tabulatorData);
                } else {
                    var filteredData = tabulatorData.filter(function (item) {
                        return item.titre.toLowerCase().includes(searchText);
                    });
                    table.setData(filteredData);
                }
            });
            $("#tabulatorContainer").show();
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


function ArchiveOffer(id) {
    $.ajax({
        url: '/offres/Archive/?id=' + id,
        type: 'POST',
        success: function (response) {
            if (!response) {
                console.error('Impossible d\'archiver l\'offre avec ID ' + id);
            }
        },
        error: function () {
            console.error('Erreur lors de l\'archivage de l\'offre avec ID ' + id);
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
    if (!Validate()) {
        return false;
    }
    var formData = new FormData();
    formData.append('Titre', $('#titre').val());
    formData.append('dateDebut', formatDate($('#dateDebut').val()));
    formData.append('dateFin', formatDate($('#dateFin').val()));
    formData.append('nbr_places', $('#nbr_places').val());
    formData.append('Diplome', $('#diplome').val());
    formData.append('Specialite', $('#specialite').val());
    formData.append('CentreConcours', $('#centreConcours').val());
    formData.append('Age', $('#age').val());
    formData.append('Description', $('#description').val());
    var fileInput = $('#path')[0];
    if (fileInput.files.length > 0) {
        var file = fileInput.files[0];
        formData.append('Path', file);
    }
    $.ajax({
        url: '/offres/Insert',
        type: 'POST',
        data: formData,
        processData: false,
        contentType: false,
        success: function (response) {
            if (response.success) {
                HideModal();
                GetOffres();
                alert(response.message);
            } else {
                alert('Erreur : ' + (response.error || 'Une erreur est survenue'));
            }
        },
        error: function (xhr, status, error) {
            console.log(xhr.responseText);
            alert('Impossible d\'enregistrer les données. Erreur : ' + error);
        }
    });
}

function formatDate0(dateString) {
    let date = new Date(dateString);
    let year = date.getFullYear();
    let month = (date.getMonth() + 1).toString().padStart(2, '0');
    let day = date.getDate().toString().padStart(2, '0');
    return `${day}-${month}-${year}`;
}

function formatDate(dateString) {
    let date = new Date(dateString);
    let year = date.getFullYear();
    let month = (date.getMonth() + 1).toString().padStart(2, '0');
    let day = date.getDate().toString().padStart(2, '0');
    return `${year}-${month}-${day}`;
}

function HideModal() {
    ClearData();
    $('#OffreModal').modal('hide');
}

function ClearData() {
    $('#titre, #dateDebut, #dateFin, #nbr_places, #diplome, #specialite, #centreConcours, #age, #path, #description').val('').css('border-color', 'lightgrey');
}

function Validate() {
    var isValid = true;
    $('#titre, #dateDebut, #dateFin, #nbr_places, #diplome, #specialite, #centreConcours, #age, #path, #description').each(function () {
        if (!$(this).val().trim()) {
            $(this).css('border-color', 'Red');
            isValid = false;
        } else {
            $(this).css('border-color', 'lightgrey');
        }
    });
    return isValid;
}

$('#titre, #dateDebut, #dateFin, #nbr_places, #diplome, #specialite, #centreConcours, #age, #path, #description').on('change', function () {
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
                $('#diplome').val(response.diplome);
                $('#specialite').val(response.specialite);
                $('#centreConcours').val(response.centreConcours);
                $('#age').val(response.age);
                $('#path').val(response.path);
                $('#description').val(response.description);  
            }
        },
        error: function () {
            alert('Impossible de modifier les données');
        }
    });
}

function Update() {
    if (!Validate()) {
        return false;
    }

    var formData = {
        Id: $('#id').val(),
        Titre: $('#titre').val(),
        dateDebut: $('#dateDebut').val(),
        dateFin: $('#dateFin').val(),
        nbr_places: $('#nbr_places').val(),
        Diplome: $('#diplome').val(),
        Specialite: $('#specialite').val(),
        CentreConcours: $('#centreConcours').val(),
        Age: $('#age').val(),
        Path: $('#path').val(),
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
