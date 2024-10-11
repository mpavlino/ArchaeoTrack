window.initializeDataTableIfExists = function (tableSelector) {
    //var tableElement = document.querySelector(tableSelector);
    //if (tableElement && tableElement.rows.length > 1) { // Check if the table has more than one row (i.e., headers + data)
    //    var initialized = sessionStorage.getItem('dataTableInitialized');
    //    if (initialized !== 'true') {
    //        $(document).ready(function () {
    //            $(tableSelector).DataTable({
    //                "order": [[0, "desc"]]
    //            });
    //        });
    //        sessionStorage.setItem('dataTableInitialized', 'true');
    //    }
    //}
    const table = new simpleDatatables.DataTable(tableSelector, {
        perPage: 10,
        searchable: true,
        sortable: true,
        deferRender: true, // If supported or consider similar options
        labels: {
            placeholder: "Pretraži",
            searchTitle: "Pretraži unutar tablice",
            pageTitle: "Stranica {page}",
            perPage: "zapisa po stranici",
            noRows: "Nisu pronađeni zapisi",
            info: "Prikaz {start} do {end} od {rows} zapisa",
            noResults: "Nema rezulatata pretrage",
        }
    });
};

function TestDataTablesRemove(table) {
    $(document).ready(function () {
        $(table).DataTable().destroy();
    });
}

