$("#count").on("click", function () {

    let top = $("#topInput").val();
    let wordsNumber = $("#nWordInput").val();
    let ingoreGrammarWords = $("#ignoreGwInput").is(":checked");
    let url = $("#linkInput").val()
    $('#records_table').empty();
    let request = {
        url: url,
        wordsNumber: wordsNumber,
        top: top,
        ingoreGrammarWords: ingoreGrammarWords
    }

    $.ajax(
        {
            url: "/Parser/ParseText",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(request),
            success: function (response) {
               
                DrawTable(response);
            }
        });
});


function DrawTable(data) {
    var trHTML = '';
    $.each(data.calculationResults, function (i, item) {
        trHTML += '<tr><td>' + item.expression + '</td><td>' + item.count + '</td><td>' + (item.frequency * 100).toFixed(2) + '%'  + '</td></tr>';
    });

    $('#records_table').append(trHTML);
}