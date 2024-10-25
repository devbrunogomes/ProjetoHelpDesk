import styles from "./styles.module.scss";
import logo from "../../assets/Solutis_Help_Desk_logo.png";
import { useEffect, useState } from "react";
import * as handleToken from "../../functions/HandleToken";

export const Navbar = (props) => {
  const [username, setUsername] = useState("");

  useEffect(() => {
    const usernameDoToken = handleToken.retornarUsernameDoToken();
    setUsername(usernameDoToken);
  }, []);

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
          <button>Logout</button>
        </div>
      </nav>
    </header>
  );
};
