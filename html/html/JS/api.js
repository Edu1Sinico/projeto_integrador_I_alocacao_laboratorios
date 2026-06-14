const API_BASE_URL = localStorage.getItem('apiBaseUrl') || 'http://localhost:5129/api';

async function apiRequest(path, options = {}) {
    const response = await fetch(`${API_BASE_URL}${path}`, {
        headers: {
            'Content-Type': 'application/json',
            ...(options.headers || {})
        },
        ...options
    });

    if (response.status === 204) {
        return null;
    }

    const contentType = response.headers.get('content-type') || '';
    const body = contentType.includes('application/json')
        ? await response.json()
        : await response.text();

    if (!response.ok) {
        const message = typeof body === 'string' ? body : 'Nao foi possivel concluir a operacao.';
        throw new Error(message);
    }

    return body;
}

const api = {
    login: (email, senha) => apiRequest('/Usuario/login', {
        method: 'POST',
        body: JSON.stringify({ email, senha })
    }),
    listarLaboratorios: () => apiRequest('/Laboratorio'),
    criarLaboratorio: (dados) => apiRequest('/Laboratorio', {
        method: 'POST',
        body: JSON.stringify(dados)
    }),
    atualizarLaboratorio: (id, dados) => apiRequest(`/Laboratorio/${id}`, {
        method: 'PUT',
        body: JSON.stringify(dados)
    }),
    removerLaboratorio: (id) => apiRequest(`/Laboratorio/${id}`, { method: 'DELETE' }),
    listarUsuarios: () => apiRequest('/Usuario'),
    criarUsuario: (dados) => apiRequest('/Usuario', {
        method: 'POST',
        body: JSON.stringify(dados)
    }),
    removerUsuario: (id) => apiRequest(`/Usuario/${id}`, { method: 'DELETE' }),
    listarDisciplinas: () => apiRequest('/Disciplina'),
    criarDisciplina: (dados) => apiRequest('/Disciplina', {
        method: 'POST',
        body: JSON.stringify(dados)
    }),
    removerDisciplina: (id) => apiRequest(`/Disciplina/${id}`, { method: 'DELETE' }),
    listarSoftwares: () => apiRequest('/Software'),
    criarSoftware: (dados) => apiRequest('/Software', {
        method: 'POST',
        body: JSON.stringify(dados)
    }),
    removerSoftware: (id) => apiRequest(`/Software/${id}`, { method: 'DELETE' }),
    criarAlocacao: (dados) => apiRequest('/Alocacao', {
        method: 'POST',
        body: JSON.stringify(dados)
    })
};
