const abrirModalBtn = document.getElementById("abrirModalBtn");
const modal1 = document.getElementById("modal1");
const modal2 = document.getElementById("modal2");
const modal3 = document.getElementById("modal3");
const modal4 = document.getElementById("modal4");
const cancelarBtn = document.getElementById("cancelarBtn");
const continuarBtn = document.getElementById("continuarBtn");
const cancelarBtn2 = document.getElementById("cancelarBtn2");
const cancelarBtn3 = document.getElementById("cancelarBtn3");
const cancelarBtn4 = document.getElementById("cancelarBtn4");
const voltarModal2Btn = document.getElementById("voltarModal2Btn");
const voltarModal2Btn4 = document.getElementById("voltarModal2Btn4");
const cartaoBtn = document.getElementById("cartaoBtn");
const pixBtn = document.getElementById("pixBtn");
const blurBackground = document.getElementById("blurBackground");


const cepInput = document.getElementById("cep");
const cidadeInput = document.getElementById("cidade");
const bairroInput = document.getElementById("bairro");
const numeroInput = document.getElementById("numero");

cepInput.addEventListener("blur", function () {
    const cep = cepInput.value.replace(/\D/g, '');  // Remove caracteres não numéricos

    if (cep.length === 8) {  // Verifica se o CEP tem 8 caracteres
        fetch(`https://viacep.com.br/ws/${cep}/json/`)
            .then(response => response.json())
            .then(data => {
                if (data.erro) {
                    alert("CEP não encontrado.");
                } else {
                    // Preenchendo os campos com as informações da API
                    cidadeInput.value = data.localidade;
                    bairroInput.value = data.bairro;
                    document.getElementById("logradouro").value = data.logradouro;  // Preenche o logradouro
                    numeroInput.focus();
                }
            })
            .catch(error => {
                console.error("Erro ao buscar CEP:", error);
                alert("Ocorreu um erro ao buscar o CEP.");
            });
    } else {
        alert("CEP inválido. Verifique o número e tente novamente.");
    }
});


abrirModalBtn.addEventListener("click", () => {
    modal1.style.display = "flex";
    blurBackground.style.display = "block";
});

cancelarBtn.addEventListener("click", () => {
    modal1.style.display = "none";
    document.getElementById("cep").value = "";
    document.getElementById("cidade").value = "";
    document.getElementById("bairro").value = "";
    document.getElementById("numero").value = "";
    blurBackground.style.display = "none";
});

continuarBtn.addEventListener("click", () => {
    modal1.style.display = "none";
    modal2.style.display = "flex";
});

cartaoBtn.addEventListener("click", () => {
    modal2.style.display = "none";
    modal3.style.display = "flex";
});

pixBtn.addEventListener("click", () => {
    modal2.style.display = "none";
    modal4.style.display = "flex";
});


cancelarBtn2.addEventListener("click", () => {
    modal2.style.display = "none";
    blurBackground.style.display = "none";
});


cancelarBtn3.addEventListener("click", () => {
    modal3.style.display = "none";
    blurBackground.style.display = "none";
});

voltarModal2Btn.addEventListener("click", () => {
    modal3.style.display = "none";
    modal2.style.display = "flex";
});


voltarModal2Btn4.addEventListener("click", () => {
    modal4.style.display = "none";
    modal2.style.display = "flex";
});


cancelarBtn4.addEventListener("click", () => {
    modal4.style.display = "none";
    blurBackground.style.display = "none";
});


[modal1, modal2, modal3, modal4].forEach(modal => {
    modal.addEventListener("click", (e) => {
        if (e.target === modal) {
            modal.style.display = "none";
            blurBackground.style.display = "none";
        }
    });
});