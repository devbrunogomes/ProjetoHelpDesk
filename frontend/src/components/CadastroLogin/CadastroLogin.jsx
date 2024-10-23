import { useState } from "react";
import styles from "./styles.module.scss";
import axios from "axios";

export const Cadastro = () => {
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
    } catch (error) {
      // Lidar com o erro de login
      console.error("Erro ao fazer login:", error.message);
    }
  };

  return (
    <main className={styles.container}>
      <section className={styles.cadastroSection}>
        <h1>Cadastro</h1>

        <form action="post">
          <label htmlFor="nome">Nome Completo</label>
          <input type="text" id="nome" />

          <label htmlFor="email">Email</label>
          <input type="email" id="email" />

          <label htmlFor="reEmail">Confirme seu Email</label>
          <input type="email" id="reEmail" />

          <label htmlFor="cep">CEP</label>
          <input type="text" name="cep" id="cep" />

          <label htmlFor="usernameCadastro">Username</label>
          <input type="text" id="usernameCadastro" />

          <label htmlFor="passwordCadastro">Senha</label>
          <input type="password" id="passwordCadastro" />

          <label htmlFor="rePassword">Confirme sua Senha</label>
          <input type="password" id="rePassword" />

          <input type="submit" value="Cadastrar" />
        </form>
      </section>

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
