import { useState } from "react";
import * as handlerEnum from "../../functions/HandleEnumFromJson";
import { Resposta } from "../Resposta/Resposta";
import styles from "./styles.module.scss";
import axios from "axios";
import * as handleToken from "../../functions/HandleToken";

export const Chamado = ({ chamado }) => {
  const [respostas, setRespostas] = useState(chamado.respostas || []);
  const [novaResposta, setNovaResposta] = useState("");

  async function handleSubmit(event) {
    event.preventDefault();

    const token = localStorage.getItem("token");
    const myRole = handleToken.verificarRoleDoToken(token).toLowerCase();

    const respostaData = {
      mensagem: novaResposta,
      chamadoId: chamado.chamadoId,
    };

    try {
      const response = await axios.post(
        `http://localhost:5089/Resposta/${myRole}`,
        respostaData,
        {
          headers: {
            Authorization: `Bearer ${token}`,
            "Content-Type": "application/json",
          },
        }
      );
      setRespostas((prevRespostas) => [...prevRespostas, response.data]);
      setNovaResposta("");
    } catch (error) {
      console.error("Erro ao enviar resposta:", error.message);
    }
  }

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
      {respostas.map((resposta) => (
        <Resposta key={resposta.id} resposta={resposta} />
      ))}

      <div>
        <h3>
          {chamado.status === 2
            ? "Não é possivel enviar mais respostas"
            : "Respostas"}
        </h3>
      </div>

      <form
        action="post"
        className={chamado.status === 2 ? "displayNone" : ""}
        onSubmit={handleSubmit}
      >
        <textarea
          name="resposta"
          id="resposta"
          cols="50"
          rows="2"
          required
          value={novaResposta}
          onChange={(e) => setNovaResposta(e.target.value)}
        ></textarea>
        <input type="submit" value="Enviar Resposta" />
      </form>
    </section>
  );
};
