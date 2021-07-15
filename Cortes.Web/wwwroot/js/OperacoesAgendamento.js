$(document).ready(function () {
    var maxLength = '-0.000.000,00'.length;
    if ($('#preco').val()) {
        $("#preco").maskMoney({
            allowNegative: true,
            thousands: '.',
            decimal: ',',
            affixesStay: false
        }).attr('maxlength', maxLength).trigger('mask.maskMoney');
    }

    GraficosCortesDiariosXCortesFinalizados();
    
});


function FazerLancamento() {
    var modelObj = {};

    //modelObj.Id = "123";
    modelObj.Nome = $('#nome').val();
    modelObj.Endereco = $('#endereco').val();
    modelObj.Dias = null;
    modelObj.DiaSelecionado = $('#ddlDiaSemana').val();
    modelObj.Horarios = null;
    modelObj.HorarioSelecionado = $("#ddlHorario option:selected").text();
    modelObj.Usuario_Id = sessionStorage.getItem("IdUser");
    modelObj.Preco = $('#preco').val();
    modelObj.Preco = String(modelObj.Preco).replace(",", ".");

    if (ValidarCamposAgendamento(modelObj) == "") {
        var model = JSON.stringify(modelObj); // convert object to json
        var token;
        token = sessionStorage.getItem("tokenAuth");

        $.ajax({
            method: 'POST',
            url: '/Agendamento/RealizarLancamento',
            headers: {
                "Authorization": "Bearer " + token
            },
            data: { model },
            success: function (data) {
                if (data === true) {
                    $('#formAgendamento').trigger("reset");
                    M.toast({
                        html: "Agendamento realizado com sucesso!",
                        classes: 'black darken-4 rounded',
                    });
                }
                else {
                    M.toast({
                        html: "Esse horário não está disponível!",
                        classes: 'red darken-4 rounded',
                    });
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
            }
        })
    }
}

function FazerAgendamento() {
    var modelObj = {};

    //modelObj.Id = "123";
    modelObj.Nome = $('#nome').val();
    modelObj.Endereco = $('#endereco').val();
    modelObj.Dias = null;
    modelObj.DiaSelecionado = $('#ddlDiaSemana').val();
    modelObj.Horarios = null;
    modelObj.HorarioSelecionado = $("#ddlHorario option:selected").text();
    modelObj.Usuario_Id = sessionStorage.getItem("IdUser");
    modelObj.Preco = $('#preco').val();
    modelObj.Preco = String(modelObj.Preco).replace(",", ".");

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
                if (data === true) {
                    $('#formAgendamento').trigger("reset");
                    M.toast({
                        html: "Agendamento realizado com sucesso!",
                        classes: 'black darken-4 rounded',
                    });
                }
                else {
                    M.toast({
                        html: "Esse horário não está disponível!",
                        classes: 'red darken-4 rounded',
                    });
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
            }
        })
    }
}

function CortouCabelo(Id) {

    var token;
    token = sessionStorage.getItem("tokenAuth");

    $.ajax({
        method: 'POST',
        url: '/Agendamento/ConfirmarPresenca',
        headers: {
            "Authorization": "Bearer " + token
        },
        data: { Id },
        success: function (data) {
            if (data === true) {
                $("#" + Id).remove();
                GraficosCortesDiariosXCortesFinalizados();

                M.toast({
                    html: "Status atualizado com sucesso!",
                    classes: 'black darken-4 rounded',
                });
            }
            else {
                
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
        }
    })
}


function NaoCortouCabelo(Id) {

    var token;
    token = sessionStorage.getItem("tokenAuth");

    $.ajax({
        method: 'POST',
        url: '/Agendamento/FaltouPresenca',
        headers: {
            "Authorization": "Bearer " + token
        },
        data: { Id },
        success: function (data) {
            if (data === true) {
                $("#" + Id).remove();
                GraficosCortesDiariosXCortesFinalizados();

                M.toast({
                    html: "Status atualizado com sucesso!",
                    classes: 'red darken-4 rounded',
                });

            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
        }
    })
}

function VerificarAgendamento() {
    var modelObj = {};

    //modelObj.Id = "123";
    modelObj.Nome = $('#nome').val();
    modelObj.Endereco = $('#endereco').val();
    modelObj.Dias = null;
    modelObj.DiaSelecionado = $('#ddlDiaSemana').val();
    modelObj.Horarios = null;
    modelObj.HorarioSelecionado = $("#ddlHorario option:selected").text();
    modelObj.Usuario_Id = sessionStorage.getItem("IdUser");
    modelObj.Preco = $('#preco').val();

    if (ValidarCamposAgendamento(modelObj) == "") {
        var model = JSON.stringify(modelObj); // convert object to json
        var token;
        token = sessionStorage.getItem("tokenAuth");

        $.ajax({
            method: 'POST',
            url: '/Agendamento/VerificarSeExisteAgendamento',
            headers: {
                "Authorization": "Bearer " + token
            },
            data: { model },
            success: function (data) {
                if (data === true) {
                    M.toast({
                        html: "Dia e horário disponível pra agendamento!",
                        classes: 'black darken-4 rounded',
                    });
                }
                else {
                    M.toast({
                        html: "Esse horário não está disponível!",
                        classes: 'red darken-4 rounded',
                    });
                }
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

    if (modelObj.DiaSelecionado === "0") {
        M.toast({
            html: "Informe um dia da semana válido!!",
            classes: 'red darken-4 rounded',
        });
        return "Informe um dia da semana válido!!";
    }

    if (modelObj.HorarioSelecionado === "Selecione o horário") {
        M.toast({
            html: "Informe um horário válido!!",
            classes: 'red darken-4 rounded',
        });
        return "Informe um horário válido!!";
    }
    if (modelObj.Preco === "0,00") {
        M.toast({
            html: "Informe um preço válido!!",
            classes: 'red darken-4 rounded',
        });
        return "Informe um preço válido!!";
    }

    return retorno;
}

$(document).ready(function () {
    GraficosCortesDiariosXCortesFinalizados();

});

function GraficosCortesDiariosXCortesFinalizados() {
    $.ajax({
        url: '/Usuarios/GraficosCortesDiario',
        method: 'GET',
        success: function (dados) {
            new Chart(document.getElementById("CortesDiario"), {
                type: 'pie',
                data: {
                    labels: ["Cortes agendados", "Cortes finalizados", "Cortes cancelados"],

                    datasets: [{
                        label: "Resumo de cortes",
                        data: [dados.cortesAbertos, dados.cortesFinalizados, dados.cortesCancelados],
                        backgroundColor: ["#0091ea", "#c62828", "#000000"]
                    }]
                },

                options: {
                    legend: {
                        labels: {
                            usePointStyle: true
                        }
                    }
                }
            });
        }
    });
}
