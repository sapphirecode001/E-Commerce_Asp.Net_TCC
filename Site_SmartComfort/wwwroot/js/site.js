let imgElement = document.getElementById('carrosel');
let currentIndex = 1;
let botoes = document.querySelectorAll('.btn_carrosel');

const img1 = "/img/carrosel1.jpeg";
const img2 = "/img/carrosel2.jpg";
const img3 = "/img/carrosel3.jpeg";

function resetarClasseAtivo() {
    botoes.forEach(botao => botao.classList.remove('ativo'));
}

function troca1() {
    imgElement.src = img1;
    currentIndex = 1;
    resetarClasseAtivo();
    botoes[0].classList.add('ativo');
}

function troca2() {
    imgElement.src = img2;
    currentIndex = 2;
    resetarClasseAtivo();
    botoes[1].classList.add('ativo');
}

function troca3() {
    imgElement.src = img3;
    currentIndex = 3;
    resetarClasseAtivo();
    botoes[2].classList.add('ativo');
}

function autoTroca() {
    if (currentIndex === 1) {
        troca2(); // Troca para a imagem 2
    } else if (currentIndex === 2) {
        troca3(); // Troca para a imagem 3
    } else if (currentIndex === 3) {
        troca1(); // Troca para a imagem 1
    }
}

// Intervalo de 5 segundos para a troca automática de imagens
setInterval(autoTroca, 3000);

