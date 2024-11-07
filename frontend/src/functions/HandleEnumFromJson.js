export function traduzirPrioridade(prioridade) {
  const prioridadeMap = {
    0: "Baixa",
    1: "Média",
    2: "Alta",
  };

  return prioridadeMap[prioridade];
}

export function traduzirStatus(status){
  const statusMap = {
    0: "Aberto",
    1: "Em andamento",
    2: "Fechado"
  }

  return statusMap[status];
}

export function formatarDataHora(dataHoraString) {
  const data = new Date(dataHoraString);

  // Formatando data no estilo "dd/mm/yyyy"
  const dia = String(data.getDate()).padStart(2, '0');
  const mes = String(data.getMonth() + 1).padStart(2, '0');
  const ano = data.getFullYear();

  // Formatando hora no estilo "hh:mm"
  const horas = String(data.getHours()).padStart(2, '0');
  const minutos = String(data.getMinutes()).padStart(2, '0');

  return `${dia}/${mes}/${ano} ${horas}:${minutos}`;
}
