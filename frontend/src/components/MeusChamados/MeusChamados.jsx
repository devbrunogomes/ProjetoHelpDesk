import { useEffect, useState } from "react";
import { Chamado } from "../Chamado/Chamado";
import styles from "./styles.module.scss";
import axios from "axios";

export const MeusChamados = (props) => {
  const [chamados, setChamados] = useState([]);

  async function getChamados() {
    const token = localStorage.getItem("token");

    try {
      const response = await axios.get(
        "http://localhost:5089/cliente/meus-chamados",
        {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        }
      );

      console.log(response);
      setChamados(response.data);
    } catch (error) {
      console.error("Erro ao buscar chamados:", error.message);
    }
  }

  useEffect(() => {
    getChamados();
  }, []);

  return (
    <div className={styles.container}>
      <div>
        <h1>MEUS CHAMADOS</h1>
      </div>
      {chamados.map((chamado) => (
        <Chamado key={chamado.chamadoId} chamado={chamado} />
      ))}
    </div>
  );
};
