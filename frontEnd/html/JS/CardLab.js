class CardLab extends HTMLElement {
    connectedCallback() {
        this.innerHTML = `
        <style>

.card{
    width: 10px;
}

.cards-grid{
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(150px, 1fr));
  gap: 30px;
  justify-items: center;
}

.lab-card{
  width: 140px;
  border-radius: 14px;
  overflow: hidden;
  border: 4px solid #000;
  background-color: #000;
  cursor: default;
}

.topo-card{
  height: 70px;
  display: flex;
  align-items: center;
  justify-content: center;
  flex-wrap: wrap;
  padding: 10px;;
}

.topo-card h2{
  font-size: 28px;
  font-weight: bold;
  color: #000;
  margin: 0px;
  padding: 0px;
}

/* STATUS */
.disponivel .topo-card {
  background-color: #51ff00;
}

.disponivel .topo-card p{
color:#123b01
}

.indisponivel .topo-card{
  background-color: #cf0000;
}

.pendente .topo-card{
  background-color: #e0ac00;
}

.info-card{
  background-color: #000;
  color: #fff;
  text-align: center;
  padding: 20px 10px;
}

.info-card h1{
  font-size: 24px;
  font-weight: bold;
  text-decoration-line: underline;
}

.trigger-btn-card {
    display: flex;
    justify-content: center;
    justify-self: center;
    padding: 15px 40px;
    cursor: pointer;
    font-size: 20px;
    background-color: #ffffff;
    border: 1px solid #cccccc;
    border-radius: 10px;
    margin-bottom: 10px;
}

.trigger-btn-card:hover{
    background-color: #423c34;
    color: #fff;
}

        </style>
        <div class="lab-card disponivel">
          <div class="topo-card">
            <h2>LAB 01</h2>
            <span id="status-card"></span>
          </div>

          <div class="info-card">
            <h1>Bloco D</h1>
            <p>50 Máquinas</p>
            <p></p>
          </div>

          <button id="modalOverlay" class="trigger-btn-card">Mais</button>
        </div>`;
    }
}

// Define a nova tag <card-lab></card-lab>
customElements.define('card-lab', CardLab);