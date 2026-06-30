// server.js (back-end)
const express = require('express'); // require é o como o javascript importa bibliotecas, o express é um framework para criar servidores web
const app = express(); // cria o servidor express

app.use(express.json()); // permite receber JSON

// Rota GET — retorna lista de usuários
app.get('/usuarios', (req, res) => {
  res.json([               // aqui estamos retornando um array de objetos JSON, cada objeto representa um usuário com id e nome
    { id: 1, nome: 'João' },
    { id: 2, nome: 'Maria' }
  ]);
});

app.listen(3000, () => console.log('Servidor rodando na porta do karaio 3000'));

// front-end (React)
import React, { useEffect, useState } from 'react'; // useEffect: hook que exxecuta algo quando a tela carrega 
// useState é um hook para guardar informações na tela
import axios from 'axios';

function App() {
  const [usuarios, setUsuarios] = useState([]);

}

export const laboratorio = [
    { id: 1, nome: 'laborátorio de computador' , capacidade: 30, disponivel: true },
    { id: 2, nome: 'laborátorio de física' , capacidade: 20, disponivel: false },
    { id: 3, nome: 'laborátorio de química' , capacidade: 25, disponivel: true },
]
