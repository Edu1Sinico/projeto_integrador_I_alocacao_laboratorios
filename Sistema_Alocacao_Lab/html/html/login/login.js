document.addEventListener('DOMContentLoaded', () => {
    const form = document.getElementById('form-login');
    const email = document.getElementById('login-email');
    const senha = document.getElementById('login-senha');
    const feedback = document.getElementById('login-feedback');

    form.addEventListener('submit', async (event) => {
        event.preventDefault();
        feedback.textContent = 'Entrando...';

        try {
            const usuario = await api.login(email.value, senha.value);
            salvarUsuarioLogado(usuario);
            window.location.href = '../tela principal/index.html';
        } catch (error) {
            feedback.textContent = error.message || 'Email ou senha invalido.';
        }
    });
});
