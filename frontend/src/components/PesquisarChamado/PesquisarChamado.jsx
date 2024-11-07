import { useState } from "react";
import styles from "./styles.module.scss";
import axios from "axios";
import { SlArrowDown, SlArrowUp } from "react-icons/sl";
import { BsSearch } from "react-icons/bs";
import { Chamado } from "../Chamado/Chamado";

export const PesquisarChamado = (props) => {
  const [isFormVisible, setIsFormVisible] = useState(false);
  const [chamadoId, setChamadoId] = useState("");
  const [chamado, setChamado] = useState(null);

  const toggleFormVisibility = () => {
    setIsFormVisible(!isFormVisible);
  };

  async function handleSubmit(e) {
    e.preventDefault();

    setChamado(null);

    const token = localStorage.getItem("token");

    try {
      const response = await axios.get(
        `http://localhost:5089/Chamado/${chamadoId}`,
        {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        }
      );

      console.log(response.data);
      setChamado(response.data);
    } catch (error) {
      setChamado(null);
      console.error(error);
    }
  }

  return (
    <div className={styles.container}>
      <div className={styles.title} onClick={toggleFormVisibility}>
        <h1>Pesquisar Chamado</h1>
        {isFormVisible ? <SlArrowUp /> : <SlArrowDown />}
      </div>
      {isFormVisible && (
        <>
          <form action="post" onSubmit={async (e) => await handleSubmit(e)}>
            <input
              placeholder="Id do Chamado"
              name="tecnicoId"
              type="text"
              value={chamadoId}
              onChange={(e) => setChamadoId(e.target.value)}
            />
            <button type="submit">
              <BsSearch />
            </button>
          </form>
          <div>{chamado && <Chamado chamado={chamado} />}</div>
        </>
      )}
    </div>
  );
};
