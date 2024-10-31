import { useState } from "react";
import styles from "./styles.module.scss";
import axios from "axios";

export const ReatribuirChamado = (props) => {
  const [chamadoId, setChamadoId] = useState("");
  const [usernameTecnico, setUsernameTecnico] = useState("");

  async function handleSubmit(e) {
    e.preventDefault();

    const token = localStorage.getItem("token");

    const data = {
      chamadoId: parseInt(chamadoId),
      tecnicoUsername: usernameTecnico,
    };

    try {
      const response = await axios.patch(
        "http://localhost:5089/Chamado/reatribuir-tecnico",
        data,
        {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        }
      );

      console.log(response);
      setChamadoId("")
      setUsernameTecnico("")
    } catch (error) {
      console.error("Erro ao reatribuir chamado:", error.message);
    }
  }

  return (
    <section className={styles.container}>
      <div>
        <h1>Reatribuir Chamado</h1>
      </div>
      <form action="post" onSubmit={handleSubmit}>
        <input
          type="text"
          placeholder="ID do Chamado"
          value={chamadoId}
          onChange={(e) => setChamadoId(e.target.value)}
        />
        <input
          type="text"
          placeholder="Username do Técnico"
          value={usernameTecnico}
          onChange={(e) => setUsernameTecnico(e.target.value)}
        />
        <input type="submit" value="Reatribuir" />
      </form>
    </section>
  );
};
