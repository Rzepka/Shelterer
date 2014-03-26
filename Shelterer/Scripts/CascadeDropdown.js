$(document).ready(function () {
    $("#ddlRegions").change(function () {
        var regionId = $(this).val();
        $.getJSON("/Region/LoadRangesByRegionId", { regionId: regionId },
               function (regionsData) {
                   var select = $("#ddlRanges");
                   select.empty();
                   select.append($('<option/>', {
                       value: 0,
                       text: "Select Mountain Range"
                   }));
                   $.each(regionsData, function (index, itemData) {
                       //alert(regionsData);
                       //alert(itemData);
                       select.append($('<option/>', {
                           value: itemData.Value,
                           text: itemData.Text
                       }));
                   });
               });
    });
});