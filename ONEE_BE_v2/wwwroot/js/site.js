document.addEventListener("DOMContentLoaded", function () {
    var dateDebutInput = document.getElementById("dateDebut");
    var dateFinInput = document.getElementById("dateFin");

    // Empêcher la sélection de dates antérieures à aujourd'hui pour dateDebut
    dateDebutInput.addEventListener("change", function () {
        // Convertir la valeur de dateDebut en objet Date
        var dateDebut = new Date(dateDebutInput.value);

        // Ajouter un jour à la date de début
        dateDebut.setDate(dateDebut.getDate() + 1);

        // Convertir la date de fin en format "YYYY-MM-DD"
        var dateFin = dateDebut.toISOString().split('T')[0];

        // Définir la date minimale pour dateFin
        dateFinInput.min = dateFin;
    });
});

document.addEventListener("DOMContentLoaded", function () {
    var dateDebutInput = document.getElementById("dateDebut");

    // Définir la date d'aujourd'hui comme valeur par défaut pour dateDebut
    var aujourdhui = new Date();
    var dd = String(aujourdhui.getDate()).padStart(2, '0');
    var mm = String(aujourdhui.getMonth() + 1).padStart(2, '0'); // Janvier est 0 !
    var yyyy = aujourdhui.getFullYear();

    aujourdhui = yyyy + '-' + mm + '-' + dd;
    dateDebutInput.value = aujourdhui;

    // Empêcher la sélection de dates antérieures à aujourd'hui pour dateDebut
    dateDebutInput.min = aujourdhui;
});


document.addEventListener('DOMContentLoaded', function () {
    var statusSelect = document.getElementById('Status');
    var descriptionField = document.getElementById('descriptionField');

    // Fonction pour afficher ou masquer le champ de description en fonction de la sélection
    function toggleDescriptionField() {
        if (statusSelect.value === 'non validé') {
            descriptionField.style.display = 'block';
            alert('Vous avez sélectionné "Non Validé".');
        } else {
            descriptionField.style.display = 'none';
        }
    }

    // Appel de la fonction pour la première fois pour vérifier l'état initial
    toggleDescriptionField();

    // Ajout d'un écouteur d'événements pour détecter les changements de sélection
    statusSelect.addEventListener('change', toggleDescriptionField);
});

document.addEventListener("DOMContentLoaded", function () {
    var dateDebutInput = document.getElementById("dateDebut");
    var dateFinInput = document.getElementById("dateFin");

    // Empêcher la sélection de dates antérieures à aujourd'hui pour dateDebut
    dateDebutInput.addEventListener("change", function () {
        // Convertir la valeur de dateDebut en objet Date
        var dateDebut = new Date(dateDebutInput.value);

        // Ajouter un jour à la date de début
        dateDebut.setDate(dateDebut.getDate() + 1);

        // Convertir la date de fin en format "YYYY-MM-DD"
        var dateFin = dateDebut.toISOString().split('T')[0];

        // Définir la date minimale pour dateFin
        dateFinInput.min = dateFin;
    });
});

document.addEventListener("DOMContentLoaded", function () {
    var dateDebutInput = document.getElementById("d");

    // Définir la date d'aujourd'hui comme valeur par défaut pour dateDebut
    var aujourdhui = new Date();
    var dd = String(aujourdhui.getDate()).padStart(2, '0');
    var mm = String(aujourdhui.getMonth() + 1).padStart(2, '0'); // Janvier est 0 !
    var yyyy = aujourdhui.getFullYear();

    aujourdhui = yyyy + '-' + mm + '-' + dd;
    dateDebutInput.value = aujourdhui;

    // Empêcher la sélection de dates antérieures à aujourd'hui pour dateDebut
    dateDebutInput.min = aujourdhui;
});