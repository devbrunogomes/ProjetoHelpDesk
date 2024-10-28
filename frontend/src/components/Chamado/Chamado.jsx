import { useState } from "react";
import * as handlerEnum from "../../functions/HandleEnumFromJson";
import { Resposta } from "../Resposta/Resposta";
import styles from "./styles.module.scss";

export const Chamado = ({ chamado }) => {
  const [respostas, setRespostas] = useState([]);

  return (
    <section>
      <div className={styles.titulo}>
        <h2>Chamado #{chamado.chamadoId} </h2>
        <h2>{handlerEnum.traduzirPrioridade(chamado.prioridade)} Prioridade</h2>
        <h2>{handlerEnum.traduzirStatus(chamado.status)}</h2>
      </div>
      <div className={styles.subTitulo}>
        <span>{handlerEnum.formatarDataHora(chamado.dataAbertura)}</span>
        <span>{chamado.titulo}</span>
      </div>
      <div className={styles.descricao}>
        <p>{chamado.descricao}</p>
      </div>
      <div>
        <h3>Respostas</h3>
      </div>
      {chamado.respostas.map((resposta) => (
        <Resposta key={resposta.id} resposta={resposta} />
      ))}

      <div>
        <h3>Reponder</h3>
      </div>

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
