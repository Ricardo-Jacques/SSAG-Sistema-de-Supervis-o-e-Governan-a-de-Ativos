function filtrarItens() {
    var tipoSelecionado = document.getElementById("tipoItem").value;
    var itemDesejado = document.getElementById("itemDesejado");
    var itens = itemDesejado.options;

    for (var i = 0; i < itens.length; i++) {
        var tipoItem = itens[i].getAttribute("data-tipo");

        if (tipoSelecionado === "" || tipoItem === tipoSelecionado) {
            itens[i].style.display = "block";
        } else {
            itens[i].style.display = "none";
        }
    }

    itemDesejado.selectedIndex = 0;
}

function filtrarIds() {
    var itemSelecionado = document.getElementById("itemDesejado").value;
    var idItem = document.getElementById("idItem");
    var ids = idItem.options;

    for (var i = 0; i < ids.length; i++) {
        var nomeItem = ids[i].getAttribute("data-item");

        if (itemSelecionado === "" || nomeItem === itemSelecionado) {
            ids[i].style.display = "block";
        } else {
            ids[i].style.display = "none";
        }
    }

    idItem.selectedIndex = 0;
}

function validarFormulario() {
    // Validação de tipo
    var tipoSelecionado = document.getElementById("tipoItem").value;
    if (tipoSelecionado === "") {
        alert("Por favor, selecione um tipo.");
        return false; // Impede o envio do formulário
    }

    // Validação de item
    var itemSelecionado = document.getElementById("itemDesejado").selectedOptions[0];
    var estadoItem = itemSelecionado.getAttribute("data-estado");

    if (estadoItem === "Emprestado") {
        alert("O item selecionado está emprestado. Por favor, escolha um item disponível.");
        return false; // Impede o envio do formulário
    }

    // Validação de ID
    var idSelecionado = document.getElementById("idItem").value;
    if (idSelecionado === "") {
        alert("Por favor, selecione um ID.");
        return false;
    }

    return true;
}

//Só exibe os campos de escolha abaixo após o de cima ter um valor selecionado
item.selecionado