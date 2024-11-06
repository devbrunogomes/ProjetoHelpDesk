export function transformarDadosTecnicoEmListaParaChaveValor(data) {
  const result = data.reduce((acc, item) => {
    acc[item.userName] = item.quantidadeChamadosFinalizados;
    return acc;
  }, {});

  return result;  
}