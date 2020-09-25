
$(".changeAccomodationType").click(function () {
    var accomodationTypeID = $(this).attr("data-id");

    $(".accomodationTypeRow").hide();
    $("div.accomodationTypeRow[data-id=" + accomodationTypeID + "]").show();

});