import { useEffect, useState } from "react";
import { Resposta } from "../Resposta/Resposta";
import styles from "./styles.module.scss";

export const Chamado = ({ chamado }) => {
  const [respostas, setRespostas] = useState([]);

  return (
    <section>
      <div className={styles.titulo}>
        <h2>Chamado #{chamado.chamadoId}</h2>
        <h2>{chamado.status}</h2>
        <h2>{chamado.prioridade} Prioridade</h2>
      </div>
      <div className={styles.subTitulo}>
        <span>{chamado.dataAbertura}</span>
        <span>{chamado.titulo}</span>
      </div>
      <div className={styles.descricao}>
        <p>{chamado.descricao}</p>
      </div>
      {chamado.respostas.map((resposta) => (
        <Resposta resposta={resposta}/>        
      ))}
      <form action="post">
        <textarea
          name="resposta"
          id="resposta"
          cols="50"
          rows="2"
          required
        ></textarea>
        <input type="submit" value="Enviar Resposta" />
      </form>
    </section>
  );
};
