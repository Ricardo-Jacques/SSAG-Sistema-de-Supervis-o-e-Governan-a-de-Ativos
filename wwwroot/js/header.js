let divDireitaHeader = document.querySelector('.direita_header');
let navHeader = document.querySelector('.nav_header');

function HeaderUser() {
    divDireitaHeader.classList.remove('direita_header');
    divDireitaHeader.classList.add('direita_header2');

    navHeader.classList.remove('nav_header');
    navHeader.classList.add('nav_header2');
}

// Executa a função automaticamente
HeaderUser();
