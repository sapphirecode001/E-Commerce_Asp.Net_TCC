
function formatarCPF(cpf) {
    // Formata o CPF no padrão: ***-456-***-**
    return cpf.replace(/^(\d{3})(\d{3})(\d{3})(\d{2})$/, "***-$2-***-**");
}
function formatarEmail(email) {
    // Mostra as 6 primeiras letras do email
    let nome = email.split('@')[0];
    let dominio = email.split('@')[1];
    let parteOculta = "*".repeat(nome.length - 6);
    return nome.substring(0, 6) + parteOculta + "@" + dominio;
}

function formatarTelefone(telefone) {
    // Formata o telefone no padrão: (11) *****-1234
    return telefone.replace(/^(\d{2})(\d{5})(\d{4})$/, "($1) *****-$3");
}


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
