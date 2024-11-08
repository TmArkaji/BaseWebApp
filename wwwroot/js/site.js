// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    $('select').select2({
        width: 'resolve'
    });

    //$(".numeric-field").on('input', function (event) {
    //    // existe en site.js
    //    replaceDotWithComma(event);
    //});
});


function replaceDotWithComma(event) {
    // console.log("event.target.value: ", event.target.value);
    event.target.value = event.target.value.replace('.', ',');
}