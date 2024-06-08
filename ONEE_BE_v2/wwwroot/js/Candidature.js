$(document).ready(function () {
    InitTabulator();
});

function InitTabulator() {
    GetCandidatures();
}

function GetCandidatures() {
    $.ajax({
        url: '/candidatures/GetCandidatures',
        type: 'GET',
        dataType: 'json',
        contentType: 'application/json',
        success: function (response) {
            var tabulatorData = response.filter(function (item) {
                return item.status === "validé" || item.status === "valide";
            });

            var table = new Tabulator("#tabulatorContainer", {
                data: tabulatorData,
                layout: "fitColumns",
                columns: [
                    { title: "Id", field: "id", headerFilter: true },
                    { title: "Nom", field: "nom", headerFilter: true },
                    { title: "Prénom", field: "prenom", headerFilter: true },
                    { title: "Email", field: "email", headerFilter: true },
                    { title: "Mot de passe", field: "motdepasse", headerFilter: true },
                    {
                        title: "Date Postulation",
                        field: "datepostulation",
                        headerFilter: true,
                        formatter: function (cell, formatterParams, onRendered) {
                            var date = new Date(cell.getValue());
                            return date.toISOString().slice(0, 10);
                        }
                    },
                    { title: "Ville", field: "ville", headerFilter: true },
                    { title: "Adresse", field: "adresse", headerFilter: true },
                    { title: "Emploi précédent", field: "emploiprecedent", headerFilter: true },
                    {
                        title: "Actions",
                        formatter: function (cell, formatterParams) {
                            var id = cell.getRow().getData().id;
                            return "<a href='/candidatures/Details/" + id + "' class='btn btn-primary btn-sm'><i class='fa fa-eye'></i> Details</a>";
                        }
                    }
                ],
                headerFilterEmptyCheck: function (filters) {
                    var isEmpty = true;
                    filters.forEach(function (filter) {
                        if (filter.value !== "") {
                            isEmpty = false;
                        }
                    });
                    if (isEmpty) {
                        alert("Aucun résultat trouvé");
                    }
                    return isEmpty;
                }
            });

            $("#tabulatorContainer").show();
        },
        error: function (xhr, status, error) {
            alert('Impossible d\'afficher les offres : ' + error);
        }
    });
}

