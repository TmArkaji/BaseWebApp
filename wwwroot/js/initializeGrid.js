// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
// Write your JavaScript code.
function initializeGrid(options) {
    if (!options.selector) {
        console.error("El selector del grid es requerido.");
        return;
    }
    if (!options.dataSource) {
        console.error("El dataSource es requerido.");
        return;
    }
    if (!options.columns) {
        console.error("Las columnas son requeridas.");
        return;
    }
    if (!options.editUrlTemplate) {
        console.error("El editUrlTemplate es requerido.");
        return;
    }

    // Definir la columna "Acciones"
    var actionsColumn = {
        caption: "Acciones",
        type: "buttons",
        buttons: options.actionsButtons || [
            {
                hint: "Editar",
                icon: "edit",
                onClick: function(e) {
                    var editUrl = options.editUrlTemplate.replace("ID_REPLACE", e.row.data.EncryptedID);
                    window.location.href = editUrl;
                },
            },
            {
                hint: "Detalles",
                icon: "info",
                onClick: function(e) {
                    var detailsUrl = options.detailsUrlTemplate.replace("ID_REPLACE", e.row.data.EncryptedID);
                    window.location.href = detailsUrl;
                }
            },
            {
                hint: "Eliminar",
                icon: "trash",
                onClick: function(e) {
                    var result = DevExpress.ui.dialog.confirm("<i>Are you sure?</i>", "Confirm changes");
                    result.done(function(dialogResult) {
                        if (dialogResult) {
                            var id = e.row.data.id;
                            $('#modelId').val(id);
                            $('#deleteForm').submit();
                        }
                        alert(dialogResult ? "Confirmed" : "Canceled");
                    });


                    //swal({
                    //    title: "Confirmación",
                    //    text: "¿Estás seguro de eliminar el registro?",
                    //    icon: "warning",
                    //    buttons: true,
                    //    dangerMode: true
                    //}).then((confirm) => {
                    //    if (confirm) {
                    //        var id = e.row.data.id;
                    //        $('#modelId').val(id);
                    //        $('#deleteForm').submit();
                    //    }
                    //});
                }
            }
        ]
    };

    // Combinar la columna "Acciones" con las columnas proporcionadas
    var columns = [actionsColumn].concat(options.columns);

    $(function() {
        DevExpress.localization.locale(navigator.language || navigator.browserLanguage);
        $(options.selector).dxDataGrid({
            dataSource: options.dataSource,
            keyExpr: options.keyExpr || "ID",
            rowAlternationEnabled: true,
            showBorders: false,
            showColumnLines: true,
            columns: columns,
            columnAutoWidth: true,
            allowColumnResizing: true,
            allowColumnReordering: true,
            columnResizingMode: "widget",
            grouping: {
                autoExpandAll: true
            },
            groupPanel: {
                visible: true
            },
            scrolling: {
                useNative: true
            },
            paging: {
                pageSize: options.pageSize || 20,
            },
            pager: {
                showPageSizeSelector: true,
                allowedPageSizes: options.allowedPageSizes || [10, 20, 50],
                showInfo: true
            },
            filterRow: {
                visible: true,
                applyFilter: "auto"
            },
            searchPanel: {
                visible: true,
                highlightCaseSensitive: true
            },
            export: {
                enabled: true,
                fileName: options.exportFileName || "Export",
            },
            onExporting: function(e) {
                var workbook = new ExcelJS.Workbook();
                var worksheet = workbook.addWorksheet('Main sheet');

                DevExpress.excelExporter.exportDataGrid({
                    worksheet: worksheet,
                    component: e.component,
                    customizeCell: options.customizeCell || function() { }
                }).then(function() {
                    workbook.xlsx.writeBuffer().then(function(buffer) {
                        saveAs(new Blob([buffer], { type: "application/octet-stream" }), (options.exportFileName || "Export") + ".xlsx");
                    });
                });
                e.cancel = true;
            },
            onToolbarPreparing: function(e) {
                var dataGrid = e.component;
                e.toolbarOptions.items.unshift({
                    location: "after",
                    widget: "dxButton",
                    options: {
                        icon: "add",
                        hint: options.createButtonHint || "Crear un nuevo registro",
                        onClick: function() {
                            window.location.href = options.createUrl;
                        }
                    }
                });
            }
        });
    });
}
