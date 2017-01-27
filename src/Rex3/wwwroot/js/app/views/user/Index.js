$(function () {
    $("#userList").kendoGrid(
        {
            toolbar: ["pdf"],
            pdf: {
                fileName: "User Grid Export.pdf"
            },
            dataSource: {
                transport: {
                    read: { url: '/User/GetUsers', type: 'GET' },
                    total: function (response) {
                        return (response.data).length;
                    }
                },
                schema: {
                    model: {
                        id: "userId"
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
                { field: "userId", title: "UserId", width: 90, filterable: { search: true } },
                { field: "name", title: "Name", filterable: true },
                { field: "countryCode", title: "Country", filterable: { multi: true } },
                { field: "isActive", title: "Active", filterable: { multi: true } },
                { field: "primaryRole", title: "Role", filterable: { multi: true } },
                { command: { text: "Details", click: showDetails }, title: " "}
            ]
        });

    function detailInit(e) {
        var detailRow = e.detailRow;
        //alert("Clicked id: " + detailRow.id);
    };

    function showDetails(e) {
        e.preventDefault();
        var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
        var id = dataItem.ID;
        alert(id);
        window.location.href = '/User/Details/?userId=Chris';
        //alert("Clicked id: " + detailRow.id);
    };
});