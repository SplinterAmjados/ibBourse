$('#type_ordre').on('change', function () {

    var type_ordre = $('#type_ordre').val();

    if (type_ordre == '1' || type_ordre == '6') {
        $('#cours_ordre_div').show();
    }
    else {
        $('#cours_ordre_div').hide();
    }

    if (type_ordre == '5' || type_ordre == '6') {
        $('#valeur_stop_div').show();
    }
    else {
        $('#valeur_stop_div').hide();
    }

});