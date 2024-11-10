import styles from "./styles.module.scss";
import { Grafico } from "../Grafico/Grafico";
import { SlArrowDown, SlArrowUp } from "react-icons/sl";
import { useEffect, useState } from "react";
import axios from "axios";
import * as handleDataChart from "../../functions/HandleDataChart"
import Chart from 'chart.js/auto';
export const GraficoTecnicos = (props) => {
  const [isContentVisible, setIsContentVisible] = useState(false);
  const [chartData, setChartData] = useState(null);

  const toggleContentVisibility = () => {
    setIsContentVisible(!isContentVisible); 
  };

  async function getChartData() {
    const token = localStorage.getItem('token');

    try {
      const response = await axios.get("http://localhost:5089/Chamado/tecnicos-dashboard", {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      })

      const dadosTranformados = handleDataChart.transformarDadosTecnicoEmListaParaChaveValor(response.data);
      
      
      setChartData(dadosTranformados)
      
    } catch (error) {
      setChartData(null) 
      console.error("Erro ao carregar dados do gráfico" + error)
    }

  }

  useEffect(() => {
    getChartData();
  }, []);

  return (
    <div className={styles.container}>
      <div className={styles.title} onClick={toggleContentVisibility}>
        <h1>Gráfico - Tecnicos</h1>
        {isContentVisible ? <SlArrowUp /> : <SlArrowDown />}
      </div>
      {isContentVisible && (
        <div className={styles.grafico}>
          <Grafico
            data={chartData}
            title="Top 5 Técnicos"
            typeData="ChamadosFinalizados"
          />
        </div>
      )}
    </div>
  );
};
