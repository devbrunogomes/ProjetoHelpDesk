import styles from "./styles.module.scss";
import { SlArrowDown, SlArrowUp } from "react-icons/sl";
import { useState } from "react";

export const TrocarSenha = (props) => {
  const [isFormVisible, setIsFormVisible] = useState(true);

  //Msg de confirmação
  const [msgConfirmacao, setMsgConfirmacao] = useState("");

  const toggleFormVisibility = () => {
    setIsFormVisible(!isFormVisible);
  };
  return (
    <div className={styles.container} >
      <div className={styles.title} onClick={toggleFormVisibility}>
        <h1>TROCAR SENHA</h1>
        {isFormVisible ? <SlArrowUp /> : <SlArrowDown />}
      </div>
      {isFormVisible && (
        <form action="post">
          <label htmlFor="senhaAntiga">Senha Antiga</label>
          <input type="password" name="senhaAntiga" />

          <label htmlFor="novaSenha">Nova Senha</label>
          <input type="password" name="novaSenha" />

          <button type="submit">Trocar Senha</button>
        </form>
      )}
    </div>
  );
};
