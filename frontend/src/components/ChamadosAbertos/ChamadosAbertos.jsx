import { useEffect, useState } from "react";
import styles from "./styles.module.scss";
import axios from "axios";
import { Chamado } from "../Chamado/Chamado";
import { SlArrowDown, SlArrowUp } from "react-icons/sl";

export const ChamadosAbertos = (props) => {
  const [chamadosAbertos, setChamadosAbertos] = useState([]);
  const [isChamadosVisible, setIsChamadosVisible] = useState(false);

  async function getChamadosAbertos() {
    const token = localStorage.getItem("token");

    try {
      const response = await axios.get(
        "http://localhost:5089/Chamado/chamados-abertos",
        {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        }
      );

      setChamadosAbertos(response.data);
    } catch (error) {
      console.error("Erro ao buscar chamados abertos:", error);
    }
  }

  const toggleChamadosVisibility = () => {
    setIsChamadosVisible(!isChamadosVisible); // Alterna visibilidade
  };

  useEffect(() => {
    getChamadosAbertos();
  }, []);

  return (
    <div className={styles.container}>
      <div className={styles.title} onClick={toggleChamadosVisibility}>
        <h1>Chamados Abertos</h1>
        {isChamadosVisible ? <SlArrowUp /> : <SlArrowDown />}
      </div>
      {isChamadosVisible && (
        <div className={styles.chamados}>
          {chamadosAbertos.map((chamado) => (
            <Chamado key={chamado.chamadoId} chamado={chamado} />
          ))}
        </div>
      )}
    </div>
  );
};
