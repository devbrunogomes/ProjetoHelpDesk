import { useEffect, useState } from "react";
import styles from "./styles.module.scss";
import axios from "axios";
import { SlArrowDown, SlArrowUp } from "react-icons/sl";



export const CriarChamado = (props) => {
  const [titulo, setTitulo] = useState("");
  const [descricao, setDescricao] = useState("");
  const [isFormVisible, setIsFormVisible] = useState(false);

  async function handleSubmit(event) {
    event.preventDefault();

    const token = localStorage.getItem("token");

    const chamadoData = {
      titulo: titulo,
      descricao: descricao,
    };

    console.log(chamadoData);

    try {
      const response = await axios.post(
        "http://localhost:5089/Chamado",
        chamadoData,
        {
          headers: {
            Authorization: `Bearer ${token}`,
            "Content-Type": "application/json",
          },
        }
      );

      console.log("Chamado criado com sucesso:", response.data);

      setTitulo("");
      setDescricao("");
    } catch (error) {
      if (error.response) {
        console.error("Erro ao criar chamado:", error.response.data);
      } else {
        console.error("Erro ao criar chamado:", error);
      }
    }
  }

  const toggleFormVisibility = () => {
    setIsFormVisible(!isFormVisible); // Alterna visibilidade
  };

  return (
    <div className={styles.container}>
      <section>
        <div className={styles.title} onClick={toggleFormVisibility}>
          <h1>  CRIAR CHAMADO</h1>
          {isFormVisible? <SlArrowUp /> : <SlArrowDown />}
          
        </div>

        <form
          action="post"
          onSubmit={handleSubmit}
          className={isFormVisible ? styles.formVisible : styles.formHidden}
        >
          <input
            type="text"
            placeholder="TÍTULO"
            value={titulo}
            onChange={(e) => setTitulo(e.target.value)}
            required
          />
          <textarea
            type="text"
            placeholder="Descreva seu problema"
            value={descricao}
            onChange={(e) => setDescricao(e.target.value)}
            required
          />
          <input type="submit" value="Enviar Chamado" />
        </form>
      </section>
    </div>
  );
};
