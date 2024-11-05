import styles from "./styles.module.scss";
import { SlArrowDown, SlArrowUp } from "react-icons/sl";
import { useEffect, useState } from "react";
import Chart from 'chart.js/auto';
import axios from "axios";
import { Grafico } from "../Grafico/Grafico";

export const GraficoChamados = (props) => {
  const [isContentVisible, setIsContentVisible] = useState(false);
  const [chartData, setChartData] = useState(null);

  

  const toggleContentVisibility = () => {
    setIsContentVisible(!isContentVisible); // Alterna visibilidade
  };

  async function getChartData() {
    const token = localStorage.getItem('token');

    try {
      const response = await axios.get("http://localhost:5089/Chamado/chamados-dashboard", {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      })

      
      setChartData(response.data)
      
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
        <h1>Gráfico - Chamados</h1>
        {isContentVisible ? <SlArrowUp /> : <SlArrowDown />}
      </div>
      {isContentVisible && (
        <div className={styles.grafico}>
          <Grafico data={chartData} />
        </div>
      )}
      
    </div>
  );
};
