function filtrarTabela() {
    var inputNome = document.getElementById('barra_pesquisa').value.toLowerCase(); // Torna indiferente maiúsculas e minúsculas.
    var selectEstado = document.getElementById('filtroPorEstado').value.trim(); // Remove espaços em branco
    var tabela = document.getElementById('tabela');
    var linhas = tabela.querySelectorAll('tr'); // Seleciona todas as linhas (tr)

    for (var i = 0; i < linhas.length; i++) {
        var nomeCelula = linhas[i].getElementsByClassName('nome')[0];
        var estadoCelula = linhas[i].getElementsByClassName('estado')[0];

        if (nomeCelula && estadoCelula) {
            var nome = nomeCelula.textContent.toLowerCase();
            var estado = estadoCelula.textContent.trim();

            // Verifica se o nome e o estado correspondem aos filtros
            if ((nome.indexOf(inputNome) > -1) && (selectEstado === '' || estado === selectEstado)) {
                linhas[i].style.display = ''; // Mostra a linha
            } else {
                linhas[i].style.display = 'none'; // Oculta a linha
            }
        }
    }
}
