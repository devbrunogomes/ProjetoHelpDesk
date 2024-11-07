import { useState } from "react";
import styles from "./styles.module.scss";
import axios from "axios";
import logo from "../../assets/Solutis_Help_Desk_logo.png";
import * as handleToken from "../../functions/HandleToken";
import { useNavigate } from "react-router-dom";

export const Login = (props) => {
  const navigate = useNavigate();
  const [usernameLogin, setUsernameLogin] = useState("");
  const [passwordLogin, setPasswordLogin] = useState("");

  //Msg de confirmação
  const [msgConfirmacao, setMsgConfirmacao] = useState("");

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
      localStorage.setItem("token", token);
      const role = handleToken.verificarRoleDoToken(token);
      console.log(role);

      if (role === "CLIENTE") {
        navigate("/cliente-dashboard");
      }

      if (role === "TECNICO") {
        navigate("/tecnico-dashboard");
      }

      if (role === "ADMINISTRADOR") {
        navigate("/adm-dashboard")
      }
    } catch (error) {
      // Lidar com o erro de login
      console.error("Erro ao fazer login:", error);
      setMsgConfirmacao("Username ou senha inválidos.");
    }
  };

  return (
    <section className={styles.loginSection}>
      <h1>LOGIN</h1>

      <form onSubmit={handleLogin}>
        <div>
          <label htmlFor="usernameLogin">Username</label>
          <input
            type="text"
            id="usernameLogin"
            value={usernameLogin}
            onChange={(e) => setUsernameLogin(e.target.value)}
            required
          />

          <label htmlFor="passwordLogin">Password</label>
          <input
            type="password"
            name="passwordLogin"
            id="passwordLogin"
            value={passwordLogin}
            onChange={(e) => setPasswordLogin(e.target.value)}
            required
          />

          <input type="submit" value="Login" />
          <span>{msgConfirmacao}</span>
        </div>
      </form>

      <img src={logo} alt="" />
    </section>
  );
};
