import { useEffect, useState } from "react";
import styles from "./styles.module.scss";
import axios from "axios";
import { Chamado } from "../Chamado/Chamado";

export const ChamadosAbertos = (props) => {
  const [chamadosAbertos, setChamadosAbertos] = useState([]);

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
      console.log(response);

      setChamadosAbertos(response.data);
    } catch (error) {
      console.error("Erro ao buscar chamados abertos:", error);
    }
  }

  useEffect(() => {
    getChamadosAbertos();
  }, []);

  return (
    <div className={styles.container}>
      <div>
        <h1>Chamados Abertos</h1>
      </div>

      <div>
        {chamadosAbertos.map((chamado)=> (
          <Chamado key={chamado.chamadoId} chamado={chamado} />
        ))}
      </div>
    </div>
  );
};
