import { useState } from "react";
import styles from "./styles.module.scss";
import axios from "axios";
import logo from "../../assets/Solutis_Help_Desk_logo.png";
import * as handleToken from "../../functions/HandleToken";

export const Login = (props) => {
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
      //localStorage.setItem("token", token);
      handleToken.verificarRoleDoToken(token);

      //TODO: inserir gancho para dashboard de cliente
    } catch (error) {
      // Lidar com o erro de login
      console.error("Erro ao fazer login:", error.message);
    }
  };

  return (
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

      <img src={logo} alt="" />
    </section>
  );
};
