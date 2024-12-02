function toggleHeart(element) {
    // Alterna entre as classes de coração vazio e preenchido
    if (element.classList.contains('far')) {
        element.classList.remove('far');
        element.classList.add('fas');
    } else {
        element.classList.remove('fas');
        element.classList.add('far');
    }
}
