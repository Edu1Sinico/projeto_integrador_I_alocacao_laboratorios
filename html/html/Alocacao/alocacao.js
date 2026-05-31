document.addEventListener('DOMContentLoaded', async () => {
    protegerPagina();

    const feedback = document.getElementById('alocacao-feedback');
    feedback.textContent = 'Carregando alocacoes...';

    try {
        const alocacoes = await apiRequest('/Alocacao');
        renderizarAlocacoes(alocacoes);
        feedback.textContent = alocacoes.length ? '' : 'Nenhuma alocacao cadastrada.';
    } catch (error) {
        feedback.textContent = 'Nao foi possivel carregar as alocacoes. Verifique se a API esta rodando.';
    }
});

function renderizarAlocacoes(alocacoes) {
    const status = {
        1: 'Pendente',
        2: 'Aprovada',
        3: 'Reprovada'
    };

    document.getElementById('tabela-alocacoes').innerHTML = alocacoes.map((alocacao) => `
        <tr>
            <td>${formatarData(alocacao.data)}</td>
            <td>${alocacao.horaInicio} - ${alocacao.horaFim}</td>
            <td>Lab ${alocacao.numeroLaboratorio} - Bloco ${alocacao.blocoLaboratorio}</td>
            <td>${alocacao.nomeDisciplina}</td>
            <td>${alocacao.nomeUsuario}</td>
            <td>${status[alocacao.status] || alocacao.status}</td>
        </tr>
    `).join('');
}

function formatarData(data) {
    return new Date(data).toLocaleDateString('pt-BR', { timeZone: 'UTC' });
}
