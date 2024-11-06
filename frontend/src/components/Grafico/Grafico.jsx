import { Bar } from "react-chartjs-2";

export const Grafico = ({data, title, typeData}) => {
  // Extraindo os rótulos e valores do JSON de forma dinâmica
  const labels = Object.keys(data);          // Labels com as chaves do JSON
  const values = Object.values(data);        // Data com os valores do JSON

  // Gerando cores dinâmicas para cada valor no gráfico
  const backgroundColors = values.map((_, index) => `rgba(${100 + index * 50}, ${99 + index * 20}, ${132 + index * 30}, 0.5)`);
  const borderColors = values.map((_, index) => `rgba(${100 + index * 50}, ${99 + index * 20}, ${132 + index * 30}, 1)`);

  const chartData = {
    labels: labels,
    datasets: [
      {
        label: typeData,
        data: values,
        backgroundColor: backgroundColors,
        borderColor: borderColors,
        borderWidth: 1,
      },
    ],
  };

  const options = {
    responsive: true,
    plugins: {
      legend: {
        display: false,
      },
      title: {
        display: true,
        text: title,
      },
    },
  };

  return <Bar data={chartData} options={options} />;
}