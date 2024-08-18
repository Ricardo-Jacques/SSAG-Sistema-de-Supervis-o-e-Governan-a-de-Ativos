let perfilUsuario = document.querySelector('.perfil');
let botaoLogin = document.querySelector('#botaoLogin');

perfilUsuario.addEventListener('click', function () {
    if (botaoLogin.style.display === 'none' || botaoLogin.style.display === '') {
        botaoLogin.style.display = 'block';
    } else {
        botaoLogin.style.display = 'none';
    }
});

function filtrarTabela() {
    var input = document.getElementById('barra_pesquisa').value.toLowerCase(); //Torna indiferente maiúsculas e minúsculas.
    var tabela = document.getElementById('tabela');
    var linhas = tabela.querySelectorAll('.tr');

    for (var i = 0; i < linhas.length; i++) {
        var nomeCelula = linhas[i].getElementsByClassName('nome')[0];

        if (nomeCelula) {
            var nome = nomeCelula.textContent.toLowerCase();

            if (nome.indexOf(input) > -1) {
                linhas[i].style.display = '';
            } else {
                linhas[i].style.display = 'none';
            }
        }
    }
}