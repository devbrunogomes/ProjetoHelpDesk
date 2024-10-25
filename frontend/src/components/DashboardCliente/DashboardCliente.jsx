import styles from "./styles.module.scss";
import logo from "../../assets/Solutis_Help_Desk_logo.png";
import { Navbar } from "../Navbar/Navbar";

export const DashboardCliente = (props) => {
  return (
    <>
      <Navbar />
      <div className={styles.container}>
        <section >
          <div>
            <h1>CRIAR CHAMADO</h1>
          </div>

          <form action="">
            <input
              type="text"
              placeholder="TÍTULO"
              required
            />
            <textarea
              type="text"
              placeholder="Descreva seu problema"
              required
            />

            <input type="submit" value="Enviar Chamado" />
          </form>
        </section>
      </div>
    </>
  );
};
