class Customfooter extends HTMLElement {
    connectedCallback() {
        this.innerHTML = `
        <style>
          footer {
    background-color: #000;
    padding: 20px;
    display: flex;
    flex-wrap: nowrap;
    gap: 10px;
    align-items: center;
    flex-direction: row;
}

.footer-logo{
    height: 120px;
}

#contato-counteiner{
    display: flex;
    flex-direction: column;
    flex-wrap: wrap;
    justify-content: flex-start;
}

#linha-footer {
    width: 3px;
    height: 200px;
    background: #fff;
    border-radius: 10px;
}

.contato{
    display: flex;
    justify-content: start;
}

.contato img{
    height: 35px;
    width: 35px;
}

.contato p{
    color: #fff;
    text-decoration: underline;
}

#contato-space {
    display: flex;
    flex-direction: row;
    align-items: center; 
    gap: 20px;
    margin-left: 20%;
}
        </style>
        <footer>
        <figure>
    <img class="footer-logo" src="../img/Logotipo_E_branco.png" alt="logo branca">
</figure>
<div id="contato-space">
    <div id="linha-footer"></div>
    <div id="contato-counteiner">
        <div class="contato">
            <figure>
                <img src="../img/whatsapp.png" alt="whatsapp">
            </figure>
            <p>19 3404-9594</p>
        </div>
        <div class="contato">
            <figure>
                <img src="../img/tel.png" alt="whatsapp">
            </figure>
            <p>19 3404-9594</p>
        </div>
        <div class="contato">
            <figure>
                <img src="../img/email.png" alt="whatsapp">
            </figure>
            <p>contato@einsteinlimeira.com.br</p>
        </div>
    </div>
</div>
</footer>`;
    }
}

// Define a nova tag <custom-footer></custom-footer>
customElements.define('custom-footer', Customfooter);