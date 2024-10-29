import styles from "./styles.module.scss";
import logo from "../../assets/Solutis_Help_Desk_logo.png";
import { useEffect, useState } from "react";
import * as handleToken from "../../functions/HandleToken";
import { useNavigate } from "react-router-dom";

export const Navbar = (props) => {
  const [username, setUsername] = useState("");
  const navigate = useNavigate();

  useEffect(() => {
    const usernameDoToken = handleToken.retornarUsernameDoToken();
    setUsername(usernameDoToken);
  }, []);

  function handleLogout() {
    handleToken.destruirToken();
    navigate("/")
  }

  return (
    <header className={styles.container}>
      <nav>
        <img src={logo} alt="Solutis Help Desk" />

        <div>
          <ul>
            <li>Dashboard</li>
            <li>Meus Chamados</li>            
          </ul>
        </div>
        <div>
          <span>
            <h4>{username}</h4>
          </span>
          <button onClick={handleLogout}>Logout</button>
        </div>
      </nav>
    </header>
  );
};
