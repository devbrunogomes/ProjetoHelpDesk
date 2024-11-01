import styles from "./styles.module.scss";

import { Navbar } from "../Navbar/Navbar";
import { CriarChamado } from "../CriarChamado/CriarChamado";
import { MeusChamados } from "../MeusChamados/MeusChamados";

export const DashboardCliente = (props) => {
  return (
    <div className={styles.container}>
      <Navbar />
      <CriarChamado />
      {/* <MeusChamados/> */}
    </div>
  );
};
