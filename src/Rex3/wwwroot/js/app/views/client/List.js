$(function () {
    $("#clientList").kendoGrid(
        {
            toolbar: ["pdf"],
            pdf: {
                fileName: "Account Grid Export.pdf"
            },
            dataSource: {
                transport: {
                    read: { url: '/Client/GetClients', type: 'GET' },
                    total: function (response) {
                        return (response.data).length;
                    }
                },
                pageSize: 10
            },
            height: 550,
            groupable: true,
            sortable: true,
            filterable: true,
            detailTemplate: kendo.template($("#template").html()),
            detailInit: detailInit,
            pageable: {
                    refresh: true,
                    pageSizes: true,
                    buttonCount: 5
            },
            columns: [
                { field: "accountId", title: "ID", width: 90, filterable: { search: true } },
                { field: "accountName", title: "Account Name", filterable: true },
                { field: "leadRegionId", title: "Region", filterable: { multi: true }  },
                { field: "AccountNameAka", title: "Alternate Name", width: 150, filterable: true }]
        });

    function detailInit(e) {
        var detailRow = e.detailRow;
        alert("Clicked id: " + detailRow.id);
    };
});