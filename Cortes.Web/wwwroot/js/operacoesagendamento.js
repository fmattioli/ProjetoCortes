function FazerAgendamento() {
    var modelObj = {};
      
    modelObj.Id = "123";
    modelObj.Nome = "";
    modelObj.Endereco = "Felipe";
    modelObj.Dias = null;
    modelObj.DiaSelecionado = 2;
    modelObj.Horarios = null;
    modelObj.HorarioSelecionado = 1;
    modelObj.Usuario_Id = "teste";
    modelObj.Horario = "teste";
    modelObj.Preco = 10.50;    // etc... make sure the properties of this model match EditScreenModelValidation

    if (modelObj.Nome === "") {
        M.toast({
            html: "Informe um nome válidoo!!",
            classes: 'red darken-4 rounded',
        });
        return;
    }
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