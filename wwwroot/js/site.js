let perfilUsuario = document.querySelector('.perfil');
let botaoLogin = document.querySelector('#botaoLogin');

perfilUsuario.addEventListener('click', function () {
    if (botaoLogin.style.display === 'none' || botaoLogin.style.display === '') {
        botaoLogin.style.display = 'block';
    } else {
        botaoLogin.style.display = 'none';
    }
});
