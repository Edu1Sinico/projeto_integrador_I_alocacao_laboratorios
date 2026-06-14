document.addEventListener('DOMContentLoaded', async () => {
    protegerPagina();

    document.getElementById('form-laboratorio').addEventListener('submit', cadastrarLaboratorio);
    document.getElementById('form-disciplina').addEventListener('submit', cadastrarDisciplina);
    document.getElementById('form-software').addEventListener('submit', cadastrarSoftware);

    await carregarCadastros();
});

async function carregarCadastros() {
    const feedback = document.getElementById('gestao-feedback');
    feedback.textContent = 'Carregando cadastros...';

    try {
        const [laboratorios, usuarios, disciplinas, softwares] = await Promise.all([
            api.listarLaboratorios(),
            api.listarUsuarios(),
            api.listarDisciplinas(),
            api.listarSoftwares()
        ]);

        renderLaboratorios(laboratorios);
        renderUsuarios(usuarios);
        renderDisciplinas(disciplinas);
        renderSoftwares(softwares);
        feedback.textContent = '';
    } catch (error) {
        feedback.textContent = 'Nao foi possivel carregar os cadastros. Verifique se a API esta rodando.';
    }
}

function renderLaboratorios(laboratorios) {
    document.getElementById('tabela-laboratorios').innerHTML = laboratorios.map((lab) => `
        <tr>
            <td>${lab.numeroLaboratorio}</td>
            <td>${lab.bloco}</td>
            <td>${lab.qtdeComputador}</td>
            <td>${lab.capacidadeMaxAluno}</td>
            <td><button type="button" data-id="${lab.idLaboratorio}" data-tipo="laboratorio">Excluir</button></td>
        </tr>
    `).join('');

    ligarBotoesExclusao();
}

function renderUsuarios(usuarios) {
    const tipos = {
        1: 'Diretor Academico',
        2: 'Coordenador',
        3: 'Responsavel TI'
    };

    document.getElementById('tabela-usuarios').innerHTML = usuarios.map((usuario) => `
        <tr>
            <td>${usuario.nome}</td>
            <td>${usuario.email}</td>
            <td>${usuario.re}</td>
            <td>${tipos[usuario.tipo] || usuario.tipo}</td>
            <td><button type="button" data-id="${usuario.id}" data-tipo="usuario">Excluir</button></td>
        </tr>
    `).join('');

    ligarBotoesExclusao();
}

function renderDisciplinas(disciplinas) {
    document.getElementById('tabela-disciplinas').innerHTML = disciplinas.map((disciplina) => `
        <tr>
            <td>${disciplina.nomeDisciplina}</td>
            <td>${disciplina.qtdeAlunos}</td>
            <td><button type="button" data-id="${disciplina.idDisciplina}" data-tipo="disciplina">Excluir</button></td>
        </tr>
    `).join('');

    ligarBotoesExclusao();
}

function renderSoftwares(softwares) {
    document.getElementById('tabela-softwares').innerHTML = softwares.map((software) => `
        <tr>
            <td>${software.nomeSoftware}</td>
            <td>${software.versao}</td>
            <td><button type="button" data-id="${software.idSoftware}" data-tipo="software">Excluir</button></td>
        </tr>
    `).join('');

    ligarBotoesExclusao();
}

function ligarBotoesExclusao() {
    document.querySelectorAll('button[data-tipo]').forEach((button) => {
        button.onclick = async () => {
            const tipo = button.dataset.tipo;
            const id = button.dataset.id;

            if (!confirm('Deseja excluir este cadastro?')) return;

            try {
                if (tipo === 'laboratorio') await api.removerLaboratorio(id);
                if (tipo === 'usuario') await api.removerUsuario(id);
                if (tipo === 'disciplina') await api.removerDisciplina(id);
                if (tipo === 'software') await api.removerSoftware(id);
                await carregarCadastros();
            } catch (error) {
                document.getElementById('gestao-feedback').textContent = error.message || 'Nao foi possivel excluir.';
            }
        };
    });
}

async function cadastrarLaboratorio(event) {
    event.preventDefault();

    await executarCadastro(event.target, () => api.criarLaboratorio({
        numeroLaboratorio: Number(document.getElementById('lab-numero').value),
        bloco: document.getElementById('lab-bloco').value,
        qtdeComputador: Number(document.getElementById('lab-computadores').value)
    }));
}

async function cadastrarDisciplina(event) {
    event.preventDefault();

    await executarCadastro(event.target, () => api.criarDisciplina({
        nomeDisciplina: document.getElementById('disciplina-nome').value,
        qtdeAlunos: Number(document.getElementById('disciplina-alunos').value)
    }));
}

async function cadastrarSoftware(event) {
    event.preventDefault();

    await executarCadastro(event.target, () => api.criarSoftware({
        nomeSoftware: document.getElementById('software-nome').value,
        versao: document.getElementById('software-versao').value
    }));
}

async function executarCadastro(form, acao) {
    const feedback = document.getElementById('gestao-feedback');
    feedback.textContent = 'Salvando cadastro...';

    try {
        await acao();
        form.reset();
        await carregarCadastros();
        feedback.textContent = 'Cadastro salvo com sucesso.';
    } catch (error) {
        feedback.textContent = error.message || 'Nao foi possivel salvar.';
    }
}
