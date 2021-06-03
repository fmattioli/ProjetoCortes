$(document).ready(function () {
    var maxLength = '-0.000.000,00'.length;

    $("#preco").maskMoney({
        allowNegative: true,
        thousands: '.',
        decimal: ',',
        affixesStay: false
    }).attr('maxlength', maxLength).trigger('mask.maskMoney');
});

function FazerAgendamento() {
    var modelObj = {};

    //modelObj.Id = "123";
    modelObj.Nome = $('#nome').val();
    modelObj.Endereco = $('#endereco').val();
    modelObj.Dias = null;
    modelObj.DiaSelecionado = $('#ddlDiaSemana').val();
    modelObj.Horarios = null;
    modelObj.HorarioSelecionado = $('#ddlHorario').val();
    modelObj.Usuario_Id = sessionStorage.getItem("IdUser");
    modelObj.Preco = $('#preco').val();

    if (ValidarCamposAgendamento(modelObj) == "") {
        var model = JSON.stringify(modelObj); // convert object to json
        var token;
        token = sessionStorage.getItem("tokenAuth");

        $.ajax({
            method: 'POST',
            url: '/Agendamento/RealizarAgendamento',
            headers: {
                "Authorization": "Bearer " + token
            },
            data: { model },
            success: function (data) {

            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
            }
        })
    }
}


function ValidarCamposAgendamento(modelObj) {
    var retorno = "";
    if (modelObj.Nome === "") {
        M.toast({
            html: "Informe um nome válido!",
            classes: 'red darken-4 rounded',
        });
        return "Informe um nome válido!";
    }

    if (modelObj.Endereco === "") {
        M.toast({
            html: "Informe um endereço válidoo!!",
            classes: 'red darken-4 rounded',
        });
        return "Informe um endereço válido!";
    }

    if (modelObj.DiaSelecionado === "0") {
        M.toast({
            html: "Informe um dia da semana válido!!",
            classes: 'red darken-4 rounded',
        });
        return "Informe um dia da semana válido!!";
    }

    if (modelObj.HorarioSelecionado === "0") {
        M.toast({
            html: "Informe um horário válido!!",
            classes: 'red darken-4 rounded',
        });
        return "Informe um horário válido!!";
    }
    if (modelObj.Preco === "") {
        M.toast({
            html: "Informe um preço válido!!",
            classes: 'red darken-4 rounded',
        });
        return "Informe um preço válido!!";
    }

    return retorno;
}