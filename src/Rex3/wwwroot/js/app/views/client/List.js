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
                }
            },
            height: 550,
            groupable: true,
            sortable: true,
            pageable: {
                    refresh: true,
                    pageSizes: true,
                    buttonCount: 5
            },
            columns: [
                { field: "accountId", title: "System ID", width: 150 },
                { field: "accountName", title: "Company Name" },
                { field: "AccountNameAka", title: "Alternate Name", width: 150 }]
    });
});