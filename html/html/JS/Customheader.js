class Customheader extends HTMLElement {
    connectedCallback() {
        const usuario = localStorage.getItem('usuarioLogado');

        this.innerHTML = `
    <style>
    header {
        background-color: #dddddd;
        display: flex;
        align-items: center;
        justify-content: space-between;
        gap: 10px;
        border-radius: 8px;
        padding: 20px;
        flex-wrap: nowrap;
        box-shadow: 0 4px 10px #a9a9a9;
        margin: 0px;
    }

    .img-box {
        margin-right: 20px;
    }

    .img-size {
        height: 50px;
    }

    nav {
        display: flex;
        gap: 15px;
        flex-wrap: wrap;
    }

    .link, .sair-btn {
        color: #000;
        text-decoration: none;
        font: inherit;
        background: transparent;
        border: none;
        cursor: pointer;
    }

    .link:hover, .sair-btn:hover {
        color: #bd9400;
    }

    #conta-section {
        display: flex;
        align-items: center;
        gap: 10px;
        margin-left: auto;
    }

    .usuario-logado {
        font-size: 0.85rem;
        color: #333;
    }
    </style>

    <header>
        <figure class="img-box">
            <img src="../img/icon_E.png" alt="logo" class="img-size">
        </figure>

        <nav>
            <a class="link" href="../tela principal/index.html">Inicio</a>
            <a class="link" href="../gestao cadastros/gestao-cadastro.html">Gestao de Cadastros</a>
            <a class="link" href="../Alocacao/alocacao.html">Alocacoes</a>
        </nav>
        
        <div id="conta-section">
            ${usuario ? `<span class="usuario-logado">${JSON.parse(usuario).nome}</span>` : ''}
            <button class="sair-btn" type="button" id="btn-sair">Sair</button>
        </div>
    </header> `;

        this.querySelector('#btn-sair')?.addEventListener('click', () => {
            localStorage.removeItem('usuarioLogado');
            window.location.href = '../login/login.html';
        });
    }
}

customElements.define('custom-header', Customheader);
