$("#btnUpdate").click(function () {
    var Amount = $(".ChangeAmount").val();
    if (isNaN(Amount) == true || Amount < 0) {
        $("#showNotifi").text("The quantity is not valid");
        return false;
    }
});