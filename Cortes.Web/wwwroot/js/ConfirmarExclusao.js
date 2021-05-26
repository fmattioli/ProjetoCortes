function ConfirmarExclusaoNomeProtocolo(id, nome, protocoloId, grupoId) {
    
    $('.modal').modal({
        dismissble: true
    });
    $('#modal').modal('open');
    $(".nome").text(nome);
    const url = '/Protocolo/ExcluirNomeProtocolo';
    var token = "";
    token = sessionStorage.getItem("tokenAuth");
    $(".btnExcluir").on('click', function () {
        $.ajax({
            method: "POST",
            url: url,
            headers: {
                "Authorization": "Bearer " + token
            },
            data: { id: id },
            success: function (data) {
                
            }
        }).done(function () {
            var _url = 'RetornarComponenteNomesProtocolo';
            $.ajax({
                async: true,
                type: "GET",
                url: _url,
                headers: {
                    "Authorization": "Bearer " + token
                },
                data: { protocoloId: protocoloId, grupoId: grupoId },
                success: function (result) {
                    $("#nomesProtocolo").html(result);
                },
            });

        }).done(function () {
            var _url = 'RetornarComponenteDocumentosProtocolo';
            $.ajax({
                type: "GET",
                url: _url,
                headers: {
                    "Authorization": "Bearer " + token
                },
                data: { protocoloId: protocoloId, grupoId: grupoId },
                success: function (result) {
                    $("#documentosProtocolo").html(result);
                },
            });
        });
    });
}
