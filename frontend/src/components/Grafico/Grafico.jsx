import { Bar } from "react-chartjs-2";
import { Chart as ChartJS, CategoryScale, LinearScale, PointElement, LineElement, Title, Tooltip, Legend } from "chart.js";

export const Grafico = ({data}) => {
  const chartData = {
    labels: ["Abertos", "Em Andamento", "Fechados"],
    datasets: [
      {
        label: "Chamados",
        data: [data.totalAbertos, data.totalEmAndamento, data.totalFechados],
        backgroundColor: ["rgba(255, 99, 132, 0.5)", "rgba(54, 162, 235, 0.5)", "rgba(75, 192, 192, 0.5)"],
        borderColor: ["rgba(255, 99, 132, 1)", "rgba(54, 162, 235, 1)", "rgba(75, 192, 192, 1)"],
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
        text: "Status dos Chamados",
      },
    },
  };

  return <Bar data={chartData} options={options} />;
}