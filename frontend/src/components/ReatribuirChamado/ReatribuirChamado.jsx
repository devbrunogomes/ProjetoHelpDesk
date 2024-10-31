import { useState } from "react";
import styles from "./styles.module.scss";

export const ReatribuirChamado = (props) => {
  const [chamadoId, setChamadoId] = useState("");
  const [usernameTecnico, setUsernameTecnico] = useState("");

  async function handleSubmit(e) {
    e.preventDefault();
  }

  return (
    <section className={styles.container}>
      <div>
        <h1>Reatribuir Chamado</h1>
      </div>
      <form action="post">
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
