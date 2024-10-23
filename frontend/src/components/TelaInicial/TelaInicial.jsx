import { useState } from "react";
import styles from "./styles.module.scss";
import axios from "axios";
import * as validacao from "../../functions/ValidacaoDados";
import { Cadastro } from "../Cadastro/Cadastro";

export const TelaInicial = () => {
  //Login Variaveis
  const [usernameLogin, setUsernameLogin] = useState("");
  const [passwordLogin, setPasswordLogin] = useState("");

  const handleLogin = async (event) => {
    event.preventDefault();

    try {
      const response = await axios.post(
        "http://localhost:5089/api/Acesso/login",
        {
          userName: usernameLogin,
          password: passwordLogin,
        }
      );

      const token = response.data;
      console.log(token);

      //TODO: inserir gancho para dashboard de cliente
    } catch (error) {
      // Lidar com o erro de login
      console.error("Erro ao fazer login:", error.message);
    }
  };

  return (
    <main className={styles.container}>
      <Cadastro />

      <section className={styles.loginSection}>
        <h1>LOGIN</h1>
        <form onSubmit={handleLogin}>
          <label htmlFor="usernameLogin">Username</label>
          <input
            type="text"
            id="usernameLogin"
            value={usernameLogin}
            onChange={(e) => setUsernameLogin(e.target.value)}
          />

          <label htmlFor="passwordLogin">Password</label>
          <input
            type="password"
            name="passwordLogin"
            id="passwordLogin"
            value={passwordLogin}
            onChange={(e) => setPasswordLogin(e.target.value)}
          />

          <input type="submit" value="Login" />
        </form>
      </section>
    </main>
  );
};
