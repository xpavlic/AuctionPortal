$(document).ready(function () {
    var min = sessionStorage.getItem("minimum");
    var max = sessionStorage.getItem("maximum");
    if (max === null) {
        max = 30000;
    }

    var outputSpan = $('#spanOutput');
    var sliderDiv = $('#slider');
    sliderDiv.slider({
        range: true,
        min: 5000,
        max: 30000,
        values: [min, max],
        slide: function (event, ui) { outputSpan.html(ui.values[0] + ' - ' + ui.values[1] + ' CZK'); },
        stop: function (event, ui) {
            $('#txtMin').val(ui.values[0]);
            $('#txtMax').val(ui.values[1]);
        }
    });
    outputSpan.html(sliderDiv.slider('values', 0) + ' - ' + sliderDiv.slider('values', 1) + ' CZK');
    $('#txtMin').val(sliderDiv.slider('values', 0));
    $('#txtMax').val(sliderDiv.slider('values', 1));
});

function saveRange() {
    var minimum = $('#slider').slider("values", 0);
    sessionStorage.setItem("minimum", minimum);
    var maximum = $('#slider').slider("values", 1);
    sessionStorage.setItem("maximum", maximum);
};

function resetRange() {
    sessionStorage.setItem("minimum", 5000);
    sessionStorage.setItem("maximum", 30000);
};