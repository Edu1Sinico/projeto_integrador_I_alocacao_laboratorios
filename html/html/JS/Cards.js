const statusClasses = ['disponivel', 'indisponivel', 'pendente'];

const statusPorTexto = {
  disponivel: 'disponivel',
  disponível: 'disponivel',
  indisponivel: 'indisponivel',
  indisponível: 'indisponivel',
  pendente: 'pendente'
};

document.querySelectorAll('.lab-card').forEach((card) => {
  const statusTexto = card.querySelector('.topo-card p');

  if (!statusTexto) return;

  const statusDigitado = statusTexto.textContent.trim().toLowerCase();
  const statusClasse = statusPorTexto[statusDigitado];

  if (!statusClasse) return;

  card.classList.remove(...statusClasses);
  card.classList.add(statusClasse);
});