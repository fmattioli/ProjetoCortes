
function MontarGraficoProtocolos() {
    $.ajax({
        url: '/Home/GraficoProtocolos',
        method: 'GET',
        success: function (dados) {
            new Chart(document.getElementById("GraficoProtocolos"), {

                type: 'pie',

                data: {
                    labels: ["Protocolizado", "Retirados", "Em andamento"],

                    datasets: [{
                        label: "Total de protocolos em andamento e finalizados",
                        data: [dados.protocolado, dados.retirado, dados.andamento],
                        backgroundColor: ["#0091ea", "#c62828", "#ad0505"]
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


function MontarGraficoRegistros() {
    $.ajax({
        url: '/Home/GraficoRegistros',
        method: 'GET',
        success: function (dados) {
            new Chart(document.getElementById("GraficoRegistros"), {

                type: 'doughnut',

                data: {
                    labels: ["Registrado", "Retirados", "Em andamento"],

                    datasets: [{
                        label: "Total de protocolos em andamento e finalizados",
                        data: [dados.protocolado, dados.retirado, dados.andamento],
                        backgroundColor: ["#0091ea", "#c62828", "#ad0505"]
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