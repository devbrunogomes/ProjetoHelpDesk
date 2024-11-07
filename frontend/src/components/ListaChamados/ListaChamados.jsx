import styles from "./styles.module.scss";
import { useEffect, useState } from "react";
import axios from "axios";
import { SlArrowDown, SlArrowUp } from "react-icons/sl";

export const ListaChamados = (props) => {
  const [isContentVisible, setIsContentVisible] = useState(true);
  const [chamados, setChamados] = useState([]);
  const [filtroChamado, setFiltroChamado] = useState("");

  const toggleContentVisibility = () => {
    setIsContentVisible(!isContentVisible); // Alterna visibilidade
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    const token = localStorage.getItem("token");
    
    if (chamados.length === 0) {
      try {
        const response = await axios.get("http://localhost:5089/Chamado", {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        });
        
        console.log(response.data)
        setChamados(response.data);
      } catch (error) {
        console.error("Erro ao consultar chamados: " + error);
      }
    }
  };

  return (
    <div className={styles.container}>
      <div className={styles.title} onClick={toggleContentVisibility}>
        <h1>Listar Chamados </h1>
        {isContentVisible ? <SlArrowUp /> : <SlArrowDown />}
      </div>
      {isContentVisible && (
        <div className={styles.content}>
          <form action="post" onSubmit={handleSubmit}>
            <label htmlFor="filter">Filtro por Status de Chamado</label>
            <select name="filter" id="filterChamados">
              <option value="0">Abertos</option>
              <option value="1">Em Andamento</option>
              <option value="2">Fechados</option>
            </select>
            <input type="submit" value="Pesquisar" />
          </form>
        </div>
      )}
    </div>
  );
};
