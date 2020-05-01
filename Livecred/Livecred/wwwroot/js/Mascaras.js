$((function () {

    var dataAtual = new Date();
    dataAtual.setDate = Date.now();
    var dataMinima = new Date();
    dataMinima.setDate(dataAtual.getDate() + 20);

    $('.dateMin').datepicker({
        format: "dd/mm/yyyy",
        language: "pt-BR",
        startDate: dataMinima,
        autoclose: true
    });

    $('.date').datepicker({
        format: "dd/mm/yyyy",
        language: "pt-BR",
        autoclose: true
    });
    $('.cpf').mask("999.999.999-99", { reverse: true });
    $('.telefone').mask("(99) 9 9999-9999");
    $('.cep').mask("99999-999");
    $('.nascimento').mask("99/99/9999");
    $('.valor').maskMoney({ allowNegative: true, thousands: '.', decimal: ',', affixesStay: false, allowZero: true, defaultZero: false });
    $('.media').mask("9,9");
    $('.preco').mask("9,99", { reverse: true });
    $('.es').maskMoney();

}));

function modalSubmetido() {
    $("#confirmacaoModal").modal('show');
}

function erroDataModal() {
    $("#erro-data-Modal").modal('show');
}

function modalParecer() {
    $("#parecerDadoModal").modal('show');
}

$("#btnParecerVoltarHome").click(function () {
    window.location.replace("/Home/Index/");
})

function modalReenviar() {
    $("#reenviarModal").modal('show');
}