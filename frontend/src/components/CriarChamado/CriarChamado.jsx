import { useState } from "react";
import styles from "./styles.module.scss";
import axios from "axios";

export const CriarChamado = (props) => {
  const [titulo, setTitulo] = useState("");
  const [descricao, setDescricao] = useState("");

  async function handleSubmit(event) {
    event.preventDefault();

    const token = localStorage.getItem("token");

    const chamadoData = {
      titulo: titulo,
      descricao: descricao,
    };

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

  return (
    <div className={styles.container}>
      <section>
        <div>
          <h1>CRIAR CHAMADO</h1>
        </div>

        <form action="post" onSubmit={handleSubmit}>
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
