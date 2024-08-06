$(document).ready(function () {
    InitTabulator();
});

function InitTabulator() {
    GetOffres1();
    // Gestionnaire d'événements pour le clic sur le bouton de recherche
}

function formatDate0(dateString) {
    let date = new Date(dateString);
    let year = date.getFullYear();
    let month = (date.getMonth() + 1).toString().padStart(2, '0');
    let day = date.getDate().toString().padStart(2, '0');
    return `${day}-${month}${year}-${year}`;

}

/*
function GetOffres1() {
    $.ajax({
        url: '/offres/GetOffres1',
        type: 'GET',
        dataType: 'json',
        contentType: 'application/json;charset=utf-8',
        success: function (response) {
            console.log(response);
            var tabulatorData = [];
            if (response && response.length > 0) {
                // Filtrer les offres avec un statut "actif"
                var offresActives = response.filter(function (item) {
                    return item.status === "archivee" || item.status === "archive"; // Assurez-vous que le champ status correspond à votre structure de données
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
                    /*{
                        title: "Actions",
                        formatter: function (cell, formatterParams) {
                            var id = cell.getData().id;
                            //return "<button class='btn btn-danger btn-sm' onclick='Archive(" + id + ")'>Archiver</button>";
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
}*/

function GetOffres1() {
    $.ajax({
        url: '/offres/GetOffres1',
        type: 'GET',
        dataType: 'json',
        contentType: 'application/json;charset=utf-8',
        success: function (response) {
            console.log(response);
            var tabulatorData = [];

            if (response && response.length > 0) {
                response.forEach(function (item) {
                    try {
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
                    } catch (error) {
                        console.error('Erreur lors de la conversion de la date de fin :', error);
                        //item.status = "erreur"; // Définir un statut pour les erreurs de date
                    }
                });
            }

            // Initialiser Tabulator avec les données
            var table = new Tabulator("#tabulatorContainer", {
                data: tabulatorData,
                layout: "fitColumns",
                columns: [
                    { title: "Id", field: "id" },
                    { title: "Titre", field: "titre" },
                    { title: "Date Début", field: "dateDebut" },
                    { title: "Date Fin", field: "dateFin" },
                    { title: "Nombre de places", field: "nbr_places" },
                    { title: "Diplome", field: "diplome" },
                    { title: "Specialite", field: "specialite" },
                    { title: "Centre de Concours", field: "centreConcours" },
                    { title: "Age", field: "age" },
                    { title: "Description", field: "description" },
                    {
                        title: "Fichier Decision",
                        field: "id",
                        formatter: function (cell, formatterParams, onRendered) {
                            var id = cell.getValue();
                            return "<a href='/offres/DownloadFile?id=" + id + "' target='_blank'>Cliquer ici pour télécharger</a>";
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
            console.error('Erreur AJAX : ' + status, error);
        }
    });
}
