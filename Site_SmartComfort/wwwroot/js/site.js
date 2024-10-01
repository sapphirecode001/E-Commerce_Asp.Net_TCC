let imgElement = document.getElementById('carrosel');
let currentIndex = 1;
let botoes = document.querySelectorAll('.btn_carrosel');

const img1 = "/img/carrosel1.jpeg";
const img2 = "/img/carrosel2.jpg";
const img3 = "/img/carrosel3.jpeg";

function troca1() {
    imgElement.src = img1;
    currentIndex = 1;
    botao.classList.add('ativo');
}

function troca2() {
    imgElement.src = img2;
    currentIndex = 2;
    botao.classList.add('ativo');
}

function troca3() {
    imgElement.src = img3;
    currentIndex = 3;
    botao.classList.add('ativo');
}

function autoTroca() {
    if (currentIndex === 1) {
        troca2();
    } else if (currentIndex === 2) {
        troca3();
    } else if (currentIndex === 3) {
        troca1();
    }
}

setInterval(autoTroca, 5000);

