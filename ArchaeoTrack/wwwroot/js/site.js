window.initializeDataTable = function () {
    $(document).ready(function () {
        $('#notesTable').DataTable({
            "order": [[0, "desc"]]
            // Sort by the first column (Date) in descending order by default
        });
    });
};
