let laboratorios = [];
let laboratorioSelecionado = null;

document.addEventListener('DOMContentLoaded', async () => {
    protegerPagina();

    document.getElementById('input-busca').addEventListener('input', renderizarLaboratorios);
    document.getElementById('select-bloco').addEventListener('change', renderizarLaboratorios);
    document.getElementById('input-computadores').addEventListener('input', renderizarLaboratorios);
    document.getElementById('btn-fechar-modal').addEventListener('click', fecharModal);
    document.getElementById('form-alocacao').addEventListener('submit', criarAlocacao);

    await carregarLaboratorios();
    await carregarDisciplinas();
});

async function carregarLaboratorios() {
    const feedback = document.getElementById('feedback-principal');
    feedback.textContent = 'Carregando laboratorios...';

    try {
        laboratorios = await api.listarLaboratorios();
        preencherBlocos();
        renderizarLaboratorios();
        feedback.textContent = laboratorios.length ? '' : 'Nenhum laboratorio cadastrado.';
    } catch (error) {
        feedback.textContent = 'Nao foi possivel carregar os laboratorios. Verifique se a API esta rodando.';
    }
}

async function carregarDisciplinas() {
    const select = document.getElementById('modal-disciplina');

    try {
        const disciplinas = await api.listarDisciplinas();
        select.innerHTML = disciplinas
            .map((disciplina) => `<option value="${disciplina.idDisciplina}">${disciplina.nomeDisciplina}</option>`)
            .join('');
    } catch {
        select.innerHTML = '<option value="">Cadastre uma disciplina antes</option>';
    }
}

function preencherBlocos() {
    const select = document.getElementById('select-bloco');
    const blocos = [...new Set(laboratorios.map((lab) => lab.bloco).filter(Boolean))].sort();

    select.innerHTML = '<option value="">Todos</option>' +
        blocos.map((bloco) => `<option value="${bloco}">Bloco ${bloco}</option>`).join('');
}

function renderizarLaboratorios() {
    const grid = document.getElementById('cards-laboratorios');
    const busca = document.getElementById('input-busca').value.trim().toLowerCase();
    const bloco = document.getElementById('select-bloco').value;
    const computadores = Number(document.getElementById('input-computadores').value || 0);

    const filtrados = laboratorios.filter((lab) => {
        const nome = `lab ${lab.numeroLaboratorio} bloco ${lab.bloco}`.toLowerCase();
        const atendeBusca = !busca || nome.includes(busca);
        const atendeBloco = !bloco || lab.bloco === bloco;
        const atendeComputadores = !computadores || lab.qtdeComputador >= computadores;

        return atendeBusca && atendeBloco && atendeComputadores;
    });

    grid.innerHTML = filtrados.map((lab) => `
        <div class="lab-card disponivel">
          <div class="topo-card">
            <h2>LAB ${lab.numeroLaboratorio}</h2>
            <p>disponivel</p>
          </div>
          <div class="info-card">
            <h1>Bloco ${lab.bloco}</h1>
            <p>${lab.qtdeComputador} maquinas</p>
            <p>${lab.capacidadeMaxAluno} alunos</p>
          </div>
          <button class="trigger-btn-card" type="button" data-id="${lab.idLaboratorio}">Mais</button>
        </div>
    `).join('');

    grid.querySelectorAll('.trigger-btn-card').forEach((button) => {
        button.addEventListener('click', () => abrirModal(button.dataset.id));
    });
}

function abrirModal(id) {
    laboratorioSelecionado = laboratorios.find((lab) => lab.idLaboratorio === id);

    if (!laboratorioSelecionado) return;

    document.getElementById('modal-titulo').textContent = `Laboratorio ${laboratorioSelecionado.numeroLaboratorio}`;
    document.getElementById('modal-detalhes').innerHTML = `
        <p>Local: Bloco ${laboratorioSelecionado.bloco}</p>
        <p>Capacidade maxima de alunos: ${laboratorioSelecionado.capacidadeMaxAluno}</p>
        <p>Quantidade de computadores: ${laboratorioSelecionado.qtdeComputador}</p>
    `;
    document.getElementById('modal-data').value = document.getElementById('filtro-data').value;
    document.getElementById('modal-hora-inicio').value = document.getElementById('filtro-hora').value;
    document.getElementById('feedback-alocacao').textContent = '';
    document.getElementById('modal-alocacao').classList.add('active');
}

function fecharModal() {
    document.getElementById('modal-alocacao').classList.remove('active');
}

async function criarAlocacao(event) {
    event.preventDefault();

    const usuario = getUsuarioLogado();
    const feedback = document.getElementById('feedback-alocacao');

    if (!usuario || !laboratorioSelecionado) return;

    feedback.textContent = 'Enviando alocacao...';

    const dados = {
        data: document.getElementById('modal-data').value,
        horaInicio: `${document.getElementById('modal-hora-inicio').value}:00`,
        horaFim: `${document.getElementById('modal-hora-fim').value}:00`,
        laboratorioId: laboratorioSelecionado.idLaboratorio,
        disciplinaId: document.getElementById('modal-disciplina').value,
        usuarioId: usuario.id
    };

    try {
        await api.criarAlocacao(dados);
        feedback.textContent = 'Alocacao solicitada com sucesso.';
        setTimeout(fecharModal, 900);
    } catch (error) {
        feedback.textContent = error.message || 'Nao foi possivel solicitar a alocacao.';
    }
}
