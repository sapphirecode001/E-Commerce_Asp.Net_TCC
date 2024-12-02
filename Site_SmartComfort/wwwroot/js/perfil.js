
$(document).ready(function () {
    $('.telefone').mask('(00) 0000-0000');
    $('.cpf').mask('000.000.000-00', { reverse: true });
    $('.cnpj').mask('00.000.000/0000-00', { reverse: true });
});



function openSenhaModal() {
    document.getElementById("modal-senha").style.display = "flex";
}

function openNomeModal() {
    document.getElementById("modal-nome").style.display = "flex";
}

function openCPFModal() {
    document.getElementById("modal-cpf").style.display = "flex";
}

function openEmailModal() {
    document.getElementById("modal-email").style.display = "flex";
}

function openNumeroModal() {
    document.getElementById("modal-numero").style.display = "flex";
}

function openNumero2Modal() {
    document.getElementById("modal-numero2").style.display = "flex";
}


function closeSenhaModal() {
    document.getElementById("modal-senha").style.display = "none";
}


function closeNomeModal() {
    document.getElementById("modal-nome").style.display = "none";
}

function closeCPFModal() {
    document.getElementById("modal-cpf").style.display = "none";
}

function closeEmailModal() {
    document.getElementById("modal-email").style.display = "none";
}

function closeNumeroModal() {
    document.getElementById("modal-numero").style.display = "none";
}

function closeNumero2Modal() {
    document.getElementById("modal-numero2").style.display = "none";
}
