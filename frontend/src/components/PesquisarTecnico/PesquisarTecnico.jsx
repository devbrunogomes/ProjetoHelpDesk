import { useState } from "react";
import styles from "./styles.module.scss";
import axios from "axios";
import { SlArrowDown, SlArrowUp } from "react-icons/sl";

export const PesquisarTecnicos = (props) => {
  const [isFormVisible, setIsFormVisible] = useState(false);
  const [tecnicoId, setTecnicoId] = useState("");

  const toggleFormVisibility = () => {
    setIsFormVisible(!isFormVisible); // Alterna visibilidade
  };

  async function handleSubmit(e) {
    e.preventDefault();

    const token = localStorage.getItem("token");

    try {
      const response = await axios.get(
        `http://localhost:5089/Tecnico/${tecnicoId}`, {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        }
      );

      console.log(response);
    } catch (error) {
      console.error(error);
      
    }
  }

  return (
    <div className={styles.container}>
      <div className={styles.title} onClick={toggleFormVisibility}>
        <h1>Pesquisar Técnico</h1>
        {isFormVisible ? <SlArrowUp /> : <SlArrowDown />}
      </div>
      {isFormVisible && (
        <form action="post" onSubmit={handleSubmit}>
          <label htmlFor="tecnicoId">Insira o Id do Técnico</label>
          <input
            name="tecnicoId"
            type="text"
            value={tecnicoId}
            onChange={(e) => setTecnicoId(e.target.value)}
          />
          <input type="submit" value="Pesquisar" />
        </form>
      )}
    </div>
  );
};
