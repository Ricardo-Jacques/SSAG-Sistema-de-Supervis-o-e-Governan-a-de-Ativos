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

function filtrarTabelaPorStatus() {
    var select, filter, td, i, txtValue;
    select = document.getElementById('filtroStatus');
    filter = select.value.trim(); // Remove espaços em branco
    var tabela = document.getElementById('tabela');
    var linhas = tabela.querySelectorAll('.tr');

    for (i = 0; i < linhas.length; i++) {
        td = linhas[i].querySelector('.status');
        if (td) {
            txtValue = td.innerHTML.trim(); // Obtém o valor do td e remove espaços
            if (filter === '' || txtValue === filter) {
                linhas[i].style.display = ''; // Mostra a linha
            } else {
                linhas[i].style.display = 'none'; // Oculta a linha
            }
        }
    }
}