﻿$(document).ready(function () {
    var token = "";
    if (sessionStorage.getItem("tokenAuth") == "" || sessionStorage.getItem("tokenAuth") == null) {
        token = $('#txtToken').val();
        sessionStorage.setItem("tokenAuth", token);
    }
    if (sessionStorage.getItem("nomeUser") == "" || sessionStorage.getItem("nomeUser") == null) {
        var nome = $('#txtNome').val();
        sessionStorage.setItem("nomeUser", nome);
    }
    if (sessionStorage.getItem("IdUser") == "" || sessionStorage.getItem("IdUser") == null) {
        var nome = $('#txtId').val();
        sessionStorage.setItem("IdUser", nome);
    }
});


function Deslogar() {
    sessionStorage.setItem("tokenAuth", "");
    sessionStorage.setItem("nomeUser", "");
    window.location.href = '/Home/Login';

}



