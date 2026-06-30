function getUsuarioLogado() {
    const usuario = localStorage.getItem('usuarioLogado');
    return usuario ? JSON.parse(usuario) : null;
}

function salvarUsuarioLogado(usuario) {
    localStorage.setItem('usuarioLogado', JSON.stringify(usuario));
}

function sair() {
    localStorage.removeItem('usuarioLogado');
    window.location.href = '../login/login.html';
}

function protegerPagina() {
    if (!getUsuarioLogado()) {
        window.location.href = '../login/login.html';
    }
}
