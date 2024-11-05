import styles from "./styles.module.scss";
import { useEffect, useState } from "react";
import { SlArrowDown, SlArrowUp } from "react-icons/sl";
import axios from "axios";

export const ListaTecnicos = (props) => {
  const [isContentVisible, setIsContentVisible] = useState(false);
  const [tecnicos, setTecnicos] = useState([]);

  const toggleContentVisibility = () => {
    setIsContentVisible(!isContentVisible); // Alterna visibilidade
  };

  async function getTecnicos() {
    const token = localStorage.getItem("token");

    try {
      const response = await axios.get("http://localhost:5089/Tecnico", {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });

      setTecnicos(response.data);
    } catch (error) {
      console.error("Erro ao carregar os técnicos:", error);
    }
  }

  const handleExcluir = async (id) => {
    const token = localStorage.getItem("token");

    try {
      // Realiza a requisição DELETE para o endpoint
      await axios.delete(`http://localhost:5089/Tecnico/${id}`, {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });

      // Atualiza o estado `tecnicos`, removendo o técnico excluído
      setTecnicos((prevTecnicos) =>
        prevTecnicos.filter((tecnico) => tecnico.tecnicoId !== id)
      );
      console.log("Técnico excluído com sucesso!");
    } catch (error) {
      console.error("Erro ao excluir o técnico:", error);
    }
  };

  useEffect(() => {
    getTecnicos();
  }, []);

  return (
    <div className={styles.container}>
      <div className={styles.title} onClick={toggleContentVisibility}>
        <h1>Listar Técnicos </h1>
        {isContentVisible ? <SlArrowUp /> : <SlArrowDown />}
      </div>
      {isContentVisible && (
        <div>
          {tecnicos.map((tecnico) => (
            <div key={tecnico.tecnicoId} className={styles.content}>
              <span>#{tecnico.tecnicoId}</span>
              <span>
                {tecnico.nomeCompleto} - {tecnico.userName}
              </span>
              <span>{tecnico.email}</span>
              <div>
                <button
                  onClick={async () => await handleExcluir(tecnico.tecnicoId)}
                >
                  Excluir
                </button>
              </div>
            </div>
          ))}
        </div>
      )}
    </div>
  );
};
