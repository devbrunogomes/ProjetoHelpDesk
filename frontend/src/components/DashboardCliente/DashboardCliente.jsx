import styles from "./styles.module.scss";
import logo from "../../assets/Solutis_Help_Desk_logo.png";

export const DashboardCliente = (props) => {
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
            <h4>maximusS2</h4>
          </span>
          <button>Logout</button>
        </div>
      </nav>
    </header>
  );
};
