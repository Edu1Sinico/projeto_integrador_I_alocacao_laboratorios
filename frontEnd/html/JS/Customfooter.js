class Customfooter extends HTMLElement {
    connectedCallback() {
        this.innerHTML = ``;
    }
}

// Define a nova tag <custom-footer></custom-footer>
customElements.define('custom-footer', Customfooter);