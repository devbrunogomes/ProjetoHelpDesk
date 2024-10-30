import { useEffect, useState } from "react";
import * as handlerEnum from "../../functions/HandleEnumFromJson";
import { Resposta } from "../Resposta/Resposta";
import styles from "./styles.module.scss";
import axios from "axios";
import * as handleToken from "../../functions/HandleToken";

export const Chamado = ({ chamado }) => {
  const [role, setRole] = useState("");
  const [respostas, setRespostas] = useState(chamado.respostas || []);
  const [novaResposta, setNovaResposta] = useState("");
  const [prioridade, setPrioridade] = useState(
    handlerEnum.traduzirPrioridade(chamado.prioridade) || ""
  );
  const [novaPrioridade, setNovaPrioridade] = useState("");
  const podeExibirAlteracaoPrioridade = role === "tecnico" && chamado.status !== 2;

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

  async function finalizarChamado() {
    const token = localStorage.getItem("token");
    const finalizarChamadoData = {
      chamadoId: chamado.chamadoId,
    };

    try {
      const response = await axios.patch(
        "http://localhost:5089/Chamado/finalizar-chamado",
        finalizarChamadoData,
        {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        }
      );

      console.log(response);
    } catch (error) {
      console.error("Erro ao finalizar chamado:", error.message);
    }
  }

  async function alterarPrioridade(event) {
    event.preventDefault();

    if (prioridade === "") {
      alert("Por favor, selecione uma prioridade.");
      return;
    }

    const token = localStorage.getItem("token");
    const intNovaPrioridade = parseInt(novaPrioridade);

    const alterarPrioridadeData = {
      chamadoId: chamado.chamadoId,
      novaPrioridade: intNovaPrioridade,
    };

    try {
      const response = await axios.patch(
        "http://localhost:5089/Chamado/alterar-prioridade",
        alterarPrioridadeData,
        {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        }
      );

      const prioridadeTraduzida =
        handlerEnum.traduzirPrioridade(intNovaPrioridade);
      setPrioridade(prioridadeTraduzida);
    } catch (error) {
      console.error("Erro ao alterar prioridade:", error.message);
    }
  }

  useEffect(() => {
    const token = localStorage.getItem("token");
    const myRole = handleToken.verificarRoleDoToken(token).toLowerCase();
    setRole(myRole);
  }, []);

  return (
    <section>
      <div className={styles.titulo}>
        <h2>Chamado #{chamado.chamadoId} </h2>
        <h2>{prioridade} Prioridade</h2>
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
      <form action="post" onSubmit={alterarPrioridade} className={podeExibirAlteracaoPrioridade ? "" : "displayNone"}>
        <select
          name="prioridade"
          id="mudarPrioridade"
          value={novaPrioridade}
          onChange={(e) => setNovaPrioridade(e.target.value)}
        >
          <option value="">---</option>
          <option value="0">Baixa</option>
          <option value="1">Média</option>
          <option value="2">Alta</option>
        </select>
        <input type="submit" value="Alterar Prioridade" />
      </form>
      <button
        disabled={chamado.status === 2}
        className={role === "tecnico" ? "" : "displayNone"}
        onClick={finalizarChamado}
      >
        Finalizar Chamado
      </button>
    </section>
  );
};
