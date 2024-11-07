import styles from "./styles.module.scss";
import axios from "axios";
import { SlArrowDown, SlArrowUp } from "react-icons/sl";
import { useState } from "react";
import * as validacao from "../../functions/ValidacaoDados";

export const TrocarSenha = (props) => {
  const [senhaAntiga, setSenhaAntiga] = useState("");
  const [novaSenha, setNovaSenha] = useState("");

  const [isFormVisible, setIsFormVisible] = useState(false);

  //Msg de confirmação
  const [msgConfirmacao, setMsgConfirmacao] = useState("");

  const toggleFormVisibility = () => {
    setIsFormVisible(!isFormVisible);
  };

  const handleSubmit = async (event) => {
    event.preventDefault();

    if (!validacao.validarSenha(novaSenha)) {
      setMsgConfirmacao("Senha inválida");
      return;
    }

    const token = localStorage.getItem("token");

    const data = {
      senhaAtual: senhaAntiga,
      novaSenha: novaSenha,
    };

    try {
      const response = await axios.patch(
        "http://localhost:5089/api/Acesso/trocar-senha",
        data,
        {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        }
      );
      setMsgConfirmacao("Senha trocada com sucesso");
      setSenhaAntiga("")
      setNovaSenha("")
    } catch (error) {
      console.error("Erro ao trocar senha:", error.message);
      setMsgConfirmacao("Não foi possível trocar a senha");
    }
  };
  return (
    <div className={styles.container}>
      <div className={styles.title} onClick={toggleFormVisibility}>
        <h1>TROCAR SENHA</h1>
        {isFormVisible ? <SlArrowUp /> : <SlArrowDown />}
      </div>
      {isFormVisible && (
        <form action="post" onSubmit={handleSubmit}>
          <label htmlFor="senhaAntiga">Senha Antiga</label>
          <input
            type="password"
            name="senhaAntiga"
            value={senhaAntiga}
            onChange={(e) => setSenhaAntiga(e.target.value)}
          />

          <label htmlFor="novaSenha">Nova Senha</label>
          <input
            type="password"
            name="novaSenha"
            value={novaSenha}
            onChange={(e) => setNovaSenha(e.target.value)}
          />
          <span>{msgConfirmacao}</span>
          <button type="submit">Trocar Senha</button>
        </form>
      )}
    </div>
  );
};
