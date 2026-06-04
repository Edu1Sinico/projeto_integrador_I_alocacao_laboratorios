class Customheader extends HTMLElement {
    connectedCallback() {
        this.innerHTML = `
    <style>
    header {
        background-color: #dddddd;
        display: flex;
        align-items: center;
        justify-content: space-between;
        gap: 10px;
        border-radius: 10px;
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

    .link {
        color: #000;
        text-decoration: none;
    }

    .link:hover {
        color: #bd9400;
    }

    #conta-section {
        display: flex;
        gap: 10px;
        margin-left: auto;
    }

    .conta-fig figure {
        margin: 0;
    }
    </style>

    <header>
        <figure class="img-box">
            <img src="../img/icon_E.png" alt="logo" class="img-size">
        </figure>

        <nav>
            <a class="link" href="index.html">Início</a>
            <a class="link" href="gestao-cadastro.html">Gestão de Cadastros</a>
            <a class="link" href="ajuda.html">Ajuda</a>
        </nav>
        
        <div id="conta-section">
            <a class="conta-fig" href="configuracoes.html">
                <figure>
                    <img src="../img/Config.png" alt="config" class="img-size">
                </figure>
            </a>
            <a class="conta-fig" href="conta.html">
                <figure>
                    <img src="../img/conta.png" alt="conta" class="img-size">
                </figure>
            </a>
        </div>
    </header> `;
    }
}

// Define a nova tag <custom-header></custom-header>
customElements.define('custom-header', Customheader);