import { useEffect, useState } from "react";
import { Chamado } from "../Chamado/Chamado";
import styles from "./styles.module.scss";
import axios from "axios";
import { SlArrowDown, SlArrowUp } from "react-icons/sl";
import * as handleToken from "../../functions/HandleToken";

export const MeusChamados = (props) => {
  const [chamados, setChamados] = useState([]);
  const [isChamadosVisible, setIsChamadosVisible] = useState(true);

  async function getChamados() {
    const token = localStorage.getItem("token");
    const myRole = handleToken.verificarRoleDoToken(token).toLowerCase();

    try {
      const response = await axios.get(
        `http://localhost:5089/${myRole}/meus-chamados`,
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

  const toggleChamadosVisibility = () => {
    setIsChamadosVisible(!isChamadosVisible); // Alterna visibilidade
  };

  useEffect(() => {
    getChamados();
  }, []);

  return (
    <div className={styles.container}>
      <div className={styles.title} onClick={toggleChamadosVisibility}>
        <h1>MEUS CHAMADOS</h1>
        {isChamadosVisible ? <SlArrowUp /> : <SlArrowDown />}
      </div>
      {isChamadosVisible && (
        <div className={styles.chamados}>
          {chamados.map((chamado) => (
            <Chamado key={chamado.chamadoId} chamado={chamado} />
          ))}
        </div>
      )}
    </div>
  );
};
