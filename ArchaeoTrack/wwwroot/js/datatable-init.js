window.initializeDataTableIfExists = function (tableSelector) {
    var tableElement = document.querySelector(tableSelector);
    if (tableElement && tableElement.rows.length > 1) { // Check if the table has more than one row (i.e., headers + data)
        var initialized = sessionStorage.getItem('dataTableInitialized');
        if (initialized !== 'true') {
            $(document).ready(function () {
                $(tableSelector).DataTable({
                    "order": [[0, "desc"]]
                });
            });
            sessionStorage.setItem('dataTableInitialized', 'true');
        }
    }
};

