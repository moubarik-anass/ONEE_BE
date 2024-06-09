$(document).ready(function () {
    InitTabulator();
});

function InitTabulator() {
    GetCandidatures();
}

function GetCandidatures() {
    try {
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
                    dataFiltered: function (filters, rows) {
                        if (rows.length === 0) {
                            table.setData([{ id: '', nom: '', prenom: '', email: '', motdepasse: '', datepostulation: '', ville: '', adresse: '', emploiprecedent: '', Actions: '<span class="text-danger">Aucun résultat trouvé.</span>' }]);
                        } else {
                            table.setData(rows);
                        }
                    }
                });
                $("#tabulatorContainer").show();
            },
            error: function (xhr, status, error) {
                throw new Error('Impossible d\'afficher les offres : ' + error);
            }
        });
    } catch (error) {
        console.error(error);
        alert(error.message);
    }
}

