document.addEventListener('DOMContentLoaded', () => {
    const form = document.getElementById('infos');
    const feedback = document.getElementById('cadastro-feedback');

    form.addEventListener('submit', async (event) => {
        event.preventDefault();
        feedback.textContent = 'Cadastrando...';

        const dados = {
            nome: document.getElementById('cad_nome').value,
            email: document.getElementById('cad_email').value,
            re: document.getElementById('cad_re').value,
            senhaHash: document.getElementById('cad_senha').value,
            tipo: Number(document.getElementById('tipo_funcionario').value)
        };

        try {
            await api.criarUsuario(dados);
            feedback.textContent = 'Usuario cadastrado com sucesso.';
            form.reset();
            setTimeout(() => {
                window.location.href = '../login/login.html';
            }, 900);
        } catch (error) {
            feedback.textContent = error.message || 'Nao foi possivel cadastrar.';
        }
    });
});
